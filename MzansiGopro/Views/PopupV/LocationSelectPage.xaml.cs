using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;
using MzansiGopro.Views.PopupV.AlertsV;
using Xamarin.CommunityToolkit.Extensions;

namespace MzansiGopro.Views.PopupV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationSelectPage : Popup
    {
        public LocationSelectPage()
        {
            InitializeComponent();
            currentLocation();


        }
        bool firstTime = true;
        string location { get; set; } = "";
        private void Map_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            var a = (Xamarin.Forms.Maps.Map)sender;

            if(a.VisibleRegion != null)
            {
                if (firstTime)
                {
                    firstTime = false;
                    return;
                }


                if(location != "")
                {
                select.BackgroundColor = Color.FromHex("#591da9");
                select.TextColor = Color.FromHex("#fff");

                }

                location = $"{a.VisibleRegion.Center.Latitude};{a.VisibleRegion.Center.Longitude}";

            }



        }

        async void currentLocation()
        {

            try
            {
                
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync();

                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude), Distance.FromMeters(400)));

            }
            catch
            {
                Shell.Current.ShowPopup(new KeepLocationOn());
            }
        }

        private void select_Clicked(object sender, EventArgs e)
        {
            if (!firstTime)
            {
                Dismiss(null);
            }
            else
            {

            }
        }

        private void maptouch_Tapped(object sender, EventArgs e)
        {
            notified.IsVisible = false;
        }
    }
}