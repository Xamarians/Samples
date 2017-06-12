using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
namespace PdfDemo.ViewModel
{
   public class PdfViewModel:INotifyPropertyChanged
    {
        string companyInfo;

        public string CompanyInfo
        {
            get
            {
                return companyInfo;
            }
            set
            {
                if (companyInfo != value)
                {
                    companyInfo = value;

                    OnPropertyChanged("CompanyInfo");
                }
            }
        }

        string clientInfo;
        public string ClientInfo
        {
            get
            {
                return clientInfo;
            }
            set
            {
                if (clientInfo != value)
                {
                    clientInfo = value;

                    OnPropertyChanged("ClientInfo");
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
