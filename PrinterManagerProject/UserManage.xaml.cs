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
using PrinterManagerProject.Model;
using PrinterManagerProject.BLL;
using System.Data;

namespace PrinterManagerProject
{
    /// <summary>
    /// UserManage.xaml 的交互逻辑
    /// </summary>
    public partial class UserManage : BaseWindow
    {
        public UserManage()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            BLL.v_users users = new BLL.v_users();
            dgvGroupRepairList.ItemsSource = users.get_v_users();
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            BLL.v_users userbll = new BLL.v_users();            
            formcheck();
            if (userbll.Exists(username.Text.Trim()))
            {
                MessageBox.Show("用户名已经存在,请修改后点击添加!");
                return;
            }
            Model.v_users usermodel = new Model.v_users();
            usermodel.user_name = username.Text.Trim();
            usermodel.password = userpwd.Text.Trim();
            usermodel.true_name = usertrue.Text.Trim();
            usermodel.type_name = usertype.Text.Trim();
            usermodel.createtime = DateTime.Now;
            
            if(userbll.Add(usermodel)>0)
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
            BLL.v_users userbll = new BLL.v_users();

            if(userid.Text.Trim()=="")
            {
                MessageBox.Show("请选择您要修改的用户");
                return;
            }
            if (userbll.Exists(username.Text.Trim()))
            {
                MessageBox.Show("用户名已经存在,请修改!");
                return;
            }
            formcheck();
            Model.v_users usermodel = new Model.v_users();
            usermodel.ID = Convert.ToInt32(userid.Text.Trim());
            usermodel.user_name = username.Text.Trim();
            usermodel.password = userpwd.Text.Trim();
            usermodel.true_name = usertrue.Text.Trim();
            usermodel.type_name = usertype.Text.Trim();
            usermodel.createtime = DateTime.Now;

            if(userbll.Update(usermodel))
            {
                MessageBox.Show("用户修改成功！");
                LoadData();
            }
            else
            {
                MessageBox.Show("用户修改失败!");
            }
        }

        private void BtnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (userid.Text.Trim() == "")
            {
                MessageBox.Show("请选择您要修改的用户");
                return;
            }
            BLL.v_users userbll = new BLL.v_users();
            int uid = Convert.ToInt32(userid.Text.Trim());
            if (userbll.Delete(uid))
            {
                MessageBox.Show("用户删除成功！");
                LoadData();
            }
            else
            {
                MessageBox.Show("用户删除失败！");
            }

        }


        private void DgvGroupRepairList_MouseUp(object sender, MouseButtonEventArgs e)
        {          

            var a = this.dgvGroupRepairList.SelectedItem;
            var b = a as PrinterManagerProject.Model.v_users;

            userid.Text = b.ID.ToString();
            username.Text = b.user_name;
            userpwd.Text = b.password;
            usertrue.Text = b.true_name;
            usertype.Text = b.type_name;

        }

        private void Btn_clear_Click(object sender, RoutedEventArgs e)
        {
            usertype.SelectedIndex = 0;
            username.Text = "";
            userpwd.Text = "";
            usertrue.Text = "";
            userid.Text = "";
        }
        /// <summary>
        /// 检测用户输入
        /// </summary>
        public void formcheck()
        {
            if (usertype.Text.Trim() == "请选择")
            {
                MessageBox.Show("请选择用户的操作身份！");
                return;
            }
            if (username.Text.Trim() == "")
            {
                MessageBox.Show("请认真填写用户名！");
                return;
            }
            if (userpwd.Text.Trim() == "" && userpwd.Text.Trim().Length < 6)
            {
                MessageBox.Show("请填写6位及以上用户密码！");
                return;
            }
        }
    }
}
