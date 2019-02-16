using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterManagerProject.Tools
{
    public class LogHelper
    {
        public string logPath = AppDomain.CurrentDomain.BaseDirectory + @"Log\";

        public void SerialPortLog(string msg)
        {
            try
            {
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }

                string logName = string.Format(@"{0}\sp_log_{1}.txt", logPath, DateTime.Now.ToString("yyyy_MM_dd"));
                File.AppendAllText(logName, string.Format("===============================================================\r\n{0}\r\n时间：{1}\r\n", msg, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message);
            }
        }
        public void PrinterLog(string msg)
        {
            try
            {
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }

                string logName = string.Format(@"{0}\printer_log_{1}.txt", logPath, DateTime.Now.ToString("yyyy_MM_dd"));
                File.AppendAllText(logName, string.Format("===============================================================\r\n{0}\r\n时间：{1}\r\n", msg, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message);
            }
        }

        public void ErrorLog(string msg)
        {
            try
            {
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }
                
                string logName = string.Format(@"{0}\error_log_{1}.txt", logPath, DateTime.Now.ToString("yyyy_MM_dd"));
                File.AppendAllText(logName, string.Format("===============================================================\r\n{0}\r\n时间：{1}\r\n", msg, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Log(string msg)
        {
            try
            {
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }
                
                string logName = string.Format(@"{0}\log_{1}.txt", logPath, DateTime.Now.ToString("yyyy_MM_dd"));
                File.AppendAllText(logName, string.Format("===============================================================\r\n{0}\r\n时间：{1}\r\n", msg, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message);
            }
        }
    }
}
