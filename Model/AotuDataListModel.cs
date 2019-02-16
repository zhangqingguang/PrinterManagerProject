using System;
using System.Collections.Generic;
using System.Text;

namespace PrinterManagerProject.Model
{
    /// <summary>
    /// 自动贴贴数据model
    /// </summary>
    public class AotuDataListModel
    {
        public AotuDataListModel()
        { }
        #region Model
        private int _row_number;
        private string _patient_id;
        private string _bed_number;
        private string _patient_name;
        private string _ydrug_name;
        private string _ydrug_spec;
        private string _sdrug_name;
        private string _sdrug_spec;
        private int _printing_status;
        private string _sbatches;
        private int _sid;
        /// <summary>
        /// 序号
        /// </summary>
        public int row_number
        {
            set { _row_number = value; }
            get { return _row_number; }
        }
        /// <summary>
        /// 病人编号
        /// </summary>
        public string patient_id
        {
            set { _patient_id = value; }
            get { return _patient_id; }
        }
        /// <summary>
        /// 床号
        /// </summary>
        public string bed_number
        {
            set { _bed_number = value; }
            get { return _bed_number; }
        }
        /// <summary>
        /// 病人姓名
        /// </summary>
        public string patient_name
        {
            set { _patient_name = value; }
            get { return _patient_name; }
        }
        /// <summary>
        /// 主药名称
        /// </summary>
        public string Ydrug_name
        {
            set { _ydrug_name = value; }
            get { return _ydrug_name; }
        }
        /// <summary>
        /// 主药规格
        /// </summary>
        public string Ydrug_spec
        {
            set { _ydrug_spec = value; }
            get { return _ydrug_spec; }
        }
        /// <summary>
        /// 溶媒名称
        /// </summary>
        public string Sdrug_name
        {
            set { _sdrug_name = value; }
            get { return _sdrug_name; }
        }
        /// <summary>
        /// 溶媒规格
        /// </summary>
        public string Sdrug_spec
        {
            set { _sdrug_spec = value; }
            get { return _sdrug_spec; }
        }
        /// <summary>
        /// 打印状态（默认为0待打印）
        /// </summary>
        public int printing_status
        {
            set { _printing_status = value; }
            get { return _printing_status; }
        }
        /// <summary>
        /// 溶媒批号
        /// </summary>
        public string sbatches
        {
            set { _sbatches = value; }
            get { return _sbatches; }
        }
        /// <summary>
        /// 溶媒在数据中的ID（通过此编号查找与之匹配的药品）
        /// </summary>
        public int Sid
        {
            set { _sid = value; }
            get { return _sid; }
        }       
        #endregion Model

    }
}
