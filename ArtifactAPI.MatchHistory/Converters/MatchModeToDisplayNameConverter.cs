using ArtifactAPI.MatchHistory.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ArtifactAPI.MatchHistory.Converters
{
    class MatchModeToDisplayNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MatchMode mode = (MatchMode)value;
            switch (mode)
            {
                case MatchMode.BotMatch:
                    return "Bot Match";
                case MatchMode.Gauntlet:
                    return MatchMode.Gauntlet.ToString();
                case MatchMode.Matchmaking:
                    return MatchMode.Matchmaking.ToString();
                default:
                    {
                        string error = $"No display name for MatchMode enum '{mode}'";
                        Logger.OutputError(error);
                        throw new NotImplementedException(error);
                    }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
