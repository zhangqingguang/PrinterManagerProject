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
using System.Windows.Shapes;
using System.Data;
using PrinterManagerProject.EF;

namespace PrinterManagerProject
{
    /// <summary>
    /// UserManage.xaml 的交互逻辑
    /// </summary>
    public partial class UserManage : BaseWindow
    {
        tUser user = null;
        EF.UserManager userManager = new UserManager();
        public UserManage()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            dgv_list.ItemsSource = userManager.GetAll();
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            tUser userbll = new tUser();            
            formcheck();
            if (userManager.Any(s=>s.user_name == username.Text.Trim()))
            {
                MessageBox.Show($"用户名【{username.Text.Trim()}】已经存在,请修改后点击添加!");
                return;
            }
            tUser usermodel = new tUser();
            usermodel.user_name = username.Text.Trim();
            usermodel.password = userpwd.Text.Trim();
            usermodel.true_name = usertrue.Text.Trim();
            usermodel.type_name = usertype.Text.Trim();
            usermodel.createtime = DateTime.Now;

            userManager.Add(usermodel);
            if (usermodel.ID>0)
            {
                MessageBox.Show("用户添加成功!");
                LoadData();
            }
            else
            {
                MessageBox.Show("用户添加失败!");
            }            
        }

        private void BtnUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            if (user == null)
            {
                MessageBox.Show("请选择您要修改的用户");
                return;
            }
            if (userManager.Any(s =>s.ID != user.ID && s.user_name == username.Text.Trim()))
            {
                MessageBox.Show($"用户名【{username.Text.Trim()}】已经存在,请修改!");
                return;
            }
            if (formcheck() == false)
            {
                return;
            }
            //usermodel.ID = Convert.ToInt32(userid.Text.Trim());
            user.user_name = username.Text.Trim();
            user.password = userpwd.Text.Trim();
            user.true_name = usertrue.Text.Trim();
            user.type_name = usertype.Text.Trim();
            user.createtime = DateTime.Now;
            try
            {
                userManager.Update(user);
                MessageBox.Show("用户修改成功！");
                LoadData();
            }
            catch (Exception exception)
            {
                MessageBox.Show("用户修改失败!");
            }
        }

        private void BtnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (user==null)
            {
                MessageBox.Show("请选择您要修改的用户");
                return;
            }
            if (formcheck() == false)
            {
                return;
            }
            try
            {
                userManager.Delete(user.ID);
                MessageBox.Show("用户删除成功！");
                LoadData();
            }
            catch (Exception exception)
            {
                MessageBox.Show("用户删除失败!");
            }

        }


        private void dgv_list_MouseUp(object sender, MouseButtonEventArgs e)
        {          

            var a = this.dgv_list.SelectedItem;
            if (a is tUser b)
            {
                username.Text = b.user_name;
                userpwd.Text = b.password;
                usertrue.Text = b.true_name;
                usertype.Text = b.type_name;

                user = b;
            }

        }

        private void Btn_clear_Click(object sender, RoutedEventArgs e)
        {
            usertype.SelectedIndex = 0;
            username.Text = "";
            userpwd.Text = "";
            usertrue.Text = "";
        }
        /// <summary>
        /// 检测用户输入
        /// </summary>
        public bool formcheck()
        {
            if (usertype.Text.Trim() == "请选择")
            {
                MessageBox.Show("请选择用户的操作身份！");
                return false;
            }
            if (username.Text.Trim() == "")
            {
                MessageBox.Show("请认真填写用户名！");
                return false;
            }
            if (userpwd.Text.Trim() == "" && userpwd.Text.Trim().Length < 6)
            {
                MessageBox.Show("请填写6位及以上用户密码！");
                return false;
            }
            return true;
        }

        private void Btn_resetPassword_Click(object sender, RoutedEventArgs e)
        {
            if (user == null)
            {
                MessageBox.Show("请选择您要修改的用户");
                return;
            }
            if (MessageBox.Show("确定要重置密码","确认", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                user.password = "888888";
                userManager.AddOrUpdate(user);
            }
        }
    }
}
