using Xamarin.Forms;
using ChatDemo.Droid.Renderer;
using ChatDemo.Controls;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using Java.Lang;

[assembly: ExportRenderer(typeof(CircleImage), typeof(CircleImageRenderer))]

namespace ChatDemo.Droid.Renderer
{
    class CircleImageRenderer:ImageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
        }

        protected override bool DrawChild(Canvas canvas, global::Android.Views.View child, long drawingTime)
        {
            try
            {
                var radius = Math.Min(Width, Height) / 2;
                var strokeWidth = 10;
                radius -= strokeWidth / 2;
                

                Path path = new Path();
                path.AddCircle(Width / 2, Height / 2, radius, Path.Direction.Ccw);
                canvas.Save();
                canvas.ClipPath(path);

                var result = base.DrawChild(canvas, child, drawingTime);

                canvas.Restore();

                path = new Path();
                path.AddCircle(Width / 2, Height / 2, radius, Path.Direction.Ccw);

                var paint = new Paint();
                paint.AntiAlias = true;
                paint.StrokeWidth = 1;
                paint.SetStyle(Paint.Style.Stroke);
                paint.Color = global::Android.Graphics.Color.Wheat;

                canvas.DrawPath(path, paint);

                paint.Dispose();
                path.Dispose();
                return result;
            }
            catch (Exception ex)
            {
            }

            return base.DrawChild(canvas, child, drawingTime);
        }

    }
}
