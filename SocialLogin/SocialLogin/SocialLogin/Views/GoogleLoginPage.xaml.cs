using SocialLogin.Platforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SocialLogin.Views
{
	public partial class GoogleLoginPage : ContentPage
    {
		public GoogleLoginPage ()
		{
            Title = "Google Login";
			InitializeComponent ();
            BackgroundColor = Color.FromHex("#F4F4F4");
        }

        public async void OnLoginClicked(object sender, EventArgs e)
        {

            var result = await App.XamariansSocialSdk.GoogleLoginAsync();

            if (result.IsAuthenticated)
            {
                lblGoogleLoginName.Text = "Google Login Name- " + result.UserName;
                btnLogout.IsVisible = true;
            }
            else
                await DisplayAlert("Error", result.ErrorMessage, "Ok");

            //XamariansSocialSdk.GoogleLoginAsync(async (googleResult) =>
            //{
            //    if (googleResult.IsSuccess)
            //    {
            //        try
            //        {
            //            lblGoogleLoginName.Text = "Google Login Name- " + googleResult.Name;
            //        }
            //        catch { }
            //    }
            //    else
            //    {
            //        await DisplayAlert("Authentication Failed", googleResult.Message, "Ok");
            //    }
            //});
        }
        public void OnLogoutClicked(object sender, EventArgs e)
        {
            lblGoogleLoginName.IsVisible = false;
            btnLogout.IsVisible = false;
        }
    }
}
