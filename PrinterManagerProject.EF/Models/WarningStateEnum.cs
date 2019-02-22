using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterManagerProject.EF.Models
{
    /// <summary>
    /// 异常状态
    /// </summary>
    public class WarningStateEnum
    {
        /// <summary>
        /// 有效期过期
        /// </summary>
        public static string Expire="有效期过期";
        /// <summary>
        /// 溶媒不符
        /// </summary>
        public static string DrugMismatch= "溶媒不符";
        /// <summary>
        /// 标签无法识别
        /// </summary>
        public static string TagUnRecognition= "标签无法识别";
        /// <summary>
        /// 未能贴覆标签
        /// </summary>
        public static string NoTag= "未能贴覆标签";
        /// <summary>
        /// 二维码不一致
        /// </summary>
        public static string QRCodeMismatch= "二维码不一致";
        /// <summary>
        /// 数据回写失败
        /// </summary>
        public static string DataUpdateFailed= "数据回写失败";
        /// <summary>
        /// 数据不在队列中
        /// </summary>
        public static string NotInQueue= "数据不在队列中";
    }
}
