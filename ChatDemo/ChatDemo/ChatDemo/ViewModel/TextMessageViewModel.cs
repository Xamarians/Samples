using ChatDemo.DI;
using ChatDemo.Helpers;
using ChatDemo.Models;
using ChatDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChatDemo.ViewModel
{
    class TextMessageViewModel : BaseViewModel
    {
        public ICommand SendMessageCommand { get; private set; }
        public ObservableCollection<IGrouping<DateTime, UserMessage>> MessageList { get; set; }
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
        string _image;
        public string Image
        {
            get { return _image; }
            set
            {
                SetProperty(ref _image, value);
            }
        }


        public TextMessageViewModel(int userId, string userFullName)
        {
            ReceiverName = userFullName;
            ReceiverUserId = userId;
            SendMessageCommand = new Command(() => SendMessageAsync());
            MessageList = new ObservableCollection<IGrouping<DateTime, UserMessage>>();

            var items = Data.Repository.Find<UserMessage>(x => x.ReceiverId == ReceiverUserId
                            || x.SenderId == ReceiverUserId);

            //var r = new Random();
            //foreach (var item in items)
            //{
            //    item.UpdatedOn = item.UpdatedOn.AddDays(r.Next(1, 3));
            //}
            items.Reverse();

            foreach (var gitem in items.GroupBy(x => x.UpdatedOn.Date))
            {
                MessageList.Add(gitem);
            }

            //foreach (var item in items)
            //{
            //    messages.Insert(0, item);
            //}
            //if (messages.Count == 0)
            //    return;
            //var sorted = from item in messages
            //             orderby item.Date
            //             group item by item.Date.ToString() into itemGroup
            //             select new Grouping<string, UserMessage>(itemGroup.Key, itemGroup);
            //MessageList = new ObservableCollection<Grouping<string, UserMessage>>(sorted);

            MessagingCenter.Subscribe<string, UserMessage>(this, MessageCenterKeys.NewMessageReceived
                , (sender, item) =>
            {
                var gMessage = MessageList.FirstOrDefault(x => x.Key == item.UpdatedOn.Date);
                if (gMessage != null)
                    gMessage.ToList().Add(item);
                else
                {
                    MessageList.Add(new List<UserMessage> { item }.GroupBy(x => x.UpdatedOn.Date).FirstOrDefault());
                }
                MessagingCenter.Send<object>(this, MessageCenterKeys.NewMessageAdded);
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
                isSend = false,
        };           
            MessagingCenter.Send<object>(this, MessageCenterKeys.NewMessageAdded);
            Data.Repository.SaveOrUpdate(item);
            IsBusy = true;
            var result = await App.AccountManager.SendMessageAsync(ReceiverName, Message, ReceiverUserId);
            if (!result.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", result.Message, "OK");
                item.isSend = true;
                Image = "close.png";
            }
            var gMessage = MessageList.FirstOrDefault(x => x.Key == item.UpdatedOn.Date);
            if (gMessage != null)
                gMessage.ToList().Add(item);
            else
            {
                MessageList.Add(new List<UserMessage> { item }.GroupBy(x => x.UpdatedOn.Date).FirstOrDefault());
            }
            item.isSend = true;
            Image = "tick.png";
            IsBusy = false;
            Message = string.Empty;
            IKeyboardInteractions keyboardInteractions = DependencyService.Get<IKeyboardInteractions>();
            keyboardInteractions.HideKeyboard();
        }
    }
    public class Grouping<K, T> : ObservableCollection<T>
    {
        // NB: This is the GroupDisplayBinding above for displaying the header
        public K GroupKey { get; private set; }

        public Grouping(K key, IEnumerable<T> items)
        {
            GroupKey = key;
            foreach (var item in items)
                this.Items.Add(item);
        }
    }
}
