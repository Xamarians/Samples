using SocialLogin.Models;
using System;

using Xamarin.Forms;

namespace SocialLogin.Views
{
	public class HomePage : MasterDetailPage
	{
        MenuPage menuPage;
        public HomePage ()
		{
            Title = "Social Login Demo";
            menuPage = new MenuPage();
            menuPage.ListView.ItemSelected += OnItemSelected;
            Master = menuPage;
            Detail = new FacebookLoginPage();
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MenuPageItem;
            if (item != null)
            {
                Detail = (Page)Activator.CreateInstance(item.TargetType);
                menuPage.ListView.SelectedItem = null;
                Title = Detail.Title;
                IsPresented = false;
            }
        }
    }
}
