using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListViewPaginationDemo.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ListViewPaginationDemo.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListViewPage : ContentPage
	{
        ListViewModel viewModel;
		public ListViewPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new ListViewModel();
		}
        void Handle_PageChanged(object sender, Controls.PageEventArgs e)
        {
            viewModel.LoadData(e.PageIndex);
        }

        protected void HideListViewRefreshing(object sender, EventArgs e)
        {
            (sender as ListView).IsRefreshing = false;
        }
    }
}
