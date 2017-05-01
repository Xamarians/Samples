using ImageCropperDemo.Helpers;
using ImageCropperDemo.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ImageCropperDemo.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PhotoPage : ContentPage
	{
        PhotoViewModel viewModel;
		public PhotoPage ()
		{
            InitializeComponent();
            BindingContext = viewModel = new PhotoViewModel();
        }

        public string destinationPath = null;
        public string sourcePath { get; set; }

        public async void TakePictureAsync()
        {
            var actions = new string[] { "Open Camera", "Open Gallery" };
            var action = await App.Current.MainPage.DisplayActionSheet("Choose Video", "Cancel", null, actions);
            if (actions[0].Equals(action))
            {
                destinationPath = FileHelper.GenerateUniqueFilePath("jpg");
                sourcePath = await XamariansLib.XamLibSdk.Instance.Media.OpenCameraAsync(destinationPath);
            }
            else if (actions[1].Equals(action))
            {
                sourcePath = await XamariansLib.XamLibSdk.Instance.Media.OpenImagePickerAsync();
                destinationPath = FileHelper.GenerateUniqueFilePath(FileHelper.GetExtension(sourcePath, "jpg"));
            }
            if (string.IsNullOrWhiteSpace(sourcePath))
            {
                return;
            }

#if __ANDROID__

            var intent = new Android.Content.Intent(Forms.Context, typeof(CropImage.CropImage));
            intent.PutExtra("image-path", sourcePath);
            intent.PutExtra("image-path-save", destinationPath);
            intent.PutExtra("scale", true);
            Droid.DS.IntentHelpers.StartIntent(intent, 2001, (r, d) =>
            {
                Device.BeginInvokeOnMainThread(() => ImageCropDone(r == Android.App.Result.Ok, EventArgs.Empty));
            });
#endif

#if __IOS__
			var cropperPage = new Views.IosImageCropperPage(sourcePath, destinationPath);
			cropperPage.CropDone += ImageCropDone;	
			await Navigation.PushAsync(cropperPage);
#endif
        }

        private void ImageCropDone(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(destinationPath))
            {
                UploadProfileImageAsync(destinationPath);
            }
        }
        public void UploadProfileImageAsync(string imagePath)
        {

           viewModel.SetImage = imagePath;

        }
    }
}

    

