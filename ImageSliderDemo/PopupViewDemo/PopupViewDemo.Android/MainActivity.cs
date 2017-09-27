
using Android.App;
using Android.Content.PM;
using Android.Views;
using Android.OS;
using Android.Util;
using Android.Content;

namespace PopupViewDemo.Droid
{
    [Activity (Label = "PopupViewDemo", Icon = "@drawable/icon", Theme="@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
        public static int height;
        public static int width;


        protected override void OnCreate (Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;
            var metrics = new DisplayMetrics();
         //   var windowManager = this.GetSystemService(WindowService) as IWindowManager;
          //  windowManager.DefaultDisplay.GetMetrics(metrics);
            base.OnCreate (bundle);
             height = metrics.HeightPixels;
             width = metrics.WidthPixels;
            global::Xamarin.Forms.Forms.Init (this, bundle);
			LoadApplication (new PopupViewDemo.App ());
		}
	}
}

