using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MzansiGopro.Services.AuthSercurity
{
   public class PasswordAbcHash
    {

        public PasswordAbcHash()
        {

        }


      public  string StandardPasswordHash(string _password)
        {
            byte[] salt = new byte[128 / 8];

            using(var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }


            //converted to Byte64

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: _password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));


               


        }






    }
}
