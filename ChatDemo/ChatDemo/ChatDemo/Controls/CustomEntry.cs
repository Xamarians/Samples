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

        public static BindableProperty KeyboardActionProperty = BindableProperty.Create("KeyboardAction", typeof(KeyboardActionType), typeof(CustomEntry), KeyboardActionType.Default);
    
        public KeyboardActionType KeyboardAction
        {
            get { return (KeyboardActionType)GetValue(KeyboardActionProperty); }
            set { SetValue(KeyboardActionProperty, value); }
        }

        public CustomEntry()
        {
            FontSize = 18;
          // Completed += OnExtendedEntryCompleted;
        }

        //private void OnExtendedEntryCompleted(object sender, EventArgs e)
        //{
        //    var control = (CustomEntry)sender;
        //    if (control.KeyboardAction != null)
        //    {
        //        var nextControl = this.FindByName<View>(control.NextField);
        //        if (nextControl != null)
        //            nextControl.Focus();
        //    }
        //    else
        //    {
        //        CompletedCommand?.Execute(null);
        //    }
        //}

    }
    public enum KeyboardActionType
    {
        Default, Next, Done, Send
    }
}
