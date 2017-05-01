
using ImageCropperDemo.Helpers;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ImageCropperDemo.ViewModel
{
    class PhotoViewModel : BaseViewModel
    {
        string _setImage;
        public string SetImage
        {
            get { return _setImage; }
            set { SetProperty(ref _setImage, value); }
        }
    }
}
       
