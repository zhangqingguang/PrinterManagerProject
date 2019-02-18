using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterManagerProject.EF.Models
{
    public class OrderModel
    {

        public int Id { get; set; }
        public int RowNumber { get; set; }
        public string drug_id { get; set; }
        public string drug_number { get; set; }
        public string drug_name { get; set; }
        public string ydrug_name { get; set; }
        public string drug_weight { get; set; }
        public string drug_spmc { get; set; }
        public string drug_class_name { get; set; }
        public string ydrug_class_name { get; set; }
        public string drug_spec { get; set; }
        public string ydrug_spec { get; set; }
        public string usage_id { get; set; }
        public string use_org { get; set; }
        public string use_count { get; set; }
        public string durg_use_sp { get; set; }
        public string drug_use_units { get; set; }
        public string use_frequency { get; set; }
        public string use_date { get; set; }
        public string use_time { get; set; }
        public string stop_date_time { get; set; }
        public string start_date_time { get; set; }
        public string order_sub_no { get; set; }
        public string order_type { get; set; }
        public string icatrepeat_indorm { get; set; }
        public Nullable<int> new_orders { get; set; }
        public Nullable<int> yebz { get; set; }
        public string special_medicationtip { get; set; }
        public Nullable<int> size_specification { get; set; }
        public string pass_remark { get; set; }
        public string patient_id { get; set; }
        public string doctor_name { get; set; }
        public string patient_name { get; set; }
        public string batch { get; set; }
        public string departmengt_name { get; set; }
        public string department_code { get; set; }
        public Nullable<int> zone { get; set; }
        public string visit_id { get; set; }
        public string group_num { get; set; }
        public string sum_num { get; set; }
        public Nullable<int> ml_speed { get; set; }
        public string create_date { get; set; }
        public string order_status { get; set; }
        public Nullable<int> is_twice_print { get; set; }
        public string checker { get; set; }
        public string deliveryer { get; set; }
        public string config_person { get; set; }
        public string config_date { get; set; }
        public string usage_name { get; set; }
        public string bed_number { get; set; }
        public Nullable<int> basket_number { get; set; }
        public Nullable<PrinterManagerProject.EF.Models.PrintStatusEnum> printing_status { get; set; }
        public Nullable<PrinterManagerProject.EF.Models.PrintModelEnum> printing_model { get; set; }
        public Nullable<System.DateTime> printing_time { get; set; }
        public string QRcode { get; set; }
        public string sbatches { get; set; }
        public Nullable<int> electroni_signature { get; set; }
        public string is_cpfh { get; set; }
        public string is_sf { get; set; }
        public string age { get; set; }
        public string is_db { get; set; }
        public string config_name { get; set; }
    }
}
