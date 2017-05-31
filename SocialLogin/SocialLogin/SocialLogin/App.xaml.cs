using SocialLogin.Platforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace SocialLogin
{
	public partial class App : Application
	{

        public const string GoogleClientId = "24516874466-c6a9n20n71f50mirf8r74pl1h8bva8qt.apps.googleusercontent.com";
        public const string GoogleClientSecret = "RbZWrF2v-B7ged0n9PL8q-rN";

        public const string TwitterConsumerKey = "1iDh38UySSOoGb4jgGtj1w4my";
        public const string TwitterConsumerSecret = "cCRzZzqWglr0xvmtrHHYsSDgK9fHk4SbVb5tyEY65x8FHbGs3S";
        //public const string TwitterCallbackUrl = "https://mobile.twitter.com/";
        public const string TwitterCallbackUrl = "https://mobile.twitter.com/home";

        public const string LinkedinClientId = "81zu8etref7kdv";
        public const string LinkedinClientSecret = "3rcxMKBDMWIsvg96";


        public const string FacebookAppId = "162285324253988";
        public const string FacebookAppName = "Social Login";

        internal static readonly XamariansSocialSdk XamariansSocialSdk = new XamariansSocialSdk();

        public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new Views.HomePage());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
