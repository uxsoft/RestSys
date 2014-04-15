using RestSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestSys.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult ServerIdentification()
        {
            return Content("A2903CDE-B4EF-455F-BA8B-30465ADD2633");
        }
    }
}
