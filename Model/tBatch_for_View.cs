using System;
using System.Collections.Generic;
using System.Text;

namespace PrinterManagerProject.Model
{
    /// <summary>
    /// 批次实体类（获取）
    /// </summary>
    [Serializable]
    public partial class tBatch_for_View
    {
        public tBatch_for_View()
        { }
        #region Model
        private string _batch;
        private string _batch_name;
        private string _start_time;
        private string _end_time;
        /// <summary>
        /// 批次编号
        /// </summary>
        public string batch
        {
            set { _batch = value; }
            get { return _batch; }
        }
        /// <summary>
        /// 批次名称
        /// </summary>
        public string batch_name
        {
            set { _batch_name = value; }
            get { return _batch_name; }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string start_time
        {
            set { _start_time = value; }
            get { return _start_time; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string end_time
        {
            set { _end_time = value; }
            get { return _end_time; }
        }
        #endregion Model
    }
}
