using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterManagerProject.Tools
{
    /// <summary>
    /// 项目配置类
    /// </summary>
    public class AppConfig
    {
        /// <summary>
        /// 收到光幕最小有效间隔时间，超过这个时间的光幕忽略
        /// </summary>
        internal static int LightTimeInterval=260;

        /// <summary>
        /// 将CCD设为空闲状态等待时间
        /// </summary>
        public static int FreeCCDBusyState = 260;
        /// <summary>
        /// 收到拍照命令后CCD拍照等待时间
        /// </summary>
        public static int CcdTakePhotoSleepTime = 100;
        /// <summary>
        /// CCD拍照结果超时时间
        /// </summary>
        public static int CcdTakePhotoExpireTime = 1000;
        /// <summary>
        /// CCD1验证通过，发送结果等待时间
        /// CCD2不通过要先退出，再复位拨板，时间会比CCD1成功直接复位拨板消耗时间长
        /// </summary>
        public static int Ccd1SuccessSleepTime = 150;
        /// <summary>
        /// CCD1拍照最大次数
        /// </summary>
        public static int CCD1TakePhotoMaxTimes = 3;
        /// <summary>
        /// 读取光幕信号间隔时间
        /// </summary>
        public static int LightReaderIntervalTime = 75;
        /// <summary>
        /// 读取警告信号间隔时间
        /// </summary>
        public static int WarningReaderIntervalTime = 1000;
        /// <summary>
        /// 队列中最大液体数
        /// </summary>
        public static int MaxQueueCount = 3;
        /// <summary>
        /// 队列中最大未打印液体数
        /// </summary>
        public static int MaxNotPrintQueueCount = 2;

    }
}
