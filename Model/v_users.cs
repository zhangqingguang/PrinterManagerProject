using System;
namespace PrinterManagerProject.Model
{
	/// <summary>
	/// v_users:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class v_users
	{
		public v_users()
		{}
        #region Model
        private int _id;
        private string _user_name;
        private string _password;
        private string _true_name;
        private string _type_name;
        private DateTime? _createtime = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string user_name
        {
            set { _user_name = value; }
            get { return _user_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string true_name
        {
            set { _true_name = value; }
            get { return _true_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string type_name
        {
            set { _type_name = value; }
            get { return _type_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? createtime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        #endregion Model

    }
}

