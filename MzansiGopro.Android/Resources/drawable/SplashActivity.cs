using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MzansiGopro.Droid.Resources.drawable
{
    [Activity(Label = "Mzansi Go pro", Theme = "@style/splashTheme.Splash",MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Log.Debug(TAG, "SplashActivity.OnCreate");
            // Create your application here
        }

        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }
        public override void OnBackPressed()
        {
          
        }



        async void SimulateStartup()
        {
           
            await Task.Delay(3000);
            
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));


        }

       






    }
}