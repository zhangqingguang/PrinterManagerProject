using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using PrinterManagerProject.EF;

namespace PrinterManagerProject
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            System.Threading.Thread.Sleep(3000);
            base.OnStartup(e);
            MapperConfig.Config();
        }
    }
}
