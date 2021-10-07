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




       async void OnLogin()
        {
            IsBusy = true;
            if(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                await Shell.Current.GoToAsync("CompanyMainPage");
            }
            else
            {
                UserDataBase userData = new UserDataBase();
                PasswordAbcHash passChange = new PasswordAbcHash();
                bool IsVerified = false;

                try
                {


                    string HashedPass = passChange.StandardPasswordHash(Password);

                    //  var process = await userData.CheckEmailAndPass(Username, HashedPass);

                 //   var position = await userData.CheckEmailAndPasswordAsync(Username, HashedPass);


              //      var userGot = await userData.GetUserByEmail(Username);

                    var authsLiat = await userData.GetAllAuths();


                    foreach(var auth in authsLiat)
                    {
                        if(auth.Email == Username && auth.Password == HashedPass)
                        {
                            IsVerified = true;
                        }
                    }
                  

                    




                }
                catch(Exception ex)
                {
                    await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
                }




                if (IsVerified)
                {
                    await Shell.Current.DisplayAlert("Success", "You are loged in", "OK");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Success", "You are No loged in", "OK");
                }





            }
            IsBusy = false;
        }





    }
}
