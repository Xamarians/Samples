using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ChatDemo.Helpers
{
    class DateConverter : IValueConverter
    {
       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = (DateTime)value;
            if (date.Equals(DateTime.Today))
            {
                return "Today";
            }
            return date.Day.ToString().PadLeft(2, '0') + @"/" + date.Month.ToString().PadLeft(2, '0') + "-" + date.Year;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = (DateTime)value;
            return date.Day.ToString().PadLeft(2, '0') + @"/" + date.Month.ToString().PadLeft(2, '0') + "-" + date.Year;
        }
    }
}