
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Content;

namespace ImageCropperDemo.Droid
{
	[Activity (Label = "ImageCropperDemo", Icon = "@drawable/icon", Theme="@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar; 

			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);
            XamariansLib.Droid.XamLibSdkAndroid.Init(this);

            LoadApplication(new ImageCropperDemo.App ());
		}

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            DS.IntentHelpers.OnActivityResult(requestCode, resultCode, data);
            XamariansLib.Droid.XamLibSdkAndroid.HandleActivity(requestCode, resultCode, data);
        }
    }
}

