using RestSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;
namespace RestSys.Controllers
{
    public class HomeController : Controller
    {
        RSDbContext db = new RSDbContext();

        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Username = (User.Identity as RSUser).Username;
            ViewBag.Name = (User.Identity as RSUser).Name;

            ViewBag.TodayOrdersTotal = db.Orders
                .Where(o => DbFunctions.TruncateTime(o.CreatedOn) == DbFunctions.TruncateTime(DateTime.Now))
                .Sum(o => o.Items.Sum(oi => oi.Price));

            ViewBag.MostPopularProducts = db.Products
                .Include("DependentOrderItems")
                .OrderBy(p => p.DependentOrderItems.Count)
                .Take(5);

            ViewBag.StocksLowOnStock = db.Stocks.
                Where(s => s.Quantity < s.CriticalQuantity && s.CriticalQuantity > 0)
                .OrderBy(s => s.Quantity / s.CriticalQuantity)
                .Take(5);

            ViewBag.TodayMostUsedStock = db.Orders
                .Where(o => DbFunctions.TruncateTime(o.CreatedOn) == DbFunctions.TruncateTime(DateTime.Now))
                .SelectMany(o => o.Items)
                .SelectMany(oi => oi.Product.Stocks)
                .GroupBy(si => si.Product.Id)
                .OrderBy(g => g.Count())
                .Select(g => new
                {
                    Title = g.FirstOrDefault().Stock.Title,
                    Quantity = g.Sum(si => si.Amount),
                    Unit = g.FirstOrDefault().Stock.Unit
                })
                .ToList()
                .Select(g => new RSStock()
                {
                    Title = g.Title,
                    Unit = g.Unit,
                    Quantity = g.Quantity
                });

            return View(db);
        }

        public ActionResult ServerIdentification()
        {
            return Content("A2903CDE-B4EF-455F-BA8B-30465ADD2633");
        }
    }
}
