using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Essentials;
using System.IO;
using MzansiGopro.Models;
using MzansiGopro.ViewModels;
using Xamarin.Forms;

namespace MzansiGopro.Services.LocalData
{
   public class LocalUserService : BaseViewModel
    {

      

        public  LocalUserService()
        {



        }

      

        public async void GetLocalUser()
        {
                UserDataBase dataBase = new UserDataBase();
            try
            {
               var id = Preferences.Get("UserID", string.Empty);

                var user = await dataBase.GetUserById(id);

                if(user != null)
                {

                RunTimeUser = user;
                }

            }
            catch
            {
                RunTimeUser = null;
            }



            try
            {
                var id = Preferences.Get("ShopId", string.Empty);
                var _shop = await dataBase.GetShopById(id);

                if(_shop != null)
                {
                    RunTimeBusiness = _shop;
                }

            }
            catch
            {
                RunTimeBusiness = null;

            }




        }

        public void ClearLocalDB()
        {
          
            var id = Preferences.Get("ShopId", string.Empty);
            Preferences.Remove(id);
        }
      

        public async void AddUser(User user)
        {
            try
            {
                Preferences.Set("UserID", user.AutomatedId);
                
            }
            catch
            {
                await Shell.Current.DisplayAlert("Error", $"Could not add user to local DB", "OK");
            }
            
        }
        public  void AddBusiness(Shop shop)
        {
            Preferences.Set("ShopId", shop.ID);

        }

        public void AddFirstUse()
        {
            Preferences.Set("FirstUse", "not first use");
        }




    }
}
