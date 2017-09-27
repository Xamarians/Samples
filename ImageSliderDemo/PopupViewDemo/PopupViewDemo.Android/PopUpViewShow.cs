using PopupViewDemo.Droid;
using PopupViewDemo.Interface;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Android.App.ActionBar;

[assembly: Dependency(typeof(PopUpViewShow))]

namespace PopupViewDemo.Droid
{
    class PopUpViewShow : IView
    {
        public void ShowPopup(ContentView view)
        {
            if (Platform.GetRenderer(view) == null)
                Platform.SetRenderer(view, Platform.CreateRenderer(view));
            var renderer = Platform.GetRenderer(view);
            // renderer.Tracker.UpdateLayout();
            //  Rectangle size = new Rectangle(0, 0, 300, 300);
            //  var viewGroup = renderer.ViewGroup;
            //  var layoutParams = new LayoutParams(500,500, Android.Views.GravityFlags.Center);
            // viewGroup.LayoutParameters = layoutParams;
            // viewGroup.SetBackgroundColor(Color.Red.ToAndroid());
            // view.Layout(size);
            AddView(view);
        }
        [Android.Runtime.Register("addView", "(Landroid/view/View;)V", "GetAddView_Landroid_view_View_Handler")]
        public virtual void AddView(View child)
        {
          
            var content = new ContentView();
            content.Content = child;
            var activity = Application.Current.MainPage as MainPage;
            activity.Content = content;
        }
    }
}