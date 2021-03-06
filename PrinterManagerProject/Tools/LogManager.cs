﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterManagerProject
{
    public class myEventLog
    {
        static log4net.ILog log = null;
        static object lockHelper = new object();
        public static log4net.ILog Log
        {
            get
            {
                if(log == null)
                {
                    lock (lockHelper)
                    {
                        log = log4net.LogManager.GetLogger("LogSystem");
                    }
                }
                return log;
            }
        }
        public static void LogInfo(object message)
        {
            Log.Info(message);
            System.Console.WriteLine("---> "+message);
        }
        public static void LogError(object message,Exception exception)
        {
            Log.Error(message,exception);
            System.Console.WriteLine("---> 【Error】 " + message);
        }
    }
}
