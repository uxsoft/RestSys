using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSys.Models;
using System.Net.Http;
using Windows.Storage;
using Windows.Foundation.Collections;
using System.Net;

namespace RestSys.Client
{
    public static class Global
    {
        static Global()
        {
            if (!Settings.ContainsKey(SETTINGS_CONNECTIONURL))
                Settings[SETTINGS_CONNECTIONURL] = "";
            if (!Settings.ContainsKey(SETTINGS_USERNAME))
                Settings[SETTINGS_USERNAME] = "";
            if (!Settings.ContainsKey(SETTINGS_PASSWORD))
                Settings[SETTINGS_PASSWORD] = "";
        }

        const string LOGIN_ENDPOINT = "{0}/Account/Login";
        const string IDENTIFICATION_ENDPOINT = "{0}/Home/ServerIdentification";
        const string SETTINGS_COOKIE = "RestSysAuthenticationCookie";
        const string SETTINGS_CONNECTIONURL = "RestSysServiceUrl";
        const string SETTINGS_USERNAME = "RestSysUsername";
        const string SETTINGS_PASSWORD = "RestSysPassword";
        const string SERVICEGUID = "A2903CDE-B4EF-455F-BA8B-30465ADD2633";

        public static HttpClient Client { get; set; }
        public static HttpClientHandler Handler { get; set; }
        public static bool IsConnected { get { return Client != null; } }
        public static string ConnectionUrl { get { return Settings[SETTINGS_CONNECTIONURL].ToString().TrimEnd("/ ".ToCharArray()); } set { Settings[SETTINGS_CONNECTIONURL] = value; } }
        public static bool IsAuthenticated { get; set; }
        public static string Username { get { return Settings[SETTINGS_USERNAME].ToString(); } set { Settings[SETTINGS_USERNAME] = value; } }
        public static string Password { get { return Settings[SETTINGS_PASSWORD].ToString(); } set { Settings[SETTINGS_PASSWORD] = value; } }
        public static IPropertySet Settings { get { return Windows.Storage.ApplicationData.Current.LocalSettings.Values; } }

        public static async Task ApplicationStart()
        {
            //Try connect with saved settings
            await Connect(ConnectionUrl);
            if (IsConnected)
                await Login(Username, Password);

            if (!IsAuthenticated || !IsConnected)
                new RestSys.Client.Views.Settings().ShowIndependent();
        }

        public static async Task<bool> Login(string username, string password)
        {
            HttpResponseMessage response = await Client.PostAsync(string.Format(LOGIN_ENDPOINT, ConnectionUrl), new FormUrlEncodedContent(new[] { 
                new KeyValuePair<string,string>("username", username),
                new KeyValuePair<string,string>("password", password)
            }));

            if (response.Headers.Contains("Set-Cookie"))
                if (response.Headers.GetValues("Set-Cookie").Any(s => s.StartsWith(SETTINGS_COOKIE)))
                {

                    if (Authenticated != null)
                        Authenticated(null, EventArgs.Empty);

                    Username = username;
                    Password = password;
                    IsAuthenticated = true;
                    return true;
                }

            return false;
        }

        public static async Task<bool> Connect(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage message = await client.GetAsync(string.Format(IDENTIFICATION_ENDPOINT, url.Trim("/ ".ToCharArray())));
                message.EnsureSuccessStatusCode();

                string guid = await message.Content.ReadAsStringAsync();
                if (guid == SERVICEGUID)
                {
                    Handler = new HttpClientHandler() { AllowAutoRedirect = false };
                    Handler.CookieContainer = new System.Net.CookieContainer();
                    Handler.UseCookies = true;
                    Handler.ClientCertificateOptions = ClientCertificateOption.Automatic;
                    Handler.PreAuthenticate = true;
                    Client = new HttpClient(Handler);
                    Client.BaseAddress = new Uri(url);

                    ConnectionUrl = url;

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

        public static void Disconnect()
        {
            Client = null;
        }

        public static void LogOut()
        {
            Username = "";
            Password = "";
            IsAuthenticated = false;
        }

        public static event EventHandler Authenticated;
        public static event EventHandler Connected;

    }
}
