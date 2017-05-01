using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoundImageDemo.Controls;
using Foundation;
using UIKit;
using Xamarin.Forms;
using RoundImageDemo.iOS.Renderer;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(RoundImage), typeof(RoundImageRenderer))]

namespace RoundImageDemo.iOS.Renderer
{
    class RoundImageRenderer:ImageRenderer
    {
		Image element;

		protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
			element = Element as RoundImage;
			if (element == null) return;

            if (e.OldElement != null || Element == null)
                return;

            CreateCircle();
        }


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == VisualElement.HeightProperty.PropertyName ||
                e.PropertyName == VisualElement.WidthProperty.PropertyName)
            {
                CreateCircle();
            }
        }
        private void CreateCircle()
        {
            try
            {
                double min = Math.Min(element.Width, element.Height);
                Control.Layer.CornerRadius = (float)(min / 2.0);
                Control.Layer.MasksToBounds = false;
				Control.Layer.BorderColor = Color.Maroon.ToCGColor();
                Control.Layer.BorderWidth = 3;
                Control.ClipsToBounds = true;
            }
            catch (Exception ex)
            {
            }
        }
    }
}