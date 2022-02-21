using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Ski_Service_Applikation
{
    public class BCryptHasher : HashAlgorithm
    {
        private MemoryStream passwordStream = null;

        protected override void HashCore(byte[] array, int ibStart, int cbSize)
        {
            if (passwordStream == null || Salt == null)
                Initialize();

            passwordStream.Write(array, ibStart, cbSize);
        }

        protected override byte[] HashFinal()
        {
            passwordStream.Flush();

            // Get the hash
            return Encoding.UTF8.GetBytes(BCrypt.Net.BCrypt.HashPassword(Encoding.UTF8.GetString(passwordStream.ToArray()), Salt));
        }

        public override void Initialize()
        {
            passwordStream = new MemoryStream();

            // Set up salt
            if (Salt == null)
            {
                if (WorkFactor == 0)
                    Salt = BCrypt.Net.BCrypt.GenerateSalt();
                else
                    Salt = BCrypt.Net.BCrypt.GenerateSalt(WorkFactor);
            }
        }

        public int WorkFactor { get; set; }

        public string Salt { get; set; }

        public bool Verify(string plain, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(plain, hash);
        }
    }
}