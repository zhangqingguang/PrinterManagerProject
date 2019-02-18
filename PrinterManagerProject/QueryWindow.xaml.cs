using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PrinterManagerProject
{
    /// <summary>
    /// QueryWindow.xaml 的交互逻辑
    /// </summary>
    public partial class QueryWindow : BaseWindow
    {
        public QueryWindow()
        {
            InitializeComponent();
        }
        private void BaseWindow_Closing(object sender, CancelEventArgs e)
        {
            if (needCloseWindowConfirm)
            {
                //MessageBoxResult result = MessageBox.Show("确定是退出综合查询系统吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
                ////关闭窗口
                //if (result == MessageBoxResult.Yes)
                //{

                //    e.Cancel = false;
                //}
                ////不关闭窗口
                //if (result == MessageBoxResult.No)
                //    e.Cancel = true;

                // 打开主窗口
                var collections = Application.Current.Windows;
                foreach (Window window in collections)
                {
                    BaseWindow win = window as BaseWindow;
                    if (win != null)
                    {
                        // 其他Window直接关闭
                        if (win.ToString().Contains("MainWindow"))
                        {
                            win.Show();
                        }
                    }
                }
            }
        }
    }
}
