using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PrinterManagerProject.DBUtility;
using PrinterManagerProject.EF.Models;

namespace PrinterManagerProject.EF
{
    /// <summary>
    /// 异常管理
    /// </summary>
    public class WarningManager : BaseDALL<tWarning>
    {
        /// <summary>
        /// 根据条件获取异常信息
        /// </summary>
        /// <param name="date"></param>
        /// <param name="batch"></param>
        /// <param name="dept"></param>
        /// <param name="drugClass"></param>
        /// <param name="mainDrug"></param>
        /// <returns></returns>
        public ObservableCollection<tWarning> GetWarning(string date, string batch, string dept, string drugClass, string mainDrug)
        {
            var result = new ObservableCollection<tWarning>();
            var list = new List<tWarning>();
            if (string.IsNullOrEmpty(date) || string.IsNullOrEmpty(batch))
            {
                return result;
            }

            if (Convert.ToDateTime(date) < DateTime.Now.AddDays(-1))
            {
                list = new WarningBakManager().GetWarning(date, batch, dept, drugClass, mainDrug);
            }
            else
            {
                var query = DBContext.tWarnings
                    .AsNoTracking()
                    .Where(s => s.use_date == date && s.batch == batch && s.WarningState == WarningStateEnum.Expire);
                if (string.IsNullOrEmpty(dept) == false)
                {
                    query = query.Where(s => s.department_code == dept);
                }
                if (string.IsNullOrEmpty(drugClass) == false)
                {
                    query = query.Where(s => s.ydrug_class_name == drugClass);
                }
                if (string.IsNullOrEmpty(mainDrug) == false)
                {
                    query = query.Where(s => s.ydrug_id == mainDrug);
                }
                list = query.ToList();
            }

            foreach (var warning in list)
            {
                result.Add(warning);
            }
            return result;
        }

        /// <summary>
        /// 增加异常信息
        /// </summary>
        /// <param name="order">医嘱</param>
        /// <param name="detectionDrugName">检测溶媒名称</param>
        /// <param name="detectionDrugSpec">检测溶媒规格</param>
        /// <param name="warningState">异常状态</param>
        public void AddWarning(tOrder order,string detectionDrugName,string detectionDrugSpec,string warningState,int printerId,string printerName,int checkerId,string checkerName)
        {
            return;
            Task.Run(() =>
            {
                try
                {
                    var warning = Mapper.Map<tWarning>(order);

                    warning.PrintUserId = printerId;
                    warning.PrintUserName = printerName;
                    warning.CheckUserId = checkerId;
                    warning.CheckUserName = checkerName;

                    warning.detection_drug_name = detectionDrugName;
                    warning.detection_drug_spec = detectionDrugSpec;
                    warning.WarningState = warningState;
                    warning.printing_time = DateTime.Now;

                    DBContext.tWarnings.Add(warning);
                    DBContext.SaveChanges();
                }
                catch (Exception e)
                {
                    System.Console.Write("记录异常信息出错："+e.Message);
                }
            });
        }
    }
}
