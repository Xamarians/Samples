using System;
using Xamarin.Forms;

namespace SocialLogin.Views
{
    public partial class TwitterLoginPage : ContentPage
    {
		public TwitterLoginPage ()
		{
            Title = "Twitter Login";
            InitializeComponent ();
            BackgroundColor = Color.FromHex("#CCD6DD"); 
        }
        public async void OnLoginClicked(object sender, EventArgs e)
        {
            var result = await App.XamariansSocialSdk.TwitterLoginAsync();

            if (result.IsAuthenticated)
            {
                lblTwitterLoginName.Text = "Twitter Login Name- " + result.UserName;
                btnLogout.IsVisible = true;
            }
            else
                await DisplayAlert("Error", result.ErrorMessage, "Ok");
        }
        public void OnLogoutClicked(object sender, EventArgs e)
        {
            lblTwitterLoginName.IsVisible = false;
            btnLogout.IsVisible = false;
        }
    }
}
