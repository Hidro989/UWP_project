using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Uwp.HCore.Converter.DateTimeToObject
{
    public class DateTimeToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime dateTime = (DateTime)value;
            return dateTime.ToString("MMMM,dd,yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            string str = (string)value;
            return DateTime.Parse(str);
        }
    }
}
