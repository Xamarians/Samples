using ChatDemo.Data;
using ChatDemo.Helpers;
using ChatDemo.Models;
using ChatDemo.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChatDemo.ViewModel
{
    class LoginPageViewModel : BaseViewModel
    {
        string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                SetProperty(ref _userName, value);
            }
        }

        string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public string Token { get; set; }
        public ICommand LoginCommand { get; private set; }

        public LoginPageViewModel()
        {
            LoginCommand = new Command(() => OnLoginCommandClicked());
        }

        public async void OnLoginCommandClicked()
        {
            //CurrentUser = Data.Repository.FindOne<User>(x => true);
            string message = "";
            if (!IsValidated(out message))
            {
                await DisplayAlert("Error", message);
                return;
            }
            IsBusy = true;
            var result = await App.AccountManager.GetToken(UserName, Password);
            if (!result.IsSuccess)
            {
                IsBusy = false;
                await DisplayAlert("Error", result.Message);
            }
            AppSecurity.Login(result.Data.Token, result.Data.ExpiryTime);
            var resultUser = await App.AccountManager.GetMe();
            if (!resultUser.IsSuccess)
            {
                IsBusy = false;
                await DisplayAlert("Error", resultUser.Message);
            }
            SaveOrUpdateUser(resultUser.Data);
            IsBusy = false;
            await new Views.UserListPage().SetItAsRootPageAsync();
        }

        private void SaveOrUpdateUser(User user)
        {
            var dbUser = Repository.FindOne<User>(x => true);
            if (dbUser == null)
                dbUser = new User { UserId = user.UserId };
            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.PhoneNumber = user.PhoneNumber;
            dbUser.UserName = user.UserName;
            dbUser.UpdatedOn = DateTime.Now;
            Repository.SaveOrUpdate(dbUser);
        }

        private bool IsValidated(out string message)
        {
            message = null;
            if (string.IsNullOrWhiteSpace(UserName))
            {
                message = "Email is required";
            }
            else if (string.IsNullOrWhiteSpace(Password))
            {
                message = "Password is required";
            }
            return string.IsNullOrWhiteSpace(message);
        }
    }
}
