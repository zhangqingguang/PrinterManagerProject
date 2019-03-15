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
        static AppConfig()
        {
            AppConfigManager appConfigManager = new AppConfigManager();

            IsDebug = appConfigManager.GetBool("IsDebug", isDebug);

            LightTimeInterval = appConfigManager.GetInt("lightTimeInterval", lightTimeInterval);
            FreeCCDBusyState = appConfigManager.GetInt("freeCCDBusyState", freeCCDBusyState);
            CcdTakePhotoSleepTime = appConfigManager.GetInt("ccdTakePhotoSleepTime", ccdTakePhotoSleepTime);
            CcdTakePhotoExpireTime = appConfigManager.GetInt("ccdTakePhotoExpireTime", ccdTakePhotoExpireTime);
            Ccd1SuccessSleepTime = appConfigManager.GetInt("ccd1SuccessSleepTime", ccd1SuccessSleepTime);
            CCD1TakePhotoMaxTimes = appConfigManager.GetInt("cCD1TakePhotoMaxTimes", cCD1TakePhotoMaxTimes);
            LightReaderIntervalTime = appConfigManager.GetInt("lightReaderIntervalTime", lightReaderIntervalTime);
            WarningReaderIntervalTime = appConfigManager.GetInt("warningReaderIntervalTime", warningReaderIntervalTime);

            MaxQueueCount = appConfigManager.GetInt("maxQueueCount", maxQueueCount);
            MaxNotPrintQueueCount = appConfigManager.GetInt("maxNotPrintQueueCount", maxNotPrintQueueCount);
            QueueIsEmptyStopTime = appConfigManager.GetInt("queueIsEmptyStopTime", queueIsEmptyStopTime);

            IsStopOnCCD1ResultDelayed = appConfigManager.GetBool("isStopOnCCD1ResultDelayed", isStopOnCCD1ResultDelayed);

            Waite81SignalTimesOnCCD1ResultDelayed = appConfigManager.GetInt("waite81SignalTimesOnCCD1ResultDelayed", waite81SignalTimesOnCCD1ResultDelayed);

            CCD2IsEnabled = appConfigManager.GetBool("cCD2IsEnabled", cCD2IsEnabled);

            BlockDetectictInterval = appConfigManager.GetInt("blockDetectictInterval", blockDetectictInterval);
            TowMedicionMinInterval = appConfigManager.GetInt("towMedicionMinInterval", towMedicionMinInterval);

            BeforeCCD2BlockDetectictIsEnabled = appConfigManager.GetBool("beforeCCD2BlockDetectictIsEnabled", beforeCCD2BlockDetectictIsEnabled);

            BeforeCCD2BlockAfterScannerLightTimes = appConfigManager.GetInt("beforeCCD2BlockAfterScannerLightTimes", beforeCCD2BlockAfterScannerLightTimes);
            BeforeCCD2MaxCount = appConfigManager.GetInt("beforeCCD2MaxCount", beforeCCD2MaxCount);

            IsEnabledPrinterLightExpireDetectict = appConfigManager.GetBool("isEnabledPrinterLightExpireDetectict", isEnabledPrinterLightExpireDetectict);

            PrinterLightExpireDetectictTimes = appConfigManager.GetInt("printerLightExpireDetectictTimes", printerLightExpireDetectictTimes);

            IsEnabledScannerLightExpireDetectict = appConfigManager.GetBool("isEnabledScannerLightExpireDetectict", isEnabledScannerLightExpireDetectict);

            ScannerLightExpireDetectictTimes = appConfigManager.GetInt("scannerLightExpireDetectictTimes", scannerLightExpireDetectictTimes);

            IsEnabledCCD2ExpireDetectict = appConfigManager.GetBool("isEnabledCCD2ExpireDetectict", isEnabledCCD2ExpireDetectict);

            CCD2ExpireDetectictTimes = appConfigManager.GetInt("cCD2ExpireDetectictTimes", cCD2ExpireDetectictTimes);
            EnqueueToPrintLightMinTime = appConfigManager.GetInt("enqueueToPrintLightMinTime", enqueueToPrintLightMinTime);
            EnqueueToScannerLightMinTime = appConfigManager.GetInt("enqueueToScannerLightMinTime", enqueueToScannerLightMinTime);
            PrintToScannerLightMinTime = appConfigManager.GetInt("printToScannerLightMinTime", printToScannerLightMinTime);
            ScannerToCCD2LightMinTime = appConfigManager.GetInt("scannerToCCD2LightMinTime", scannerToCCD2LightMinTime);
            EnqueueToCCD2LightMinTime = appConfigManager.GetInt("enqueueToCCD2LightMinTime", enqueueToCCD2LightMinTime);



        }
        private static bool isDebug = true;
        /// <summary>
        /// 收到光幕最小有效间隔时间，超过这个时间的光幕忽略
        /// </summary>
        private static int lightTimeInterval = 180;
        /// <summary>
        /// 将CCD设为空闲状态等待时间
        /// </summary>
        private static int freeCCDBusyState = 280;
        /// <summary>
        /// 收到拍照命令后CCD拍照等待时间
        /// </summary>
        private static int ccdTakePhotoSleepTime = 100;
        /// <summary>
        /// CCD拍照结果超时时间
        /// </summary>
        private static int ccdTakePhotoExpireTime = 1000;
        /// <summary>
        /// CCD1验证通过，发送结果等待时间
        /// CCD2不通过要先退出，再复位拨板，时间会比CCD1成功直接复位拨板消耗时间长
        /// </summary>
        private static int ccd1SuccessSleepTime = 150;
        /// <summary>
        /// CCD1拍照最大次数
        /// </summary>
        private static int cCD1TakePhotoMaxTimes = 1;
        /// <summary>
        /// 读取光幕信号间隔时间
        /// </summary>
        private static int lightReaderIntervalTime = 100;
        /// <summary>
        /// 读取警告信号间隔时间
        /// </summary>
        private static int warningReaderIntervalTime = 1000;
        /// <summary>
        /// 队列中最大液体数
        /// </summary>
        private static int maxQueueCount = 3;
        /// <summary>
        /// 队列中最大未打印液体数
        /// </summary>
        private static int maxNotPrintQueueCount = 2;
        /// <summary>
        /// 长时间未放药停机时间（10分钟）
        /// </summary>
        private static int queueIsEmptyStopTime = 10 * 60 * 1000;
        /// <summary>
        /// CCD1信号丢失或延时是否停止打印
        /// </summary>
        private static bool isStopOnCCD1ResultDelayed = false;
        /// <summary>
        /// 检测CCD1信号丢失或延时的81信号个数
        /// </summary>
        private static int waite81SignalTimesOnCCD1ResultDelayed = 0;

        /// <summary>
        /// 是否启用CCD2检测，如false，CCD2不拍照
        /// </summary>
        private static bool cCD2IsEnabled = false;

        /// <summary>
        /// 卡药检测时间间隔
        /// </summary>
        private static int blockDetectictInterval = 20;

        /// <summary>
        /// 两个液体从CCD1推出的最小间隔时间
        /// </summary>
        private static int towMedicionMinInterval = 1020;

        #region CCD2前卡药配置
        /// <summary>
        /// 是否启用CCD2前未拨药数量检测
        /// </summary>
        private static bool beforeCCD2BlockDetectictIsEnabled = false;

        /// <summary>
        /// 
        /// </summary>
        private static int beforeCCD2BlockAfterScannerLightTimes = 500;
        /// <summary>
        /// 挡板处卡药时间间隔（CCD1继续到打印机光幕时间间隔）
        /// </summary>
        private static int beforeCCD2MaxCount = 2;
        #endregion

        #region 规定时间没到打印机超时
        /// <summary>
        /// 是否开启规定时间内没到打印机超时报警检测
        /// </summary>
        private static bool isEnabledPrinterLightExpireDetectict = false;

        /// <summary>
        /// 从发送CCD1成功到收到打印机光幕时间
        /// </summary>
        private static int printerLightExpireDetectictTimes = 2400;  //1800
        #endregion

        #region 规定时间没到扫码枪光幕超时
        /// <summary>
        /// 是否开启规定时间内没到扫码枪光幕超时报警检测
        /// </summary>
        private static bool isEnabledScannerLightExpireDetectict = false;

        /// <summary>
        /// 从收到打印机光幕时间到收到扫码枪光幕时间
        /// </summary>
        private static int scannerLightExpireDetectictTimes = 2500; //2300
        #endregion

        #region 规定时间没到CCD2超时
        /// <summary>
        /// 是否开启规定时间内没到CCD2超时报警检测
        /// </summary>
        private static bool isEnabledCCD2ExpireDetectict = false;

        /// <summary>
        /// 从收到收到扫码枪光幕时间到CCD2光幕时间
        /// </summary>
        private static int cCD2ExpireDetectictTimes = 2500; //2000
        #endregion

        #region 信号间最小间隔时间，用于过滤无效信号
        /// <summary>
        /// 入队时间到打印光幕最小时间
        /// </summary>
        private static int enqueueToPrintLightMinTime = 700; // 1000
        /// <summary>
        /// 入队时间到扫码枪光幕时间
        /// </summary>
        private static int enqueueToScannerLightMinTime = 2400;// 2700
        /// <summary>
        /// 打印光幕时间到扫码枪光幕时间
        /// </summary>
        private static int printToScannerLightMinTime = 900;// 1200
        /// <summary>
        /// 扫码枪光幕到CCD2光幕最小时间
        /// </summary>
        private static int scannerToCCD2LightMinTime = 600; // 1300

        /// <summary>
        /// 入队时间到CCD2光幕最小时间
        /// </summary>
        private static int enqueueToCCD2LightMinTime = 3700; //4200
        #endregion


        public static int LightTimeInterval { get => lightTimeInterval; set => lightTimeInterval = value; }
        public static int FreeCCDBusyState { get => freeCCDBusyState; set => freeCCDBusyState = value; }
        public static int CcdTakePhotoSleepTime { get => ccdTakePhotoSleepTime; set => ccdTakePhotoSleepTime = value; }
        public static int CcdTakePhotoExpireTime { get => ccdTakePhotoExpireTime; set => ccdTakePhotoExpireTime = value; }
        public static int Ccd1SuccessSleepTime { get => ccd1SuccessSleepTime; set => ccd1SuccessSleepTime = value; }
        public static int CCD1TakePhotoMaxTimes { get => cCD1TakePhotoMaxTimes; set => cCD1TakePhotoMaxTimes = value; }
        public static int LightReaderIntervalTime { get => lightReaderIntervalTime; set => lightReaderIntervalTime = value; }
        public static int WarningReaderIntervalTime { get => warningReaderIntervalTime; set => warningReaderIntervalTime = value; }

        public static int MaxQueueCount { get => maxQueueCount; set => maxQueueCount = value; }
        public static int MaxNotPrintQueueCount { get => maxNotPrintQueueCount; set => maxNotPrintQueueCount = value; }
        public static int QueueIsEmptyStopTime { get => queueIsEmptyStopTime; set => queueIsEmptyStopTime = value; }

        public static bool IsStopOnCCD1ResultDelayed { get => isStopOnCCD1ResultDelayed; set => isStopOnCCD1ResultDelayed = value; }

        public static int Waite81SignalTimesOnCCD1ResultDelayed { get => waite81SignalTimesOnCCD1ResultDelayed; set => waite81SignalTimesOnCCD1ResultDelayed = value; }

        public static bool CCD2IsEnabled { get => cCD2IsEnabled; set => cCD2IsEnabled = value; }

        public static int BlockDetectictInterval { get => blockDetectictInterval; set => blockDetectictInterval = value; }
        public static int TowMedicionMinInterval { get => towMedicionMinInterval; set => towMedicionMinInterval = value; }

        public static bool BeforeCCD2BlockDetectictIsEnabled { get => beforeCCD2BlockDetectictIsEnabled; set => beforeCCD2BlockDetectictIsEnabled = value; }

        public static int BeforeCCD2BlockAfterScannerLightTimes { get => beforeCCD2BlockAfterScannerLightTimes; set => beforeCCD2BlockAfterScannerLightTimes = value; }
        public static int BeforeCCD2MaxCount { get => beforeCCD2MaxCount; set => beforeCCD2MaxCount = value; }

        public static bool IsEnabledPrinterLightExpireDetectict { get => isEnabledPrinterLightExpireDetectict; set => isEnabledPrinterLightExpireDetectict = value; }

        public static int PrinterLightExpireDetectictTimes { get => printerLightExpireDetectictTimes; set => printerLightExpireDetectictTimes = value; }
        public static bool IsEnabledScannerLightExpireDetectict { get => isEnabledScannerLightExpireDetectict; set => isEnabledScannerLightExpireDetectict = value; }

        public static int ScannerLightExpireDetectictTimes { get => scannerLightExpireDetectictTimes; set => scannerLightExpireDetectictTimes = value; }

        public static bool IsEnabledCCD2ExpireDetectict { get => isEnabledCCD2ExpireDetectict; set => isEnabledCCD2ExpireDetectict = value; }

        public static int CCD2ExpireDetectictTimes { get => cCD2ExpireDetectictTimes; set => cCD2ExpireDetectictTimes = value; }
        public static int EnqueueToPrintLightMinTime { get => enqueueToPrintLightMinTime; set => enqueueToPrintLightMinTime = value; }
        public static int EnqueueToScannerLightMinTime { get => enqueueToScannerLightMinTime; set => enqueueToScannerLightMinTime = value; }
        public static int PrintToScannerLightMinTime { get => printToScannerLightMinTime; set => printToScannerLightMinTime = value; }
        public static int ScannerToCCD2LightMinTime { get => scannerToCCD2LightMinTime; set => scannerToCCD2LightMinTime = value; }
        public static int EnqueueToCCD2LightMinTime { get => enqueueToCCD2LightMinTime; set => enqueueToCCD2LightMinTime = value; }
        public static bool IsDebug { get => isDebug; set => isDebug = value; }
    }
}
