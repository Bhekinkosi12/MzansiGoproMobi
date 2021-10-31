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
        bool isSent = false;
        bool isError = false;
        public Command login { get; }

        public Command ResetPassword { get; }




        public LoginVM()
        {
            login = new Command(OnLogin);
            ResetPassword = new Command(OnForgotPassword);
        }

        public bool IsError
        {
            get => isError;
            set
            {
                SetProperty(ref isSent, value);
                OnPropertyChanged(nameof(IsError));
            }
        }


        public bool IsSent
        {
            get => isSent;
            set
            {
                SetProperty(ref isSent, value);
                OnPropertyChanged(nameof(IsSent));
            }
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



        public async void OnForgotPassword()
        {
            IsBusy = true;
            AuthenticationService authentication = new AuthenticationService();
            if(Username.Length > 4)
            {
               var _IsSent = await authentication.ForgotPassword(Username);

                if (_IsSent)
                {
                    IsSent = true;
                }
                else
                {
                    IsSent = false;
                }
            }

            IsBusy = false;
        }

     public  async void OnLogin()
        {
            IsBusy = true;
           
            if(!string.IsNullOrEmpty(Username) || Password.Length > 6)
            {
                AuthMemory authMemory = new AuthMemory();
                AuthenticationService authentication = new AuthenticationService();
               var IsLoged = await authentication.Login(Username, Password);

                if (IsLoged != string.Empty)
                {
                    authMemory.SetToken(IsLoged);
                    await Shell.Current.GoToAsync("//TabbedPage");
                }
                else
                {
                    IsError = true;
                }

            }
            else
            {

            }


            IsBusy = false;
        }





    }
}
