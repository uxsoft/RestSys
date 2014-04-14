using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSys.Client.Services.EntityService;
using System.Net.Http;

namespace RestSys.Client
{
    public static class Global
    {
        const string API_ENDPOINT = "http://localhost:56345/EntityService.svc/";
        const string LOGIN_ENDPOINT = "http://localhost:56345/Account/Login";
        const string COOKIE_NAME = "RestSysAuthenticationCookie";

        public static bool IsAuthenticated { get; set; }
        private static string AuthenticationCookie { get; set; }

        public static async void ApplicationStart()
        {
            Db = new RSDbContext(new Uri(API_ENDPOINT));
            Db.BuildingRequest += Db_BuildingRequest;

            await Login("uxsoft", "TotallySecretPassword");
        }

        static void Db_BuildingRequest(object sender, System.Data.Services.Client.BuildingRequestEventArgs e)
        {
            e.Headers["Cookie"] = AuthenticationCookie;
        }


        public static async Task<bool> Login(string username, string password)
        {
            HttpClient client = new HttpClient(new HttpClientHandler() { AllowAutoRedirect = false });
            HttpResponseMessage response = await client.PostAsync(LOGIN_ENDPOINT, new FormUrlEncodedContent(new[] { 
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
            IsAuthenticated = true;
            AuthenticationCookie = cookie;

            if (Authenticated != null)
                Authenticated(null, EventArgs.Empty);

            return true;
        }

        public static event EventHandler Authenticated;

        public static RSDbContext Db { get; set; }
    }
}
