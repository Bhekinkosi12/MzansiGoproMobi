using MzansiGopro.Services.LocalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MzansiGopro.Services.LocalData;
using MzansiGopro.Services.AuthSercurity;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using Xamarin.CommunityToolkit.Extensions;
using MzansiGopro.Views.PopupV.IntroV;

namespace MzansiGopro.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPage : ContentPage
    {
        public TabbedPage()
        {
            InitializeComponent();
        }

        private async void SfTabView_CenterButtonTapped(object sender, EventArgs e)
        {
            
        }

        private async void center_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("MainUserProfilePage");
        }

        protected async override void OnAppearing()
        {
           
            var token = Preferences.Get("RefreshToken", string.Empty);

            AuthenticationService authentication = new AuthenticationService();

            

            if(token != string.Empty)
            {

              await authentication.AutoLogin();
            }
            else
            {
                await Shell.Current.GoToAsync("//LoginPage");
            }


            base.OnAppearing();
        }




       

    }
}