using System;
using System.Globalization;
using System.Windows.Data;

namespace ArtifactAPI.MatchHistory.Converters
{
    class SecondsToTimeFormatSeconds : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int totalSeconds = (int)value;
            TimeSpan span = TimeSpan.FromSeconds(totalSeconds);
            return new TimeToDisplayConverter().Convert(span, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
