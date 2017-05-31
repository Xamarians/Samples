using SocialLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SocialLogin.Views
{
	public partial class MenuPage : ContentPage
    {
        public ListView ListView { get { return listView; } }
        public MenuPage()
        {
            BackgroundColor = Color.FromHex("#D2DDCC");
            InitializeComponent();
            Title = "Menu";
            var menuPageItems = new List<MenuPageItem>();
            menuPageItems.Add(new MenuPageItem
            {
                Title = "Facebook",
                IconSource = "facebook.png",
                TargetType = typeof(FacebookLoginPage)
            });
            menuPageItems.Add(new MenuPageItem
            {
                Title = "Google",
                IconSource = "google.png",
                TargetType = typeof(GoogleLoginPage)
            });
            menuPageItems.Add(new MenuPageItem
            {
                Title = "LinkedIn",
                IconSource = "linkedin.png",
                TargetType = typeof(LinkedInLoginPage)
            });

            menuPageItems.Add(new MenuPageItem
            {
                Title = "Twitter",
                IconSource = "twitter.png",
                TargetType = typeof(TwitterLoginPage)
            });

            listView.ItemsSource = menuPageItems;
        }
    }
}
