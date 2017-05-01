
using Xamarin.Forms;

namespace ImageCropperDemo
{
	public partial class App : Application
	{
        public const string PictureFolderPath = "ImageCropper/Pictures/";

        public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new Views.PhotoPage());
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
