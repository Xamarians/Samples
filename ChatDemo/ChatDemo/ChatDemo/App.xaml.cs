
using ChatDemo.Services;
using Xamarin.Forms;

namespace ChatDemo
{
    public partial class App : Application
    {
        internal static readonly AccountManager AccountManager = new AccountManager();

        public App()
        {
            InitializeComponent();
            Page page;
            if (Helpers.AppSecurity.IsAuthenticated)
            {
                page = new Views.UserListPage();
            }
            else
            {
                page = new Views.LoginPage();
            }
            MainPage = new NavigationPage(page)
            {
                BarBackgroundColor = Color.FromHex("#f76262")
            };           
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
