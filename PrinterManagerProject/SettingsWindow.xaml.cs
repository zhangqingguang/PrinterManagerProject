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
    /// SettingsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingsWindow : BaseWindow
    {
        private Label lblCurrentMenu;

        public SettingsWindow(string pageName="")
        {
            InitializeComponent();
            lblCurrentMenu = lblTimeSetting;

            if (string.IsNullOrEmpty(pageName) == false)
            {
                if (this.FindName(pageName) is Label label)
                {
                    lblCurrentMenu = label;
                }
            }

            // 打开默认选项对应页面
            this.PageContext.Source = new Uri(lblCurrentMenu.Tag.ToString(), UriKind.Relative);

            if (isSuperAdmin() == false)
            {
                lblParamSetting.IsEnabled = false;
                lblSizeSetting.IsEnabled = false;
            }
        }
        private void BaseWindow_Closing(object sender, CancelEventArgs e)
        {
            if (needCloseWindowConfirm)
            {
                //MessageBoxResult result = MessageBox.Show("确定是退出系统设置吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
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

        private void lblSetting_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // 清除背景色
            lblTimeSetting.Background = Brushes.Transparent;
            lblParamSetting.Background = Brushes.Transparent;
            lblSizeSetting.Background = Brushes.Transparent;

            // 设置当前选中
            lblCurrentMenu = (Label)sender;
            lblCurrentMenu.Background = new SolidColorBrush(Colors.White);
            
            // 打开页面
            this.PageContext.Source = new Uri(lblCurrentMenu.Tag.ToString(), UriKind.Relative);
        }

        private void lblSetting_MouseMove(object sender, MouseEventArgs e)
        {
            // 清除背景色
            lblTimeSetting.Background = Brushes.Transparent;
            lblParamSetting.Background = Brushes.Transparent;
            lblSizeSetting.Background = Brushes.Transparent;

            ((Label)sender).Background = new SolidColorBrush(Colors.WhiteSmoke);

            // 保持选中状态
            lblCurrentMenu.Background = new SolidColorBrush(Colors.White);
        }
        
    }
}
