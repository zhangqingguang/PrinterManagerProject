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
        /// 将CCD设为空闲状态等待时间
        /// </summary>
        public static int FreeCCDBusyState = 100;
        /// <summary>
        /// 收到拍照命令后CCD拍照等待时间
        /// </summary>
        public static int CcdTakePhotoSleepTime = 10;
        /// <summary>
        /// CCD拍照结果超时时间
        /// </summary>
        public static int CcdTakePhotoExpireTime = 1500;
        /// <summary>
        /// CCD1拍照最大次数
        /// </summary>
        public static int CCD1TakePhotoMaxTimes = 3;
        /// <summary>
        /// 读取光幕信号间隔时间
        /// </summary>
        public static int LightReaderIntervalTime = 100;
        /// <summary>
        /// 读取警告信号间隔时间
        /// </summary>
        public static int WarningReaderIntervalTime = 1000;

    }
}
