using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
namespace SocialLogin.Views
{
    public partial class LinkedInLoginPage : ContentPage
    {
		public LinkedInLoginPage ()
		{
            Title = "LinkedIn Login";
            InitializeComponent();
            BackgroundColor = Color.FromHex("#333333");
        }

        public async void OnLoginClicked(object sender, EventArgs e)
        {
            var result = await App.XamariansSocialSdk.LinkedInLoginAsync();

            if (result.IsAuthenticated)
            {
                lblLinkedInLoginName.Text = "LinkedIn Login Name- " + result.UserName;
                btnLogout.IsVisible = true;
            }
            else
                await DisplayAlert("Error", result.ErrorMessage, "Ok");
        }
        public void OnLogoutClicked(object sender, EventArgs e)
        {
            lblLinkedInLoginName.IsVisible = false;
            btnLogout.IsVisible = false;
        }
    }
}
