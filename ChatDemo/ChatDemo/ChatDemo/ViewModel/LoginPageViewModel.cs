using ChatDemo.Helpers;
using ChatDemo.Models;
using ChatDemo.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChatDemo.ViewModel
{
    class LoginPageViewModel:BaseViewModel
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
        public ICommand OnLoginClicked { get; private set; }
        public static User CurrentUser { get; private set; }

        public LoginPageViewModel()
        {
            OnLoginClicked = new Command(() =>
            {
                LoginClicked();
            });
            MessagingCenter.Subscribe<string>(this, "Token", (sender) => {
                Token = sender;
             // var result = App.AccountManager.GetUserID(Token, CurrentUser.ContactNo, CurrentUser.Name);               
            });
        }

        public async void LoginClicked()
        {
            CurrentUser = Data.Repository.FindOne<User>(x => true);
            string message = "";
            if (!IsValidated(out message))
            {
                await Application.Current.MainPage.DisplayAlert("", message, "ok");
                return;
            }
            if (CurrentUser == null)
            {
                await Application.Current.MainPage.DisplayAlert("error", "User is not registered. Please register to continue.", "ok");
                return;
            }
            if (CurrentUser.ContactNo != UserName & CurrentUser.Password != Password)
            {
                await Application.Current.MainPage.DisplayAlert("error", "User is not registered. Please register to continue.", "ok");
                return;
            }
            var result = await App.AccountManager.GetToken(UserName,Password);
            await new Views.UserListPage().SetItAsRootPageAsync();
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
