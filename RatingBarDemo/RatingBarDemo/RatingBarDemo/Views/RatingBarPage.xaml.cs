using Xamarin.Forms;

namespace RatingBarDemo.Views
{
    public partial class RatingBarPage : ContentPage
	{
		public RatingBarPage()
		{
            Title = "Rating Bar";
            InitializeComponent ();
		}

        private void OnRatingChanged(object sender, float e)
        {
            customRatingBar.Rating = e;
        }
    }
}
