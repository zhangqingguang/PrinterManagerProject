using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterManagerProject.EF
{
    /// <summary>
    /// 主药分组比较器
    /// </summary>
    public class OrderMainDrugCompare:IEqualityComparer<tOrder>
    {
        public bool Equals(tOrder x, tOrder y)
        {
            return x.ydrug_id == y.ydrug_id;
        }

        public int GetHashCode(tOrder obj)
        {
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return obj.ToString().GetHashCode();
            }
        }
    }
}
