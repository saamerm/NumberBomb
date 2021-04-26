using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NumberBomb
{
    public class OrderStatusToColorConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valueAsString = value.ToString();
            switch (valueAsString)
            {
                case ("Please enter a number between 1 - 100"):
                {
                    return Color.FromHex("#000000");
                }
                default:
                {
                    return Color.FromHex("#FFFFFF");
                }
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Only one way bindings are supported with this converter");
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
