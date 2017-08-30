using System;

using Android.App;
using Android.Content.PM;
using Android.OS;
using Firebase.Iid;
using Firebase;
using System.Threading.Tasks;
using Android.Runtime;

namespace ChatDemo.Droid
{
    [Activity(Label = "ChatDemo", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            AndroidEnvironment.UnhandledExceptionRaiser += AndroidEnvironment_UnhandledExceptionRaiser;
            base.OnCreate(bundle);
            Xamarin.Forms.Forms.Init(this, bundle);
            FirebaseApp.InitializeApp(this);
            Task.Run(() =>
           {
               var instanceId = FirebaseInstanceId.Instance;
               instanceId.DeleteInstanceId();
               Console.WriteLine("TAG", "{0} {1}", instanceId?.Token?.ToString(), instanceId.GetToken(GetString(Resource.String.gcm_defaultSenderId), Firebase.Messaging.FirebaseMessaging.InstanceIdScope));              
           });
            if (Intent.Extras != null)
            {
                foreach (var key in Intent.Extras.KeySet())
                {
                    var value = Intent.Extras.GetString(key);
                    Console.WriteLine("tag", "Key: {0} Value: {1}", key, value);
                }
            }
            LoadApplication(new App());           
        }
        private void AndroidEnvironment_UnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs e)
        {
            
            
                e.Handled = true;
                var alertDialog = new AlertDialog.Builder(this);
                alertDialog.SetTitle("Exception");
                alertDialog.SetMessage(e.Exception.Message + "____" + e.Exception.ToString());
                alertDialog.SetNeutralButton("Ok", (s, ee) => { });
                alertDialog.Show();
            
        }
    }
}

