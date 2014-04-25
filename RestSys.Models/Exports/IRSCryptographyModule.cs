using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestSys.Models.Exports
{
    public interface IRSCryptographyModule
    {
        /// <summary>
        /// Generates a new random salt
        /// </summary>
        /// <returns>random generated salt bytes</returns>
        byte[] Salt();
        
        /// <summary>
        /// Hashes a password by the specified salt for secure storage
        /// </summary>
        /// <param name="password">password to be hashed</param>
        /// <param name="salt">Salt value used to hash the password</param>
        /// <returns>Hashed password</returns>
        byte[] Hash(byte[] password, byte[] salt);
    }
}
