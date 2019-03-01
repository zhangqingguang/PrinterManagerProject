using PrinterManagerProject.EF;
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
    /// ChangePassword.xaml 的交互逻辑
    /// </summary>
    public partial class ChangePassword : BaseWindow
    {
        UserManager userManager = new UserManager();
        tUser user= null;
        int UserId = 0;
        public ChangePassword()
        {
            InitializeComponent();
            this.Closed += ChangePassword_Closed;
        }

        private void ChangePassword_Closed(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        public void Init(int userid)
        {
            this.UserId = userid;
            user = userManager.FirstOrDefault(s => s.ID == UserId);
            this.btn_UserName.Content = user.true_name;
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            var originalPwd = this.tb_originalPwd.Password.Trim();
            var newPwd = this.tb_newPwd.Password.Trim();
            var reNewPwd = this.tb_reNewPwd.Password.Trim();

            if (string.IsNullOrEmpty(originalPwd))
            {
                MessageBox.Show("请输入原密码");
                return;
            }
            if (string.IsNullOrEmpty(newPwd))
            {
                MessageBox.Show("请输入新密码");
                return;
            }
            if (string.IsNullOrEmpty(reNewPwd))
            {
                MessageBox.Show("请输入重复密码");
                return;
            }

            if (originalPwd != user.password)
            {
                MessageBox.Show("原始密码不正确");
                return;
            }

            if (newPwd.Length<6)
            {
                MessageBox.Show("请填写6位及以上用户密码！");
                return;
            }

            if (newPwd != reNewPwd)
            {
                MessageBox.Show("新密码与重复密码不一致");
                return;
            }

            try
            {
                user.password = newPwd;
                userManager.AddOrUpdate(user);

                MessageBox.Show("密码修改成功！");
                OpenMainWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show("密码修改失败，请重试！");
            }

        }

        private void Btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            OpenMainWindow();
        }

        private void OpenMainWindow()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.needCloseWindowConfirm = false;
            this.Close();
        }
    }
}
