using RatingBarDemo.Helpers;
using System;
using Xamarin.Forms;

namespace RatingBarDemo.Controls
{
    class CustomRatingBar:View
    {
        public event EventHandler<float> RatingChanged;
        public event EventHandler Tapped;

        public static BindableProperty IsSmallStyleProperty = BindableProperty.Create("IsSmallStyle", typeof(bool), typeof(CustomRatingBar), false);
        public bool IsSmallStyle
        {
            get { return (bool)GetValue(IsSmallStyleProperty); }
            set { SetValue(IsSmallStyleProperty, value); }
        }

        public static BindableProperty IsReadonlyProperty = BindableProperty.Create("IsReadonly", typeof(bool), typeof(CustomRatingBar), true);
        public bool IsReadonly
        {
            get { return (bool)GetValue(IsReadonlyProperty); }
            set { SetValue(IsReadonlyProperty, value); }
        }

        public static BindableProperty MaxStarsProperty = BindableProperty.Create("MaxStars", typeof(int), typeof(CustomRatingBar), 5);
        public int MaxStars
        {
            get { return (int)GetValue(MaxStarsProperty); }
            set { SetValue(MaxStarsProperty, value); }
        }

        public static BindableProperty RatingProperty = BindableProperty.Create("Rating", typeof(float), typeof(CustomRatingBar), 0f);
        public float Rating
        {
            get { return (float)GetValue(RatingProperty); }
            set { SetValue(RatingProperty, value); OnPropertyChanged("Rating"); }
        }

        public static BindableProperty StepSizeProperty = BindableProperty.Create("StepSize", typeof(float), typeof(CustomRatingBar), 0.5f);
        public float StepSize
        {
            get { return (float)GetValue(StepSizeProperty); }
            set { SetValue(StepSizeProperty, value); }
        }

        public void OnRatingChanged(float rating)
        {
            if (RatingChanged != null)
                RatingChanged.Invoke(this, rating);
        }

        public Color GetFillColor()
        {
            return Rating == 0 ? Color.Gray : Rating <= 2 ? Colors.StarRed : Rating <= 3 ? Colors.StarOrange : Rating <= 4 ? Colors.StarYellow : Colors.StarGreen;
        }

        public void OnTapped()
        {
            if (Tapped != null)
                Tapped.Invoke(this, null);
        }
    }
}
