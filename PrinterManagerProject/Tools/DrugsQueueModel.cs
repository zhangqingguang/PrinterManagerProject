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
        public string CMD { get; set; }
        public string Spec { get; set; }
        public string ML { get; set; }

        #endregion

        /// <summary>
        /// 实际数据源(库)数据
        /// </summary>
        public Model.ListAllModel Drug { get; set; }

        /// <summary>
        /// 二维码扫描到的数据
        /// </summary>
        public string QRData { get; set; }

        /// <summary>
        /// 二维码扫描到的数据
        /// </summary>
        public string ScanData { get; set; }

        /// <summary>
        /// 去扫描扫码
        /// </summary>
        public bool GoToScan { get; set; }

        /// <summary>
        /// 重复扫描次数
        /// </summary>
        public int Count { get; set; }
    }
}
