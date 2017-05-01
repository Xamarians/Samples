using CallMessageEmailDemo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CallMessageEmailDemo.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MediaPage : ContentPage
	{
		public MediaPage ()
		{
			InitializeComponent ();
		}
        private void OnSendMessageClicked(object sender, EventArgs e)
        {
            DependencyService.Get<IMedia>().SendMessage();
        }
        private void OnSendEmailClicked(object sender, EventArgs e)
        {
            DependencyService.Get<IMedia>().SendEmail();
        }
        private void OnMakeACallClicked(object sender, EventArgs e)
        {
            DependencyService.Get<IMedia>().MakeACall();
        }
    }
}
