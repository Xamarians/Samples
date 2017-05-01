using CallMessageEmailDemo.Interfaces;
using MessageUI;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using CallMessageEmailDemo.iOS.DS;
using Foundation;

[assembly: Xamarin.Forms.Dependency(typeof(Media))]
namespace CallMessageEmailDemo.iOS.DS
{
	class Media : IMedia
	{
		MFMailComposeViewController mailController;
		private static UIViewController GetController()
		{

			var vc = UIApplication.SharedApplication.KeyWindow.RootViewController;
			while (vc.PresentedViewController != null)
				vc = vc.PresentedViewController;
			return vc;
		}
		public void MakeACall()
		{
			var url = new NSUrl("tel:" + "9811521033");
			UIApplication.SharedApplication.OpenUrl (url);
		}
		public void SendEmail()
		{
			if (MFMailComposeViewController.CanSendMail)
			{
				mailController = new MFMailComposeViewController();
				mailController.SetToRecipients(new string[] { "john@doe.com" });
				mailController.SetSubject("mail test");
				mailController.SetMessageBody("this is a test", false);
                GetController().PresentViewController(mailController, true, null);
				mailController.Finished += (object s, MFComposeResultEventArgs args) =>
				{
					Console.WriteLine(args.Result.ToString());
					args.Controller.DismissViewController(true, null);


				};
			}
		}

		public void SendMessage()
		{
				var smsTo = NSUrl.FromString("sms:18015551234");
			UIApplication.SharedApplication.OpenUrl(smsTo);

		}
	}
}
