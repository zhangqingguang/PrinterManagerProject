using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PrinterManagerProject.DBUtility;

namespace PrinterManagerProject.EF
{
    public class DataSync
    {

        /// <summary>
        /// 同步医嘱数据
        /// </summary>
        public void SyncOrder(DateTime dateTime)
        {
            DownloadOrder(dateTime);
            CompareData(dateTime);
        }
        /// <summary>
        /// 对比并更新数据
        /// </summary>
        /// <param name="dateTime"></param>
        private void CompareData(DateTime dateTime)
        {
            using (var db = new PrintTagDbEntities())
            {
                var date = dateTime.ToString("yyyy-MM-dd");


                var orders = db.tOrders.Where(s => s.use_date == date).ToList();

                var zhys = db.tZHies.Where(s => s.use_date == date).GroupBy(s=>new
                {
                    s.use_date,
                    s.group_num,
                    s.use_time
                }).OrderBy(s=>s.Key.use_date)
                .ThenBy(s=>s.Key.group_num)
                .ThenBy(s=>s.Key.use_time)
                .ToList();

                var index = 0;
                var number = 1;
                tOrder currentOrder = null;
                if (orders.Any())
                {
                    currentOrder = orders.ElementAt(0);
                    number = orders.Max(s => s.RowNumber) + 1;
                }
                foreach (var zhy in zhys)
                {
                    var item = zhy.FirstOrDefault(s => s.drug_weight == "5");
                    if (item == null)
                    {
                        item = zhy.FirstOrDefault(s => s.drug_weight == "1");
                    }
                    if (orders.Any() && currentOrder != null && zhy.Key.group_num == currentOrder.group_num &&
                        zhy.Key.use_time == currentOrder.use_time)
                    {
                        // 更新
                        if (item.drug_spec != currentOrder.drug_spec)
                        {
                            currentOrder.drug_spec = item.drug_spec;
                        }

                        if (item.special_medicationtip != currentOrder.special_medicationtip)
                        {
                            currentOrder.special_medicationtip = item.special_medicationtip;
                        }

                        if (item.is_cpfh != currentOrder.is_cpfh)
                        {
                            currentOrder.is_cpfh = item.is_cpfh;
                        }

                        if (item.is_sf != currentOrder.is_sf)
                        {
                            currentOrder.is_sf = item.is_sf;
                        }

                        if (item.order_status != currentOrder.order_status)
                        {
                            currentOrder.order_status = item.order_status;
                        }
                        if (item.is_db != currentOrder.is_db)
                        {
                            currentOrder.is_db = item.is_db;
                        }
                        index++;

                        db.tOrders.AddOrUpdate(currentOrder);
                    }
                    else
                    {

                        System.Console.WriteLine(DateTime.Now);

                        var zhyItem = zhy.FirstOrDefault(s => s.drug_weight == "1");
                        var order = Mapper.Map<tOrder>(zhyItem);

                        order.ydrug_class_name = item.drug_class_name;
                        order.ydrug_name = item.drug_name;
                        order.ydrug_spec = item.drug_spec;
                        order.RowNumber = number++;
                        order.printing_status = 0;

                        db.tOrders.Add(order);
                    }
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 从Pivas下载数据到tZHY中
        /// </summary>
        /// <param name="dateTime"></param>
        private void DownloadOrder(DateTime dateTime)
        {
            var useDateParam = new SqlParameter("@usedate", dateTime.ToString("yyyy-MM-dd"));
            var dataset = PivasDbHelperSQL.Query("select * from v_for_ydwl where use_date=@usedate", useDateParam);
            try
            {
                var dt = dataset.Tables[0];
                dt.Columns.Remove(dt.Columns["id"]);

                dt.Columns.Add(new DataColumn("Id", typeof(Guid)));
                foreach (DataRow dataRow in dt.Rows)
                {
                    dataRow["Id"] = Guid.NewGuid();
                }

                // 删除原有数据
                DbHelperSQL.ExecuteSql("delete tZHY where use_date=@usedate", useDateParam);

                // 添加新数据
                DbHelperSQL.SqlBulkCopyByDataTable("tZHY", dataset.Tables[0]);

#warning 需要判断是否新增医嘱、是否停药等
            }
            catch (Exception e)
            {
                System.Console.Write(e);
                throw;
            }
        }
    }
}
