using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            return full;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
