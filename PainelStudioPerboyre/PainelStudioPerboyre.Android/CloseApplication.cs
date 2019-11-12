using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PainelStudioPerboyre.Droid;
using PainelStudioPerboyre.Interface;
using Xamarin.Forms;


[assembly: Xamarin.Forms.Dependency(typeof(CloseApplication))]
namespace PainelStudioPerboyre.Droid
{
    public class CloseApplication : ICloseApplication
    {
        public void closeApplication()
        {
            var activity = (Activity)Forms.Context;
            activity.FinishAffinity();
        }
    }
}