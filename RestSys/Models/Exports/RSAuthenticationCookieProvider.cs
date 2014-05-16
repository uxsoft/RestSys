using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Web;
namespace RestSys.Models.Exports
{
    [Export(typeof(IAuthenticationCookieProvider))]
    public class RSAuthenticationCookieProvider : IAuthenticationCookieProvider
    {
        private const string COOKIE_NAME = "RestSysAuthenticationCookie";
        private const string COOKIE_USERNAME = "u";
        private const string COOKIE_PASSWORDHASH = "p";

        private static RSUser CurrentUser { get; set; }

        public System.Security.Principal.IIdentity CreateIdentity()
        {
            using (RSDbContext Db = new RSDbContext())
            {
                if (HttpContext.Current.Request.Cookies[COOKIE_NAME] == null)
                    return null;

                string username = HttpContext.Current.Request.Cookies[COOKIE_NAME][COOKIE_USERNAME];
                string password = HttpContext.Current.Request.Cookies[COOKIE_NAME][COOKIE_PASSWORDHASH];

                if (CurrentUser == null)
                {
                    var matchingUsers = Db.Users.Where(u => u.Name == username).ToList();
                    CurrentUser = matchingUsers.SingleOrDefault(u => BitConverter.ToString(u.PasswordHash) == password);
                }
                return CurrentUser;
            }
        }

        public System.Security.Principal.IPrincipal CreatePrincipal()
        {
            return new RSPrincipal((RSUser)CreateIdentity());
        }

        public bool LogIn(string username, string password)
        {
            using (RSDbContext Db = new RSDbContext())
            {
                RSUser user = Db.Users.FirstOrDefault(u => u.Username == username);

                if (user == null) return false;
                if (!user.CheckPassword(password)) return false;

                HttpContext.Current.Response.Cookies[COOKIE_NAME][COOKIE_USERNAME] = user.Username;
                HttpContext.Current.Response.Cookies[COOKIE_NAME][COOKIE_PASSWORDHASH] = BitConverter.ToString(user.PasswordHash);
                HttpContext.Current.Response.Cookies[COOKIE_NAME].Expires = DateTime.Now.AddDays(30);

                CurrentUser = user;
                return true;
            }
        }

        public void LogOut()
        {
            CurrentUser = null;
            HttpContext.Current.Response.Cookies[COOKIE_NAME].Expires = DateTime.MinValue;
        }

        public bool IsAuthenticated
        {
            get { return CreateIdentity() != null; }
        }
    }
}