using System;
using System.Collections.Generic;
using System.Text;
using PrinterManagerProject.Model;
using System.Data;
using System.Data.SqlClient;
using PrinterManagerProject.DBUtility;

namespace PrinterManagerProject.DAL
{
    public class AotuDataListDal
    {
        /// <summary>
        /// 自动贴签数据管理类
        /// </summary>
        public AotuDataListDal()
        { }
        /// <summary>
        /// 从本地数据库获取贴签数据
        /// </summary>
        /// <param name="usedate"></param>
        /// <param name="batch"></param>
        /// <returns></returns>
        public List<PrinterManagerProject.Model.ListAllModel> getlist(string usedate,string batch)
        {
            List<PrinterManagerProject.Model.ListAllModel> li = new List<ListAllModel>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select row_number() over(order by a.printing_status) as row_number,a.*,b.drug_name as ydrug_name,b.drug_spec as ydrug_spec,b.drug_class_name as ydrug_class_name  from(select *,"
                + " (select top 1 b.Id from tZHY_for_View b where b.drug_weight = 5 and b.use_time = a.use_time and b.group_num = a.group_num and b.use_date = a.use_date) ydrug_id "
                + " from tZHY_for_View a where a.drug_weight = 1) a "
                + " left join tZHY_for_View b on a.ydrug_id = b.Id ");
            if (usedate=="" || usedate == "0001-01-01")
            {
                if(batch!="" && batch!="0")
                {
                    strSql.Append(" where a.batch="+batch);
                }
            }
            else
            {
                if (batch != "" && batch != "0")
                {
                    strSql.Append(" where a.use_date='"+usedate+"' and a.batch='"+ batch + "'");
                }
                else
                {
                    strSql.Append(" where a.use_date='" + usedate + "'");
                }
            }
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if(ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
            {
                DataTable dt = ds.Tables[0];
                for(int i=0;i<dt.Rows.Count;i++)
                {
                    li.Add(DataRowToModel(dt.Rows[i]));
                }
            }
            return li;
        }
        /// <summary>
		/// 得到一个对象实体(ListAllModel)
		/// </summary>
		public ListAllModel DataRowToModel(DataRow row)
        {
            ListAllModel model = new ListAllModel();
            if (row != null)
            {
                if (row["row_number"] != null && row["row_number"].ToString() != "")
                {
                    model.row_number = long.Parse(row["row_number"].ToString());
                }
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["drug_id"] != null)
                {
                    model.drug_id = row["drug_id"].ToString();
                }
                if (row["drug_number"] != null)
                {
                    model.drug_number = row["drug_number"].ToString();
                }
                if (row["drug_name"] != null)
                {
                    model.drug_name = row["drug_name"].ToString();
                }
                if (row["drug_weight"] != null)
                {
                    model.drug_weight = row["drug_weight"].ToString();
                }
                if (row["drug_spmc"] != null)
                {
                    model.drug_spmc = row["drug_spmc"].ToString();
                }
                if (row["drug_class_name"] != null)
                {
                    model.drug_class_name = row["drug_class_name"].ToString();
                }
                if (row["drug_spec"] != null)
                {
                    model.drug_spec = row["drug_spec"].ToString();
                }
                if (row["usage_id"] != null)
                {
                    model.usage_id = row["usage_id"].ToString();
                }
                if (row["use_org"] != null)
                {
                    model.use_org = row["use_org"].ToString();
                }
                if (row["use_count"] != null)
                {
                    model.use_count = row["use_count"].ToString();
                }
                if (row["durg_use_sp"] != null)
                {
                    model.durg_use_sp = row["durg_use_sp"].ToString();
                }
                if (row["drug_use_units"] != null)
                {
                    model.drug_use_units = row["drug_use_units"].ToString();
                }
                if (row["use_frequency"] != null)
                {
                    model.use_frequency = row["use_frequency"].ToString();
                }
                if (row["use_date"] != null)
                {
                    model.use_date = row["use_date"].ToString();
                }
                if (row["use_time"] != null)
                {
                    model.use_time = row["use_time"].ToString();
                }
                if (row["stop_date_time"] != null)
                {
                    model.stop_date_time = row["stop_date_time"].ToString();
                }
                if (row["start_date_time"] != null)
                {
                    model.start_date_time = row["start_date_time"].ToString();
                }
                if (row["order_sub_no"] != null)
                {
                    model.order_sub_no = row["order_sub_no"].ToString();
                }
                if (row["order_type"] != null)
                {
                    model.order_type = row["order_type"].ToString();
                }
                if (row["icatrepeat_indorm"] != null)
                {
                    model.icatrepeat_indorm = row["icatrepeat_indorm"].ToString();
                }
                if (row["new_orders"] != null && row["new_orders"].ToString() != "")
                {
                    model.new_orders = int.Parse(row["new_orders"].ToString());
                }
                if (row["yebz"] != null && row["yebz"].ToString() != "")
                {
                    model.yebz = int.Parse(row["yebz"].ToString());
                }
                if (row["special_medicationtip"] != null)
                {
                    model.special_medicationtip = row["special_medicationtip"].ToString();
                }
                if (row["size_specification"] != null && row["size_specification"].ToString() != "")
                {
                    model.size_specification = int.Parse(row["size_specification"].ToString());
                }
                if (row["pass_remark"] != null)
                {
                    model.pass_remark = row["pass_remark"].ToString();
                }
                if (row["patient_id"] != null)
                {
                    model.patient_id = row["patient_id"].ToString();
                }
                if (row["doctor_name"] != null)
                {
                    model.doctor_name = row["doctor_name"].ToString();
                }
                if (row["patient_name"] != null)
                {
                    model.patient_name = row["patient_name"].ToString();
                }
                if (row["batch"] != null)
                {
                    model.batch = row["batch"].ToString();
                }
                if (row["departmengt_name"] != null)
                {
                    model.departmengt_name = row["departmengt_name"].ToString();
                }
                if (row["department_code"] != null)
                {
                    model.department_code = row["department_code"].ToString();
                }
                if (row["zone"] != null && row["zone"].ToString() != "")
                {
                    model.zone = int.Parse(row["zone"].ToString());
                }
                if (row["visit_id"] != null)
                {
                    model.visit_id = row["visit_id"].ToString();
                }
                if (row["group_num"] != null)
                {
                    model.group_num = row["group_num"].ToString();
                }
                if (row["sum_num"] != null)
                {
                    model.sum_num = row["sum_num"].ToString();
                }
                if (row["ml_speed"] != null && row["ml_speed"].ToString() != "")
                {
                    model.ml_speed = int.Parse(row["ml_speed"].ToString());
                }
                if (row["create_date"] != null)
                {
                    model.create_date = row["create_date"].ToString();
                }
                if (row["order_status"] != null)
                {
                    model.order_status = row["order_status"].ToString();
                }
                if (row["is_twice_print"] != null && row["is_twice_print"].ToString() != "")
                {
                    model.is_twice_print = int.Parse(row["is_twice_print"].ToString());
                }
                if (row["checker"] != null)
                {
                    model.checker = row["checker"].ToString();
                }
                if (row["deliveryer"] != null)
                {
                    model.deliveryer = row["deliveryer"].ToString();
                }
                if (row["config_person"] != null)
                {
                    model.config_person = row["config_person"].ToString();
                }
                if (row["config_date"] != null)
                {
                    model.config_date = row["config_date"].ToString();
                }
                if (row["usage_name"] != null)
                {
                    model.usage_name = row["usage_name"].ToString();
                }
                if (row["bed_number"] != null)
                {
                    model.bed_number = row["bed_number"].ToString();
                }
                if (row["basket_number"] != null && row["basket_number"].ToString() != "")
                {
                    model.basket_number = int.Parse(row["basket_number"].ToString());
                }
                if (row["printing_status"] != null && row["printing_status"].ToString() != "")
                {
                    model.printing_status = int.Parse(row["printing_status"].ToString());
                }
                if (row["printing_model"] != null && row["printing_model"].ToString() != "")
                {
                    model.printing_model = int.Parse(row["printing_model"].ToString());
                }
                if (row["printing_time"] != null && row["printing_time"].ToString() != "")
                {
                    model.printing_time = DateTime.Parse(row["printing_time"].ToString());
                }
                if (row["QRcode"] != null)
                {
                    model.QRcode = row["QRcode"].ToString();
                }
                if (row["sbatches"] != null)
                {
                    model.sbatches = row["sbatches"].ToString();
                }
                if (row["electroni_signature"] != null && row["electroni_signature"].ToString() != "")
                {
                    model.electroni_signature = int.Parse(row["electroni_signature"].ToString());
                }
                if (row["is_cpfh"] != null)
                {
                    model.is_cpfh = row["is_cpfh"].ToString();
                }
                if (row["is_sf"] != null)
                {
                    model.is_sf = row["is_sf"].ToString();
                }
                if (row["age"] != null)
                {
                    model.age = row["age"].ToString();
                }
                if (row["is_db"] != null)
                {
                    model.is_db = row["is_db"].ToString();
                }
                if (row["config_name"] != null)
                {
                    model.config_name = row["config_name"].ToString();
                }
                if (row["ydrug_id"] != null && row["ydrug_id"].ToString() != "")
                {
                    model.ydrug_id = int.Parse(row["ydrug_id"].ToString());
                }
                if (row["ydrug_name"] != null)
                {
                    model.ydrug_name = row["ydrug_name"].ToString();
                }
                if (row["ydrug_spec"] != null)
                {
                    model.ydrug_spec = row["ydrug_spec"].ToString();
                }
                if (row["ydrug_class_name"] != null)
                {
                    model.ydrug_class_name = row["ydrug_class_name"].ToString();
                }
            }
            return model;
        }
        /// <summary>
        /// 返回单瓶药品列表（不包括溶媒）
        /// </summary>
        /// <param name="Sid">溶媒ID</param>
        /// <returns></returns>
        public List<PrinterManagerProject.Model.Print_ymodel> getPrint_y_no(int Sid)
        {
            List<PrinterManagerProject.Model.Print_ymodel> list = new List<Print_ymodel>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.id, a.drug_name,a.use_count from dbo.tZHY_for_View a,(select * from dbo.tZHY_for_View where [id]="+Sid+") b "
                + " where a.drug_weight <>1 and b.use_time = a.use_time and b.group_num = a.group_num and b.use_date = a.use_date ");               
            
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(DataRowToModel_Print_ymodel(dt.Rows[i]));
                }
            }
            
            return list;
        }
        /// <summary>
        /// 返回单瓶药品列表（包括溶媒）
        /// </summary>
        /// <param name="Sid">溶媒ID</param>
        /// <returns></returns>
        public List<PrinterManagerProject.Model.Print_ymodel> getPrint_y(int Sid)
        {
            List<PrinterManagerProject.Model.Print_ymodel> list = new List<Print_ymodel>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.id, a.drug_name,a.use_count from dbo.tZHY_for_View a,(select * from dbo.tZHY_for_View where [id]=" + Sid + ") b "
                + " where b.use_time = a.use_time and b.group_num = a.group_num and b.use_date = a.use_date ");

            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(DataRowToModel_Print_ymodel(dt.Rows[i]));
                }
            }
            return list;
        }
        /// <summary>
        /// 根据溶媒ID修改整瓶药品的打印状态(1)，打印时间(当前)，打印模式（默认为自动0），并写入二维码
        /// </summary>
        /// <param name="Sid">溶媒ID</param>
        /// <param name="strQRcode">二维码</param>
        /// <returns></returns>
        public bool update_status(int Sid,string strQRcode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tZHY_for_View set printing_model=0,printing_status=1,printing_time=GETDATE(),QRcode='"+ strQRcode + "' where [id] in "
                + " (select a.Id from dbo.tZHY_for_View a,(select * from dbo.tZHY_for_View where [id]="+ Sid + ") b "
                + " where b.use_time = a.use_time and b.group_num = a.group_num and b.use_date = a.use_date)");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
		/// 得到一个对象实体(ListAllModel)
		/// </summary>
		public Print_ymodel DataRowToModel_Print_ymodel(DataRow row)
        {
            Print_ymodel model = new Print_ymodel();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["drug_name"] != null && row["drug_name"].ToString() != "")
                {
                    model.drug_name = row["drug_name"].ToString();
                }
                if (row["use_count"] != null)
                {
                    model.use_count = row["use_count"].ToString();
                }                
            }
            return model;
        }
    }
}
