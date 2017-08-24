using System.Collections.Generic;
using ChatDemo.Services;
using System.Threading.Tasks;

namespace ChatDemo.ViewModel
{
    class UserListViewModel:BaseViewModel
    {
        public List<string> _userList; 
        public List<string> UserList
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
            UserList = new List<string>();
             GetDataAsync();


        }
        public async Task<Result> GetDataAsync()
        {
            IsBusy = true;
            var result = await App.AccountManager.GetUserList();

            if (result.IsSuccess)
            {
                if (result.Data != null)
                {
                    UserList = result.Data;
                }
            }
            IsBusy = false;
            return result;
        }
       

        public void OnUserNameRefreshing()
        {
           // await GetDataAsync();
            IsRefreshing = false;
        }
    }
}