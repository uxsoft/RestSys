using RestSys.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestSys.Controllers
{
    public class StylesController : Controller
    {
        RSDbContext db = new RSDbContext();

        public ActionResult Receipt(int orderId)
        {
            return View(db.Receipts.Find(orderId));
        }

        public ActionResult ReceiptStyle()
        {
            RSStyle style = db.Styles.Where(s => s.Type == (int)RSStyleType.ReceiptStyle).FirstOrDefault(s => s.Selected);
            if (System.IO.File.Exists(style.Path))
            {
                using (StreamReader sr = new StreamReader(style.Path))
                {
                    return View(sr.ReadToEnd());
                }
            }
            else return View(style.Code);

        }

        public ActionResult Menu()
        {
            return View(db.Products.Where(p => p.ShowOnMenu));
        }

        public ActionResult MenuStyle()
        {
            RSStyle style = db.Styles.Where(s => s.Type == (int)RSStyleType.MenuStyle).FirstOrDefault(s => s.Selected);
            if (System.IO.File.Exists(style.Path))
            {
                using (StreamReader sr = new StreamReader(style.Path))
                {
                    return View(sr.ReadToEnd());
                }
            }
            else return View(style.Code);
        }
    }
}