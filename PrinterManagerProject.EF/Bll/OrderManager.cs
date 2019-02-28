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
    public class OrderManager : BaseDALL<tOrder>
    {

        /// <summary>
        /// 获取当天医嘱
        /// </summary>
        /// <param name="dateTime">用药日期</param>
        /// <param name="batch">批次编号</param>
        /// <returns></returns>
        public ObservableCollection<tOrder> GetAllOrderByDateTime(DateTime dateTime,string batch)
        {
            var date = dateTime.ToString("yyyy-MM-dd");
            List<tOrder> list = new List<tOrder>();
            ObservableCollection<tOrder> oList = new ObservableCollection<tOrder>();
            if (dateTime < DateTime.Now.AddDays(-1).Date)
            {
                // 从备份中获取数据
                list = new OrderBakManager().GetAllOrderByDateTime(dateTime, batch);
            }
            else
            {
                var query = DBContext.tOrders.AsNoTracking().Where(s => s.use_date == date);
                if (string.IsNullOrEmpty(batch) == false)
                {
                    query = query.Where(s => s.batch == batch);
                }

                if (dateTime.Date >= DateTime.Now.Date)
                {
                    if (DBContext.tOrders.Any(s => s.use_date == date) == false)
                    {
                        new DataSync().SyncOrder(dateTime,batch);
                    }
                }
                // 列表按照打印状态、医嘱组号、用药时间排序
                list = query.OrderBy(s=>s.printing_status).ThenBy(s => s.group_num).ThenBy(s => s.use_time).ToList();
            }

            foreach (var order in list)
            {
                oList.Add(order);
            }

            return oList;
        }

        /// <summary>
        /// 根据Order Id获取打印药品列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<PrintDrugModel> GetPrintDrugs(int id)
        {
            var order = Find(id);
            if (order == null)
            {
                return new List<PrintDrugModel>();
            }
            return DBContext.tZHies.AsNoTracking().Where(s =>
                    s.use_date == order.use_date && s.use_time == order.use_time && s.group_num == order.group_num)
                .Select(s => new
                {
                    s.sum_num,
                    drug_name = s.drug_name,
                    use_count = s.use_count,
                    id = s.drug_id
                })
                .OrderBy(s=>s.sum_num)
                .ToList()
                .Select(s => new PrintDrugModel()
                {
                    drug_name = s.drug_name,
                    use_count = s.use_count,
                    id = s.id
                }).ToList();
        }

        /// <summary>
        /// 记录打印成功
        /// </summary>
        /// <param name="id"></param>
        /// <param name="printModel"></param>
        /// <param name="sbatches"></param>
        public bool PrintSuccess(int id, PrintModelEnum printModel, string sbatches, int czrUserId, string czrUserName, int shrUserId, string shrUserName)
        {
            var item = DBContext.tOrders.FirstOrDefault(s => s.Id == id);
            item.printing_status = PrintStatusEnum.Success;
            item.printing_model = printModel;
            item.printing_time = DateTime.Now;
            item.sbatches = "";

            item.PrintUserId = czrUserId;
            item.PrintUserName = czrUserName;
            item.CheckUserId = shrUserId;
            item.CheckUserName = shrUserName;

            DBContext.SaveChanges();

            return true;
        }
    }
}
