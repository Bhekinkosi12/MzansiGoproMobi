using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MzansiGopro.ViewModels.AuthenticationVM;
using MzansiGopro.Services.AuthSercurity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MzansiGopro.Views.AuthenticationV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();
        }

        string defaultColor = "#191919";
        string errorColor = "#800000";
        string successColor = "#009000";







        private void number_TextChanged(object sender, TextChangedEventArgs e)
        {
          
        }

        private void password_TextChanged(object sender, TextChangedEventArgs e)
        {
          

        }

        private void confirmPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (password.Text != confirmPassword.Text)
            {
                confirmPassword.TextColor = Color.FromHex(errorColor);
            }
            else
            {
                confirmPassword.TextColor = Color.FromHex(successColor);
            }


        }

        private async void signIn_Clicked(object sender, EventArgs e)
        {
            AuthenticationService authenticationService = new AuthenticationService();
            var model = BindingContext as SignInVM;
            model.IsBusy = true;
            if (!InputCheckIsError())
            {
                // Check if email is already registered


                var isRegistered = await authenticationService.CheckEmailExist(entryEmail.Text);

                if (isRegistered)
                {
                    emailError.IsVisible = true;
                }
                else
                {

                    if (model.IsShop)
                    {
                        model.AddUserRunTimeUser();
                        await Shell.Current.GoToAsync("StoreSetupPage");
                    }
                    else
                    {

                   


                        model.Onsignin();


                    }
                }





            }
            else
            {

            }
        }




       bool InputCheckIsError()
        {
           bool IsError = false;
            

             

            if (string.IsNullOrEmpty(entryFullName.Text))
            {
                IsError = true;
                entryFullName.TextColor = Color.FromHex(errorColor);

            }
            else
            {

            }


            if (!entryEmail.Text.Contains('@'))
            {
                IsError = true;
                entryEmail.TextColor = Color.FromHex(errorColor);

            }
            else
            {

            }
            
            if(string.IsNullOrEmpty(entryNumber.Text) || entryNumber.Text.Length != 10)
            {
                IsError = true;
                entryNumber.TextColor = Color.FromHex(errorColor);
            }
            else { }


           if(password.Text != confirmPassword.Text || string.IsNullOrEmpty(password.Text))
            {
                IsError = true;
                confirmPassword.TextColor = Color.FromHex(errorColor);
            }else { }

            return IsError;

        }

        private void entryFullName_TextChanged(object sender, TextChangedEventArgs e)
        {
          //  entryFullName.TextColor = Color.FromHex(defaultColor);


        }

        private void entryEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
           // entryEmail.TextColor = Color.FromHex(defaultColor);

            if (entryEmail.Text.Contains('@'))
            {
                entryEmail.TextColor = Color.FromHex(successColor);
            }
            else
            {
                entryEmail.TextColor = Color.FromHex(errorColor);
            }

        }

        private void entryNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            //entryNumber.TextColor = Color.FromHex(defaultColor);

            if(entryNumber.Text.Length != 10)
            {
                entryNumber.TextColor = Color.FromHex(errorColor);
            }
            else
            {
                entryNumber.TextColor = Color.FromHex(successColor);
            }

        }

        private async void loginBTN_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        protected override void OnAppearing()
        {
            var model = BindingContext as SignInVM;
            model.IsBusy = false;
            base.OnAppearing();
        }
    }
}