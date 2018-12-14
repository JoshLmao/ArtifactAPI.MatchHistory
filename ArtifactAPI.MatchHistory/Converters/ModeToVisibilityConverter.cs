using ArtifactAPI.MatchHistory.Enums;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ArtifactAPI.MatchHistory.Converters
{
    public class ModeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MatchMode mode = (MatchMode)value;
            MatchMode targetMode = (MatchMode)int.Parse((string)parameter);
            if(mode == targetMode)
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
