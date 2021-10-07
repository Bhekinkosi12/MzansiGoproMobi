using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;
using MzansiGopro.Services.LocalData;

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


        void OnLogOut()
        {
            LocalUserService localUser = new LocalUserService();
            localUser.ClearLocalDB();
            RunTimeBusiness = null;
            RunTimeUser = null;
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


       async void OnToBusiness()
        {
            if(RunTimeUser != null)
            {

                if(RunTimeBusiness != null)
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
