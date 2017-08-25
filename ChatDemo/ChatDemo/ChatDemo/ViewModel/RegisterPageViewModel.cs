using ChatDemo.Helpers;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
namespace ChatDemo.ViewModel
{
    class RegisterPageViewModel:BaseViewModel
    {
        string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                SetProperty(ref _password, value);
            }
        }
        string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }
        string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }
        string _number;
        public string Number
        {
            get { return _number; }
            set { SetProperty(ref _number, value); }
        }

        public ICommand OnSignUpClicked { get; private set; }
        public RegisterPageViewModel()
        {
            OnSignUpClicked = new Command(() =>
            {
                SignUpClicked();
            });
        }
        string errorMessage;

        public async void SignUpClicked()
        {
            if (IsBusy)
                return;
            if (!Validate(out errorMessage))
            {
                await Application.Current.MainPage.DisplayAlert("Error", errorMessage, "Ok");
                return;
            }
            if (!ValidatePassword(out errorMessage))
            {
                await Application.Current.MainPage.DisplayAlert("Error", errorMessage, "Ok");
                return;
            }
            if (!ValidateEmail(out errorMessage))
            {
                await Application.Current.MainPage.DisplayAlert("Error", errorMessage, "Ok");
                return;
            }
            IsBusy = true;
             AppSecurity.Register(FirstName,LastName,UserName, Number, Password);
             await Task.Delay(1000);
            IsBusy = false;
            await new Views.UserListPage().SetItAsRootPageAsync();
        }
        private bool Validate(out string message)
        {
            message = null;
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                message = "First Name is required";
            }
           else if (string.IsNullOrWhiteSpace(LastName))
            {
                message = "Last Name is required";
            }
            else if (string.IsNullOrWhiteSpace(UserName))
            {
                message = "Email is required";
            }

            else
            {
                return UIHelper.ValidatePhone("Phone", Number, true, out message);
            }
            return string.IsNullOrWhiteSpace(message);
        }

        private bool ValidatePassword(out string message)
        {
            return UIHelper.Validate("Password", Password, true, 6, 15, out message); 
        }

        private bool ValidateEmail(out string message)
        {
            return UIHelper.IsValidEmail("Email", UserName, true, out message);
        }
    }
}
