using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterManagerProject.EF
{
    public class OrderConfig
    {
        public static string GetBakDate() {
            return DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
        }
    }
}
