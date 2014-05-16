using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestSys.Models;

namespace RestSys.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private RSDbContext db = new RSDbContext();

        [HttpPost]
        public async Task<ActionResult> AddStock(int id, int stockId, double amount)
        {
            RSStock stock = await db.Stocks.FindAsync(stockId);
            RSProduct product = await db.Products.FindAsync(id);

            if (stock != null && product != null)
            {
                RSStockItem stockItem = new RSStockItem();
                stockItem.Amount = amount;
                stockItem.Stock = stock;
                stockItem.Product = product;

                db.StockItems.Add(stockItem);
                await db.SaveChangesAsync();
                return Json(true);
            }
            else throw new HttpException(400, "Error inserting item");
        }

        [HttpPost]
        public async Task<ActionResult> RemoveStock(int id, int stockId)
        {
            RSStockItem stock = await db.StockItems.FindAsync(stockId);
            RSProduct product = await db.Products.FindAsync(id);
            if (stock != null && product != null)
            {
                if (product.Stocks.Remove(stock))
                {
                    db.StockItems.Remove(stock);
                    await db.SaveChangesAsync();
                    return Json(true);
                }
            }
            throw new HttpException(400, "Error removing item");
        }

        [HttpGet]
        public ActionResult GetStocks(int id)
        {
            return Json(db.StockItems.Where(si => si.Product.Id == id).Select(s => new { title = s.Stock.Title, amount = s.Amount, unit = s.Stock.Unit, id = s.Id }), JsonRequestBehavior.AllowGet);
        }

        // GET: Products
        public async Task<ActionResult> Index()
        {
            return View(await db.Products.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            RSProduct rSProduct = await db.Products.Include(p => p.Stocks).SingleOrDefaultAsync(p => p.Id == id);
            if (rSProduct == null)
                return HttpNotFound();

            return View(rSProduct);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RSProduct rSProduct)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(rSProduct);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(rSProduct);
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RSProduct rSProduct = await db.Products.FindAsync(id);
            if (rSProduct == null)
            {
                return HttpNotFound();
            }
            return View(rSProduct);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RSProduct rSProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rSProduct).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(rSProduct);
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RSProduct rSProduct = await db.Products.FindAsync(id);
            if (rSProduct == null)
            {
                return HttpNotFound();
            }
            return View(rSProduct);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RSProduct rSProduct = await db.Products.FindAsync(id);
            db.Products.Remove(rSProduct);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
