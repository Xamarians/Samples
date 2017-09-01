using Xamarin.Forms;
using ChatDemo.Controls;
using ChatDemo.Droid.Renderer;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;

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
                Control.Background = null;
                GradientDrawable gd = new GradientDrawable();
               gd.SetColor(Color.White.ToAndroid());
                gd.SetCornerRadius(25);
                gd.SetStroke(2, Color.LightGray.ToAndroid());
                Control.SetBackgroundDrawable(gd);
            }
        }
    }
}