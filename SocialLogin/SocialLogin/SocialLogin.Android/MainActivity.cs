using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Facebook;
using Android.Content;

namespace SocialLogin.Droid
{
	[Activity (Label = "SocialLogin", Icon = "@drawable/icon", Theme= "@style/CustomTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
		protected override void OnCreate (Bundle bundle)
		{
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);
			LoadApplication (new SocialLogin.App ());

            FacebookSdk.ApplicationId = App.FacebookAppId;
            FacebookSdk.ApplicationName = App.FacebookAppName;
            FacebookSdk.SdkInitialize(this);
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }

    }
}

