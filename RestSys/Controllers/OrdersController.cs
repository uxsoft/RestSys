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
    public class OrdersController : Controller
    {
        private RSDbContext db = new RSDbContext();

        [HttpPost]
        public async Task<ActionResult> AddProduct(int id, int productId)
        {
            RSProduct product = await db.Products.FindAsync(productId);
            RSOrder order = await db.Orders.FindAsync(id);

            if (product != null && order != null)
            {
                RSOrderItem orderItem = new RSOrderItem();

                orderItem.Order = order;

                db.OrderItems.Add(orderItem);
                await db.SaveChangesAsync();
                return Json(true);
            }
            else throw new HttpException(400, "Error inserting OrderItem");
        }

        [HttpPost]
        public async Task<ActionResult> RemoveProduct(int id, int productId)
        {
            RSOrderItem product = await db.OrderItems.FindAsync(productId);
            RSOrder order = await db.Orders.FindAsync(id);
            if (product != null && order != null)
            {
                if (order.Items.Remove(product))
                {
                    db.OrderItems.Remove(product);
                    await db.SaveChangesAsync();
                    return Json(true);
                }
            }
            throw new HttpException(400, "Error removing item");
        }

        // GET: /Orders/
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        // GET: /Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RSOrder rsorder = db.Orders.Find(id);
            if (rsorder == null)
            {
                return HttpNotFound();
            }
            return View(rsorder);
        }

        // GET: /Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Title,CreatedOn,Active,Notes")] RSOrder rsorder)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(rsorder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rsorder);
        }

        // GET: /Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RSOrder rsorder = db.Orders.Find(id);
            if (rsorder == null)
            {
                return HttpNotFound();
            }
            return View(rsorder);
        }

        // POST: /Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Title,CreatedOn,Active,Notes")] RSOrder rsorder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rsorder).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rsorder);
        }

        // GET: /Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RSOrder rsorder = db.Orders.Find(id);
            if (rsorder == null)
            {
                return HttpNotFound();
            }
            return View(rsorder);
        }

        // POST: /Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RSOrder rsorder = db.Orders.Find(id);
            db.Orders.Remove(rsorder);
            db.SaveChanges();
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
