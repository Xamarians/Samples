using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
	}
}
