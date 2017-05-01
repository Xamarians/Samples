using Android.Content;
using Android.Telephony;
using CallMessageEmailDemo.Interfaces;
using CallMessageEmailDemo.Droid.DS;


[assembly: Xamarin.Forms.Dependency(typeof(Media))]
namespace CallMessageEmailDemo.Droid.DS
{
    class Media : IMedia
    {
        public void SendEmail()
        {
            var email = new Intent(Android.Content.Intent.ActionSend);
            email.PutExtra(Android.Content.Intent.ExtraEmail,
            new string[] { "person1@xamarin.com", "person2@xamrin.com" });
            email.PutExtra(Android.Content.Intent.ExtraCc,
            new string[] { "person3@xamarin.com" });
            email.PutExtra(Android.Content.Intent.ExtraSubject, "Hello Email");
            email.PutExtra(Android.Content.Intent.ExtraText,
            "Hello from Xamarin.Android");
            email.SetType("message/rfc822");
            (Xamarin.Forms.Forms.Context).StartActivity(email);
        }

        public void SendMessage()
        {
            SmsManager.Default.SendTextMessage("1234567890",
                null, "Hello from Xamarin.Android", null, null);
            var smsUri = Android.Net.Uri.Parse("smsto:1234567890");
            var smsIntent = new Intent(Intent.ActionSendto, smsUri);
            smsIntent.PutExtra("sms_body", "Hello from Xamarin.Android");
            (Xamarin.Forms.Forms.Context).StartActivity(smsIntent);
        }
        public void MakeACall()
        {
            Intent intent = new Intent(Intent.ActionDial, Android.Net.Uri.Parse("tel:" + "Your Phone_number"));
            (Xamarin.Forms.Forms.Context).StartActivity(intent);
        }
    }
}