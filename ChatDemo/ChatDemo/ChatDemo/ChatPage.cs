using ChatDemo.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ChatDemo
{
    public class ChatPage : ContentPage
    {
        public ChatPage()
        {
           Title = "Chat";
            var sendButton = new Button();
            sendButton.Text = " Send ";
            sendButton.VerticalOptions = LayoutOptions.EndAndExpand;
            sendButton.SetBinding(Button.CommandProperty, new Binding("SendMessageCommand"));

            var inputBox = new Entry();
            inputBox.HorizontalOptions = LayoutOptions.FillAndExpand;
            inputBox.Keyboard = Keyboard.Chat;
            inputBox.Placeholder = "Type a message...";
            inputBox.HeightRequest = 30;
            inputBox.SetBinding(Entry.TextProperty, new Binding("InputText", BindingMode.TwoWay));

            var messageList = new ChatListView();
            messageList.VerticalOptions = LayoutOptions.FillAndExpand;
            messageList.SetBinding(ChatListView.ItemsSourceProperty, new Binding("Events"));
            messageList.ItemTemplate = new DataTemplate(CreateMessageCell);

            Content = new StackLayout
            {
                Padding = Device.OnPlatform(new Thickness(6, 6, 6, 6), new Thickness(0), new Thickness(0)),
                Children =
                        {
                    messageList,
                            new StackLayout
                                {
                                    Children = {inputBox, sendButton },
                                    Orientation = StackOrientation.Horizontal,
                                    Padding = new Thickness(0, Device.OnPlatform(0, 20, 0),0,0),
                                }

                        }
            };
        }

        private Cell CreateMessageCell()
        {
           
            var messageLabel = new Label();
            messageLabel.SetBinding(Label.TextProperty, new Binding("Text"));
            messageLabel.Font = Font.SystemFontOfSize(14);

            var stack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = {messageLabel }
            };

           

            var view = new MessageViewCell
            {
                View = stack
            };
            return view;
        }
    }
}
