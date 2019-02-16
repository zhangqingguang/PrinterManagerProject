using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using PrinterManagerProject.Model;
namespace PrinterManagerProject.BLL
{
    /// <summary>
    /// v_users
    /// </summary>
    public partial class v_users
    {
        private readonly PrinterManagerProject.DAL.v_users dal = new PrinterManagerProject.DAL.v_users();
        public v_users()
        { }
        #region  BasicMethod



        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }
        public bool Exists(string username)
        {
            return dal.Exists(username);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(PrinterManagerProject.Model.v_users model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(PrinterManagerProject.Model.v_users model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            return dal.Delete(ID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            return dal.DeleteList(Maticsoft.Common.PageValidate.SafeLongFilter(IDlist, 0));
        }
        public List<Model.v_users> get_v_users()
        {
            return dal.get_v_users();
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public PrinterManagerProject.Model.v_users GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public PrinterManagerProject.Model.v_users GetModelByCache(int ID)
        {

            string CacheKey = "v_usersModel-" + ID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ID);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (PrinterManagerProject.Model.v_users)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<PrinterManagerProject.Model.v_users> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<PrinterManagerProject.Model.v_users> DataTableToList(DataTable dt)
        {
            List<PrinterManagerProject.Model.v_users> modelList = new List<PrinterManagerProject.Model.v_users>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                PrinterManagerProject.Model.v_users model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="czrname">操作员</param>
        /// <param name="czrpwd">操作员密码</param>
        /// <param name="shrname">审核员</param>
        /// <param name="shrpwd">审核员密码</param>
        /// <returns>true(false)</returns>
        public bool UserLogin(string czrname, string czrpwd, string shrname, string shrpwd)
        {
            return dal.UserLogin(czrname, czrpwd, shrname, shrpwd);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

