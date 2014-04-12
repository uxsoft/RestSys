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
    public class NavigationController : Controller
    {
        private RSDbContext db = new RSDbContext();

        #region SubItems
        [HttpPost]
        public async Task<ActionResult> AddChild(int id, int childId)
        {
            RSNavigationItem child = await db.Navigation.FindAsync(childId);
            RSNavigationItem navigationItem = await db.Navigation.FindAsync(id);
            if (child != null && navigationItem != null)
            {
                navigationItem.Children.Add(child);
                await db.SaveChangesAsync();
                return Json(true);
            }
            else throw new HttpException(400, "Error inserting item");
        }

        [HttpPost]
        public async Task<ActionResult> RemoveChild(int id, int childId)
        {
            RSNavigationItem child = await db.Navigation.FindAsync(childId);
            RSNavigationItem navigationItem = await db.Navigation.FindAsync(id);
            if (child != null && navigationItem != null)
            {
                if (navigationItem.Children.Remove(child))
                {
                    await db.SaveChangesAsync();
                    return Json(true);
                }
            }
            throw new HttpException(400, "Error removing item");
        }

        [HttpGet]
        public async Task<ActionResult> GetChildren(int id)
        {
            RSNavigationItem navigationItem = await db.Navigation.FindAsync(id);
            if (navigationItem != null)
            {
                return Json(navigationItem.Children.Select(s => new { title = s.Title, id = s.Id }), JsonRequestBehavior.AllowGet);
            }
            else throw new HttpException(400, "Error fetching items");
        }

        #endregion

        // GET: Navigation
        public async Task<ActionResult> Index()
        {
            return View(await db.Navigation.ToListAsync());
        }

        // GET: Navigation/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RSNavigationItem rSNavigationItem = await db.Navigation.FindAsync(id);
            if (rSNavigationItem == null)
            {
                return HttpNotFound();
            }
            return View(rSNavigationItem);
        }

        // GET: Navigation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Navigation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Position,Title,Description,Image")] RSNavigationItem rSNavigationItem)
        {
            if (ModelState.IsValid)
            {
                db.Navigation.Add(rSNavigationItem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(rSNavigationItem);
        }

        // GET: Navigation/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RSNavigationItem rSNavigationItem = await db.Navigation.FindAsync(id);
            if (rSNavigationItem == null)
            {
                return HttpNotFound();
            }
            return View(rSNavigationItem);
        }

        // POST: Navigation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ChildrenOrder,Title,Description,Image")] RSNavigationItem rSNavigationItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rSNavigationItem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(rSNavigationItem);
        }

        // GET: Navigation/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RSNavigationItem rSNavigationItem = await db.Navigation.FindAsync(id);
            if (rSNavigationItem == null)
            {
                return HttpNotFound();
            }
            return View(rSNavigationItem);
        }

        // POST: Navigation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RSNavigationItem rSNavigationItem = await db.Navigation.FindAsync(id);
            db.Navigation.Remove(rSNavigationItem);
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
