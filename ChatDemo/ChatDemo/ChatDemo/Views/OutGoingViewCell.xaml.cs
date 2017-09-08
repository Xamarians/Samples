using ChatDemo.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatDemo.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OutGoingViewCell : ViewCell
    {
		public OutGoingViewCell ()
		{
			InitializeComponent ();
        }

        private void OnDelete(object sender, SelectedItemChangedEventArgs e)
        {
            var mi = sender as MenuItem;
            var value = (UserMessage)mi.CommandParameter;
            MessagingCenter.Send("", MessageCenterKeys.MessageDelete, value);
        }
    }
}
