using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrinterManagerProject.DBUtility;

namespace PrinterManagerProject.EF.Bll
{
    /// <summary>
    /// 药品管理
    /// </summary>
    public class DrugManager : BaseDALL<tDrug>
    {
        /// <summary>
        /// 同步科室
        /// </summary>
        public void SyncDrug()
        {
            var ds = PivasDbHelperSQL.Query("select * from v_for_ydwl_drug");
            var dt = ds.Tables[0];

            var drugList = DBContext.tDrugs.ToList();

            foreach (DataRow zhyDrugt in dt.Rows)
            {
                var drug = drugList.FirstOrDefault(s => s.drug_code == zhyDrugt["drug_code"].ToString());
                if (drug == null)
                {
                    DBContext.tDrugs.Add(new tDrug()
                    {
                        drug_code = zhyDrugt["drug_code"].ToString(),
                        drug_form = zhyDrugt["drug_form"].ToString(),
                        drug_name = zhyDrugt["drug_name"].ToString(),
                        drug_spec = zhyDrugt["drug_spec"].ToString(),
                        drug_units = zhyDrugt["drug_units"].ToString(),
                        drug_use_spec = zhyDrugt["drug_use_spec"].ToString(),
                        drug_use_units = zhyDrugt["drug_use_units"].ToString(),
                        input_code = zhyDrugt["input_code"].ToString()
                    });
                }
                else
                {
                    drug.drug_code = zhyDrugt["drug_code"].ToString();
                    drug.drug_form = zhyDrugt["drug_form"].ToString();
                    drug.drug_name = zhyDrugt["drug_name"].ToString();
                    drug.drug_spec = zhyDrugt["drug_spec"].ToString();
                    drug.drug_units = zhyDrugt["drug_units"].ToString();
                    drug.drug_use_spec = zhyDrugt["drug_use_spec"].ToString();
                    drug.drug_use_units = zhyDrugt["drug_use_units"].ToString();
                    drug.input_code = zhyDrugt["input_code"].ToString();
                }
            }
            DBContext.SaveChanges();
        }
    }
}
