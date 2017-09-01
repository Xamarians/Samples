
using Android.App;
using Android.OS;

namespace ChatDemo.Droid
{
    [Activity(Label = "Chat App", MainLauncher = true, NoHistory = true, Theme = "@style/Theme.Splash")]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(typeof(MainActivity));
            // Create your application here
        }
    }
}