using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using PrinterManagerProject.EF.Models;

namespace PrinterManagerProject.Converter
{
    [ValueConversion(typeof(PrintStatusEnum), typeof(String))]
    public class RowBgColorConverter : IValueConverter
    {
        private string NotPrintColor = "#FFdddddd";
        private string SuccessColor = "#FFcae8fc";
        private string FailColor = "#607EEA";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "";
            }
            PrintStatusEnum status = (PrintStatusEnum)value;
            switch (status)
            {
                case PrintStatusEnum.NotPrint:
                    return NotPrintColor;
                case PrintStatusEnum.Success:
                    return SuccessColor;
                case PrintStatusEnum.Fail:
                    return FailColor;
                default:
                    return NotPrintColor;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
