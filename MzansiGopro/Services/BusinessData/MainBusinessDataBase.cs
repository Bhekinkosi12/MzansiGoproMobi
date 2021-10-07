using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
using Firebase.Database.Query;
using System.Threading.Tasks;

namespace MzansiGopro.Services.BusinessData
{
   public  class MainBusinessDataBase
    {
        FirebaseClient client;

        public MainBusinessDataBase()
        {
            client = new FirebaseClient("https://mzansi-go-pro-default-rtdb.firebaseio.com/");
        }






    }
}
