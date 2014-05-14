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
                //TODO Order by ChildrenOrder
                return Json(navigationItem.OrderedChildren().Select(s => new { title = s.Title, id = s.Id }), JsonRequestBehavior.AllowGet);
            }
            else throw new HttpException(400, "Error fetching items");
        }

        #endregion

        // GET: Navigation
        public async Task<ActionResult> Index()
        {
            return View(await db.Navigation.OrderByDescending(ni => ni.IsRoot).ToListAsync());
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
        public async Task<ActionResult> Create(RSNavigationItem rSNavigationItem)
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
            RSNavigationItem rSNavigationItem = await db.Navigation.Include("ProductLink").SingleOrDefaultAsync(i => i.Id == id);
            if (rSNavigationItem == null)
            {
                return HttpNotFound();
            }
            return View(rSNavigationItem);
        }

        // POST: Navigation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RSNavigationItem rSNavigationItem, int productid)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rSNavigationItem).State = EntityState.Modified;
                await db.SaveChangesAsync();

                rSNavigationItem = db.Navigation.Find(rSNavigationItem.Id);
                rSNavigationItem.ProductLink = await db.Products.FindAsync(productid);
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
