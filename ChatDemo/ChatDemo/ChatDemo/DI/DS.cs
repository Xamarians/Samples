using Xamarin.Forms;

namespace ChatDemo.DI
{

    public static class DS
    {
        public static IKeyboardInteractions MobileFeatures
        {
            get
            {
                return DependencyService.Get<IKeyboardInteractions>(DependencyFetchTarget.GlobalInstance);
            }
        }
    }
}