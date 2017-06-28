
namespace CompressedVideoDemo.ViewModel
{
    public class CompressVideoViewModel : BaseViewModel
    {
        string _videoPath;
        public string VideoPath
        {
            get { return _videoPath; }
            set { SetProperty(ref _videoPath, value); }
        }

        string _compressVideoPath;
        public string CompressVideoPath
        {
            get { return _compressVideoPath; }
            set { SetProperty(ref _compressVideoPath, value); }
        }

        bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }
    }
}
