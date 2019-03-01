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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrinterManagerProject
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : BaseWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            if (IsAdmin()==false)
            {
                btnUserWindow.IsEnabled = false;
            }
        }

        private void btnPrintWindow_Click(object sender, RoutedEventArgs e)
        {
            PrintWindow print = new PrintWindow();
            print.Show();
            this.Hide();
        }

        private void btnQueryWindow_Click(object sender, RoutedEventArgs e)
        {
            QueryWindow query = new QueryWindow();
            query.Owner = this;
            query.Show();
            this.Hide();
        }

        private void btnSettingsWindow_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settings = new SettingsWindow();
            settings.Show();
            this.Hide();
        }

        private void BaseWindow_Closing(object sender, CancelEventArgs e)
        {
            if (needCloseWindowConfirm)
            {
                MessageBoxResult result = MessageBox.Show("确定要退出系统吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
                //关闭窗口
                if (result == MessageBoxResult.Yes)
                {
                    var collections = Application.Current.Windows;
                    foreach (Window window in collections)
                    {
                        if (window != this)
                            window.Close();
                    }
                    base.OnClosed(e);
                    e.Cancel = false;
                }

                //不关闭窗口
                if (result == MessageBoxResult.No)
                    e.Cancel = true;
            }
        }

        private void BtnUserWindow_Click(object sender, RoutedEventArgs e)
        {
            UserManage users = new UserManage();
            users.Show();
            this.Hide();
        }
    }
}
