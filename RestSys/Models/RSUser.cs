using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using RestSys.Models.Exports;
using System.Collections;
using RestSys;
using System.Composition;

namespace RestSys.Models
{
    public class RSUser : IRSEntity, IIdentity
    {
        public RSUser()
        {
            this.DependencyInjection();
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Jméno")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Uživatelské jméno")]
        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }


        [Display(Name = "Vedoucí")]
        public bool IsAdmin { get; set; }

        [Display(Name = "Číšník")]
        public bool IsWaiter { get; set; }

        
        //Relations
        public virtual ICollection<RSReceipt> Receipts { get; set; }
        public virtual ICollection<RSOrder> AttendedOrders { get; set; }

        public string AuthenticationType
        {
            get { return "RestSysAuthentication"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        public void CreatePassword(string newPassword)
        {
            PasswordSalt = CryptoModule.Salt();
            PasswordHash = CryptoModule.Hash(Encoding.UTF8.GetBytes(newPassword), PasswordSalt);
        }

        public bool CheckPassword(string attemptedPassword)
        {
            IStructuralEquatable hash = CryptoModule.Hash(Encoding.UTF8.GetBytes(attemptedPassword), PasswordSalt);
            return hash.Equals(PasswordHash, StructuralComparisons.StructuralEqualityComparer);
        }

        [Import]
        public IRSCryptographyModule CryptoModule { get; set; }
    }
}
