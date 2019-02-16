using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using PrinterManagerProject.DBUtility;//Please add references
using System.Collections.Generic;

namespace PrinterManagerProject.DAL
{
    /// <summary>
    /// 数据访问类:v_users
    /// </summary>
    public partial class v_users
    {
        public v_users()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from v_users");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 查询用户名是否存在
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool Exists(string username)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from v_users");
            strSql.Append(" where user_name=@user_name");
            SqlParameter[] parameters = {
                    new SqlParameter("@user_name", SqlDbType.NVarChar,50)
            };
            parameters[0].Value = username;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(PrinterManagerProject.Model.v_users model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into v_users(");
            strSql.Append("user_name,password,true_name,type_name,createtime)");
            strSql.Append(" values (");
            strSql.Append("@user_name,@password,@true_name,@type_name,@createtime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@user_name", SqlDbType.NVarChar,50),
                    new SqlParameter("@password", SqlDbType.NVarChar,50),
                    new SqlParameter("@true_name", SqlDbType.NVarChar,50),
                    new SqlParameter("@type_name", SqlDbType.NVarChar,50),
                    new SqlParameter("@createtime", SqlDbType.DateTime)};
            parameters[0].Value = model.user_name;
            parameters[1].Value = model.password;
            parameters[2].Value = model.true_name;
            parameters[3].Value = model.type_name;
            parameters[4].Value = model.createtime;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        public bool Update(PrinterManagerProject.Model.v_users model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update v_users set ");
            strSql.Append("user_name=@user_name,");
            strSql.Append("password=@password,");
            strSql.Append("true_name=@true_name,");
            strSql.Append("type_name=@type_name,");
            strSql.Append("createtime=@createtime");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@user_name", SqlDbType.NVarChar,50),
                    new SqlParameter("@password", SqlDbType.NVarChar,50),
                    new SqlParameter("@true_name", SqlDbType.NVarChar,50),
                    new SqlParameter("@type_name", SqlDbType.NVarChar,50),
                    new SqlParameter("@createtime", SqlDbType.DateTime),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.user_name;
            parameters[1].Value = model.password;
            parameters[2].Value = model.true_name;
            parameters[3].Value = model.type_name;
            parameters[4].Value = model.createtime;
            parameters[5].Value = model.ID;

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
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from v_users ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;

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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from v_users ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
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
        /// 得到一个对象实体
        /// </summary>
        public PrinterManagerProject.Model.v_users GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,user_name,password,true_name,type_name,createtime from v_users ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;

            PrinterManagerProject.Model.v_users model = new PrinterManagerProject.Model.v_users();
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
        /// 得到用户数据库List<Model.v_users>
        /// </summary>
        /// <returns></returns>
        public List<Model.v_users> get_v_users()
        {
            List<Model.v_users> list = new List<Model.v_users>();
            DataSet ds = GetList("");
            if(ds!=null &&ds.Tables.Count>0 &&ds.Tables[0].Rows.Count>0)
            {
                for(int i=0;i<ds.Tables[0].Rows.Count;i++)
                {
                    list.Add(DataRowToModel(ds.Tables[0].Rows[i]));
                }
            }
            return list;
        }
        

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public PrinterManagerProject.Model.v_users DataRowToModel(DataRow row)
        {
            PrinterManagerProject.Model.v_users model = new PrinterManagerProject.Model.v_users();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["user_name"] != null)
                {
                    model.user_name = row["user_name"].ToString();
                }
                if (row["password"] != null)
                {
                    model.password = row["password"].ToString();
                }
                if (row["true_name"] != null)
                {
                    model.true_name = row["true_name"].ToString();
                }
                if (row["type_name"] != null)
                {
                    model.type_name = row["type_name"].ToString();
                }
                if (row["createtime"] != null && row["createtime"].ToString() != "")
                {
                    model.createtime = DateTime.Parse(row["createtime"].ToString());
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
            strSql.Append("select ID,user_name,password,true_name,type_name,createtime ");
            strSql.Append(" FROM v_users ");
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
            strSql.Append(" ID,user_name,password,true_name,type_name,createtime ");
            strSql.Append(" FROM v_users ");
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
            strSql.Append("select count(1) FROM v_users ");
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
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from v_users T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }
        
        public bool UserLogin(string czrname, string czrpwd, string shrname, string shrpwd)
        {
            //判断操作用户(如果存在返回ID，否则返回0)
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ID FROM v_users");
            strSql.Append(" Where user_name='" + czrname + "' and [password]='" + czrpwd + "' and type_name='操作员' ");
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            int czrIsAt = 0;
            if (obj == null)
            {
                czrIsAt = 0;
            }
            else
            {
                czrIsAt=Convert.ToInt32(obj);
            }

            //判断审核用户(如果存在返回ID，否则返回0)
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("SELECT ID FROM v_users");
            strSql1.Append(" Where user_name='" + shrname + "' and [password]='" + shrpwd + "' and type_name='审核员' ");
            object obj1 = DbHelperSQL.GetSingle(strSql1.ToString());
            int shrIsAt = 0;
            if (obj1 == null)
            {
                shrIsAt = 0;
            }
            else
            {
                shrIsAt = Convert.ToInt32(obj1);
            }

            if(czrIsAt!=0 && shrIsAt!=0)
            {
                return true;
            }
            else
            {
                return false;
            }
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
			parameters[0].Value = "v_users";
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

