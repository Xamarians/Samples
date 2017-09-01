using ChatDemo.DI;
using ChatDemo.Helpers;
using ChatDemo.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChatDemo.ViewModel
{
    class TextMessageViewModel : BaseViewModel
    {
        public ICommand SendMessageCommand { get; private set; }
        public ObservableCollection<UserMessage> MessageList { get; set; }
        private readonly string ReceiverName;
        private readonly int ReceiverUserId;

        string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                SetProperty(ref _message, value);
            }
        }        
        public TextMessageViewModel(int userId, string userFullName)
        {
            ReceiverName = userFullName;
            ReceiverUserId = userId;
            SendMessageCommand = new Command(() => SendMessageAsync());
            MessageList = new ObservableCollection<UserMessage>();

            var items = Data.Repository.Find<UserMessage>(x => x.ReceiverId == ReceiverUserId
                            || x.SenderId == ReceiverUserId);
            items.Reverse();
            foreach (var item in items)
            {
                MessageList.Insert(0,item);
            }
            MessagingCenter.Subscribe<string, UserMessage>(this, MessageCenterKeys.NewMessageReceived
                , (sender, item) =>
            {
                MessageList.Add(item);
                MessagingCenter.Send<object>(this,MessageCenterKeys.NewMessageAdded);
            });
        }

        public async void SendMessageAsync()
        {
            if (string.IsNullOrWhiteSpace(Message))
                return;
            var item = new UserMessage
            {
                Message = Message,
                ReceiverId = ReceiverUserId,
                ReceiverName = ReceiverName,
                SenderId = AppSecurity.CurrentUser.UserId,
                SenderName = AppSecurity.CurrentUser.GetFullName(),
                IsIncoming = false,
                Date = DateTime.UtcNow
        };
            MessageList.Add(item);
            MessagingCenter.Send<object>(this,MessageCenterKeys.NewMessageAdded);
            Data.Repository.SaveOrUpdate(item);
            IsBusy = true;
            var result = await App.AccountManager.SendMessageAsync(ReceiverName, Message, ReceiverUserId);
            if (!result.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", result.Message, "OK");
            }
            IsBusy = false;
            Message = string.Empty;
            IKeyboardInteractions keyboardInteractions = DependencyService.Get<IKeyboardInteractions>();
            keyboardInteractions.HideKeyboard();
        }
    }
}
