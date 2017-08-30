using ChatDemo.Helpers;
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
	public partial class LoginPage : ContentPage
	{
        ViewModel.LoginPageViewModel viewModel;
        public LoginPage ()
		{       
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = viewModel = new ViewModel.LoginPageViewModel();
        }
     
        private void SignUpClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}