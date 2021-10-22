using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;
using MzansiGopro.Services.LocalData;
using MzansiGopro.Services;
using MzansiGopro.Views.PopupV.ErrorHandlingV;
using Xamarin.CommunityToolkit.Extensions;
using Newtonsoft.Json;

namespace MzansiGopro.ViewModels.ProfileVM
{
   public class ProfileViewModel : BaseViewModel
    {

        bool isLogged = false;
        public bool IsLogged
        {
            get => isLogged;
            set
            {
                SetProperty(ref isLogged, value);
                OnPropertyChanged(nameof(IsLogged));
            }
        }
       

        public Command ToBusiness { get; }
        public Command ToEvents { get; }
        public Command LogOut { get; }

        public ProfileViewModel()
        {
            ToBusiness = new Command(OnToBusiness);
            ToEvents = new Command(OnTOEvent);
            LogOut = new Command(OnLogOut);

        }


       async void OnLogOut()
        {
            LocalUserService localUser = new LocalUserService();
            localUser.ClearLocalDB();
            RunTimeBusiness = null;
            RunTimeUser = null;
            Preferences.Remove("RefreshToken");
            await Shell.Current.GoToAsync("//LoginPage");
        }

        public void OnIsLogged()
        {
            string name;
            try
            {
                var id = Preferences.Get("UserID", string.Empty);
                if (!string.IsNullOrEmpty(id))
                {

                IsLogged = true;


                }
                else
                {
                    IsLogged = false;
                }


            }
            catch
            {

                IsLogged = false;
            }

        
           
            
        }

        public async void BusinessValable()
        {
            var id = Preferences.Get("RefreshToken", string.Empty);
            UserDataBase userData = new UserDataBase();


          

            if (id != string.Empty)
            {

                try
                {
            var savedAuth = JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("RefreshToken", ""));

                   RunTimeUser = await userData.GetUserByEmailAsync(savedAuth.User.Email);



                    if(RunTimeUser != null)
                    {

                    RunTimeBusiness = await userData.GetShopById(RunTimeUser.AutomatedId);

                        if(RunTimeBusiness != null)
                        {
                        await Shell.Current.GoToAsync("CompanyMainPage");

                        }
                        else
                        {

                        }

                    }
                    else
                    {

                    }
                }
                catch
                {
                    Shell.Current.ShowPopup(new InternetConnectionPop());
                }





            }
            else
            {
                await Shell.Current.GoToAsync("LoginPage");
            }

        }
       async void OnToBusiness()
        {
            if(RunTimeUser != null)
            {

                UserDataBase userData = new UserDataBase();

                if (RunTimeUser != null)
                {

                    RunTimeBusiness = await userData.GetShopById(RunTimeUser.AutomatedId);

                    if (RunTimeBusiness != null)
                    {
                        await Shell.Current.GoToAsync("CompanyMainPage");
                    }
                    else
                    {
                        await Shell.Current.GoToAsync("StoreSetupPage");
                    }

                }
                else
                {

                }








                



            }
            else
            {
                await Shell.Current.GoToAsync("LoginPage");
            }
        } 
        async void OnTOEvent()
        {
            if(RunTimeUser == null)
            {
                await Shell.Current.GoToAsync("LoginPage");
            }
            else
            {
                await Shell.Current.GoToAsync("MainAdminEventsPage");
            }
        }


    }
}
