using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MzansiGopro.ViewModels.EventsVM.AdminEventsVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;

namespace MzansiGopro.Views.EventsV.AdminEventsV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminDisplayEventPage : ContentPage
    {
        public AdminDisplayEventPage()
        {
            InitializeComponent();
        }



        protected override void OnAppearing()
        {

            var model = BindingContext as AdminDisplayViewModel;

            var location = model.Location.Split(';');

            locationMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(double.Parse(location[0]), double.Parse(location[1])), Distance.FromMeters(300)));



            base.OnAppearing();
        }


    }
}