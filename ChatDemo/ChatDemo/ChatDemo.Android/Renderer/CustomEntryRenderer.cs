using ChatDemo.Droid.Renderer;
using Xamarin.Forms;
using ChatDemo.Controls;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using Android.Graphics;
using Android.Views.InputMethods;
using Android.Widget;

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
            Control.ImeOptions = ImeAction.Next;
            Control.Background.SetColorFilter(Android.Graphics.Color.White, PorterDuff.Mode.SrcAtop);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName.Equals(CustomEntry.KeyboardActionProperty))
            {
                Control.ImeOptions = GetImeOption();
            }
        }

        private ImeAction GetImeOption()
        {
            switch (element.KeyboardAction)
            {
                case KeyboardActionType.Done:
                    return ImeAction.Done;
                case KeyboardActionType.Next:
                    return ImeAction.Next;
                case KeyboardActionType.Send:
                    return ImeAction.Send;
                default:
                    return ImeAction.None;
            }
        }
    }
}