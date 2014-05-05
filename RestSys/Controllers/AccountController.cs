using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSys.Models.Exports;
using System.Web.Security;
using System.Composition;
using RestSys.Models;
using System.Threading.Tasks;
using System.Net;
using System.Data.Entity;

namespace RestSys.Controllers
{
    public class AccountController : Controller
    {
        private RSDbContext db = new RSDbContext();

        public AccountController()
        {
            this.DependencyInjection();
        }

        [Import]
        public IAuthenticationCookieProvider AuthenticationCookieProvider { get; set; }

        public ActionResult Login()
        {
            if (AuthenticationCookieProvider.IsAuthenticated)
            {
                ViewBag.ShowMessage = true;
                ViewBag.MessageColor = "lightblue";
                ViewBag.Message = Resources.Resources.LoginRedundant;
            }
            else
            {
                ViewBag.ShowMessage = false;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (AuthenticationCookieProvider.LogIn(username, password))
            {
                return Redirect(FormsAuthentication.GetRedirectUrl(username, false));
            }
            else
            {
                ViewBag.ShowMessage = true;
                ViewBag.MessageColor = "red";
                ViewBag.Message = Resources.Resources.LoginFailed;
                return View();
            }
        }

        public ActionResult LogOut()
        {
            AuthenticationCookieProvider.LogOut();
            return RedirectToAction("Login");
        }

        [Authorize]
        public ActionResult Index()
        {
            return View(db.Users);
        }

        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RSUser user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RSUser user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RSUser user, string newpassword, string confirmpassword)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;

                if (!string.IsNullOrWhiteSpace(newpassword) & newpassword == confirmpassword)
                {
                    user.CreatePassword(newpassword);
                }

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View(new RSUser());
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RSUser user, string newpassword, string confirmpassword)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);

                if (newpassword == confirmpassword)
                {
                    user.CreatePassword(newpassword);
                }
                else
                {
                    ViewBag.ShowMessage = true;
                    ViewBag.MessageColor = "red";
                    ViewBag.Message = Resources.Resources.PasswordsDontMatch;
                    return View(user);
                }
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RSUser user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RSUser user = await db.Users.FindAsync(id);
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}