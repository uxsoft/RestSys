using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace RestSys.Models
{
    public class RSPrincipal: IPrincipal
    {
        public RSUser User { get; set; }

        public IIdentity Identity
        {
            get { return User; }
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}
