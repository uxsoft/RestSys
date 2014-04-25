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
        /// <summary>
        /// Validates the supplied credentials and creates a cookie that identifies this user
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <returns>If user was logged in</returns>
        bool LogIn(string username, string password);

        /// <summary>
        /// Removes users identification cookie
        /// </summary>
        void LogOut();

        /// <summary>
        /// Indicates if the current user is logged in
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// Creates a principal object for the current user.
        /// </summary>
        /// <returns>A principal object for the current user. Returns null if no user is currently logged in.</returns>
        IPrincipal CreatePrincipal();

        /// <summary>
        /// Creates an identity object for the current user
        /// </summary>
        /// <returns>An identity object for the current user. Returns null if no user is currently logged in.</returns>
        IIdentity CreateIdentity();
    }
}
