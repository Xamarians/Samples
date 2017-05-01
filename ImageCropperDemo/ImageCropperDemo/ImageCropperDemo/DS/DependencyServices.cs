
using Xamarin.Forms;
using ImageCropperDemo.Interface;
namespace ImageCropperDemo.DS
{
    public static class DependencyServices
    {
        public static Imedia media
        {
            get
            {
                return DependencyService.Get<Imedia>(DependencyFetchTarget.GlobalInstance);

            }
        }
    }
}

