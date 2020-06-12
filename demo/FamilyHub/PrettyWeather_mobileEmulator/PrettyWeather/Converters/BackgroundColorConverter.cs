using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrettyWeather.Converters
{
    public class BackgroundColorConverter : IMarkupExtension, IValueConverter
    {
        public bool IsStart { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int temp)
            {
                var resources = Application.Current.Resources;

                if (temp >= 70)
                    return IsStart ? resources["StartColor70"] : resources["EndColor70"];
                if (temp >= 60)
                    return IsStart ? resources["StartColor60"] : resources["EndColor60"];
                if (temp >= 50)
                    return IsStart ? resources["StartColor50"] : resources["EndColor50"];
                if (temp >= 40)
                    return IsStart ? resources["StartColor40"] : resources["EndColor40"];
                if (temp >= 30)
                    return IsStart ? resources["StartColor30"] : resources["EndColor30"];
                return IsStart ? resources["NightStartColor"] : resources["NightEndColor"];
            }

            return Color.Black;
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
