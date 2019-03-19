using PrinterManagerProject.DBUtility;
using PrinterManagerProject.EF.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterManagerProject.EF.Bll
{
    /// <summary>
    /// Pivas用户管理
    /// </summary>
    public class PivasUserManager
    {
        /// <summary>
        /// 根据用户名获取用户信息
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public tUser GetUser(string login)
        {

            SqlParameter loginParameter = new SqlParameter("@login", login);
            var dataset = PivasDbHelperSQL.Query("select top 1 * from v_for_ydwl_user where login=@login", loginParameter);
            var dt = dataset.Tables[0];
            if(dt.Rows.Count == 0)
            {
                return null;
            }
            DataRow dr = dt.Rows[0];
            return new tUser()
            {
                user_name = dr["login"].ToString(),
                true_name = dr["username"].ToString(),
                password = dr["pwd"].ToString()
            };
        }
    }
}
