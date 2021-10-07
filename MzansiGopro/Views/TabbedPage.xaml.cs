using MzansiGopro.Services.LocalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

        protected override void OnAppearing()
        {
            
            base.OnAppearing();
        }

    }
}