using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ImageCropperDemo.ViewModel
{
    class BaseViewModel : INotifyPropertyChanged
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
    }
}