using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MzansiGopro.ViewModels.EventsVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;

namespace MzansiGopro.Views.EventsV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DisplayEventPage : ContentPage
    {
        public DisplayEventPage()
        {
            InitializeComponent();
        }
         


        protected override void OnAppearing()
        {
            var model = BindingContext as EventDisplayViewModel;
            var location = model.Location.Split(';');

            locationMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(double.Parse(location[0]), double.Parse(location[1])), Distance.FromMeters(300)));



            
            

            base.OnAppearing();
        }

        private void goingToEvent_Clicked(object sender, EventArgs e)
        {

            var model = BindingContext as EventDisplayViewModel;
            model.OnGoingToEvent();

        }
    }
}