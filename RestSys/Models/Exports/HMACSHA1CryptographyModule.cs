using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace RestSys.Models.Exports
{
    [Export(typeof(IRSCryptographyModule))]
    public class HMACSHA1CryptographyModule : IRSCryptographyModule
    {
        public byte[] Salt()
        {
            return System.Text.Encoding.UTF8.GetBytes(System.Web.Security.Membership.GeneratePassword(100, 20));
        }

        public byte[] Hash(byte[] password, byte[] salt)
        {
            return new HMACMD5(salt).ComputeHash(password);
        }
    }
}