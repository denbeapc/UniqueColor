using System;
using System.Globalization;
using Xamarin.Forms;

namespace UniqueColor.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((bool)value) ? (Color)Application.Current.Resources["DarkColor"] 
                                 : (Color)Application.Current.Resources["ErrorColor"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
