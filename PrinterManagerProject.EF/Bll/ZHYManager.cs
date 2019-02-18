using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PrinterManagerProject.EF.Models;

namespace PrinterManagerProject.EF
{
    /// <summary>
    /// Pivas临时医嘱表
    /// </summary>
    public class ZhyManager : BaseDALL<tZHY>
    {
        /// <summary>
        /// 根据医嘱Id获取当前医嘱的所有药品（不包括溶媒）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<PrintDrugModel> GetDrugListByOrderId(int id)
        {
            var order = Find(id);
            if (order == null)
            {
                return new List<PrintDrugModel>();
            }

            return GetAll(s => s.use_date == order.use_date && s.use_time == order.use_time &&
                               s.group_num == order.group_num && s.drug_weight != "1")
                .Select(s => new PrintDrugModel()
                {
                    id = s.drug_id,
                    drug_name = s.drug_name,
                    use_count = s.use_count
                }).ToList();
        }
    }
}
