using PersoBank_UWP.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace PersoBank_UWP.Converter
{
    public class StringToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            try
            {
                if(value.ToString() == String.Empty)
                {
                    return null;
                }
                return Int32.Parse(value.ToString());
            }
            catch(FormatException e)
            {
                return GeneralConstants.WrongInteger;
            }
        }
    }
}
