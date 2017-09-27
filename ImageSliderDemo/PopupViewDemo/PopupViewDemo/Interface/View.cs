using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PopupViewDemo.Interface
{
    public interface IView
    {
        void ShowPopup(ContentView view);
    }
}
