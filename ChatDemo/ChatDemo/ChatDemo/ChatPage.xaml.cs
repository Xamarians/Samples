using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        ViewModel.TextMessageViewModel viewModel;
        public ChatPage(string toUserName)
        {
            InitializeComponent();
            Title = toUserName;
            NavigationPage.SetHasNavigationBar(this, true);
            BindingContext = viewModel = new ViewModel.TextMessageViewModel(toUserName);
        }
    }
}
