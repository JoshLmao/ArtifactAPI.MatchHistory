using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ArtifactAPI.MatchHistory.Converters
{
    public class FlagToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int flag = (int)value;
            return flag == 0 ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
