using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace PersoBank_UWP.Converter
{
    public class DoubleToNbDaysConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (int)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
            /*switch (value)
            {
                case "1 semaine": return 7;
                case "2 semaines": return 15;
                case "1 mois": return 30;
                case "3 mois": return 90;
                case "6 mois": return 180;
                case "12 mois": return 365;
                default: return 0;
            }*/
            /*string nbDays = (string)value;
            if (nbDays.Equals("1 semaine"))
                return 7;

            if (nbDays.Equals("2 semaines"))
                return 15;

            if (nbDays.Equals("1 mois"))
                return 30;

            if (nbDays.Equals("3 mois"))
                return 90;

            if (nbDays.Equals("6 mois"))
                return 180;

            if (nbDays.Equals("1 an"))
                return 365;

            return 0;*/
        }
    }
}
