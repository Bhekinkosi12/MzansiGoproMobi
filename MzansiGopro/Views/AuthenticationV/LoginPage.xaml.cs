using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using MzansiGopro.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.CommunityToolkit.Extensions;
using MzansiGopro.Views.PopupV.IntroV;
using MzansiGopro.Services.AuthSercurity;
using MzansiGopro.Services.LocalData;

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

        private async void forgot_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("ForgotPasswordPage");
        }


        protected override void OnAppearing()
        {

            var id = Preferences.Get("FirstUse", string.Empty);
            
            if (VersionTracking.IsFirstLaunchEver)
            {
                LocalUserService localUser = new LocalUserService();
                localUser.AddFirstUse();
                Shell.Current.ShowPopup(new AppIntroPop());

            }
            else
            {

            }


            base.OnAppearing();
        }

    }
}