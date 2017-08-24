using ChatDemo.Droid.Renderer;
using Xamarin.Forms;
using ChatDemo.Controls;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using Android.Views;
using Android.OS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]

namespace ChatDemo.Droid.Renderer
{
    class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.TextSize = 20;
                Control.SetBackgroundResource(Resource.Drawable.border);

            }
        }        
    }
}