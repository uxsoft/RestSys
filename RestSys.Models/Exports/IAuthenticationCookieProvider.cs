using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace RestSys.Models.Exports
{
    public interface IAuthenticationCookieProvider
    {
        bool LogIn(string username, string password);
        void LogOut();
        bool IsAuthenticated { get; }
        IPrincipal CreatePrincipal();
        IIdentity CreateIdentity();
    }
}
