
using Xamarin.Forms;
namespace ImageCropperDemo.Views
{
    public abstract class BaseContentPage : ContentPage
    {
        public BaseContentPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);
            BackgroundColor = Color.White;
        }

        protected void HideListViewRefreshing(object sender, System.EventArgs e)
        {
            (sender as ListView).IsRefreshing = false;
        }
    }
}
