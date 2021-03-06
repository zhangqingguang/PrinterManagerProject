﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterManagerProject.EF.Models
{
    public partial class PrintDrugModel
    {
        /// <summary>
        /// 
        /// </summary>
        public PrintDrugModel()
        { }
        #region Model
        private string _drugid;
        private string _drug_name;
        private string _use_count;
        /// <summary>
        /// 药品id
        /// </summary>
        public string id
        {
            set { _drugid = value; }
            get { return _drugid; }
        }
        /// <summary>
        /// 药品名称
        /// </summary>
        public string drug_name { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string spec { get; set; }
        /// <summary>
        /// 药品用量
        /// </summary>
        public string durg_use_sp { get; set; }
        /// <summary>
        /// 药品数量
        /// </summary>
        public string use_count { get; set; }
        #endregion
    }
}
