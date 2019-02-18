using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrinterManagerProject.DBUtility;

namespace PrinterManagerProject.EF.Bll
{
    /// <summary>
    /// 数据库连接状态管理
    /// </summary>
    public class ConnectionManager
    {
        /// <summary>
        /// 检查Pivas数据库连接状态
        /// </summary>
        /// <returns></returns>
        public static bool CheckPivasConnetionStatus()
        {
            try
            {
                PivasDbHelperSQL.Query("select top 1 * from v_for_ydwl_batch");
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 检查数据库连接状态
        /// </summary>
        /// <returns></returns>
        public static bool CheckConnetionStatus()
        {
            try
            {
                DbHelperSQL.Query("select top 1 * from tBatch");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
