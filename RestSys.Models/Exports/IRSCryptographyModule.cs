using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestSys.Models.Exports
{
    public interface IRSCryptographyModule
    {
        byte[] Salt();
        byte[] Hash(byte[] password, byte[] salt);
    }
}
