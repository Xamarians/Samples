
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatDemo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        ViewModel.TextMessageViewModel viewModel;
        public ChatPage(int id, string name)
        {
            InitializeComponent();
            Title = name;
            NavigationPage.SetHasNavigationBar(this, true);
            BindingContext = viewModel = new ViewModel.TextMessageViewModel(id, name);

            MessagingCenter.Subscribe<object>(this, MessageCenterKeys.NewMessageAdded, (sender) =>
            {
                var v = MessagesListView.ItemsSource.Cast<object>().LastOrDefault();
                MessagesListView.ScrollTo(v, ScrollToPosition.End, true);
            });
        }

        private void MessagesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MessagesListView.SelectedItem = null;
        }

        //private void Handle_Focused(object sender, FocusEventArgs e)
        //{
        //    ((Entry)sender).Text = string.Empty;
        //}

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
