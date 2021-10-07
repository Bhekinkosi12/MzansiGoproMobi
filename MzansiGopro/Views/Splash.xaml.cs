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
    public partial class Splash : ContentPage
    {
        public Splash()
        {
            InitializeComponent();
           
        }

        protected override void OnAppearing()
        {
            LocalUserService localDB = new LocalUserService();
            localDB.GetLocalUser();
            

            base.OnAppearing();
        }
        async void pass()
        {
            try
            {

            await Task.Delay(2000);
            await Shell.Current.GoToAsync("//MainPage");
            }
            catch(Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }

        }
    }
}