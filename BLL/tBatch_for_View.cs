using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PrinterManagerProject.BLL
{
    /// <summary>
    /// tBatch_for_View
    /// </summary>
    public partial class tBatch_for_View
    {
        private readonly PrinterManagerProject.DAL.tBatch_for_View dal = new PrinterManagerProject.DAL.tBatch_for_View();
        public tBatch_for_View()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string batch, string batch_name, string start_time, string end_time)
        {
            return dal.Exists(batch, batch_name, start_time, end_time);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(PrinterManagerProject.Model.tBatch_for_View model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(PrinterManagerProject.Model.tBatch_for_View model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string batch, string batch_name, string start_time, string end_time)
        {

            return dal.Delete(batch, batch_name, start_time, end_time);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public PrinterManagerProject.Model.tBatch_for_View GetModel(string batch, string batch_name, string start_time, string end_time)
        {

            return dal.GetModel(batch, batch_name, start_time, end_time);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public PrinterManagerProject.Model.tBatch_for_View GetModelByCache(string batch, string batch_name, string start_time, string end_time)
        {

            string CacheKey = "tBatch_for_ViewModel-" + batch + batch_name + start_time + end_time;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(batch, batch_name, start_time, end_time);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (PrinterManagerProject.Model.tBatch_for_View)objModel;
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
        public List<PrinterManagerProject.Model.tBatch_for_View> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<PrinterManagerProject.Model.tBatch_for_View> DataTableToList(DataTable dt)
        {
            List<PrinterManagerProject.Model.tBatch_for_View> modelList = new List<PrinterManagerProject.Model.tBatch_for_View>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                PrinterManagerProject.Model.tBatch_for_View model;
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
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.tBatch_for_View> GetList()
        {
            List<Model.tBatch_for_View> list = new List<Model.tBatch_for_View>();
            DataSet ds = dal.GetList("");
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                list.Add(dal.DataRowToModel(item));
            }
            return list;
        }

        #endregion  ExtensionMethod
    }
}
