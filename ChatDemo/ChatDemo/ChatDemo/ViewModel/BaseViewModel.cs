using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ChatDemo.ViewModel
{
   public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void SetProperty<T>(ref T filed, T value, [CallerMemberName]string propertyName = null)
        {
            filed = value;
            NotifyPropertyChanged(propertyName);
        }

        bool _isbusy;
        public bool IsBusy
        {
            get { return _isbusy; }
            set
            {
                SetProperty(ref _isbusy, value);
            }
        }

        public Task DisplayAlert(string title, string message, string cancel = "OK")
        {
            return App.Current.MainPage.DisplayAlert(title, message, cancel);
        }
    }

}
