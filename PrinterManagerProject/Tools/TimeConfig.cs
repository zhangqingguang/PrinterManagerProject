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
        internal static int LightTimeInterval=180;
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
        /// <summary>
        /// 长时间未放药停机时间（10分钟）
        /// </summary>
        public static int QueueIsEmptyStopTime = 10*60*1000;
        /// <summary>
        /// CCD1信号丢失或延时是否停止打印
        /// </summary>
        public static bool IsStopOnCCD1ResultDelayed = false;
        /// <summary>
        /// 检测CCD1信号丢失或延时的81信号个数
        /// </summary>
        public static int Waite81SignalTimesOnCCD1ResultDelayed = 0;

        /// <summary>
        /// 是否启用CCD2检测，如false，CCD2不拍照
        /// </summary>
        public static bool CCD2IsEnabled = true;

        /// <summary>
        /// 卡药检测时间间隔
        /// </summary>
        public static int BlockDetectictInterval = 20;

        /// <summary>
        /// 两个液体从CCD1推出的最小间隔时间
        /// </summary>
        public static int TowMedicionMinInterval = 1700;

        #region CCD2前卡药配置
        /// <summary>
        /// 是否启用CCD2前未拨药数量检测
        /// </summary>
        public static bool BeforeCCD2BlockDetectictIsEnabled = false;

        /// <summary>
        /// 
        /// </summary>
        public static int BeforeCCD2BlockAfterScannerLightTimes = 500;
        /// <summary>
        /// 挡板处卡药时间间隔（CCD1继续到打印机光幕时间间隔）
        /// </summary>
        public static int BeforeCCD2MaxCount = 2;
        #endregion
        
        #region 规定时间没到打印机超时
        /// <summary>
        /// 是否开启规定时间内没到打印机超时报警检测
        /// </summary>
        public static bool IsEnabledPrinterLightExpireDetectict = true;

        /// <summary>
        /// 从发送CCD1成功到收到打印机光幕时间
        /// </summary>
        public static int PrinterLightExpireDetectictTimes = 2200;  //1800
        #endregion

        #region 规定时间没到扫码枪光幕超时
        /// <summary>
        /// 是否开启规定时间内没到扫码枪光幕超时报警检测
        /// </summary>
        public static bool IsEnabledScannerLightExpireDetectict = true;

        /// <summary>
        /// 从收到打印机光幕时间到收到扫码枪光幕时间
        /// </summary>
        public static int ScannerLightExpireDetectictTimes = 2100; //2300
        #endregion

        #region 规定时间没到CCD2超时
        /// <summary>
        /// 是否开启规定时间内没到CCD2超时报警检测
        /// </summary>
        public static bool IsEnabledCCD2ExpireDetectict = true;

        /// <summary>
        /// 从收到收到扫码枪光幕时间到CCD2光幕时间
        /// </summary>
        public static int CCD2ExpireDetectictTimes = 2200; //2000
        #endregion

        #region 信号间最小间隔时间，用于过滤无效信号
        /// <summary>
        /// 入队时间到打印光幕最小时间
        /// </summary>
        public static int EnqueueToPrintLightMinTime = 900; // 1000
        /// <summary>
        /// 入队时间到扫码枪光幕时间
        /// </summary>
        public static int EnqueueToScannerLightMinTime = 2600;// 2700
        /// <summary>
        /// 打印光幕时间到扫码枪光幕时间
        /// </summary>
        public static int PrintToScannerLightMinTime = 1100;// 1200
        /// <summary>
        /// 扫码枪光幕到CCD2光幕最小时间
        /// </summary>
        public static int ScannerToCCD2LightMinTime = 1200; // 1300

        /// <summary>
        /// 入队时间到CCD2光幕最小时间
        /// </summary>
        public static int EnqueueToCCD2LightMinTime = 4200;
        #endregion

    }
}
