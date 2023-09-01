using System;
using System.Globalization;
using System.Windows.Data;

namespace WeatherApp.ViewModel.ValueConverters
{
    public class BoolToRainConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool IsRaining = (bool)value;
            if (IsRaining)
                return "Currently Raining";
            return "Currently not Raining";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string isRaining = (string)value;
            if (isRaining == "Currently Raining")
                return true;
            return false;
        }
    }
}
