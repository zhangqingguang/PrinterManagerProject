using System;
namespace PrinterManagerProject.Model
{
	/// <summary>
	/// v_order_info:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class v_order_info
	{
		public v_order_info()
		{}
		#region Model
		private int _id;
		private long? _drug_id;
		private string _drug_number;
		private int? _drug_weight;
		private string _drug_name;
		private string _drug_spec;
		private string _usage_id;
		private string _use_org;
		private int? _use_count;
		private string _use_unit;
		private string _use_frequency;
		private string _start_date;
		private string _stop_date;
		private string _use_time;
		private string _order_number;
		private string _order_remark;
		private int? _order_type;
		private string _pass_remark;
		private string _doctor_name;
		private string _patientid;
		private string _patient_name;
		private string _patient_gender;
		private string _batch;
		private string _department_number;
		private string _department_name;
		private int? _visitid;
		private int? _group_num;
		private int? _sn_num;
		private int? _ml_speed;
		private DateTime? _create_date;
		private string _order_status;
		private string _is_twice_print;
		private string _checker;
		private string _deliveryer;
		private string _rechecker;
		private string _config_person;
		private DateTime? _config_date;
		private string _usage_name;
		private string _bed_number;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 药品id
		/// </summary>
		public long? drug_id
		{
			set{ _drug_id=value;}
			get{return _drug_id;}
		}
		/// <summary>
		/// 药品编号
		/// </summary>
		public string drug_number
		{
			set{ _drug_number=value;}
			get{return _drug_number;}
		}
		/// <summary>
		/// 药品权重
		/// </summary>
		public int? drug_weight
		{
			set{ _drug_weight=value;}
			get{return _drug_weight;}
		}
		/// <summary>
		/// 药品名称
		/// </summary>
		public string drug_name
		{
			set{ _drug_name=value;}
			get{return _drug_name;}
		}
		/// <summary>
		/// 药品规格
		/// </summary>
		public string drug_spec
		{
			set{ _drug_spec=value;}
			get{return _drug_spec;}
		}
		/// <summary>
		/// 用法id
		/// </summary>
		public string usage_id
		{
			set{ _usage_id=value;}
			get{return _usage_id;}
		}
		/// <summary>
		/// 使用单位
		/// </summary>
		public string use_org
		{
			set{ _use_org=value;}
			get{return _use_org;}
		}
		/// <summary>
		/// 使用数量
		/// </summary>
		public int? use_count
		{
			set{ _use_count=value;}
			get{return _use_count;}
		}
		/// <summary>
		/// 使用单位
		/// </summary>
		public string use_unit
		{
			set{ _use_unit=value;}
			get{return _use_unit;}
		}
		/// <summary>
		/// 使用频次
		/// </summary>
		public string use_frequency
		{
			set{ _use_frequency=value;}
			get{return _use_frequency;}
		}
		/// <summary>
		/// 开始时间
		/// </summary>
		public string start_date
		{
			set{ _start_date=value;}
			get{return _start_date;}
		}
		/// <summary>
		/// 结束时间
		/// </summary>
		public string stop_date
		{
			set{ _stop_date=value;}
			get{return _stop_date;}
		}
		/// <summary>
		/// 使用时间
		/// </summary>
		public string use_time
		{
			set{ _use_time=value;}
			get{return _use_time;}
		}
		/// <summary>
		/// 医嘱编号
		/// </summary>
		public string order_number
		{
			set{ _order_number=value;}
			get{return _order_number;}
		}
		/// <summary>
		/// 医嘱备注
		/// </summary>
		public string order_remark
		{
			set{ _order_remark=value;}
			get{return _order_remark;}
		}
		/// <summary>
		/// 医嘱类型
		/// </summary>
		public int? order_type
		{
			set{ _order_type=value;}
			get{return _order_type;}
		}
		/// <summary>
		/// 静配备注
		/// </summary>
		public string pass_remark
		{
			set{ _pass_remark=value;}
			get{return _pass_remark;}
		}
		/// <summary>
		/// 医生姓名
		/// </summary>
		public string doctor_name
		{
			set{ _doctor_name=value;}
			get{return _doctor_name;}
		}
		/// <summary>
		/// 患者id
		/// </summary>
		public string patientid
		{
			set{ _patientid=value;}
			get{return _patientid;}
		}
		/// <summary>
		/// 患者姓名
		/// </summary>
		public string patient_name
		{
			set{ _patient_name=value;}
			get{return _patient_name;}
		}
		/// <summary>
		/// 患者性别
		/// </summary>
		public string patient_gender
		{
			set{ _patient_gender=value;}
			get{return _patient_gender;}
		}
		/// <summary>
		/// 划分批次
		/// </summary>
		public string batch
		{
			set{ _batch=value;}
			get{return _batch;}
		}
		/// <summary>
		/// 部门编号
		/// </summary>
		public string department_number
		{
			set{ _department_number=value;}
			get{return _department_number;}
		}
		/// <summary>
		/// 部门名称
		/// </summary>
		public string department_name
		{
			set{ _department_name=value;}
			get{return _department_name;}
		}
		/// <summary>
		/// 本次住院标识
		/// </summary>
		public int? visitid
		{
			set{ _visitid=value;}
			get{return _visitid;}
		}
		/// <summary>
		/// 组号
		/// </summary>
		public int? group_num
		{
			set{ _group_num=value;}
			get{return _group_num;}
		}
		/// <summary>
		/// 序号
		/// </summary>
		public int? sn_num
		{
			set{ _sn_num=value;}
			get{return _sn_num;}
		}
		/// <summary>
		/// 滴速
		/// </summary>
		public int? ml_speed
		{
			set{ _ml_speed=value;}
			get{return _ml_speed;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime? create_date
		{
			set{ _create_date=value;}
			get{return _create_date;}
		}
		/// <summary>
		/// 医嘱状态
		/// </summary>
		public string order_status
		{
			set{ _order_status=value;}
			get{return _order_status;}
		}
		/// <summary>
		/// 是否重打
		/// </summary>
		public string is_twice_print
		{
			set{ _is_twice_print=value;}
			get{return _is_twice_print;}
		}
		/// <summary>
		/// 审核人
		/// </summary>
		public string checker
		{
			set{ _checker=value;}
			get{return _checker;}
		}
		/// <summary>
		/// 摆药人
		/// </summary>
		public string deliveryer
		{
			set{ _deliveryer=value;}
			get{return _deliveryer;}
		}
		/// <summary>
		/// 复核人
		/// </summary>
		public string rechecker
		{
			set{ _rechecker=value;}
			get{return _rechecker;}
		}
		/// <summary>
		/// 配药人
		/// </summary>
		public string config_person
		{
			set{ _config_person=value;}
			get{return _config_person;}
		}
		/// <summary>
		/// 配置时间
		/// </summary>
		public DateTime? config_date
		{
			set{ _config_date=value;}
			get{return _config_date;}
		}
		/// <summary>
		/// 用法说明
		/// </summary>
		public string usage_name
		{
			set{ _usage_name=value;}
			get{return _usage_name;}
		}
		/// <summary>
		/// 床位编号
		/// </summary>
		public string bed_number
		{
			set{ _bed_number=value;}
			get{return _bed_number;}
		}
		#endregion Model

	}
}

