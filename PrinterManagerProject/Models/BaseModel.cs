using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PrinterManagerProject.Models
{
    public class BaseModel
    {
        public int Number { get; set; }
        public string No { get; set; }
        public string Area { get; set; }
        public int Position { get; set; }
        public string PeopleName { get; set; }
        public string DrugName { get; set; }
        public string DrugSpec { get; set; }
        public string Company { get; set; }
        public string SolventName { get; set; }
        public string SolventSpec { get; set; }
        public string SolventBatchNo { get; set; }
        public string Stataus { get; set; }
        public SolidColorBrush StatusColor { get; set; }
        public DateTime MarkTime { get; set; }
    }
}
