using Xamarin.Forms;
using ChatDemo.Controls;
using ChatDemo.Droid.Renderer;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using Android.Graphics;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(RoundedEntry), typeof(RoundedEntryRenderer))]

namespace ChatDemo.Droid.Renderer
{
    class RoundedEntryRenderer:EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
              //  Control.Background = null;
                Control.Background.SetColorFilter(Android.Graphics.Color.LightGray, PorterDuff.Mode.SrcAtop);

             //   GradientDrawable gd = new GradientDrawable();
             //  gd.SetColor(Color.White.ToAndroid());
             ////   gd.SetCornerRadius(25);
             //   gd.SetStroke(2, Color.LightGray.ToAndroid());
             //   Control.SetBackgroundDrawable(gd);
            }
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);         
        }
    }
}