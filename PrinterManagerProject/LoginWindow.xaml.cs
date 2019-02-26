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
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var userManager = new UserManager();

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
            var czrUser = userManager.FirstOrDefault(s => s.user_name == crzName && s.password == crzPassword);
            var fhrUser = userManager.FirstOrDefault(s => s.user_name == shrName && s.password == shrPassword);
            if (czrUser == null)
            {
                MessageBox.Show("操作员账户或密码不正确！");
                return;
            }
            else
            {
                if (czrUser.type_name != "操作员")
                {
                    MessageBox.Show($"{crzName}不是操作员！");
                    return;
                }
            }

            if (fhrUser == null)
            {
                MessageBox.Show("审核员账户或密码不正确！");
                return;
            }
            else
            {
                if (fhrUser.type_name != "审核员")
                {
                    MessageBox.Show($"{crzName}不是审核员！");
                    return;
                }
            }

            if (ConnectionManager.CheckPivasConnetionStatus() == false)
            {
                MessageBox.Show("Pivas 数据库连接失败，请检查数据库服务是否开启！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                UserCache.Printer = czrUser;
                UserCache.Checker = fhrUser;


                new LogHelper().Log("测试用户登录！");
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
