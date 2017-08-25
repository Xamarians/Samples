using ChatDemo.Droid.Renderer;
using Xamarin.Forms;
using ChatDemo.Controls;
using Xamarin.Forms.Platform.Android;
using Android.Text;
using System.ComponentModel;
using Android.Graphics.Drawables;
using Android.Graphics;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]

namespace ChatDemo.Droid.Renderer
{
    class CustomEntryRenderer : EntryRenderer
    {
        CustomEntry element;
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
                return;
            element = Element as CustomEntry;
            Control.Background.SetColorFilter(Android.Graphics.Color.White, PorterDuff.Mode.SrcAtop);

        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }     
    }
}