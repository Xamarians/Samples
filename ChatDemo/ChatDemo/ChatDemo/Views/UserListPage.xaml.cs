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
            Title = "ChatApp";
            InitializeComponent();
            BindingContext = viewModel = new ViewModel.UserListViewModel();
            ToolbarItems.Add(new ToolbarItem("Sign Out", "", OnSignoutClicked));
        }

        private async void OnSignoutClicked()
        {
            AppSecurity.Logout();
            await new LoginPage().SetItAsRootPageAsync(false);
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            var item = (User)e.SelectedItem;        
            Navigation.PushAsync(new ChatPage(item.UserId, item.GetFullName()));
            listView.SelectedItem = null;
        }
    }
}
