using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace CompressedVideoDemo.Droid
{
	[Activity (Label = "CompressedVideoDemo", Icon = "@drawable/icon", Theme="@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar; 

			base.OnCreate (bundle);
            AndroidEnvironment.UnhandledExceptionRaiser += AndroidEnvironment_UnhandledExceptionRaiser;    
            global::Xamarin.Forms.Forms.Init (this, bundle);
			LoadApplication (new CompressedVideoDemo.App ());
		}

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            DS.IntentHelpers.OnActivityResult(requestCode, resultCode, data);


            if (DS.IntentHelper.IsMobileIntent(requestCode))
            {
                DS.IntentHelper.ActivityResult(requestCode, resultCode, data);
            }
        }

        private void AndroidEnvironment_UnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs e)
        {

        }
    }
}

