using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterManagerProject.LoggerApp
{
    public class LogHelper
    {
        #region 工具方法
        public static DateTime getTime(string log)
        {
            var datestr = log.Split('|')[0].Trim();
            return str2Time(datestr);
        }
        public static DateTime str2Time(string datestr)
        {
            //"2019-01-20 10-10-10 222"
            // 01234567890123456789012
            int year = int.Parse(datestr.Substring(0, 4));
            int month = int.Parse(datestr.Substring(5, 2));
            int day = int.Parse(datestr.Substring(8, 2));
            int hour = int.Parse(datestr.Substring(11, 2));
            int minute = int.Parse(datestr.Substring(14, 2));
            int second = int.Parse(datestr.Substring(17, 2));
            int millisecond = int.Parse(datestr.Substring(20, 3));

            return new DateTime(year, month, day, hour, minute, second, millisecond); ;
        }
        public static string getLogInfo(string log)
        {
            return log.Split('|').LastOrDefault().Trim();
        }
        public static string getTimeStr(string log)
        {
            return log.Split('|').FirstOrDefault().Trim();
        }
        #endregion
    }
}
