using ArtifactAPI.MatchHistory.Enums;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ArtifactAPI.MatchHistory.Converters
{
    public class OutcomeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Outcome outcome = (Outcome)value;
            switch (outcome)
            {
                case Outcome.Loss:
                    return Brushes.IndianRed;
                case Outcome.Victory:
                    return Brushes.LightGreen;
                case Outcome.Draw:
                    return Brushes.OrangeRed;
                default:
                    throw new NotImplementedException("Implement case");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
