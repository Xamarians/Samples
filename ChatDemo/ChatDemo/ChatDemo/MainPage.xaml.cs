using ChatDemo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChatDemo
{
	public partial class MainPage : ContentPage
	{
        ViewModel.MainPageViewModel viewModel; 
		public MainPage()
		{
			InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = viewModel = new ViewModel.MainPageViewModel();        
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string message = "";
            if (!IsValidated(out message))
            {
              await  Application.Current.MainPage.DisplayAlert("", message, "ok");
                return;
            }
            AppSecurity.Login(viewModel.Name,viewModel.UserName);
            var result = App.AccountManager.GetUserID(viewModel.Token, AppSecurity.userName, AppSecurity.userName);
            await Navigation.PushAsync(new UserListPage());
        }

        private bool IsValidated(out string message)
        {
            message = null;
            if (string.IsNullOrWhiteSpace(viewModel.UserName))
            {
                message = "UserName is required";
            }
            if (string.IsNullOrWhiteSpace(viewModel.Name))
            {
                message = "Name is required";
            }
            return string.IsNullOrWhiteSpace(message);
        }
    }
}
