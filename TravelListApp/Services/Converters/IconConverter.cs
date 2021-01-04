using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelListApp.Services.Icons;
using Windows.UI.Xaml.Data;

namespace TravelListApp.Services.Converters
{
    public class IconConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return Icon.GetIcon((string)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
