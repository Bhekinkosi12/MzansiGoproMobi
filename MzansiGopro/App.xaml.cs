using MzansiGopro.Services;
using MzansiGopro.Views;
using System;
using Xamarin.Forms;
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
            InitializeComponent();

            DependencyService.Register<MockDataStore>();

            splash();
        }

       async void splash()
        {
            MainPage = new Splash();
            await Task.Delay(1000);
            MainPage = new AppShell();
        }


        protected override void OnStart()
        {
            MainBusinessDataBase businessDataBase = new MainBusinessDataBase();
            businessDataBase.InitialData();
            EventsServices _services = new EventsServices();
            _services.eventData();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
