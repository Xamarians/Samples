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

#if __ANDROID__
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

#endif
#if __iOS__

			var result = await App.XamariansSocialSdk.FacebookLoginWithPublishPermissionAsync();
			if (result.IsAuthenticated)
			{
				lblFbLoginName.Text = "Facebook Login Name- " + result.AccountName;
                btnLogout.IsVisible = true;
			}
			else
				await DisplayAlert("Error", result.ErrorMessage, "Ok");

#endif

		}

        public void OnLogoutClicked(object sender, EventArgs e)
        {
            lblFbLoginName.IsVisible=false;
            btnLogout.IsVisible = false;
        }
    }
}
