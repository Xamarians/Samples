using System.Collections.Generic;
using ChatDemo.Models;
using ChatDemo.Helpers;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace ChatDemo.ViewModel
{
    class UserListViewModel : BaseViewModel
    {
        public ObservableCollection<User> _userList;
        public ObservableCollection<User> UserList
        {
            get { return _userList; }
            set
            {
                SetProperty(ref _userList, value);
            }
        }

        //private bool _isRefreshing;
        //public bool IsRefreshing
        //{
        //    get { return _isRefreshing; }
        //    set { SetProperty(ref _isRefreshing, value); }
        //}

       

        public ICommand RefreshCommand { get; protected set; }

        public UserListViewModel()
        {
            // RefreshCommand = new Command(() => LoadUserAsync());
            UserList = new ObservableCollection<User>();
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
            if (result.Data == null)
                return;
            foreach (var item in result.Data)
            {
                if (item.UserId == AppSecurity.CurrentUser.UserId)
                    continue;
                 UserList.Add(item);
            }
        }
    }
}