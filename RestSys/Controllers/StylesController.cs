using RestSys.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RestSys.Controllers
{
    [Authorize]
    public class StylesController : Controller
    {
        RSDbContext db = new RSDbContext();

        public ActionResult Receipt(int orderId)
        {
            return View(db.Receipts.Find(orderId));
        }

        public ActionResult ReceiptStyle()
        {
            return View((object)GetStyleCode(RSStyleType.ReceiptStyle));
        }

        public ActionResult Menu()
        {
            return View(db.Products.Where(p => p.ShowOnMenu));
        }

        public ActionResult MenuStyle()
        {
            return View((object)GetStyleCode(RSStyleType.MenuStyle));
        }

        private string GetStyleCode(RSStyleType type)
        {
            RSStyle style = db.Styles.Where(s => s.Type == (int)RSStyleType.MenuStyle).FirstOrDefault(s => s.Selected);
            if (style == null)
                return "";

            if (System.IO.File.Exists(Server.MapPath(style.Path)))
            {
                using (StreamReader sr = new StreamReader(Server.MapPath(style.Path)))
                {
                    return sr.ReadToEnd();
                }
            }
            else return style.Code;
        }
    }
}