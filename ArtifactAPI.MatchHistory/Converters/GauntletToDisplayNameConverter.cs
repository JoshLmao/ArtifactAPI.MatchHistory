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
    class GauntletToDisplayNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            GauntletType type = (GauntletType)value;
            switch (type)
            {
                case GauntletType.None:
                    return "No Gauntlet";
                case GauntletType.CasualPhantomDraft:
                    return "Expert Phantom Draft";
                case GauntletType.Constructed:
                    return "Constructed";
                case GauntletType.ConstructedExpert:
                    return "Expert Constructed";
                case GauntletType.PhantomDraftExpert:
                    return "Expert Phantom Draft";
                case GauntletType.KeeperDraftExpert:
                    return "Keeper's Draft";
                case GauntletType.CallToArms:
                    return "Random Meta";
                default:
                    {
                        string error = $"Not implemented case for GauntletType enum - {type}";
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
