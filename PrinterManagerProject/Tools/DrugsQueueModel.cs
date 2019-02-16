using PrinterManagerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterManagerProject.Tools
{
    /// <summary>
    /// 药品队列模型
    /// </summary>
    public class DrugsQueueModel
    {
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
        public Model.ListAllModel Drug { get; set; }

        /// <summary>
        /// 打印标签时生成的二维码
        /// </summary>
        public string QRData { get; set; }

        /// <summary>
        /// 二维码扫描到的数据
        /// </summary>
        public string ScanData { get; set; }

        /// <summary>
        /// 去扫描扫码：过打印光幕时设置为true，
        /// </summary>
        public bool GoToScan { get; set; }

        /// <summary>
        /// CCD1拍照次数，CCD2的拍照次数不能超过CCD1的拍照次数
        /// </summary>
        public int CCD1TakePhotoCount { get; set; }
    }
}
