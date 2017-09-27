using System;
using Android.Views;
using Xamarin.Forms;
using ImageSliderDemo;
using ImageSliderDemo.Droid;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SliderView), typeof(SliderViewRenderer))]

namespace ImageSliderDemo.Droid
{
    public class SliderViewRenderer : ViewRenderer<SliderView, Android.Views.View>
    {
        private readonly Gesture _listener;
        private readonly GestureDetector _detector;

        //Create two x points to find out if the swipe was to the left or to the right
        private float x1, x2;

        SliderView _sliderView;

        public SliderViewRenderer()
        {
            _listener = new Gesture();
            _detector = new GestureDetector(_listener);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SliderView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                this.Touch += HandleGenericMotion;
                _sliderView = e.NewElement;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (_sliderView.CurrentView is Layout)
            {
                //This viewgroup contains the ViewScreen that we need to get at child 0
                ViewGroup view = (ViewGroup)this.ViewGroup.GetChildAt(0);
                Android.Views.View currentLayout = (ViewGroup)view.GetChildAt(0);
                currentLayout.Touch -= HandleGenericMotion;
            }
            Touch -= HandleGenericMotion;
            //Call the garbage collection to make sure the BitMaps are cleaned up
            GC.Collect();
            base.Dispose(disposing);
        }

        void HandleGenericMotion(object sender, TouchEventArgs e)
        {
            _detector.OnTouchEvent(e.Event);
            switch (e.Event.Action)
            {

                //If action is Down, then we set the x1 value and break
                case MotionEventActions.Down:
                    x1 = e.Event.GetX();
                    break;
                //If action is Up, then we set the x2 and caluclate whether it was a swipe left or right AND swipe was greater then MinimumSwipeDistance
                case MotionEventActions.Up:
                    x2 = e.Event.GetX();
                    float delta = x2 - x1;
                    if (Math.Abs(delta) > _sliderView.MinimumSwipeDistance)
                    {
                        if (delta > 0)
                        {
                            _sliderView.OnLeftButtonClicked();
                            Console.WriteLine("Swipe to the left");
                        }
                        else if (delta < 0)
                        {
                            _sliderView.OnRightButtonClicked();
                        }
                    }
                    break;
            }
        }

    }
}