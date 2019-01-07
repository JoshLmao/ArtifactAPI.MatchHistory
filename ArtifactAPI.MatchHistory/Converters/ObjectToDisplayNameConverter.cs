using System;
using System.Globalization;
using System.Windows.Data;

namespace ArtifactAPI.MatchHistory.Converters
{
    class ObjectToDisplayNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
            {
                if (value is string[] stArray)
                {
                    return string.Join(", ", stArray);
                }

                return value.ToString();
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
