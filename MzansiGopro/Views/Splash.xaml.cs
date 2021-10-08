using MzansiGopro.Services.LocalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using MzansiGopro.Views.PopupV.ErrorHandlingV;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MzansiGopro.Services.BusinessData;

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

            await Task.Delay(1000);
            await Shell.Current.GoToAsync("//MainPage");
            }
            catch
            {
                Shell.Current.ShowPopup(new UnexpectedErrorPop());
            }

        }
    }
}