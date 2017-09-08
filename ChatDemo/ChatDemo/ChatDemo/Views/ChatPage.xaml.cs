
using ChatDemo.Models;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatDemo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        ViewModel.TextMessageViewModel viewModel;
        int userId;
        public ChatPage(int id, string name)
        {
            InitializeComponent();
            Title = name;
            userId = id;
            NavigationPage.SetHasNavigationBar(this, true);
            BindingContext = viewModel = new ViewModel.TextMessageViewModel(id, name);
            ToolbarItems.Add(new ToolbarItem("Clear", "", OnClearClicked));

            MessagingCenter.Subscribe<object>(this, MessageCenterKeys.NewMessageAdded, (sender) =>
            {
                var v = MessagesListView.ItemsSource.Cast<object>().LastOrDefault();
                MessagesListView.ScrollTo(v, ScrollToPosition.End, true);
            });
        }

        private async void OnClearClicked()
        {
            if (await App.Current.MainPage.DisplayAlert("", "Do you want to clear all conversation.", "ok", "cancel"))
            {
                var items = Data.Repository.Find<UserMessage>(x => x.ReceiverId == userId
                          || x.SenderId == userId);
                if (items.Count == 0)
                    return;
                foreach (var item in items)
                    Data.Repository.Delete(item);

                viewModel.MessageList.Clear();
            }
        }

        private void MessagesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MessagesListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var v = viewModel.MessageList.LastOrDefault();
            if (v == null)
                return;
            MessagesListView.ScrollTo(v, ScrollToPosition.MakeVisible, false);
        }
    }
}
