using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Projekt_Siłownia
{
    public static class HashVerify
    {
        public static bool VerifySHA(string plain, string dbhash)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.Unicode.GetBytes(plain));
            var computeHash = BitConverter.ToString(bytes).Replace("-", "").ToLower();

            return computeHash == dbhash.ToLower();
        }
    }

}
