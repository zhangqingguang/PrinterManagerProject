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
    public class WarningBakManager : BaseDALL<tWarningBak>
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
        public List<tWarning> GetWarning(string date, string batch, string dept, string drugClass, string mainDrug)
        {
            var list = new List<tWarning>();
            if (string.IsNullOrEmpty(date) || string.IsNullOrEmpty(batch))
            {
                return list;
            }

            var query = DBContext.tWarningBaks
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

            var warningList = query.ToList();

            return Mapper.Map<List<tWarning>>(warningList);
        }
    }
}
