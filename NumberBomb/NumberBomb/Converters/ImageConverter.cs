using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NumberBomb
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string ButtonImage = "volume_off_24px.png";

            if (value is string && !string.IsNullOrEmpty((string)value))
            {
                ButtonImage = (string)value;
            }
            
            return ButtonImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}