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
    public class OrderManager : BaseDALL<tOrder>
    {

        /// <summary>
        /// 获取当天医嘱
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public List<tOrder> GetAllOrderByDateTime(DateTime dateTime)
        {
            var date = dateTime.ToString("yyyy-MM-dd");
            var list = DBContext.tOrders
                .AsNoTracking()
                .Where(s => s.use_date == date)
                .ToList();

            if (dateTime.Date >= DateTime.Now.Date)
            {
                if (list.Any() == false)
                {
                    new DataSync().SyncOrder(dateTime);
                    list = DBContext.tOrders
                        .AsNoTracking()
                        .Where(s => s.use_date == date)
                        .ToList();
                }
            }

            return list;
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
                    drug_name = s.drug_name,
                    use_count = s.use_count,
                    id = s.drug_id
                })
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
        public bool PrintSuccess(int id, PrintModelEnum printModel, string sbatches,int czrUserId,string czrUserName,int shrUserId,string shrUserName)
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
