using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android;
using NativeMedia;
using Android.Content;

namespace MzansiGopro.Droid
{
    [Activity(Label = "Mzansi Go pro", Icon = "@mipmap/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        const int RequiredLocationId = 0;

        readonly string[] LocationPermission =
        {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation
        };

        protected override void OnStart()
        {
            base.OnStart();
            if((int)Build.VERSION.SdkInt >= 23)
            {
                if(CheckSelfPermission(Manifest.Permission.AccessFineLocation) != Permission.Granted)
                {
                    RequestPermissions(LocationPermission, RequiredLocationId);
                }
                else
                {

                }

            }



        }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            NativeMedia.Platform.Init(this, savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            LoadApplication(new App());
        }




        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if(NativeMedia.Platform.CheckCanProcessResult(requestCode,resultCode, data))
            {
                NativeMedia.Platform.OnActivityResult(requestCode, resultCode, data);
            }

            base.OnActivityResult(requestCode, resultCode, data);
        }










        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            if(requestCode == RequiredLocationId)
            {
                if((grantResults.Length == 1) && (grantResults[0] == (int)Permission.Granted))
                {
                    // permission granted
                }
                else
                {
                    // permission denied
                }

            }
            else
            {
                
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }




            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}