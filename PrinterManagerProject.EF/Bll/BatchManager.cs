using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrinterManagerProject.DBUtility;

namespace PrinterManagerProject.EF
{
    public class BatchManager:BaseDALL<tBatch>
    {
        /// <summary>
        /// 同步批次
        /// </summary>
        public void SyncBatch()
        {
            var ds = PivasDbHelperSQL.Query("select * from v_for_ydwl_batch");
            var dt = ds.Tables[0];

            var batchList = DBContext.tBatches.ToList();
            foreach (DataRow zhyBatch in dt.Rows)
            {
                var batch = batchList.FirstOrDefault(s => s.batch == zhyBatch["batch"].ToString());
                if (batch == null)
                {
                    batch = new tBatch();
                    batch.batch = zhyBatch["batch"].ToString();
                    batch.batch_name = zhyBatch["batch_name"].ToString();
                    batch.end_time = zhyBatch["end_time"].ToString();
                    batch.start_time = zhyBatch["start_time"].ToString();
                    DBContext.tBatches.Add(batch);
                }
                else
                {
                    batch.batch = zhyBatch["batch"].ToString();
                    batch.batch_name = zhyBatch["batch_name"].ToString();
                    batch.end_time = zhyBatch["end_time"].ToString();
                    batch.start_time = zhyBatch["start_time"].ToString();
                }
            }
            DBContext.SaveChanges();
        }


    }
}
