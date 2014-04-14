using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using RestSys.Models.Exports;
using RestSys.Models;
using System.Security.Principal;
using System.Composition;

namespace RestSys
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public WebApiApplication()
        {
            this.DependencyInjection();
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        [Import]
        public IAuthenticationCookieProvider AuthenticationCookieProvider { get; set; }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            if (AuthenticationCookieProvider.IsAuthenticated)
                HttpContext.Current.User = AuthenticationCookieProvider.CreatePrincipal();
        }
    }
}
