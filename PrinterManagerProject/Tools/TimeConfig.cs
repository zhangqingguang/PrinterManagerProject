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

        public static bool CCD2IsEnabled = true;


        /// <summary>
        /// 卡药检测时间间隔
        /// </summary>
        public static int BlockDetectictInterval = 20;

        #region 档板卡药配置
        /// <summary>
        /// 是否启用挡板处卡药检测
        /// </summary>
        public static bool BeforePrintLightBlockDetectictIsEnabled = false;
        /// <summary>
        /// 挡板处卡药时间间隔（CCD1继续到打印机光幕时间间隔）
        /// 90条数据统计最大时间1523
        /// </summary>
        public static int BeforePrintLightBlockDuration = 1900;
        #endregion



        #region CCD2前卡药配置
        /// <summary>
        /// 是否启用挡板处卡药检测
        /// </summary>
        public static bool BeforeCCD2BlockDetectictIsEnabled = true;

        /// <summary>
        /// 
        /// </summary>
        public static int BeforeCCD2BlockAfterScannerLightTimes = 500;
        /// <summary>
        /// 挡板处卡药时间间隔（CCD1继续到打印机光幕时间间隔）
        /// </summary>
        public static int BeforeCCD2MaxCount = 2;
        #endregion

//CCD1-打印光幕时间	1523
//CCD1-扫码枪光幕时间	3328
//CCD1-CCD2光幕时间
//CCD1-完成时间	5611
	
//打印光幕时间-扫码枪光幕时间	2041
//打印光幕时间-CCD2光幕时间
//打印光幕时间-完成时间	4175
	
//扫码枪光幕时间-CCD2光幕时间
//扫码枪光幕时间-完成时间	2495
	
//CCD2光幕时间-完成时间

    }
}
