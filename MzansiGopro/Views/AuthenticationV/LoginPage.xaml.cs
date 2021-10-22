using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using MzansiGopro.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MzansiGopro.Views.AuthenticationV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void sign_Tapped(object sender, EventArgs e)
        {

            

            await Shell.Current.GoToAsync("SignInPage");
        }
    }
}