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
        /// <param name="dateTime">用药日期</param>
        /// <param name="batch">批次编号</param>
        public void SyncOrder(DateTime dateTime, string batch)
        {
            DownloadOrder(dateTime, batch);
            CompareData(dateTime);
        }
        /// <summary>
        /// 对比并更新数据
        /// </summary>
        /// <param name="dateTime"></param>
        private void CompareData(DateTime dateTime)
        {

            #region EF对比并更新数据
            //using (var db = new PrintTagDbEntities())
            //{
            //    var date = dateTime.ToString("yyyy-MM-dd");


            //    var orders = db.tOrders.Where(s => s.use_date == date)
            //        .OrderBy(s=>s.use_date)
            //        .ThenBy(s => s.group_num)
            //        .ThenBy(s => s.use_time)
            //        .ToList();

            //    var zhys = db.tZHies.Where(s => s.use_date == date).GroupBy(s=>new
            //    {
            //        s.use_date,
            //        s.group_num,
            //        s.use_time
            //    }).OrderBy(s=>s.Key.use_date)
            //    .ThenBy(s=>s.Key.group_num)
            //    .ThenBy(s=>s.Key.use_time)
            //    .ToList();

            //    var index = 0;
            //    var number = 1;
            //    tOrder currentOrder = null;
            //    if (orders.Any())
            //    {
            //        currentOrder = orders.ElementAt(0);
            //        number = orders.Max(s => s.RowNumber) + 1;
            //    }
            //    foreach (var zhy in zhys)
            //    {
            //        var item = zhy.FirstOrDefault(s => s.drug_weight == "5");
            //        if (item == null)
            //        {
            //            item = zhy.FirstOrDefault(s => s.drug_weight == "1");
            //        }

            //        currentOrder = orders.Count > index ? orders.ElementAt(index) : null;

            //        if (orders.Any() && currentOrder != null && zhy.Key.group_num == currentOrder.group_num &&
            //            zhy.Key.use_time == currentOrder.use_time)
            //        {
            //            // 更新
            //            if (item.drug_spec != currentOrder.drug_spec)
            //            {
            //                currentOrder.drug_spec = item.drug_spec;
            //            }

            //            if (item.special_medicationtip != currentOrder.special_medicationtip)
            //            {
            //                currentOrder.special_medicationtip = item.special_medicationtip;
            //            }

            //            if (item.is_cpfh != currentOrder.is_cpfh)
            //            {
            //                currentOrder.is_cpfh = item.is_cpfh;
            //            }

            //            if (item.is_sf != currentOrder.is_sf)
            //            {
            //                currentOrder.is_sf = item.is_sf;
            //            }

            //            if (item.order_status != currentOrder.order_status)
            //            {
            //                currentOrder.order_status = item.order_status;
            //            }
            //            if (item.is_db != currentOrder.is_db)
            //            {
            //                currentOrder.is_db = item.is_db;
            //            }
            //            index++;

            //            db.tOrders.AddOrUpdate(currentOrder);
            //        }
            //        else
            //        {

            //            System.Console.WriteLine(DateTime.Now);

            //            var zhyItem = zhy.FirstOrDefault(s => s.drug_weight == "1");
            //            var order = Mapper.Map<tOrder>(zhyItem);

            //            order.ydrug_class_name = item.drug_class_name;
            //            order.ydrug_name = item.drug_name;
            //            order.ydrug_id = item.drug_id;
            //            order.ydrug_spec = item.drug_spec;
            //            order.RowNumber = number++;
            //            order.printing_status = 0;

            //            db.tOrders.Add(order);
            //        }
            //    }
            //    db.SaveChanges();
            //} 
            #endregion
        }

        /// <summary>
        /// 从Pivas下载数据到tZHY中
        /// </summary>
        /// <param name="dateTime">用药日期</param>
        /// <param name="batch">批次时间</param>
        private void DownloadOrder(DateTime dateTime, string batch)
        {
            var useDateParam = new SqlParameter("@usedate", dateTime.ToString("yyyy-MM-dd"));
            var batchParam = new SqlParameter("@batch", batch);
            var dataset = PivasDbHelperSQL.Query(@"select [drug_id]
      ,[drug_number]
      ,[drug_name]
      ,[drug_weight]
      ,[drug_spmc]
      ,[drug_class_name]
      ,[drug_spec]
      ,[usage_id]
      ,[use_org]
      ,[use_count]
      ,[durg_use_sp]
      ,[drug_use_units]
      ,[use_frequency]
      ,[use_date]
      ,[use_time]
      ,[stop_date_time]
      ,[start_date_time]
      ,[order_sub_no]
      ,[order_type]
      ,[icatrepeat_indorm]
      ,[new_orders]
      ,[yebz]
      ,config_name_jc [special_medicationtip]
      ,[size_specification]
      ,[pass_remark]
      ,[patient_id]
      ,[doctor_name]
      ,[patient_name]
      ,[batch]
      ,'' as [batch_name]
      ,[departmengt_name]
      ,[department_code]
      ,[zone]
      ,[visit_id]
      ,[group_num]
      ,[sum_num]
      ,[ml_speed]
      ,[create_date]
      ,[order_status]
      ,[is_twice_print]
      ,[checker]
      ,[deliveryer]
      ,[config_person]
      ,[config_date]
      ,[usage_name]
      ,[bed_number]
      ,[basket_number]
      ,[sorting_status]
      ,[sorting_model]
      ,[electroni_signature]
      ,[is_cpfh]
      ,[is_sf]
      ,[age]
      ,[is_db]
      ,[config_name]
      ,is_print_sn as [is_print_snv] 
      ,[barcode]
      ,[sex]
      ,[xsyxj]
      ,[is_cpfhr]
      ,[pyhfr]
from v_for_ydwl where use_date=@usedate and batch=@batch", useDateParam, batchParam);

            var dt = dataset.Tables[0];
            //dt.Columns.Remove(dt.Columns["id"]);

            dt.Columns.Add(new DataColumn("Id", typeof(Guid)));
            foreach (DataRow dataRow in dt.Rows)
            {
                dataRow["Id"] = Guid.NewGuid();
            }

            // tZHY:Pivas医嘱缓存表
            // tOrder:待贴签医嘱表

            // 删除原有数据
            DbHelperSQL.ExecuteSql("delete tZHY where use_date=@usedate and batch=@batch", useDateParam, batchParam);

            // 添加新数据
            DbHelperSQL.SqlBulkCopyByDataTable("tZHY", dataset.Tables[0]);

            // 对比医嘱数据，将需要增加的医嘱信息添加到tOrder表中
            DbHelperSQL.ExecuteSql("exec P_InsertIntotOrderSelecttZHY @usedate,@batch", useDateParam, batchParam);

            var bakDateParam = new SqlParameter("@usedate", OrderConfig.GetBakDate());

            // 更新医嘱信息
            DbHelperSQL.ExecuteSql("exec P_UpdatetOrderFromtZHY @usedate,@batch", bakDateParam, batchParam);

            // 备份历史数据
            DbHelperSQL.ExecuteSql("exec P_BakHistoryData @usedate", bakDateParam);
        }
        /// <summary>
        /// 提交贴签状态
        /// </summary>
        public void SubmitPrinter()
        {
            using (var db = new PrintTagDbEntities())
            {
                var orders = db.Database.SqlQuery<tOrder>("select * from torder where hasSubmit is null or hasSubmit <>1").ToList();

                SqlParameter id = null;
                SqlParameter barcode = null;
                SqlParameter printer = null;

                foreach (var order in orders)
                {
                    id = new SqlParameter("@id", order.Id);
                    barcode = new SqlParameter("@barcode", order.barcode);
                    printer = new SqlParameter("@printer", order.PrintUserId);

                    PivasDbHelperSQL.ExecuteSql("exec p_for_ydwl_update 1,@barcode,@printer", barcode, printer);
                }

                db.Database.ExecuteSqlCommand("update torder set hasSubmit=1 where (hasSubmit is null or hasSubmit <>1) and printing_status=1");

            }
        }
    }
}
