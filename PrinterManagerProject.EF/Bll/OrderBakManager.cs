using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PrinterManagerProject.EF.Models;

namespace PrinterManagerProject.EF
{
    public class OrderBakManager : BaseDALL<tOrderBak>
    {
        /// <summary>
        /// 获取当天医嘱
        /// </summary>
        /// <param name="dateTime">用药日期</param>
        /// <param name="batch">批次编号</param>
        /// <returns></returns>
        public List<tOrder> GetAllOrderByDateTime(DateTime dateTime, string batch)
        {
            var date = dateTime.ToString("yyyy-MM-dd");
            List<tOrder> list = new List<tOrder>();
            var query = DBContext.tOrders.AsNoTracking().Where(s => s.use_date == date);
            if (string.IsNullOrEmpty(batch) == false)
            {
                query = query.Where(s => s.batch == batch);
            }

            // 列表按照医嘱组号、用药时间排序
            list = query.OrderBy(s => s.group_num).ThenBy(s => s.use_time).ToList();

            return list;
        }
    }
}
