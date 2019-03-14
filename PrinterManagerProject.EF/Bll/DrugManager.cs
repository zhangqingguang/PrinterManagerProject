using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrinterManagerProject.DBUtility;

namespace PrinterManagerProject.EF.Bll
{
    /// <summary>
    /// 药品管理
    /// </summary>
    public class DrugManager : BaseDALL<tDrug>
    {
        /// <summary>
        /// 同步药品信息
        /// </summary>
        public void SyncDrug()
        {
            var pivasDrugCount = PivasDbHelperSQL.GetSingle(@"select count(*) from v_for_ydwl_drug");
            var drugCount = DbHelperSQL.GetSingle(@"select count(*) from tDrug");

            if (Convert.ToInt32(pivasDrugCount) != Convert.ToInt32(drugCount))
            {
                DbHelperSQL.ExecuteSql(@"truncate table tdrug");

                var ds = PivasDbHelperSQL.Query(@"select [drug_code]
      ,[drug_name]
      ,[drug_spec]
      ,[drug_units]
      ,[drug_use_spec]
      ,[drug_use_units]
      ,[drug_form]
      ,[input_code] from v_for_ydwl_drug");

                DbHelperSQL.SqlBulkCopyByDataTable("tDrug", ds.Tables[0]);
            }


        }

        /// <summary>
        /// 获取主药分组列表
        /// </summary>
        /// <param name="useDate"></param>
        /// <param name="batch"></param>
        /// <returns></returns>
        public List<tDrug> GetMainDrugListForPrintWindow(DateTime useDate,string batch)
        {
            SqlParameter useDateParameter = new SqlParameter("@useDate", useDate.ToString("yyyy-MM-dd"));
            SqlParameter batchParameter = new SqlParameter("@batch", batch);

            return DBContext.Database.SqlQuery<tDrug>("exec p_getMainDrugList @useDate,@batch",useDateParameter,batchParameter).ToList();
        }
    }
}
