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
    public class ProductsController : Controller
    {
        private RSDbContext db = new RSDbContext();

        // GET: Products
        public async Task<ActionResult> Index()
        {
            return View(await db.Products.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
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

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Description,Price,ShowOnMenu,SerialNumber,Amount,Category")] RSProduct rSProduct)
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Description,Price,ShowOnMenu,SerialNumber,Amount,Category")] RSProduct rSProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rSProduct).State = EntityState.Modified;
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
