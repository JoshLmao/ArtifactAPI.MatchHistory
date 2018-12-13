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
            if(towerHealth <= 0)
                return "/Images/tower_destroyed.png";
            else
                return "/Images/tower.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
