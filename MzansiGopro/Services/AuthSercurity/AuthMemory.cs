using System;
using System.Collections.Generic;
using System.Text;

namespace MzansiGopro.Services.AuthSercurity
{
    public class AuthMemory
    {

        private static string Token { get; set; }



        public void SetToken(string token)
        {
            Token = token;
        }
        public string GetToken()
        {
            return Token;
        }
        public void killToken()
        {
            Token = string.Empty;
        }





    }
}
