using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using PrinterManagerProject.DBUtility;

namespace PrinterManagerProject.DAL
{
    /// <summary>
	/// 数据访问类:tBatch_for_View
	/// </summary>
	public partial class tBatch_for_View
    {
        public tBatch_for_View()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string batch, string batch_name, string start_time, string end_time)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tBatch_for_View");
            strSql.Append(" where batch=@batch and batch_name=@batch_name and start_time=@start_time and end_time=@end_time ");
            SqlParameter[] parameters = {
                    new SqlParameter("@batch", SqlDbType.VarChar,10),
                    new SqlParameter("@batch_name", SqlDbType.VarChar,10),
                    new SqlParameter("@start_time", SqlDbType.VarChar,10),
                    new SqlParameter("@end_time", SqlDbType.VarChar,10)         };
            parameters[0].Value = batch;
            parameters[1].Value = batch_name;
            parameters[2].Value = start_time;
            parameters[3].Value = end_time;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(PrinterManagerProject.Model.tBatch_for_View model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tBatch_for_View(");
            strSql.Append("batch,batch_name,start_time,end_time)");
            strSql.Append(" values (");
            strSql.Append("@batch,@batch_name,@start_time,@end_time)");
            SqlParameter[] parameters = {
                    new SqlParameter("@batch", SqlDbType.VarChar,10),
                    new SqlParameter("@batch_name", SqlDbType.VarChar,10),
                    new SqlParameter("@start_time", SqlDbType.VarChar,10),
                    new SqlParameter("@end_time", SqlDbType.VarChar,10)};
            parameters[0].Value = model.batch;
            parameters[1].Value = model.batch_name;
            parameters[2].Value = model.start_time;
            parameters[3].Value = model.end_time;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        /// 更新一条数据
        /// </summary>
        public bool Update(PrinterManagerProject.Model.tBatch_for_View model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tBatch_for_View set ");
            strSql.Append("batch=@batch,");
            strSql.Append("batch_name=@batch_name,");
            strSql.Append("start_time=@start_time,");
            strSql.Append("end_time=@end_time");
            strSql.Append(" where batch=@batch and batch_name=@batch_name and start_time=@start_time and end_time=@end_time ");
            SqlParameter[] parameters = {
                    new SqlParameter("@batch", SqlDbType.VarChar,10),
                    new SqlParameter("@batch_name", SqlDbType.VarChar,10),
                    new SqlParameter("@start_time", SqlDbType.VarChar,10),
                    new SqlParameter("@end_time", SqlDbType.VarChar,10)};
            parameters[0].Value = model.batch;
            parameters[1].Value = model.batch_name;
            parameters[2].Value = model.start_time;
            parameters[3].Value = model.end_time;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        public bool Delete(string batch, string batch_name, string start_time, string end_time)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tBatch_for_View ");
            strSql.Append(" where batch=@batch and batch_name=@batch_name and start_time=@start_time and end_time=@end_time ");
            SqlParameter[] parameters = {
                    new SqlParameter("@batch", SqlDbType.VarChar,10),
                    new SqlParameter("@batch_name", SqlDbType.VarChar,10),
                    new SqlParameter("@start_time", SqlDbType.VarChar,10),
                    new SqlParameter("@end_time", SqlDbType.VarChar,10)         };
            parameters[0].Value = batch;
            parameters[1].Value = batch_name;
            parameters[2].Value = start_time;
            parameters[3].Value = end_time;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        public PrinterManagerProject.Model.tBatch_for_View GetModel(string batch, string batch_name, string start_time, string end_time)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 batch,batch_name,start_time,end_time from tBatch_for_View ");
            strSql.Append(" where batch=@batch and batch_name=@batch_name and start_time=@start_time and end_time=@end_time ");
            SqlParameter[] parameters = {
                    new SqlParameter("@batch", SqlDbType.VarChar,10),
                    new SqlParameter("@batch_name", SqlDbType.VarChar,10),
                    new SqlParameter("@start_time", SqlDbType.VarChar,10),
                    new SqlParameter("@end_time", SqlDbType.VarChar,10)         };
            parameters[0].Value = batch;
            parameters[1].Value = batch_name;
            parameters[2].Value = start_time;
            parameters[3].Value = end_time;

            PrinterManagerProject.Model.tBatch_for_View model = new PrinterManagerProject.Model.tBatch_for_View();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
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
        public PrinterManagerProject.Model.tBatch_for_View DataRowToModel(DataRow row)
        {
            PrinterManagerProject.Model.tBatch_for_View model = new PrinterManagerProject.Model.tBatch_for_View();
            if (row != null)
            {
                if (row["batch"] != null)
                {
                    model.batch = row["batch"].ToString();
                }
                if (row["batch_name"] != null)
                {
                    model.batch_name = row["batch_name"].ToString();
                }
                if (row["start_time"] != null)
                {
                    model.start_time = row["start_time"].ToString();
                }
                if (row["end_time"] != null)
                {
                    model.end_time = row["end_time"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select batch,batch_name,start_time,end_time ");
            strSql.Append(" FROM tBatch_for_View ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" batch,batch_name,start_time,end_time ");
            strSql.Append(" FROM tBatch_for_View ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM tBatch_for_View ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.end_time desc");
            }
            strSql.Append(")AS Row, T.*  from tBatch_for_View T ");
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
			parameters[0].Value = "tBatch_for_View";
			parameters[1].Value = "end_time";
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
