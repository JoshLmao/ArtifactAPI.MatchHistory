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
                case Outcome.Unknown:
                    return Brushes.Blue;
                case Outcome.Loss:
                    return Brushes.DarkRed;
                case Outcome.Victory:
                    return Brushes.DarkGreen;
                default:
                    {
                        Logger.OutputError($"Unable to Convert outcome to color - Unknown Outcome number '{value}'");
                        throw new NotImplementedException("Implement case");
                    }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
