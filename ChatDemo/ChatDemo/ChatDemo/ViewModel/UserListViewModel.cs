using System.Collections.Generic;
using ChatDemo.Services;
using System.Threading.Tasks;
using ChatDemo.Models;
using System.Collections.ObjectModel;
using ChatDemo.Data;
using ChatDemo.Helpers;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChatDemo.ViewModel
{
    class UserListViewModel : BaseViewModel
    {
        //public ObservableCollection<UserList> _userList; 
        //public ObservableCollection<UserList> UserList
        //{
        //    get { return _userList; }
        //    set { SetProperty(ref _userList, value); }
        //}

        //private bool _isRefreshing;
        //public bool IsRefreshing
        //{
        //    get { return _isRefreshing; }
        //    set { SetProperty(ref _isRefreshing, value); }
        //}

        public List<User> UserList { get; set; }

        public ICommand RefreshCommand { get; protected set; }

        public UserListViewModel()
        {
            // RefreshCommand = new Command(() => LoadUserAsync());
            UserList = new List<User>();
            LoadUserAsync();
        }

        private async void LoadUserAsync()
        {
            IsBusy = true;
            var result = await App.AccountManager.GetUserAsync();
            IsBusy = false;
            if (!result.IsSuccess)
            {
                await DisplayAlert("error", result.Message, "ok");
            }
            foreach (var item in result.Data)
            {
                if (item.UserId == AppSecurity.CurrentUser.UserId)
                    continue;
                 UserList.Add(item);
            }
        }
    }
}