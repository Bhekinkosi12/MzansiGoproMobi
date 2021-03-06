using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MzansiGopro.Views.CompanyV;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using MzansiGopro.ViewModels;
using MzansiGopro.ViewModels.ProfileVM;

namespace MzansiGopro.Views.ProfileV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainUserProfilePage : ContentPage
    {
        public MainUserProfilePage()
        {
            InitializeComponent();
        }

        private  void business_Tapped(object sender, EventArgs e)
        {
            var model = BindingContext as ProfileViewModel;
            model.BusinessValable();



        }

        private async void events_Tapped(object sender, EventArgs e)
        {
            try
            {

            await Shell.Current.GoToAsync("MainAdminEventsPage");
            }
            catch
            {
                await Shell.Current.GoToAsync("LoginPage");
            }
        }

        protected override void OnAppearing()
        {
            var model = BindingContext as ProfileViewModel;

            model.OnIsLogged();

            base.OnAppearing();
        }
    }
}