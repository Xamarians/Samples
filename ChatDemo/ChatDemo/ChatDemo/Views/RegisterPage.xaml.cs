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
	public partial class RegisterPage : ContentPage
	{
        ViewModel.RegisterPageViewModel viewModel;
		public RegisterPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = viewModel = new ViewModel.RegisterPageViewModel();

        }
        public async void HaveAccountClicked()
        {
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }
    }
}
