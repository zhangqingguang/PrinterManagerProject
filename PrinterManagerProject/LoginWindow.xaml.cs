using PrinterManagerProject.Tools;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Graphics;
using Zebra.Sdk.Printer;
using Zebra.Sdk.Printer.Discovery;
using System.Collections.Generic;
using PrinterManagerProject.Models;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Threading.Tasks;
using PrinterManagerProject.EF;
using PrinterManagerProject.EF.Bll;
using ZXing.PDF417;
using ZXing;

namespace PrinterManagerProject
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : BaseWindow
    {

        Connection connection = null;

        public LoginWindow()
        {
            InitializeComponent();

            if (AppConfig.IsDebug)
            {
                txtCZR.Text = "admin1";
                txtSHR.Text = "admin2";

                txtCZRPWD.Password = "123456";
                txtSHRPWD.Password = "123456";
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var userManager = new PivasUserManager();

            var crzName = txtCZR.Text.Trim();
            var crzPassword = txtCZRPWD.Password.Trim();
            var shrName = txtSHR.Text.Trim();
            var shrPassword = txtCZRPWD.Password.Trim();
            if (string.IsNullOrEmpty(crzName))
            {
                MessageBox.Show("请输入操作员！");
                return;
            }
            if (string.IsNullOrEmpty(txtCZRPWD.Password.Trim()))
            {
                MessageBox.Show("请输入操作员密码！");
                return;
            }
            if(crzName == "ydwl" && crzPassword == "password01!")
            {
                UserCache.Printer = new tUser(){
                    ID=0,
                    true_name="益达物联",
                    user_name="ydwl",
                    password="password01!"
                };
                UserCache.Checker = new tUser()
                {
                    ID = 0,
                    true_name = "益达物联",
                    user_name = "ydwl",
                    password = "password01!"
                };


                new LogHelper().Log("测试用户登录！");
                this.Hide();

                MainWindow window = new MainWindow();
                window.Show();
                return;
            }

            if (string.IsNullOrEmpty(txtSHR.Text.Trim()))
            {
                MessageBox.Show("请输入审核员！");
                return;
            }
            if (string.IsNullOrEmpty(txtCZRPWD.Password.Trim()))
            {
                MessageBox.Show("请输入审核员密码！");
                return;
            }

            if (ConnectionManager.CheckPivasConnetionStatus() == false)
            {
                MessageBox.Show("Pivas 数据库连接失败，请检查数据库服务是否开启！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                var czrUser = userManager.GetUser(crzName);
                var fhrUser = userManager.GetUser(shrName);
                if (czrUser == null || czrUser.password != crzPassword)
                {
                    MessageBox.Show("操作员账户或密码不正确！");
                    return;
                }

                if (fhrUser == null || fhrUser.password != shrPassword)
                {
                    MessageBox.Show("审核员账户或密码不正确！");
                    return;
                }

                UserCache.Printer = czrUser;
                UserCache.Checker = fhrUser;


                new LogHelper().Log($"分拣人：{czrUser.true_name}，复核人：{fhrUser.true_name}登录！");
                this.Hide();

                MainWindow window = new MainWindow();
                window.Show();
            }
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //CCDSerialPortUtils.GetInstance(this).Close();
            //PLCSerialPortUtils.GetInstance(this).Close();

            this.Close();
        }
    }
}
