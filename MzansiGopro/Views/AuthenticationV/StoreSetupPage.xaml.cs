using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NativeMedia;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System.Threading;
using MzansiGopro.ViewModels.AuthenticationVM;
using MzansiGopro.Models.microModel;
using MzansiGopro.Services;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;
using Xamarin.CommunityToolkit.Extensions;
using MzansiGopro.Views.PopupV.ErrorHandlingV;
using MzansiGopro.Views.PopupV;
using Plugin.Geolocator;
using MzansiGopro.Views.PopupV.AlertsV;
using Xamarin.CommunityToolkit.Extensions;

namespace MzansiGopro.Views.AuthenticationV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreSetupPage : ContentPage
    {
        public StoreSetupPage()
        {
            InitializeComponent();
            current_Location();
        }

        private async void firstNext_Clicked(object sender, EventArgs e)
        {
            var model = BindingContext as SignInVM;

         

            if (string.IsNullOrEmpty(model.Location))
            {
                //  await Shell.Current.DisplayAlert("Alert", "Invalid location", "OK");

                currentLocation.TextColor = Color.Red;
                currentLocation.BorderColor = Color.Red;

            }
            else
            {

            first.IsVisible = false;

            second.IsVisible = true;
            }

        }

        private async void addimage_Clicked(object sender, EventArgs e)
        {


            var binds = BindingContext as SignInVM;

            


            var cancel = new CancellationTokenSource();
            IMediaFile[] files = null;


            try
            {
                var request = new MediaPickRequest(5, MediaFileType.Image, MediaFileType.Video)
                {
                    PresentationSourceBounds = System.Drawing.Rectangle.Empty,
                    Title = "Select"
                };

                cancel.CancelAfter(TimeSpan.FromMinutes(5));

                var results = await MediaGallery.PickAsync(request, cancel.Token);


                files = results?.Files?.ToArray();







            }
            catch (OperationCanceledException)
            {

            }
            catch(Exception)
            {
                Shell.Current.ShowPopup(new UnexpectedErrorPop());
            }
            finally
            {
                cancel.Dispose();
            }






            if(files == null)
            {
                Shell.Current.ShowPopup(new UnexpectedErrorPop());
            }





            

            foreach (var media in files)
            {


            }









        }

        private async void currentLocation_Clicked(object sender, EventArgs e)
        {
            var model = BindingContext as SignInVM;

            locationFrame.IsVisible = true;

            /*
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
            finally
            {

            model.IsBusy = false;
            }

            */



        }

        private void secondback_Clicked(object sender, EventArgs e)
        {
            first.IsVisible = true;

            second.IsVisible = false;
        }

        private async void firstBack_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private void create_Clicked(object sender, EventArgs e)
        {
            
            var model = BindingContext as SignInVM;
            model.IsBusy = true;

            if(model.Offer.Count != 0 && model.Images.Count != 0)
            {
                model.AddUserToDB();
            }
            else
            {

            }

        }














        bool firstTime = true;
        string location_ { get; set; } = "";
        private void Map_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            var a = (Xamarin.Forms.Maps.Map)sender;

            if (a.VisibleRegion != null)
            {
                if (firstTime)
                {
                    firstTime = false;
                    return;
                }


                if (location_ != "")
                {
                    select.BackgroundColor = Color.FromHex("#591da9");
                    select.TextColor = Color.FromHex("#fff");

                }

                location_ = $"{a.VisibleRegion.Center.Latitude};{a.VisibleRegion.Center.Longitude}";

            }



        }

        async void current_Location()
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
                var model = BindingContext as SignInVM;

            if (!string.IsNullOrEmpty(location_))
            {
                model.Location = location_;
                locationFrame.IsVisible = false;
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