using ChatDemo.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatDemo
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
            var dbUser = Data.Repository.FindOne<User>(x => true);
            if (dbUser == null)
            {
                await DisplayAlert("", "User Not Found", "OK");
                return;
            }
            Helpers.AppSecurity.Logout();
            await Navigation.PopAsync();
        }

        protected void HideListViewRefreshing(object sender, EventArgs e)
        {
            viewModel.OnUserNameRefreshing();
        }
        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var name = e.SelectedItem;        
           
            Navigation.PushAsync(new ChatPage(name.ToString()));
        }
    }
}
