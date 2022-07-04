using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Uwp.HCore.Converter.BooleanToObject
{
    public class BooleanToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool isVisibility = (bool)value;
            return isVisibility ? Visibility.Visible : Visibility.Collapsed;   
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            Visibility visibility = (Visibility)value;
            return visibility == Visibility.Visible ? true : false;
        }
    }
}
