using System;
using CompressedVideoDemo.DS;
using Xamarin.Forms;
using CompressedVideoDemo.ViewModel;
using System.IO;

namespace CompressedVideoDemo.Views
{
    public partial class CompressVideoPage : ContentPage
    {
        string _destFile = string.Empty;
        CompressVideoViewModel viewModel;
        public CompressVideoPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new CompressVideoViewModel();
            _destFile = Helpers.FileHelper.CreateNewVideoPath();
        }
        private async void ChooseVideoAndCompressBtn_Clicked(object Sender, EventArgs e)
        {
            var actions = new string[] { "Open Camera", "Open Gallery" };
            var action = await App.Current.MainPage.DisplayActionSheet("Choose Video", "Cancel", null, actions);
            if (actions[0].Equals(action))
            {
                viewModel.VideoPath = await DependencyServices.mobileFeature.RecordVideo();
                await App.Current.MainPage.DisplayAlert("Success", "Video is saved to gallery", "ok");
                CompressVideo();
            }
            else if (actions[1].Equals(action))
            {
                viewModel.VideoPath = await DependencyServices.mobileFeature.SelectVideo();
                CompressVideo();
            }
        }

        private async void CompressVideo()
        {
            if (!File.Exists(viewModel.VideoPath))
                return;

            var isCompressCmd = await App.Current.MainPage.DisplayAlert("Action", "Do You want to compress this video?", "Ok", "Cancel");
            if (isCompressCmd)
            {
                viewModel.IsBusy = true;
                var isCompressed = await DependencyServices.mobileFeature.CompressVideo(viewModel.VideoPath, _destFile);
                viewModel.IsBusy = false;

                if (isCompressed)
                {
                    viewModel.CompressVideoPath = _destFile;
                    await App.Current.MainPage.DisplayAlert("", "Video compressed successfully", "Ok");
                }
                else
                    await App.Current.MainPage.DisplayAlert("", "Video not compressed", "Ok");
            }
        }


        private async void PlayBtn_Clicked(object Sender, EventArgs e)
        {
            var actions = new string[] { "Original Video", "Compressed Video" };
            var action = await App.Current.MainPage.DisplayActionSheet("Choose Video To Play", "Cancel", null, actions);
            if (actions[0].Equals(action))
            {
                if (File.Exists(viewModel.VideoPath))
                {
                    var isPlayed = await DependencyServices.mobileFeature.PlayVideo(viewModel.VideoPath);
                }
                else
                    await App.Current.MainPage.DisplayAlert("Alert", "Please choose video first to play", "Ok");
            }
            else if (actions[1].Equals(action))
            {
                if (File.Exists(viewModel.CompressVideoPath))
                {
                    var isPlayed = await DependencyServices.mobileFeature.PlayVideo(viewModel.CompressVideoPath);
                }
                else
                    await App.Current.MainPage.DisplayAlert("Alert", "Please compress video first to play", "Ok");
            }
        }
    }
}
