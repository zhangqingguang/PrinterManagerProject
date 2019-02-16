using System;
using System.Collections.Generic;
using System.Text;

namespace PrinterManagerProject.Model
{
    /// <summary>
	/// View_all:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
    public partial class ListAllModel
    {
        public ListAllModel()
        { }
        #region Model
        private long? _row_number;
        private int _id;
        private string _drug_id;
        private string _drug_number;
        private string _drug_name;
        private string _drug_weight;
        private string _drug_spmc;
        private string _drug_class_name;
        private string _drug_spec;
        private string _usage_id;
        private string _use_org;
        private string _use_count;
        private string _durg_use_sp;
        private string _drug_use_units;
        private string _use_frequency;
        private string _use_date;
        private string _use_time;
        private string _stop_date_time;
        private string _start_date_time;
        private string _order_sub_no;
        private string _order_type;
        private string _icatrepeat_indorm;
        private int? _new_orders;
        private int? _yebz;
        private string _special_medicationtip;
        private int? _size_specification;
        private string _pass_remark;
        private string _patient_id;
        private string _doctor_name;
        private string _patient_name;
        private string _batch;
        private string _departmengt_name;
        private string _department_code;
        private int? _zone;
        private string _visit_id;
        private string _group_num;
        private string _sum_num;
        private int? _ml_speed;
        private string _create_date;
        private string _order_status;
        private int? _is_twice_print;
        private string _checker;
        private string _deliveryer;
        private string _config_person;
        private string _config_date;
        private string _usage_name;
        private string _bed_number;
        private int? _basket_number;
        private int? _printing_status;
        private int? _printing_model;
        private DateTime? _printing_time;
        private string _qrcode;
        private string _sbatches;
        private int? _electroni_signature;
        private string _is_cpfh;
        private string _is_sf;
        private string _age;
        private string _is_db;
        private string _config_name;
        private int? _ydrug_id;
        private string _ydrug_name;
        private string _ydrug_spec;
        private string _ydrug_class_name;
        /// <summary>
        /// 序号
        /// </summary>
        public long? row_number
        {
            set { _row_number = value; }
            get { return _row_number; }
        }
        /// <summary>
		/// 溶媒数据编号
		/// </summary>
		public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 药品Id
        /// </summary>
        public string drug_id
        {
            set { _drug_id = value; }
            get { return _drug_id; }
        }
        /// <summary>
        /// 药品编号
        /// </summary>
        public string drug_number
        {
            set { _drug_number = value; }
            get { return _drug_number; }
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
        /// 药品权重
        /// </summary>
        public string drug_weight
        {
            set { _drug_weight = value; }
            get { return _drug_weight; }
        }
        /// <summary>
        /// 药品商品名
        /// </summary>
        public string drug_spmc
        {
            set { _drug_spmc = value; }
            get { return _drug_spmc; }
        }
        /// <summary>
        /// 药品作用代码
        /// </summary>
        public string drug_class_name
        {
            set { _drug_class_name = value; }
            get { return _drug_class_name; }
        }
        /// <summary>
        /// 药品规格
        /// </summary>
        public string drug_spec
        {
            set { _drug_spec = value; }
            get { return _drug_spec; }
        }
        /// <summary>
        /// 用法Id
        /// </summary>
        public string usage_id
        {
            set { _usage_id = value; }
            get { return _usage_id; }
        }
        /// <summary>
        /// 使用单位
        /// </summary>
        public string use_org
        {
            set { _use_org = value; }
            get { return _use_org; }
        }
        /// <summary>
        /// 使用量
        /// </summary>
        public string use_count
        {
            set { _use_count = value; }
            get { return _use_count; }
        }
        /// <summary>
        /// 计量规格
        /// </summary>
        public string durg_use_sp
        {
            set { _durg_use_sp = value; }
            get { return _durg_use_sp; }
        }
        /// <summary>
        /// 计量单位
        /// </summary>
        public string drug_use_units
        {
            set { _drug_use_units = value; }
            get { return _drug_use_units; }
        }
        /// <summary>
        /// 使用频次
        /// </summary>
        public string use_frequency
        {
            set { _use_frequency = value; }
            get { return _use_frequency; }
        }
        /// <summary>
        /// 用药日期
        /// </summary>
        public string use_date
        {
            set { _use_date = value; }
            get { return _use_date; }
        }
        /// <summary>
        /// 用药时间
        /// </summary>
        public string use_time
        {
            set { _use_time = value; }
            get { return _use_time; }
        }
        /// <summary>
        /// 停药时间
        /// </summary>
        public string stop_date_time
        {
            set { _stop_date_time = value; }
            get { return _stop_date_time; }
        }
        /// <summary>
        /// 开始用药时间
        /// </summary>
        public string start_date_time
        {
            set { _start_date_time = value; }
            get { return _start_date_time; }
        }
        /// <summary>
        /// 医嘱子序号
        /// </summary>
        public string order_sub_no
        {
            set { _order_sub_no = value; }
            get { return _order_sub_no; }
        }
        /// <summary>
        /// 医嘱类型
        /// </summary>
        public string order_type
        {
            set { _order_type = value; }
            get { return _order_type; }
        }
        /// <summary>
        /// 长期/临时医嘱标志
        /// </summary>
        public string icatrepeat_indorm
        {
            set { _icatrepeat_indorm = value; }
            get { return _icatrepeat_indorm; }
        }
        /// <summary>
        /// 新医嘱标志
        /// </summary>
        public int? new_orders
        {
            set { _new_orders = value; }
            get { return _new_orders; }
        }
        /// <summary>
        /// 婴儿标志
        /// </summary>
        public int? yebz
        {
            set { _yebz = value; }
            get { return _yebz; }
        }
        /// <summary>
        /// 特殊用药提示（先用、半量、冷藏、特殊低速、避光滴注、儿童慎用、18岁以下禁用）
        /// </summary>
        public string special_medicationtip
        {
            set { _special_medicationtip = value; }
            get { return _special_medicationtip; }
        }
        /// <summary>
        /// 大小规格小计量药品用下划线标志
        /// </summary>
        public int? size_specification
        {
            set { _size_specification = value; }
            get { return _size_specification; }
        }
        /// <summary>
        /// 静配备注
        /// </summary>
        public string pass_remark
        {
            set { _pass_remark = value; }
            get { return _pass_remark; }
        }
        /// <summary>
        /// 患者id
        /// </summary>
        public string patient_id
        {
            set { _patient_id = value; }
            get { return _patient_id; }
        }
        /// <summary>
        /// 医生姓名
        /// </summary>
        public string doctor_name
        {
            set { _doctor_name = value; }
            get { return _doctor_name; }
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
        /// 批次编号
        /// </summary>
        public string batch
        {
            set { _batch = value; }
            get { return _batch; }
        }
        /// <summary>
        /// 科室名称
        /// </summary>
        public string departmengt_name
        {
            set { _departmengt_name = value; }
            get { return _departmengt_name; }
        }
        /// <summary>
        /// 科室编号
        /// </summary>
        public string department_code
        {
            set { _department_code = value; }
            get { return _department_code; }
        }
        /// <summary>
        /// 病区
        /// </summary>
        public int? zone
        {
            set { _zone = value; }
            get { return _zone; }
        }
        /// <summary>
        /// 本次住院标识
        /// </summary>
        public string visit_id
        {
            set { _visit_id = value; }
            get { return _visit_id; }
        }
        /// <summary>
        /// 组号
        /// </summary>
        public string group_num
        {
            set { _group_num = value; }
            get { return _group_num; }
        }
        /// <summary>
        /// 序号
        /// </summary>
        public string sum_num
        {
            set { _sum_num = value; }
            get { return _sum_num; }
        }
        /// <summary>
        /// 低速
        /// </summary>
        public int? ml_speed
        {
            set { _ml_speed = value; }
            get { return _ml_speed; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string create_date
        {
            set { _create_date = value; }
            get { return _create_date; }
        }
        /// <summary>
        /// 医嘱状态（正常/停药）
        /// </summary>
        public string order_status
        {
            set { _order_status = value; }
            get { return _order_status; }
        }
        /// <summary>
        /// 是否重打
        /// </summary>
        public int? is_twice_print
        {
            set { _is_twice_print = value; }
            get { return _is_twice_print; }
        }
        /// <summary>
        /// 审核人
        /// </summary>
        public string checker
        {
            set { _checker = value; }
            get { return _checker; }
        }
        /// <summary>
        /// 摆药人
        /// </summary>
        public string deliveryer
        {
            set { _deliveryer = value; }
            get { return _deliveryer; }
        }
        /// <summary>
        /// 配药人
        /// </summary>
        public string config_person
        {
            set { _config_person = value; }
            get { return _config_person; }
        }
        /// <summary>
        /// 配置时间
        /// </summary>
        public string config_date
        {
            set { _config_date = value; }
            get { return _config_date; }
        }
        /// <summary>
        /// 用法说明
        /// </summary>
        public string usage_name
        {
            set { _usage_name = value; }
            get { return _usage_name; }
        }
        /// <summary>
        /// 床位编号
        /// </summary>
        public string bed_number
        {
            set { _bed_number = value; }
            get { return _bed_number; }
        }
        /// <summary>
        /// 药篮编号
        /// </summary>
        public int? basket_number
        {
            set { _basket_number = value; }
            get { return _basket_number; }
        }
        /// <summary>
        /// 打印状态
        /// </summary>
        public int? printing_status
        {
            set { _printing_status = value; }
            get { return _printing_status; }
        }
        /// <summary>
        /// 打印模式
        /// </summary>
        public int? printing_model
        {
            set { _printing_model = value; }
            get { return _printing_model; }
        }
        /// <summary>
        /// 打印时间
        /// </summary>
        public DateTime? printing_time
        {
            set { _printing_time = value; }
            get { return _printing_time; }
        }
        /// <summary>
        /// 二维码字符
        /// </summary>
        public string QRcode
        {
            set { _qrcode = value; }
            get { return _qrcode; }
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
        /// 电子签名
        /// </summary>
        public int? electroni_signature
        {
            set { _electroni_signature = value; }
            get { return _electroni_signature; }
        }
        /// <summary>
        /// 是否成品复核
        /// </summary>
        public string is_cpfh
        {
            set { _is_cpfh = value; }
            get { return _is_cpfh; }
        }
        /// <summary>
        /// 是否收费
        /// </summary>
        public string is_sf
        {
            set { _is_sf = value; }
            get { return _is_sf; }
        }
        /// <summary>
        /// 年龄
        /// </summary>
        public string age
        {
            set { _age = value; }
            get { return _age; }
        }
        /// <summary>
        /// 是否打包药
        /// </summary>
        public string is_db
        {
            set { _is_db = value; }
            get { return _is_db; }
        }
        /// <summary>
        /// 配置名称
        /// </summary>
        public string config_name
        {
            set { _config_name = value; }
            get { return _config_name; }
        }
        /// <summary>
        /// 主药ID
        /// </summary>
        public int? ydrug_id
        {
            set { _ydrug_id = value; }
            get { return _ydrug_id; }
        }
        /// <summary>
        /// 主药名称
        /// </summary>
        public string ydrug_name
        {
            set { _ydrug_name = value; }
            get { return _ydrug_name; }
        }
        /// <summary>
        /// 主药规格
        /// </summary>
        public string ydrug_spec
        {
            set { _ydrug_spec = value; }
            get { return _ydrug_spec; }
        }
        /// <summary>
		/// 主药类型
		/// </summary>
		public string ydrug_class_name
        {
            set { _ydrug_class_name = value; }
            get { return _ydrug_class_name; }
        }
        #endregion Model

    }
}
