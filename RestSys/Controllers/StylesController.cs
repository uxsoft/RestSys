﻿using RestSys.Models;
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

        public ActionResult Receipt(int id)
        {
            RSReceipt receipt = db.Receipts.Include("Order").Include("PaidItems.Product").SingleOrDefault(r => r.Id == id);
            if (receipt != null)
                return View(receipt);
            else return HttpNotFound();
        }

        public ActionResult ReceiptStyle()
        {
            return Content(GetStyleCode(RSStyleType.ReceiptStyle), "text/css");
        }

        public ActionResult Menu()
        {
            return View(db.Products.Include("Stocks.Stock").Where(p => p.ShowOnMenu));
        }

        public ActionResult MenuStyle()
        {
            return Content(GetStyleCode(RSStyleType.MenuStyle), "text/css");
        }

        private string GetStyleCode(RSStyleType type)
        {
            RSStyle style = db.Styles.Where(s => s.Type == (int)type).FirstOrDefault(s => s.Selected);
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