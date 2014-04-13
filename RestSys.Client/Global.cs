using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSys.Client.Services.EntityService;
namespace RestSys.Client
{
    public static class Global
    {
        const string API_ENDPOINT = "http://localhost:56345/EntityService.svc/";

        public static async void ApplicationStart()
        {
            Db = new RSDbContext(new Uri(API_ENDPOINT));
            

        }

        public static RSDbContext Db { get; set; }
    }
}
