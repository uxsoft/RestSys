using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace RestSys.Models
{
    public class RSPrincipal : IPrincipal
    {
        public RSPrincipal(RSUser user)
        {
            User = user;
        }

        public RSUser User { get; set; }

        public IIdentity Identity
        {
            get { return User; }
        }

        public bool IsInRole(string role)
        {
            switch (role.ToLowerInvariant())
            {
                case "admin":
                    return User.IsAdmin;
                case "waiter":
                    return User.IsWaiter;
                case "discountmanager":
                    return User.IsDiscountManager;
                default:
                    return false;
            }
        }
    }
}
