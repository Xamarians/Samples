using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ImageSliderDemo.Droid
{
    public class Gesture : GestureDetector.SimpleOnGestureListener
    {
        public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
        {
            return base.OnFling(e1, e2, velocityX, velocityY);
        }
    }
}