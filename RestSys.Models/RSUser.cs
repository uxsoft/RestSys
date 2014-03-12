using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
namespace RestSys.Models
{
    public class RSUser : IIdentity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<RSShift> Shifts { get; set; }

        public string AuthenticationType
        {
            get { return "RestSysAuthentication"; }
        }

        public bool IsAuthenticated
        {
            get { throw new NotImplementedException(); }
        }
    }
}
