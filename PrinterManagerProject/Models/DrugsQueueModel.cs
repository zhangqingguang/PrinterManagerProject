using PrinterManagerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrinterManagerProject.EF;

namespace PrinterManagerProject.Models
{
    /// <summary>
    /// 药品队列模型
    /// </summary>
    public class OrderQueueModel
    {
        /// <summary>
        /// 插入队列中的顺序号
        /// </summary>
        public int Index = 1;
        #region 识别出的溶媒基本信息
        /// <summary>
        /// CCD1命令内容
        /// </summary>
        public string CMD { get; set; }
        /// <summary>
        /// 液体规格
        /// </summary>
        public string Spec { get; set; }
        /// <summary>
        /// 液体毫升数
        /// </summary>
        public string ML { get; set; }

        #endregion

        /// <summary>
        /// 实际数据源(库)数据
        /// </summary>
        public tOrder Drug { get; set; }

        /// <summary>
        /// 打印标签时生成的二维码
        /// </summary>
        public string QRData { get; set; }

        /// <summary>
        /// 二维码扫描到的数据
        /// </summary>
        public string ScanData { get; set; }
        /// <summary>
        /// 打印机光幕扫描，
        /// </summary>
        public bool PrinterLightScan { get; set; }
        /// <summary>
        /// 扫码枪光幕扫描
        /// </summary>
        public bool ScannerLightScan { get; set; }
        /// <summary>
        /// 是否过CCD2光幕扫描（正在拍照识别状态）
        /// </summary>
        public bool CCD2LightScan { get; set; }

        /// <summary>
        /// CCD1拍照次数，CCD2的拍照次数不能超过CCD1的拍照次数
        /// </summary>
        public int CCD1TakePhotoCount { get; set; }
        /// <summary>
        /// 发给PLC的规格代码
        /// </summary>
        public string SpecCmd { get; internal set; }
        /// <summary>
        /// 入队时间，给PLC发送成功指令时间
        /// </summary>
        public DateTime EnqueueTime { get; set; }
        /// <summary>
        /// 收到打印光幕时间
        /// </summary>
        public DateTime PrintLightTime { get; set; }
        /// <summary>
        /// 过扫码枪光幕时间
        /// </summary>
        public DateTime ScannerLightTime { get; set; }
        /// <summary>
        /// 收到84信号时间
        /// </summary>
        public DateTime CCD2Time { get; set; }
    }
}
