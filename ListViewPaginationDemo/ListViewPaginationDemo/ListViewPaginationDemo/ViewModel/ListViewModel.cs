using System;
using System.Collections.Generic;
using System.Text;
using ListViewPaginationDemo.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;

namespace ListViewPaginationDemo.ViewModel
{
  public  class ListViewModel:INotifyPropertyChanged
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
        public ObservableCollection<UserData> ListItems { get; set; }
        public ICommand PullToRefreshCommand { get; protected set; }

        public ListViewModel()
        {
            ListItems = new ObservableCollection<UserData>();
            ListItems.Add(new UserData { Title = "Apple", Description = "Red", Image = "fruit.png" });
            ListItems.Add(new UserData { Title = "Banana", Description = "Yellow", Image = "fruit.png" });
            ListItems.Add(new UserData { Title = "Orange", Description = "Orange", Image = "fruit.png" });
            ListItems.Add(new UserData { Title = "Kiwi", Description = "Green", Image = "fruit.png" });
            ListItems.Add(new UserData { Title = "Cherry", Description = "Red", Image = "fruit.png" });
            ListItems.Add(new UserData { Title = "Mango", Description = "Yellow", Image = "fruit.png" });
            ListItems.Add(new UserData { Title = "Watermelon", Description = "Red", Image = "fruit.png" });
            ListItems.Add(new UserData { Title = "Papaya", Description = "Yellow", Image = "fruit.png" });
            ListItems.Add(new UserData { Title = "Grapes", Description = "Green", Image = "fruit.png" });
            PullToRefreshCommand = new Command(() => LoadData());
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
        public void LoadData(int pageIndex = 1)
        {
            IsBusy = true;
            for (int i = 0; i < 10; i++)
            {
            ListItems.Add(new UserData { Title = "Mango", Description = "Yellow", Image = "fruit.png" });              
            }
            IsBusy = false;
        }
    }


}

    

