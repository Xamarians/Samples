using Xamarin.Forms;

namespace ChatDemo.DI
{

    public static class DS
    {
        public static IMobileFeature MobileFeatures
        {
            get
            {
                return DependencyService.Get<IMobileFeature>(DependencyFetchTarget.GlobalInstance);
            }
        }
    }
}