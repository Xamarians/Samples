using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
namespace PdfDemo.ViewModel
{
   public class PdfViewModel:INotifyPropertyChanged
    {
        string _sheetNo;

        public string SheetNo
        {
            get
            {
                return _sheetNo;
            }
            set
            {
                if (_sheetNo != value)
                {
                    _sheetNo = value;

                    OnPropertyChanged("SheetNo");
                }
            }
        }

        string _date;
        public string Date
        {
            get
            {
                return _date;
            }
            set
            {
                if (_date != value)
                {
                    _date = value;

                    OnPropertyChanged("Date");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
