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
                new DataSync().SyncOrder(dateTime, batch);
                // 从备份中获取数据
                list = new OrderBakManager().GetAllOrderByDateTime(dateTime, batch);
            }
            else
            {
                var query = DBContext.tOrders.AsNoTracking().Where(s => s.use_date == date && s.order_status =="正常");
                if (string.IsNullOrEmpty(batch) == false)
                {
                    query = query.Where(s => s.batch == batch);
                }

                if (dateTime.Date >= DateTime.Now.Date)
                {
                    if (DBContext.tOrders.Any(s => s.use_date == date && s.batch == batch) == false)
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
                    durg_use_sp=s.durg_use_sp+" "+s.drug_use_units,
                    s.drug_spec,
                    s.xsyxj,
                    drug_name = s.drug_name,
                    use_count = s.use_count,
                    id = s.drug_id
                })
                .ToList()
                .OrderBy(s=> string.IsNullOrEmpty(s.xsyxj)?0: Convert.ToInt32(s.xsyxj))
                .Select(s => new PrintDrugModel()
                {
                    drug_name = s.drug_name,
                    use_count = s.use_count,
                    spec = s.drug_spec,
                    durg_use_sp = s.durg_use_sp,
                    id = s.id
                }).ToList();
        }

        /// <summary>
        /// 记录打印成功
        /// </summary>
        /// <param name="id"></param>
        /// <param name="printModel"></param>
        /// <param name="sbatches"></param>
        public bool PrintSuccess(int id, PrintModelEnum printModel, string sbatches, string czrUserAccount, string czrUserName, string shrUserAccount, string shrUserName)
        {
            using (var db = new PrintTagDbEntities())
            {
                var orderIdParam = new SqlParameter("@orderId", id);

                var statusParam = new SqlParameter("@status", PrintStatusEnum.Success);
                var modelParam = new SqlParameter("@model", printModel);
                var datetimeParam = new SqlParameter("@datetime", DateTime.Now);
                var printerUserIdParam = new SqlParameter("@printerUserId", czrUserAccount);
                var printerUserNameParam = new SqlParameter("@printerUserName", czrUserName);
                var checkUserIdParam = new SqlParameter("@checkUserId", shrUserAccount);
                var checkerUserNameParam = new SqlParameter("@checkerUserName", shrUserName);

                var execCount = db.Database.ExecuteSqlCommand(@"update tOrder set 
printing_status = @status,
printing_model = @model,
printing_time = @datetime,
sbatches = '',
PrintUserId = @printerUserId,
PrintUserName = @printerUserName,
CheckUserId = @checkUserId,
CheckUserName = @checkerUserName
where id=@orderId
", statusParam
, modelParam, datetimeParam, printerUserIdParam, printerUserNameParam, checkUserIdParam, checkerUserNameParam, orderIdParam);

                return execCount > 0;

                //var item = db.tOrders.FirstOrDefault(s => s.Id == id);
                //item.printing_status = PrintStatusEnum.Success;
                //item.printing_model = printModel;
                //item.printing_time = DateTime.Now;
                //item.sbatches = "";

                //item.PrintUserId = czrUserId;
                //item.PrintUserName = czrUserName;
                //item.CheckUserId = shrUserId;
                //item.CheckUserName = shrUserName;
                //DBContext.SaveChanges();
            }
        }
    }
}
