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
    public class PrintStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int status = (int)value;
            switch(status)
            {
                case 0:
                    return "待贴签";
                case 1:
                    return "已贴签";
                default:
                    return "异常";
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

}
