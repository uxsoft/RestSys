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
            if (!Settings.ContainsKey(CONNETIONURL_NAME))
                Settings[CONNETIONURL_NAME] = "";
        }

        const string API_ENDPOINT = "{0}/EntityService.svc/";
        const string LOGIN_ENDPOINT = "{0}/Account/Login";
        const string IDENTIFICATION_ENDPOINT = "{0}/Home/ServerIdentification";
        const string COOKIE_NAME = "RestSysAuthenticationCookie";
        const string CONNETIONURL_NAME = "RestSysServiceUrl";
        const string SERVICEGUID = "A2903CDE-B4EF-455F-BA8B-30465ADD2633";

        public static RSDbContext Db { get; set; }
        public static bool IsConnected { get { return Db != null; } }
        public static string ConnectionUrl { get { return Settings[CONNETIONURL_NAME].ToString().TrimEnd("/ ".ToCharArray()); } set { Settings[CONNETIONURL_NAME] = value; } }
        public static bool IsAuthenticated { get { return Settings.ContainsKey(COOKIE_NAME); } }
        private static string AuthenticationCookie { get { return Settings[COOKIE_NAME].ToString(); } set { Settings[COOKIE_NAME] = value; } }
        public static IPropertySet Settings { get { return Windows.Storage.ApplicationData.Current.LocalSettings.Values; } }

        public static async void ApplicationStart()
        {
            Connected += (a1, a2) => Db.BuildingRequest += Db_BuildingRequest;

            //Try connect with saved settings
            await Connect(Settings[CONNETIONURL_NAME].ToString());
        }

        static void Db_BuildingRequest(object sender, System.Data.Services.Client.BuildingRequestEventArgs e)
        {
            e.Headers["Cookie"] = AuthenticationCookie;
        }


        public static async Task<bool> Login(string username, string password)
        {
            HttpClient client = new HttpClient(new HttpClientHandler() { AllowAutoRedirect = false });
            HttpResponseMessage response = await client.PostAsync(string.Format(LOGIN_ENDPOINT, ConnectionUrl), new FormUrlEncodedContent(new[] { 
                new KeyValuePair<string,string>("username",username),
                new KeyValuePair<string,string>("password", password)
            }));

            if (!response.Headers.Contains("Set-Cookie"))
                return false;

            var cookies = response.Headers.GetValues("Set-Cookie");
            string cookie = cookies.SingleOrDefault(s => s.StartsWith(COOKIE_NAME));

            if (cookie == null)
                return false;

            //Save cookie
            AuthenticationCookie = cookie;

            if (Authenticated != null)
                Authenticated(null, EventArgs.Empty);

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

                    return true;
                }

            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public static event EventHandler Authenticated;
        public static event EventHandler Connected;
    }
}
