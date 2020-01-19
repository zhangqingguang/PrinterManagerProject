using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterManagerProject.LoggerApp
{
    public class ConditionConfig
    {
        public string ActionName { get; set; }
        public bool First { get; set; }
        public string Condiiton1ActionName { get; set; }
        public bool Condiiton1Before { get; set; }
        public string Condiiton2ActionName { get; set; }
        public bool Condiiton2Before { get; set; }
    }
}
