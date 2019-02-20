using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterManagerProject.EF.Models
{
    public partial class tUser : INotifyPropertyChanged
    {
        public tUser()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected internal virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void PropertyChang(string propertyName)
        {
            OnPropertyChanged(propertyName);
        }
    }
}
