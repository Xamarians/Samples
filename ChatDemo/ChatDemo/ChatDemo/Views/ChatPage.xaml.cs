
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatDemo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChatPage : ContentPage
	{
        ViewModel.TextMessageViewModel viewModel;
        public ChatPage(int id,string name)
        {
            InitializeComponent();
            Title = name;
            NavigationPage.SetHasNavigationBar(this, true);
            BindingContext = viewModel = new ViewModel.TextMessageViewModel(id,name);
        }
    }
}
