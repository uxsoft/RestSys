using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSys.Models.Exports;
using System.Web.Security;
using System.Composition;

namespace RestSys.Controllers
{
    public class AccountController : Controller
    {
        public AccountController()
        {
            this.DependencyInjection();
        }

        [Import]
        public IAuthenticationCookieProvider AuthenticationCookieProvider { get; set; }

        public ActionResult Login()
        {
            if (AuthenticationCookieProvider.IsAuthenticated)
            {
                ViewBag.ShowMessage = true;
                ViewBag.MessageColor = "lightblue";
                ViewBag.Message = Resources.Resources.LoginRedundant;
            }
            else
            {
                ViewBag.ShowMessage = false;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (AuthenticationCookieProvider.LogIn(username, password))
            {
                return Redirect(FormsAuthentication.GetRedirectUrl(username, false));
            }
            else
            {
                ViewBag.ShowMessage = true;
                ViewBag.MessageColor = "red";
                ViewBag.Message = Resources.Resources.LoginFailed;
                return View();
            }
        }

        public ActionResult LogOut()
        {
            AuthenticationCookieProvider.LogOut();
            return RedirectToAction("Login");
        }
    }
}