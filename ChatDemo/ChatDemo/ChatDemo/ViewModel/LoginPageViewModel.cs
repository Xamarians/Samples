using ChatDemo.Data;
using ChatDemo.Helpers;
using ChatDemo.Models;
using System;
using System.Collections.Generic;
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

       // public static string FcmToken { get; set; }
        public ICommand LoginCommand { get; private set; }

        public LoginPageViewModel()
        {
            //MessagingCenter.Subscribe<string>(this, "GetFcmToken", (token) => {
            //    FcmToken = token;
            //});

            //UserName = "test@gmail.com";
            //Password = "abcd12";
            LoginCommand = new Command(() => OnLoginCommandClicked());
        }

        public async void OnLoginCommandClicked()
        {
            string message = "";
            if (!IsValidated(out message))
            {
                await DisplayAlert("Error", message);
                return;
            }
            IsBusy = true;
            var result = await App.AccountManager.GetTokenAsync(UserName, Password);
            if (!result.IsSuccess)
            {
                IsBusy = false;
                await DisplayAlert("Error", result.Message);
                return;
            }
            AppSecurity.Login(result.Data.Token, result.Data.ExpiryTime);
          //  sendToken();
            var resultUser = await App.AccountManager.GetMeAsync();
            if (!resultUser.IsSuccess)
            {
                IsBusy = false;
                await DisplayAlert("Error", resultUser.Message);
                return;
            }
            SaveOrUpdateUser(resultUser.Data);
            IsBusy = false;
            await new Views.UserListPage().SetItAsRootPageAsync();
        }

        //private async void sendToken()
        //{
        //    var result = await App.AccountManager.RegisterGcmToken(FcmToken);

        //    if (!result.IsSuccess)
        //    {
        //        await DisplayAlert("Error", result.Message,"ok");
        //        return;
        //    }
        //}

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
