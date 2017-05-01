using System;

namespace ImageCropperDemo.Views
{
    class IosImageCropperPage:BaseContentPage
    {
        public string ImageUrl;
        public string DestinationPath;
        public EventHandler CropDone;
        public EventHandler CropCancel;

        public IosImageCropperPage(string imageUrl, string destinationPath)
        {
            ImageUrl = imageUrl;
            DestinationPath = destinationPath;
        }

        public async void OnSave()
        {
            if (CropDone != null)
                CropDone.Invoke(this, null);

            await Navigation.PopAsync();
        }

        public async void OnCancel()
        {
            if (CropCancel != null)
                CropCancel.Invoke(this, null);

            await Navigation.PopAsync();
        }
    }
}

