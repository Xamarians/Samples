using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Firebase.Iid;
using ChatDemo.Services;
using System.Linq;
using Xamarin.Forms;

namespace ChatDemo.Droid
{
    [Service]
    [IntentFilter(new[] {
        "com.google.firebase.INSTANCE_ID_EVENT"
    })]

    class MyFirebaseIIDService : FirebaseInstanceIdService
    {
        const string TAG = "MyFirebaseIIDService";
        public string refreshedToken;
       
        public override void OnTokenRefresh()
        {
            refreshedToken = FirebaseInstanceId.Instance.Token;
            Console.WriteLine(TAG, "Refreshed token: " + refreshedToken);
            SendRegistrationToServer(refreshedToken);
        }
        
        private  void SendRegistrationToServer(string token)
        {
           
        }
    }
}
