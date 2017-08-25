using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
namespace ChatDemo.Controls
{
    class CustomEntry : Entry
    {
        public static BindableProperty CompletedCommandProperty = BindableProperty.Create("CompletedCommand", typeof(ICommand), typeof(CustomEntry), null);
        public ICommand CompletedCommand
        {
            get { return (ICommand)GetValue(CompletedCommandProperty); }
            set { SetValue(CompletedCommandProperty, value); }
        }

        public static BindableProperty NextFieldProperty = BindableProperty.Create("NextField", typeof(string), typeof(CustomEntry), null);
        public string NextField
        {
            get { return (string)GetValue(NextFieldProperty); }
            set { SetValue(NextFieldProperty, value); }
        }

        public CustomEntry()
        {
            FontSize = 18;
            Completed += OnExtendedEntryCompleted;
        }

        private void OnExtendedEntryCompleted(object sender, EventArgs e)
        {
            var control = (CustomEntry)sender;
            if (control.NextField != null)
            {
                var nextControl = this.FindByName<View>(control.NextField);
                if (nextControl != null)
                    nextControl.Focus();
            }
            else
            {
                CompletedCommand?.Execute(null);
            }
        }

    }
}
