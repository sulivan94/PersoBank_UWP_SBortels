using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace PersoBank_UWP.Converter
{
    public class NbDaysToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int nbDays = (int)value;
            switch(nbDays)
            {
                case 7: return "1 semaine";
                case 15: return "2 semaines";
                case 30: return "1 mois";
                case 90: return "3 mois";
                case 180: return "6 mois";
                case 365: return "1 an";
                default: return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
