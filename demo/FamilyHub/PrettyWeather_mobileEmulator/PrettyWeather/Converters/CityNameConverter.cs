using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrettyWeather.Converters
{
    public class CityNameConverter : IMarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.Equals("Seoul-teukbyeolsi"))
            {
                return "Seoul";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
