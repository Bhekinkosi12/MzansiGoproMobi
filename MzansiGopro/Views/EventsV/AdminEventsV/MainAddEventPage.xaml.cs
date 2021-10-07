using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MzansiGopro.ViewModels.EventsVM.AdminEventsVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.IO;
using MzansiGopro.Services;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;

namespace MzansiGopro.Views.EventsV.AdminEventsV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainAddEventPage : ContentPage
    {
        public MainAddEventPage()
        {
            InitializeComponent();
        }

        private async void cancel_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private void save_Clicked(object sender, EventArgs e)
        {
            var model = BindingContext as AdminEventsViewModel;

            if (!string.IsNullOrEmpty(model.Cover) && !string.IsNullOrEmpty(model.Name) && !string.IsNullOrEmpty(model.Location) )
            {
                model.OnAddEvent();
            }
            else
            {

            }
            


        }

        private async void selectPhoto_Tapped(object sender, EventArgs e)
        {
            var model = BindingContext as AdminEventsViewModel;

            model.OnAddMediaCover();


        }

        private async void useLocation_Clicked(object sender, EventArgs e)
        {
            var model = BindingContext as AdminEventsViewModel;
            model.IsBusy = true;

            try
            {

                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync();
                signMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude), Distance.FromMiles(1)));


                signFrame.IsVisible = true;




                model.Location = $"{position.Latitude};{position.Longitude}";

            }
            catch
            {
                await Shell.Current.DisplayAlert("Alert", "Please allow location permission within your settings and keep location On", "OK");

            }

            model.IsBusy = false;
        }



        protected override void OnAppearing()
        {
            var model = BindingContext as AdminEventsViewModel;
            model.GetSelectedEvent();
            base.OnAppearing();
        }

    }
}