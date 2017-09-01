using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChatDemo.Helpers
{
    static class Extensions
    {
        public static Task SetItAsRootPageAsync(this Page page, bool animation = false)
        {
            App.Current.MainPage.Navigation.InsertPageBefore(page, App.Current.MainPage.Navigation.NavigationStack.First());
            return App.Current.MainPage.Navigation.PopToRootAsync(animation);
        }

    }
}
