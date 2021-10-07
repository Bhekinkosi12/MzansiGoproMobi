using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MzansiGopro.ViewModels.EventsVM.AdminEventsVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MzansiGopro.Views.EventsV.AdminEventsV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainAdminEventsPage : ContentPage
    {
        public MainAdminEventsPage()
        {
            InitializeComponent();
        }

        private async void addEvent_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("MainAddEventPage");
        }


        protected override void OnAppearing()
        {
            var model = BindingContext as AdminEventsViewModel;

            model.OnLoadPage();

            base.OnAppearing();
        }


    }
}