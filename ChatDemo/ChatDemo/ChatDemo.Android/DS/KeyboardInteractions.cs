
using Android.App;
using Android.Content;
using Xamarin.Forms;
using ChatDemo.Droid.DS;
using ChatDemo.DI;
using Android.Views.InputMethods;

[assembly: Dependency(typeof(KeyboardInteractions))]
namespace ChatDemo.Droid.DS
{
    public class KeyboardInteractions : IKeyboardInteractions
        {
            public void HideKeyboard()
            {
                var inputMethodManager = Forms.Context.GetSystemService(Context.InputMethodService) as InputMethodManager;
                if (inputMethodManager != null && Forms.Context is Activity)
                {
                    var activity = Forms.Context as Activity;
                    var token = activity.CurrentFocus == null ? null : activity.CurrentFocus.WindowToken;
                    inputMethodManager.HideSoftInputFromWindow(token, 0);
                }
            }
        }
    }