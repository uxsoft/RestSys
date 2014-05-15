using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSys.Client.Services.EntityService;
using System.Net.Http;
using Windows.Storage;
using Windows.Foundation.Collections;

namespace RestSys.Client
{
    public static class Global
    {
        static Global()
        {
            if (!Settings.ContainsKey(SETTINGS_CONNECTIONURL))
                Settings[SETTINGS_CONNECTIONURL] = "";

            Client = new HttpClient(new HttpClientHandler() { AllowAutoRedirect = false });
        }

        const string API_ENDPOINT = "{0}/EntityService.svc/";
        const string LOGIN_ENDPOINT = "{0}/Account/Login";
        const string IDENTIFICATION_ENDPOINT = "{0}/Home/ServerIdentification";
        const string SETTINGS_COOKIE = "RestSysAuthenticationCookie";
        const string SETTINGS_CONNECTIONURL = "RestSysServiceUrl";
        const string SETTINGS_USERNAME = "username";
        const string SERVICEGUID = "A2903CDE-B4EF-455F-BA8B-30465ADD2633";

        public static HttpClient Client { get; set; }
        public static RSDbContext Db { get; set; }
        public static bool IsConnected { get { return Db != null; } }
        public static string ConnectionUrl { get { return Settings[SETTINGS_CONNECTIONURL].ToString().TrimEnd("/ ".ToCharArray()); } set { Settings[SETTINGS_CONNECTIONURL] = value; } }
        public static bool IsAuthenticated { get { return Settings.ContainsKey(SETTINGS_COOKIE); } }
        private static string AuthenticationCookie { get { return Settings[SETTINGS_COOKIE].ToString(); } set { Settings[SETTINGS_COOKIE] = value; } }
        public static string Username { get { return Settings[SETTINGS_USERNAME].ToString(); } set { Settings[SETTINGS_USERNAME] = value; } }
        public static IPropertySet Settings { get { return Windows.Storage.ApplicationData.Current.LocalSettings.Values; } }

        public static async Task ApplicationStart()
        {
            Connected += (a1, a2) => Db.BuildingRequest += Db_BuildingRequest;
            Connected += (a1, a2) => Client.DefaultRequestHeaders.Add("Cookie", AuthenticationCookie);

            //Try connect with saved settings
            await Connect(Settings[SETTINGS_CONNECTIONURL].ToString());

            if (!IsAuthenticated || !IsConnected)
                new RestSys.Client.Views.Settings().ShowIndependent();
        }

        static void Db_BuildingRequest(object sender, System.Data.Services.Client.BuildingRequestEventArgs e)
        {
            e.Headers["Cookie"] = AuthenticationCookie;
        }


        public static async Task<bool> Login(string username, string password)
        {
            HttpResponseMessage response = await Client.PostAsync(string.Format(LOGIN_ENDPOINT, ConnectionUrl), new FormUrlEncodedContent(new[] { 
                new KeyValuePair<string,string>("username",username),
                new KeyValuePair<string,string>("password", password)
            }));

            if (!response.Headers.Contains("Set-Cookie"))
                return false;

            var cookies = response.Headers.GetValues("Set-Cookie");
            string cookie = cookies.SingleOrDefault(s => s.StartsWith(SETTINGS_COOKIE));

            if (cookie == null)
                return false;

            //Save cookie
            AuthenticationCookie = cookie;

            if (Authenticated != null)
                Authenticated(null, EventArgs.Empty);

            Username = username;
            return true;
        }

        public static async Task<bool> Connect(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage message = await client.GetAsync(string.Format(IDENTIFICATION_ENDPOINT, url.Trim("/ ".ToCharArray())));
                if (!message.IsSuccessStatusCode)
                    return false;

                string guid = await message.Content.ReadAsStringAsync();
                if (guid == SERVICEGUID)
                {
                    Db = new RSDbContext(new Uri(string.Format(API_ENDPOINT, url.Trim("/ ".ToCharArray()))));
                    if (Connected != null)
                        Connected(null, EventArgs.Empty);

                    Client.BaseAddress = new Uri(url);

                    return true;
                }

            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public static void Disconnect()
        {
            Db = null;
        }

        public static void LogOut()
        {
            Settings.Remove(SETTINGS_USERNAME);
            Settings.Remove(SETTINGS_COOKIE);
        }

        public static event EventHandler Authenticated;
        public static event EventHandler Connected;
    }
}
