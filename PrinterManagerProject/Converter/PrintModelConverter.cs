using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PrinterManagerProject.Converter
{   

    [ValueConversion(typeof(int), typeof(String))]
    public class PrintModelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "";
            }
            int status = (int)value;
            switch(status)
            {
                case 0:
                    return "自动贴签";
                case 1:
                    return "手动贴签";
                default:
                    return "";
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

}
