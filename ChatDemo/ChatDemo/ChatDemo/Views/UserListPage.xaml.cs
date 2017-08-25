using ChatDemo.Helpers;
using ChatDemo.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatDemo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserListPage : ContentPage
	{
        ViewModel.UserListViewModel viewModel;

        public UserListPage()
        {
            NavigationPage.SetHasNavigationBar(this, true);
            Title = "UserList";
            InitializeComponent();
            BindingContext = viewModel = new ViewModel.UserListViewModel();
            ToolbarItems.Add(new ToolbarItem("Sign Out", "", OnSignoutClicked));
        }

        private async void OnSignoutClicked()
        {          
            AppSecurity.Logout();
            await new LoginPage().SetItAsRootPageAsync();
        }

        protected void HideListViewRefreshing(object sender, EventArgs e)
        {
            viewModel.OnUserNameRefreshing();
        }
        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var name = e.SelectedItem as UserList;         
            Navigation.PushAsync(new ChatPage(name.Name.ToString()));
        }
    }
}
