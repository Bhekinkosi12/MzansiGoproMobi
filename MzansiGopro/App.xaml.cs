using MzansiGopro.Services;
using MzansiGopro.Views;
using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using MzansiGopro.Services.BusinessData;
using System.Threading.Tasks;
using MzansiGopro.Services.EventsSV;

namespace MzansiGopro
{
    public partial class App : Application
    {

        public App()
        {
            VersionTracking.Track();
            InitializeComponent();


            DependencyService.Register<MockDataStore>();

            MainPage = new AppShell();
        }


        

       async void splash()
        {
            MainPage = new Splash();
            await Task.Delay(1000);
            MainPage = new AppShell();
        }


        protected override void OnStart()
        {
           
           
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
