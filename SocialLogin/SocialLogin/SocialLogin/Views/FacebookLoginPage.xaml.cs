using Newtonsoft.Json;
using SocialLogin.Platforms;
using System;
using Xamarin.Forms;

namespace SocialLogin.Views
{
    public partial class FacebookLoginPage : ContentPage
    {
		public FacebookLoginPage()
		{
            Title = "Facebook login";
			InitializeComponent ();
            BackgroundColor = Color.FromHex("#E9EBEE");
        }

        public void OnLoginClicked(object sender, EventArgs e)
        {
           XamariansSocialSdk.FacebookLoginAsync(async (fbResult) =>
            {
                if (fbResult.Status == FBStatus.Success)
                {
                    if (fbResult.JsonData != null)
                    {
                        try
                        {
                            var resultData = JsonConvert.DeserializeObject<FBUserProfile>(fbResult.JsonData);
                            if (resultData != null)
                            {
                                lblFbLoginName.Text = "Facebook Login Name- " + resultData.Name;
                                btnLogout.IsVisible = true;
                            }
                        }
                        catch { }
                    }
                    else
                    {
                        await DisplayAlert("Error", "Sorry, We are unable to process your request. Please try later.", "Ok");
                    }
                }
                else
                {
                    await DisplayAlert("Authentication Failed", fbResult.Message, "Ok");
                }
            });

        }

        public void OnLogoutClicked(object sender, EventArgs e)
        {
            lblFbLoginName.IsVisible=false;
            btnLogout.IsVisible = false;
        }
    }
}
