using System;
using System.Globalization;
using System.Windows.Data;

namespace ArtifactAPI.MatchHistory.Converters
{
    public class TimeToDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan duration = (TimeSpan)value;
            string full = "";

            if(duration.Hours > 0)
            {
                full += $"{duration.Hours}h ";
            }

            if (duration.Minutes > 0)
            {
                full += $"{duration.Minutes}m ";
            }

            if (duration.Seconds > 0)
            {
                full += $"{duration.Seconds}s";
            }

            //If time is 0, display "0s"
            if (string.IsNullOrEmpty(full))
                full += duration.Seconds + "s";

            return full;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
