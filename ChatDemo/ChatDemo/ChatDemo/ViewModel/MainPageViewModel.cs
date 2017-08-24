using ChatDemo.Helpers;
using ChatDemo.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChatDemo.ViewModel
{
    class MainPageViewModel:BaseViewModel
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

        string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
       public string Token { get; set; }
        public MainPageViewModel()
        {
            MessagingCenter.Subscribe<string>(this, "Token", (sender) => {
                Token = sender;
            });
        }     
    }
}
