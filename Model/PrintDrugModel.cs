using System;
using System.Collections.Generic;
using System.Text;

namespace PrinterManagerProject.Model
{
    public partial class PrintDrugModel
    {
        /// <summary>
        /// 
        /// </summary>
        public PrintDrugModel()
        { }
        #region Model
        private int _drugid;
        private string _drug_name;
        private string _use_count;
        /// <summary>
        /// 药品id
        /// </summary>
        public int id
        {
            set { _drugid = value; }
            get { return _drugid; }
        }
        /// <summary>
        /// 药品名称
        /// </summary>
        public string drug_name
        {
            set { _drug_name = value; }
            get { return _drug_name; }
        }
        /// <summary>
        /// 药品用量
        /// </summary>
        public string use_count
        {
            set { _use_count = value; }
            get { return _use_count; }
        }
        #endregion
    }
}
