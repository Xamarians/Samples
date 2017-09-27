using Android.Content;
using Android.Graphics;
using ImageSliderDemo.Droid;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:Dependency(typeof(ImageResizeRenderer))]

namespace ImageSliderDemo.Droid
{
    public class ImageResizeRenderer: ImageResizer
    {
        public async Task<byte[]> ResizeImageAndroid(ImageSource image, float width, float height )
        {
            var task= new TaskCompletionSource<byte[]>();

            var handler = GetHandler(image);
            var originalImage = (Bitmap)null;
            Context context = Android.App.Application.Context;
            originalImage = await handler.LoadImageAsync(image,context);        

            float newHeight = 0;
            float newWidth = 0;

            var originalHeight = originalImage.Height;
            var originalWidth = originalImage.Width;

            if (originalHeight > originalWidth)
            {
                newHeight = height;
                float ratio = originalHeight / height;
                newWidth = originalWidth / ratio;
            }
            else
            {
                newWidth = width;
                float ratio = originalWidth / width;
                newHeight = originalHeight / ratio;
            }

            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)newWidth, (int)newHeight, true);

            originalImage.Recycle();

            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Png, 100, ms);
                resizedImage.Recycle();            
                var dt= ms.ToArray();
                task.SetResult(dt);
                return await task.Task;
            }
            
        }
       
        private static IImageSourceHandler GetHandler(ImageSource source)
        {
            IImageSourceHandler returnValue = null;
            if (source is UriImageSource)
            {
                returnValue = new ImageLoaderSourceHandler();
            }
            else if (source is FileImageSource)
            {
                returnValue = new FileImageSourceHandler();
            }
            else if (source is StreamImageSource)
            {
                returnValue = new StreamImagesourceHandler();
            }
            return returnValue;
        }     
    }
}
    

