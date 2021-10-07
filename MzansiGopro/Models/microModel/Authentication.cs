using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MzansiGopro.Models.microModel
{
   public class Authentication
    {
        [AutoIncrement]
        public int ID { get; set; }
        public string Email { get; set; }
        public bool IsShop { get; set; }
        public string Password { get; set; }
    }
}
