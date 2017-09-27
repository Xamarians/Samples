using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ImageSliderDemo
{
   public interface ImageResizer
    {
       Task<byte[]> ResizeImageAndroid(ImageSource image, float width, float height);
    }
}
