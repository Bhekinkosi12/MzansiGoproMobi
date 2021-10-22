using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Firebase.Database;
using Firebase.Database.Query;
using MzansiGopro.Services.AuthSercurity;
using MzansiGopro.Services;

namespace MzansiGopro.ViewModels.AuthenticationVM
{
   public class LoginVM : BaseViewModel
    {


        private string username;
        private string password;
        public Command login { get; }


        public LoginVM()
        {
            login = new Command(OnLogin);
        }

        public string Username
        {
            get => username;
            set
            {
                SetProperty(ref username,value);;
                OnPropertyChanged(nameof(Username));

            }
        }
        public string Password
        {
            get => password;
            set
            {
                SetProperty(ref password, value);
                OnPropertyChanged(nameof(Password));
            }
        }




     public  async void OnLogin()
        {
            IsBusy = true;
           
            if(!string.IsNullOrEmpty(Username) || Password.Length > 6)
            {

                AuthenticationService authentication = new AuthenticationService();
               var IsLoged = await authentication.Login(Username, Password);

                if (IsLoged)
                {
                    await Shell.Current.GoToAsync("//TabbedPage");
                }
                else
                {

                }

            }
            else
            {

            }


            IsBusy = false;
        }





    }
}
