using System;
using System.Collections.Generic;
using System.Data;
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
    public class StocksController : Controller
    {
        private RSDbContext db = new RSDbContext();

        // GET: Stocks
        public async Task<ActionResult> Index()
        {
            return View(await db.Stocks.ToListAsync());
        }

        // GET: Stocks/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RSStock rSStock = await db.Stocks.FindAsync(id);
            if (rSStock == null)
            {
                return HttpNotFound();
            }
            return View(rSStock);
        }

        // GET: Stocks/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Quantity,Notes,SerialNumber,Unit")] RSStock rSStock)
        {
            if (ModelState.IsValid)
            {
                db.Stocks.Add(rSStock);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(rSStock);
        }

        // GET: Stocks/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RSStock rSStock = await db.Stocks.FindAsync(id);
            if (rSStock == null)
            {
                return HttpNotFound();
            }
            return View(rSStock);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Quantity,Notes,SerialNumber,Unit")] RSStock rSStock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rSStock).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(rSStock);
        }

        // GET: Stocks/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RSStock rSStock = await db.Stocks.FindAsync(id);
            if (rSStock == null)
            {
                return HttpNotFound();
            }
            return View(rSStock);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RSStock rSStock = await db.Stocks.FindAsync(id);
            db.Stocks.Remove(rSStock);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
