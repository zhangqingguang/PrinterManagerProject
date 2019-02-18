using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrinterManagerProject.EF;

namespace PrinterManagerProject.Tools
{
    /// <summary>
    /// 用户缓存
    /// </summary>
    public class UserCache
    {
        /// <summary>
        /// 操作员
        /// </summary>
        public static tUser Printer = new tUser(){ID=0,true_name = ""};
        /// <summary>
        /// 审核员
        /// </summary>
        public static tUser Checker = new tUser() { ID = 0, true_name = "" };
    }
}
