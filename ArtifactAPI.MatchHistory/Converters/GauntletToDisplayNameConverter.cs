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
                    return "Phantom Draft (c)";
                case GauntletType.Constructed:
                    return "Constructed (c)";
                case GauntletType.ConstructedExpert:
                    return "Constructed (r)";
                case GauntletType.KeeperDraftExpert:
                    return "Keeper's Draft (r)";
                case GauntletType.PhantomDraftExpert:
                    return "Phantom Draft (r)";
                case GauntletType.RandomMeta:
                    return "Random Meta";
                default:
                    throw new NotImplementedException("");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
