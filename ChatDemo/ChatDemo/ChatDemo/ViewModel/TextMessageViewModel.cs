using ChatDemo.Helpers;
using ChatDemo.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChatDemo.ViewModel
{
    class TextMessageViewModel : BaseViewModel
    {
        public ICommand SendMessageCommand { get; private set; }
        public ObservableCollection<UserMessage> MessageList { get; set; }
        public static string ToUserName { get; set; }

        string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                SetProperty(ref _message, value);
            }
        }
        
        public TextMessageViewModel(string toUserName)
        {
            ToUserName = toUserName;
            SendMessageCommand = new Command(() =>
            {
                SendMessageAsync();
            });
            MessageList = new ObservableCollection<UserMessage>();
            var items = Data.Repository.Find<UserMessage>(x=>x.ToUserName== toUserName& x.FromUserName==AppSecurity.ContactNumber);
            foreach (var item in items)
            {
                MessageList.Add(item);
            }

            MessagingCenter.Subscribe<string,UserMessage>(this, "UpdateMessage", (sender, _item) => {
                 MessageList.Add(_item);
              
            });
        }

        public async void SendMessageAsync()
        {
            if (string.IsNullOrWhiteSpace(Message))
                return;
            var item = new UserMessage
            {
                Content = Message,
                ToUserName = ToUserName,
                FromUserName =
                AppSecurity.ContactNumber,
                IsIncoming=false,
            };
            MessageList.Add(item);
            Data.Repository.SaveOrUpdate(item);
            var result = await App.AccountManager.SendMessage(Message, AppSecurity.ContactNumber, ToUserName);
            if (!result.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", result.Message, "OK");
            }                                 
        }      
    }
}
