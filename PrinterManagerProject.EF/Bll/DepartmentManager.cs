using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrinterManagerProject.DBUtility;

namespace PrinterManagerProject.EF
{
    public class DepartmentManager : BaseDALL<tDept>
    {

        /// <summary>
        /// 同步科室
        /// </summary>
        public void SyncDepartment()
        {
            var ds = PivasDbHelperSQL.Query("select * from v_for_ydwl_dept where Name like '%2%' or isuse=1");
            var dt = ds.Tables[0];

            var deptList = DBContext.tDepts.ToList();

            foreach (DataRow zhyDept in dt.Rows)
            {
                var dept = deptList.FirstOrDefault(s => s.Code == zhyDept["Code"].ToString());
                if (dept == null)
                {
                    DBContext.tDepts.Add(new tDept()
                    {
                        Code = zhyDept["Code"].ToString(),
                        Isuse = Convert.ToInt32(zhyDept["Isuse"]),
                        Name = zhyDept["Name"].ToString(),
                        ShortCut = zhyDept["ShortCut"].ToString()
                    });
                }
                else
                {
                    dept.Name = zhyDept["Name"].ToString();
                }
            }
            DBContext.SaveChanges();
        }
    }
}
