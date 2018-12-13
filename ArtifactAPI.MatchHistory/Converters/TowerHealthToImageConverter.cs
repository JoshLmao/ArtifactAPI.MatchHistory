using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ArtifactAPI.MatchHistory.Converters
{
    class TowerHealthToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int towerHealth = (int)value;
            string num = parameter != null ? (string)parameter : "0";
            bool isAncient = num == "1"; 

            if (isAncient)
                return towerHealth <= 0 ? "/Images/ancient_destroyed.png" : "/Images/ancient.png";
            else
                return towerHealth <= 0 ? "/Images/tower_destroyed.png" : "/Images/tower.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
