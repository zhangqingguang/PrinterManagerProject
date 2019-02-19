using System;
using System.Collections.Generic;
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
using PrinterManagerProject.EF;
using PrinterManagerProject.EF.Bll;

namespace PrinterManagerProject.Pages
{
    /// <summary>
    /// TimeSettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class DrugSettingPage : Page
    {
        DrugManager drugManager = new DrugManager();
        public DrugSettingPage()
        {
            InitializeComponent();

            BindList();
        }

        public void BindList()
        {
            var batchs = drugManager.GetAll();
            this.dgv_list.ItemsSource = batchs;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (ConnectionManager.CheckPivasConnetionStatus() == false)
            {

                MessageBox.Show("Pivas 数据库连接失败，请检查数据库服务是否开启！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                try
                {
                    this.update.IsEnabled = false;
                    this.update.Content = "正在从Pivas同步药品信息";
                    drugManager.SyncDrug();
                    BindList();

                    myEventLog.LogInfo("成功从Pivas同步药品信息");
                }
                catch (Exception exception)
                {
                    myEventLog.LogError(exception.Message,exception);
                }

                this.update.IsEnabled = true;
                this.update.Content = "从Pival更新";

            }
        }
    }
}
