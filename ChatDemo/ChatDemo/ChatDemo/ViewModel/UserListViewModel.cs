using System.Collections.Generic;
using ChatDemo.Services;
using System.Threading.Tasks;
using ChatDemo.Models;
using System.Collections.ObjectModel;

namespace ChatDemo.ViewModel
{
    class UserListViewModel:BaseViewModel
    {
        public ObservableCollection<UserList> _userList; 
        public ObservableCollection<UserList> UserList
        {
            get { return _userList; }
            set { SetProperty(ref _userList, value); }
        }
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value); }
        }

        public UserListViewModel()
        {
            UserList = new ObservableCollection<UserList>();
            GetDataAsync();

        }


        private async void GetDataAsync()
        {
            IsBusy = true;
            //var result = await App.AccountManager.GetUserList();

            //if (result.IsSuccess)
            //{
            //    if (result.Data != null)
            //    {
            //        UserList = result.Data;
            //    }
            //}

            UserList.Add(new UserList {image= "empty_contact.jpg", Name = "Robbin"});
            UserList.Add(new UserList {image="", Name = "Joe" });
            UserList.Add(new UserList {image="", Name = "Harry" });
            UserList.Add(new UserList {image="", Name = "Stephan" });
            IsBusy = false;
        }
       

        public void OnUserNameRefreshing()
        {
           // GetDataAsync();
            IsRefreshing = false;
        }
    }
}