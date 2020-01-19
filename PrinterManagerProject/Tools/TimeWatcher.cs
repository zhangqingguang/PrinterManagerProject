using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterManagerProject.Tools
{
    public class TimeWatcher
    {
        /// <summary>
        /// 收到84光幕信号
        /// </summary>
        public static DateTime Receive84Time { get; set; }
        /// <summary>
        /// 接收有效的84光幕信号时间
        /// </summary>
        public static DateTime ReceiveEffect84Time { get; set; }
        /// <summary>
        /// 开始发送CCD2成功或失败时间
        /// </summary>
        public static DateTime SendCCD2Time { get; set; }
        /// <summary>
        /// 发送CCD2成功或失败指令成功时间
        /// </summary>
        public static DateTime SendedCCD2Time { get; set; }
        /// <summary>
        /// 收到CCD2返回时间
        /// </summary>
        public static DateTime ReceiveCCD2Time { get; set; }
    }
}
