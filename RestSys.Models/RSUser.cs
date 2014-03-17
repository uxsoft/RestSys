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

        }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

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
            PasswordSalt = CryptoModule.Salt();
            PasswordHash = CryptoModule.Hash(Encoding.UTF8.GetBytes(newPassword), PasswordSalt);
        }

        public bool CheckPassword(string attemptedPassword)
        {
            IStructuralEquatable hash = CryptoModule.Hash(Encoding.UTF8.GetBytes(attemptedPassword), PasswordSalt);
            return hash.Equals(PasswordHash, StructuralComparisons.StructuralEqualityComparer);
        }

        [Import]
        private IRSCryptographyModule CryptoModule { get; set; }
    }
}
