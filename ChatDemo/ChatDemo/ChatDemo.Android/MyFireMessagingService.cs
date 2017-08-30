
using Android.App;
using Android.Content;
using Firebase.Messaging;
using Android.Graphics;
using ChatDemo.Models;
using ChatDemo.Helpers;
using Xamarin.Forms;
using ChatDemo.ViewModel;
using Newtonsoft.Json;

namespace ChatDemo.Droid
{
    [Service]
    [IntentFilter(new[] {
        "com.google.firebase.MESSAGING_EVENT"
    })]
    class MyFireMessagingService : FirebaseMessagingService
    {

        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);
            try
            {
                string jsonBody = message.GetNotification().Body;
                var item = JsonConvert.DeserializeObject<UserMessage>(jsonBody);
                Data.Repository.SaveOrUpdate(item);
                MessagingCenter.Send("", MessageCenterKeys.NewMessageReceived, item);
                string image = message.GetNotification().Icon;
                string sound = message.GetNotification().Sound;
                SendNotificatios(item.Message, item.Title);
            }
            catch
            {

            }
        }

        public void SendNotificatios(string body, string Header)
        {
            Notification.Builder builder = new Notification.Builder(this);
            builder.SetSmallIcon(Resource.Drawable.Icon);
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            PendingIntent pendingIntent = PendingIntent.GetActivity(this, 0, intent, 0);
            builder.SetContentIntent(pendingIntent);
            builder.SetLargeIcon(BitmapFactory.DecodeResource(Resources, Resource.Drawable.Icon));
            builder.SetContentTitle(Header);
            builder.SetContentText(body);
            builder.SetDefaults(NotificationDefaults.Sound);
            builder.SetAutoCancel(true);
            NotificationManager notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.Notify(1, builder.Build());
        }
    }
}


