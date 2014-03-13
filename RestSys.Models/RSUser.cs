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
        [Required]
        public string Name { get; set; }
        [Required]
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsWaiter { get; set; }
        public bool IsStorekeeper { get; set; }
        public bool IsDiscountManager { get; set; }

        //Relations
        public virtual ICollection<RSShift> Shifts { get; set; }
        public virtual ICollection<RSOrder> Orders { get; set; }
        public virtual ICollection<RSDiscount> Discounts { get; set; }

        public string AuthenticationType
        {
            get { return "RestSysAuthentication"; }
        }

        public bool IsAuthenticated
        {
            //TODO: RSUser.IsAuthenticated
            get { throw new NotImplementedException(); }
        }

        public void CreatePassword(string newPassword)
        { 
            //TODO: Implement password hashing
        }

        public bool CheckPassword(string attemptedPassword)
        { 
            //TODO: Implement password hashing check
            throw new NotImplementedException();
        }
    }
}
