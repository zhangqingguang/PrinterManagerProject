using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using PrinterManagerProject.DBUtility;//Please add references
namespace PrinterManagerProject.DAL
{
	/// <summary>
	/// 数据访问类:v_order_info
	/// </summary>
	public partial class v_order_info
	{
		public v_order_info()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "v_order_info"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from v_order_info");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(PrinterManagerProject.Model.v_order_info model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into v_order_info(");
			strSql.Append("drug_id,drug_number,drug_weight,drug_name,drug_spec,usage_id,use_org,use_count,use_unit,use_frequency,start_date,stop_date,use_time,order_number,order_remark,order_type,pass_remark,doctor_name,patientid,patient_name,patient_gender,batch,department_number,department_name,visitid,group_num,sn_num,ml_speed,create_date,order_status,is_twice_print,checker,deliveryer,rechecker,config_person,config_date,usage_name,bed_number)");
			strSql.Append(" values (");
			strSql.Append("@drug_id,@drug_number,@drug_weight,@drug_name,@drug_spec,@usage_id,@use_org,@use_count,@use_unit,@use_frequency,@start_date,@stop_date,@use_time,@order_number,@order_remark,@order_type,@pass_remark,@doctor_name,@patientid,@patient_name,@patient_gender,@batch,@department_number,@department_name,@visitid,@group_num,@sn_num,@ml_speed,@create_date,@order_status,@is_twice_print,@checker,@deliveryer,@rechecker,@config_person,@config_date,@usage_name,@bed_number)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@drug_id", SqlDbType.BigInt,8),
					new SqlParameter("@drug_number", SqlDbType.VarChar,20),
					new SqlParameter("@drug_weight", SqlDbType.Int,4),
					new SqlParameter("@drug_name", SqlDbType.NVarChar,100),
					new SqlParameter("@drug_spec", SqlDbType.NVarChar,40),
					new SqlParameter("@usage_id", SqlDbType.VarChar,10),
					new SqlParameter("@use_org", SqlDbType.NVarChar,20),
					new SqlParameter("@use_count", SqlDbType.Int,4),
					new SqlParameter("@use_unit", SqlDbType.NVarChar,10),
					new SqlParameter("@use_frequency", SqlDbType.NVarChar,30),
					new SqlParameter("@start_date", SqlDbType.VarChar,10),
					new SqlParameter("@stop_date", SqlDbType.VarChar,10),
					new SqlParameter("@use_time", SqlDbType.VarChar,16),
					new SqlParameter("@order_number", SqlDbType.VarChar,20),
					new SqlParameter("@order_remark", SqlDbType.NVarChar,200),
					new SqlParameter("@order_type", SqlDbType.Int,4),
					new SqlParameter("@pass_remark", SqlDbType.NVarChar,200),
					new SqlParameter("@doctor_name", SqlDbType.NVarChar,20),
					new SqlParameter("@patientid", SqlDbType.VarChar,50),
					new SqlParameter("@patient_name", SqlDbType.VarChar,50),
					new SqlParameter("@patient_gender", SqlDbType.VarChar,2),
					new SqlParameter("@batch", SqlDbType.VarChar,10),
					new SqlParameter("@department_number", SqlDbType.VarChar,20),
					new SqlParameter("@department_name", SqlDbType.NVarChar,50),
					new SqlParameter("@visitid", SqlDbType.Int,4),
					new SqlParameter("@group_num", SqlDbType.Int,4),
					new SqlParameter("@sn_num", SqlDbType.Int,4),
					new SqlParameter("@ml_speed", SqlDbType.Int,4),
					new SqlParameter("@create_date", SqlDbType.DateTime),
					new SqlParameter("@order_status", SqlDbType.VarChar,2),
					new SqlParameter("@is_twice_print", SqlDbType.VarChar,2),
					new SqlParameter("@checker", SqlDbType.NVarChar,20),
					new SqlParameter("@deliveryer", SqlDbType.NVarChar,20),
					new SqlParameter("@rechecker", SqlDbType.NVarChar,20),
					new SqlParameter("@config_person", SqlDbType.NVarChar,20),
					new SqlParameter("@config_date", SqlDbType.DateTime),
					new SqlParameter("@usage_name", SqlDbType.NVarChar,50),
					new SqlParameter("@bed_number", SqlDbType.VarChar,20)};
			parameters[0].Value = model.drug_id;
			parameters[1].Value = model.drug_number;
			parameters[2].Value = model.drug_weight;
			parameters[3].Value = model.drug_name;
			parameters[4].Value = model.drug_spec;
			parameters[5].Value = model.usage_id;
			parameters[6].Value = model.use_org;
			parameters[7].Value = model.use_count;
			parameters[8].Value = model.use_unit;
			parameters[9].Value = model.use_frequency;
			parameters[10].Value = model.start_date;
			parameters[11].Value = model.stop_date;
			parameters[12].Value = model.use_time;
			parameters[13].Value = model.order_number;
			parameters[14].Value = model.order_remark;
			parameters[15].Value = model.order_type;
			parameters[16].Value = model.pass_remark;
			parameters[17].Value = model.doctor_name;
			parameters[18].Value = model.patientid;
			parameters[19].Value = model.patient_name;
			parameters[20].Value = model.patient_gender;
			parameters[21].Value = model.batch;
			parameters[22].Value = model.department_number;
			parameters[23].Value = model.department_name;
			parameters[24].Value = model.visitid;
			parameters[25].Value = model.group_num;
			parameters[26].Value = model.sn_num;
			parameters[27].Value = model.ml_speed;
			parameters[28].Value = model.create_date;
			parameters[29].Value = model.order_status;
			parameters[30].Value = model.is_twice_print;
			parameters[31].Value = model.checker;
			parameters[32].Value = model.deliveryer;
			parameters[33].Value = model.rechecker;
			parameters[34].Value = model.config_person;
			parameters[35].Value = model.config_date;
			parameters[36].Value = model.usage_name;
			parameters[37].Value = model.bed_number;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(PrinterManagerProject.Model.v_order_info model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update v_order_info set ");
			strSql.Append("drug_id=@drug_id,");
			strSql.Append("drug_number=@drug_number,");
			strSql.Append("drug_weight=@drug_weight,");
			strSql.Append("drug_name=@drug_name,");
			strSql.Append("drug_spec=@drug_spec,");
			strSql.Append("usage_id=@usage_id,");
			strSql.Append("use_org=@use_org,");
			strSql.Append("use_count=@use_count,");
			strSql.Append("use_unit=@use_unit,");
			strSql.Append("use_frequency=@use_frequency,");
			strSql.Append("start_date=@start_date,");
			strSql.Append("stop_date=@stop_date,");
			strSql.Append("use_time=@use_time,");
			strSql.Append("order_number=@order_number,");
			strSql.Append("order_remark=@order_remark,");
			strSql.Append("order_type=@order_type,");
			strSql.Append("pass_remark=@pass_remark,");
			strSql.Append("doctor_name=@doctor_name,");
			strSql.Append("patientid=@patientid,");
			strSql.Append("patient_name=@patient_name,");
			strSql.Append("patient_gender=@patient_gender,");
			strSql.Append("batch=@batch,");
			strSql.Append("department_number=@department_number,");
			strSql.Append("department_name=@department_name,");
			strSql.Append("visitid=@visitid,");
			strSql.Append("group_num=@group_num,");
			strSql.Append("sn_num=@sn_num,");
			strSql.Append("ml_speed=@ml_speed,");
			strSql.Append("create_date=@create_date,");
			strSql.Append("order_status=@order_status,");
			strSql.Append("is_twice_print=@is_twice_print,");
			strSql.Append("checker=@checker,");
			strSql.Append("deliveryer=@deliveryer,");
			strSql.Append("rechecker=@rechecker,");
			strSql.Append("config_person=@config_person,");
			strSql.Append("config_date=@config_date,");
			strSql.Append("usage_name=@usage_name,");
			strSql.Append("bed_number=@bed_number");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@drug_id", SqlDbType.BigInt,8),
					new SqlParameter("@drug_number", SqlDbType.VarChar,20),
					new SqlParameter("@drug_weight", SqlDbType.Int,4),
					new SqlParameter("@drug_name", SqlDbType.NVarChar,100),
					new SqlParameter("@drug_spec", SqlDbType.NVarChar,40),
					new SqlParameter("@usage_id", SqlDbType.VarChar,10),
					new SqlParameter("@use_org", SqlDbType.NVarChar,20),
					new SqlParameter("@use_count", SqlDbType.Int,4),
					new SqlParameter("@use_unit", SqlDbType.NVarChar,10),
					new SqlParameter("@use_frequency", SqlDbType.NVarChar,30),
					new SqlParameter("@start_date", SqlDbType.VarChar,10),
					new SqlParameter("@stop_date", SqlDbType.VarChar,10),
					new SqlParameter("@use_time", SqlDbType.VarChar,16),
					new SqlParameter("@order_number", SqlDbType.VarChar,20),
					new SqlParameter("@order_remark", SqlDbType.NVarChar,200),
					new SqlParameter("@order_type", SqlDbType.Int,4),
					new SqlParameter("@pass_remark", SqlDbType.NVarChar,200),
					new SqlParameter("@doctor_name", SqlDbType.NVarChar,20),
					new SqlParameter("@patientid", SqlDbType.VarChar,50),
					new SqlParameter("@patient_name", SqlDbType.VarChar,50),
					new SqlParameter("@patient_gender", SqlDbType.VarChar,2),
					new SqlParameter("@batch", SqlDbType.VarChar,10),
					new SqlParameter("@department_number", SqlDbType.VarChar,20),
					new SqlParameter("@department_name", SqlDbType.NVarChar,50),
					new SqlParameter("@visitid", SqlDbType.Int,4),
					new SqlParameter("@group_num", SqlDbType.Int,4),
					new SqlParameter("@sn_num", SqlDbType.Int,4),
					new SqlParameter("@ml_speed", SqlDbType.Int,4),
					new SqlParameter("@create_date", SqlDbType.DateTime),
					new SqlParameter("@order_status", SqlDbType.VarChar,2),
					new SqlParameter("@is_twice_print", SqlDbType.VarChar,2),
					new SqlParameter("@checker", SqlDbType.NVarChar,20),
					new SqlParameter("@deliveryer", SqlDbType.NVarChar,20),
					new SqlParameter("@rechecker", SqlDbType.NVarChar,20),
					new SqlParameter("@config_person", SqlDbType.NVarChar,20),
					new SqlParameter("@config_date", SqlDbType.DateTime),
					new SqlParameter("@usage_name", SqlDbType.NVarChar,50),
					new SqlParameter("@bed_number", SqlDbType.VarChar,20),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.drug_id;
			parameters[1].Value = model.drug_number;
			parameters[2].Value = model.drug_weight;
			parameters[3].Value = model.drug_name;
			parameters[4].Value = model.drug_spec;
			parameters[5].Value = model.usage_id;
			parameters[6].Value = model.use_org;
			parameters[7].Value = model.use_count;
			parameters[8].Value = model.use_unit;
			parameters[9].Value = model.use_frequency;
			parameters[10].Value = model.start_date;
			parameters[11].Value = model.stop_date;
			parameters[12].Value = model.use_time;
			parameters[13].Value = model.order_number;
			parameters[14].Value = model.order_remark;
			parameters[15].Value = model.order_type;
			parameters[16].Value = model.pass_remark;
			parameters[17].Value = model.doctor_name;
			parameters[18].Value = model.patientid;
			parameters[19].Value = model.patient_name;
			parameters[20].Value = model.patient_gender;
			parameters[21].Value = model.batch;
			parameters[22].Value = model.department_number;
			parameters[23].Value = model.department_name;
			parameters[24].Value = model.visitid;
			parameters[25].Value = model.group_num;
			parameters[26].Value = model.sn_num;
			parameters[27].Value = model.ml_speed;
			parameters[28].Value = model.create_date;
			parameters[29].Value = model.order_status;
			parameters[30].Value = model.is_twice_print;
			parameters[31].Value = model.checker;
			parameters[32].Value = model.deliveryer;
			parameters[33].Value = model.rechecker;
			parameters[34].Value = model.config_person;
			parameters[35].Value = model.config_date;
			parameters[36].Value = model.usage_name;
			parameters[37].Value = model.bed_number;
			parameters[38].Value = model.ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from v_order_info ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from v_order_info ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		/// 得到一个对象实体
		/// </summary>
		public PrinterManagerProject.Model.v_order_info GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,drug_id,drug_number,drug_weight,drug_name,drug_spec,usage_id,use_org,use_count,use_unit,use_frequency,start_date,stop_date,use_time,order_number,order_remark,order_type,pass_remark,doctor_name,patientid,patient_name,patient_gender,batch,department_number,department_name,visitid,group_num,sn_num,ml_speed,create_date,order_status,is_twice_print,checker,deliveryer,rechecker,config_person,config_date,usage_name,bed_number from v_order_info ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			PrinterManagerProject.Model.v_order_info model=new PrinterManagerProject.Model.v_order_info();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public PrinterManagerProject.Model.v_order_info DataRowToModel(DataRow row)
		{
			PrinterManagerProject.Model.v_order_info model=new PrinterManagerProject.Model.v_order_info();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["drug_id"]!=null && row["drug_id"].ToString()!="")
				{
					model.drug_id=long.Parse(row["drug_id"].ToString());
				}
				if(row["drug_number"]!=null)
				{
					model.drug_number=row["drug_number"].ToString();
				}
				if(row["drug_weight"]!=null && row["drug_weight"].ToString()!="")
				{
					model.drug_weight=int.Parse(row["drug_weight"].ToString());
				}
				if(row["drug_name"]!=null)
				{
					model.drug_name=row["drug_name"].ToString();
				}
				if(row["drug_spec"]!=null)
				{
					model.drug_spec=row["drug_spec"].ToString();
				}
				if(row["usage_id"]!=null)
				{
					model.usage_id=row["usage_id"].ToString();
				}
				if(row["use_org"]!=null)
				{
					model.use_org=row["use_org"].ToString();
				}
				if(row["use_count"]!=null && row["use_count"].ToString()!="")
				{
					model.use_count=int.Parse(row["use_count"].ToString());
				}
				if(row["use_unit"]!=null)
				{
					model.use_unit=row["use_unit"].ToString();
				}
				if(row["use_frequency"]!=null)
				{
					model.use_frequency=row["use_frequency"].ToString();
				}
				if(row["start_date"]!=null)
				{
					model.start_date=row["start_date"].ToString();
				}
				if(row["stop_date"]!=null)
				{
					model.stop_date=row["stop_date"].ToString();
				}
				if(row["use_time"]!=null)
				{
					model.use_time=row["use_time"].ToString();
				}
				if(row["order_number"]!=null)
				{
					model.order_number=row["order_number"].ToString();
				}
				if(row["order_remark"]!=null)
				{
					model.order_remark=row["order_remark"].ToString();
				}
				if(row["order_type"]!=null && row["order_type"].ToString()!="")
				{
					model.order_type=int.Parse(row["order_type"].ToString());
				}
				if(row["pass_remark"]!=null)
				{
					model.pass_remark=row["pass_remark"].ToString();
				}
				if(row["doctor_name"]!=null)
				{
					model.doctor_name=row["doctor_name"].ToString();
				}
				if(row["patientid"]!=null)
				{
					model.patientid=row["patientid"].ToString();
				}
				if(row["patient_name"]!=null)
				{
					model.patient_name=row["patient_name"].ToString();
				}
				if(row["patient_gender"]!=null)
				{
					model.patient_gender=row["patient_gender"].ToString();
				}
				if(row["batch"]!=null)
				{
					model.batch=row["batch"].ToString();
				}
				if(row["department_number"]!=null)
				{
					model.department_number=row["department_number"].ToString();
				}
				if(row["department_name"]!=null)
				{
					model.department_name=row["department_name"].ToString();
				}
				if(row["visitid"]!=null && row["visitid"].ToString()!="")
				{
					model.visitid=int.Parse(row["visitid"].ToString());
				}
				if(row["group_num"]!=null && row["group_num"].ToString()!="")
				{
					model.group_num=int.Parse(row["group_num"].ToString());
				}
				if(row["sn_num"]!=null && row["sn_num"].ToString()!="")
				{
					model.sn_num=int.Parse(row["sn_num"].ToString());
				}
				if(row["ml_speed"]!=null && row["ml_speed"].ToString()!="")
				{
					model.ml_speed=int.Parse(row["ml_speed"].ToString());
				}
				if(row["create_date"]!=null && row["create_date"].ToString()!="")
				{
					model.create_date=DateTime.Parse(row["create_date"].ToString());
				}
				if(row["order_status"]!=null)
				{
					model.order_status=row["order_status"].ToString();
				}
				if(row["is_twice_print"]!=null)
				{
					model.is_twice_print=row["is_twice_print"].ToString();
				}
				if(row["checker"]!=null)
				{
					model.checker=row["checker"].ToString();
				}
				if(row["deliveryer"]!=null)
				{
					model.deliveryer=row["deliveryer"].ToString();
				}
				if(row["rechecker"]!=null)
				{
					model.rechecker=row["rechecker"].ToString();
				}
				if(row["config_person"]!=null)
				{
					model.config_person=row["config_person"].ToString();
				}
				if(row["config_date"]!=null && row["config_date"].ToString()!="")
				{
					model.config_date=DateTime.Parse(row["config_date"].ToString());
				}
				if(row["usage_name"]!=null)
				{
					model.usage_name=row["usage_name"].ToString();
				}
				if(row["bed_number"]!=null)
				{
					model.bed_number=row["bed_number"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,drug_id,drug_number,drug_weight,drug_name,drug_spec,usage_id,use_org,use_count,use_unit,use_frequency,start_date,stop_date,use_time,order_number,order_remark,order_type,pass_remark,doctor_name,patientid,patient_name,patient_gender,batch,department_number,department_name,visitid,group_num,sn_num,ml_speed,create_date,order_status,is_twice_print,checker,deliveryer,rechecker,config_person,config_date,usage_name,bed_number ");
			strSql.Append(" FROM v_order_info ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" ID,drug_id,drug_number,drug_weight,drug_name,drug_spec,usage_id,use_org,use_count,use_unit,use_frequency,start_date,stop_date,use_time,order_number,order_remark,order_type,pass_remark,doctor_name,patientid,patient_name,patient_gender,batch,department_number,department_name,visitid,group_num,sn_num,ml_speed,create_date,order_status,is_twice_print,checker,deliveryer,rechecker,config_person,config_date,usage_name,bed_number ");
			strSql.Append(" FROM v_order_info ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM v_order_info ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from v_order_info T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "v_order_info";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

