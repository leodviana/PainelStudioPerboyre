using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PainelStudioPerboyre.Droid
{
    [Activity(
          Theme = "@style/Theme.Splash",
          MainLauncher = false,
         ScreenOrientation = ScreenOrientation.Locked,
          NoHistory = true)]
    public class splashperboyreActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            System.Threading.Thread.Sleep(1);
            StartActivity(typeof(MainActivity));
        }
    }
}