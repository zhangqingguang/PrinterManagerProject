using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterManagerProject.EF.Models
{
    /// <summary>
    /// 标签打印状态
    /// </summary>
    public enum PrintStatusEnum
    {
        /// <summary>
        /// 未打印
        /// </summary>
        NotPrint = 0,
        /// <summary>
        /// 已打印
        /// </summary>
        Success=1,
        /// <summary>
        /// 打印失败
        /// </summary>
        Fail=2
    }
}
