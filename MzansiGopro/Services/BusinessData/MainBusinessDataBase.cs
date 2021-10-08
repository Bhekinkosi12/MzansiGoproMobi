using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
using Firebase.Database.Query;
using System.Threading.Tasks;
using MzansiGopro.Models;
using System.Linq;

namespace MzansiGopro.Services.BusinessData
{
   public  class MainBusinessDataBase
    {
        FirebaseClient client;
        public static List<Shop> Shops { get; set; }

        public MainBusinessDataBase()
        {
            client = new FirebaseClient("https://mzansi-go-pro-default-rtdb.firebaseio.com/");
           
        }



      public async void InitialData()
        {
           var items = await GetAllBusiness();
            if(items != null)
            {
                Shops = items;
            }

        }

        public List<Shop> ReturnShops()
        {
            return Shops;
        }


        async Task<List<Shop>> GetAllBusiness()
        {
            try
            {

                List<Shop> shopList = new List<Shop>();
                var items = (await client
                     .Child("Users")
                     .Child("IsBusiness")
                     .OnceAsync<Shop>()).ToList();

                foreach (var shop in items)
                {
                    shopList.Add(shop.Object);
                }


                return shopList;
            }
            catch
            {
                return null;
            }


        }




    }
}
