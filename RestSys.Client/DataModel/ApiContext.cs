using RestSys.ClientCore.Exports;
using RestSys.Models;
using RestSys.Models.Exports;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSys.Client.DataModel
{
    public class ApiContext
    {
        public ApiContext()
        {
            this.DependencyInjection();


            Styles.AttachWebApiEndpoint("http://localhost:56345/Api/Styles/");
            Users.AttachPersistencyProvider(PersistencyProvider);
        }

        [Import]
        public IRSPersistencyProvider PersistencyProvider { get; set; }

        [Import]
        public IRSApiConnector<RSUser> Users { get; set; }

        [Import]
        public IRSApiConnector<RSStyle> Styles { get; set; }
    }
}
