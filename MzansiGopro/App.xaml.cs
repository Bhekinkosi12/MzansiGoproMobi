using MzansiGopro.Services;
using MzansiGopro.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Threading.Tasks;

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
            await Task.Delay(2000);
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
