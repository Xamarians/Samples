using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ImageSliderDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            //Create the views for the Slider

            string[] item = new string[4];
            item[0] = "image1.png";
            item[1] = "image2.jpg";
            item[2] = "image3.jpg";
            item[3] = "image4.jpg";

            Image[] image = new Image[4];
            for (int i = 0; i < 4; i++)
            {
                image[i] = new Image();
                var data = DependencyService.Get<ImageResizer>().ResizeImageAndroid(ImageSource.FromFile(item[i]), 400, 400);
                image[i].Source = ImageSource.FromStream(() => new MemoryStream(data.Result));
                image[i].Aspect = Aspect.AspectFill;

            }
            //Create the Slider by passing in the first view and the sizes
            SliderView slider = new SliderView(image[1], 450, 300)
            {

                StyleId = "SliderView",
                MinimumSwipeDistance = 50
            };

            //Add the views to the slider
            slider.Children.Add(image[0]);
            slider.Children.Add(image[2]);
            slider.Children.Add(image[3]);



            //Set the content of the page
            Content = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Vertical,
                Margin = 10,
                Children = {
                    slider,
                }
            };
        }
    }
}