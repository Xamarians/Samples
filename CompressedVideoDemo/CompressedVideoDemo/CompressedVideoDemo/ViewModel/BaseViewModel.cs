﻿using System.ComponentModel;

namespace CompressedVideoDemo.ViewModel
{
   public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName]string propertyName = null)
        {
            field = value;
            NotifyPropertyChange(propertyName);
        }

        protected virtual void NotifyPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
