using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using PrinterManagerProject.Tools;

namespace PrinterManagerProject
{
    public class BaseWindow : Window
    {
        // 默认 需要关闭页面提示
        protected bool needCloseWindowConfirm = true;

        private Button CloseButton;
        private Button MinButton;
        private StackPanel SpSettings;
        private TextBlock WindowTitleTbl;

        private Label lblTime;
        //private MenuItem miPrinterChangePassword;
        //private MenuItem miCheakerChangePassword;
        private MenuItem miPrinterLogout;
        private MenuItem miExit;
        private MenuItem miPrint;
        private MenuItem miQuery;

        //private MenuItem miUserManager;
        private MenuItem miBatch;
        private MenuItem miDrug;
        private MenuItem miOperatorParam;
        private MenuItem miSolventSize;
        private MenuItem miPrintTemplate;

        private MenuItem printerHeader;
        private MenuItem checkerHeader;

        private MenuItem menu;

        private DispatcherTimer dispatcherTimer;


        public BaseWindow()
        {
            this.Loaded += BaseWindow_Loaded;
        }

        private void BaseWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // 查找窗体模板
            ControlTemplate metroWindowTemplate
                    = App.Current.Resources["MetroWindowTemplate"] as ControlTemplate;

            if (metroWindowTemplate != null)
            {
                // 初始化控件
                CloseButton = metroWindowTemplate.FindName("CloseWinButton", this) as Button;
                MinButton = metroWindowTemplate.FindName("MinWinButton", this) as Button;
                WindowTitleTbl = metroWindowTemplate.FindName("WindowTitleTbl", this) as TextBlock;
                SpSettings = metroWindowTemplate.FindName("spSettings", this) as StackPanel;

                // 事件绑定
                CloseButton.Click += CloseButton_Click;
                MinButton.Click += MinButton_Click;

                // 如果是登录界面，隐藏设置按钮
                if (this.ToString().Contains("LoginWindow"))
                {
                    SpSettings.Visibility = Visibility.Hidden;
                }
                else
                {
                    menu = metroWindowTemplate.FindName("Menu", this) as MenuItem;
                    // 初始化控件
                    lblTime = metroWindowTemplate.FindName("lblTime", this) as Label;
                    printerHeader = metroWindowTemplate.FindName("printerHeader", this) as MenuItem;
                    checkerHeader = metroWindowTemplate.FindName("checkerHeader", this) as MenuItem;
                    //miPrinterChangePassword = metroWindowTemplate.FindName("miPrinterChangePassword", this) as MenuItem;
                    //miCheakerChangePassword = metroWindowTemplate.FindName("miCheakerChangePassword", this) as MenuItem;
                    miPrinterLogout = metroWindowTemplate.FindName("miPrinterLogout", this) as MenuItem;
                    miExit = metroWindowTemplate.FindName("miExit", this) as MenuItem;
                    miPrint = metroWindowTemplate.FindName("miPrint", this) as MenuItem;
                    miQuery = metroWindowTemplate.FindName("miQuery", this) as MenuItem;

                    //miUserManager = metroWindowTemplate.FindName("miUserManager", this) as MenuItem;
                    miBatch = metroWindowTemplate.FindName("miBatch", this) as MenuItem;
                    miDrug = metroWindowTemplate.FindName("miDrug", this) as MenuItem;
                    miOperatorParam = metroWindowTemplate.FindName("miOperatorParam", this) as MenuItem;
                    miSolventSize = metroWindowTemplate.FindName("miSolventSize", this) as MenuItem;
                    miPrintTemplate = metroWindowTemplate.FindName("miPrintTemplate", this) as MenuItem;

                    if (isSuperAdmin() == false)
                    {
                        miOperatorParam.IsEnabled = false;
                        miSolventSize.IsEnabled = false;
                    }

                    if(IsAdmin()==false)
                    {
                        //miUserManager.IsEnabled = false;
                    }

                    checkerHeader.Header = UserCache.Checker.true_name;
                    printerHeader.Header = UserCache.Printer.true_name;

                    // 事件绑定
                    //miPrinterChangePassword.Click += miPrinterChangePassword_Click;
                    //miCheakerChangePassword.Click += miCheakerChangePassword_Click;
                    miPrinterLogout.Click += miPrinterLogout_Click;
                    miExit.Click += miExit_Click;
                    miPrint.Click += miPrint_Click;
                    miQuery.Click += miQuery_Click;

                    //miUserManager.Click += MiUserManager_Click; ;
                    miBatch.Click += MiBatch_Click; ;
                    miDrug.Click += MiDrug_Click; ;
                    miOperatorParam.Click += MiOperatorParam_Click; ;
                    miSolventSize.Click += MiSolventSize_Click; ;
                    miPrintTemplate.Click += MiPrintTemplate_Click; ;

                    dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                    // 当间隔时间过去时发生的事件
                    dispatcherTimer.Tick += new EventHandler(ShowCurrentTime);
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
                    dispatcherTimer.Start();
                }
            }
        }

        /// <summary>
        /// 点击打印模板设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiPrintTemplate_Click(object sender, RoutedEventArgs e)
        {
            PrinterManagerProject.SettingsWindow userManageWindow = new PrinterManagerProject.SettingsWindow("lblPrintTemplate");
            userManageWindow.Show();
            this.needCloseWindowConfirm = false;
            CloseCurrentWindow();
        }

        /// <summary>
        /// 点击溶媒尺寸设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiSolventSize_Click(object sender, RoutedEventArgs e)
        {
            if (isSuperAdmin())
            {
                PrinterManagerProject.SettingsWindow userManageWindow = new PrinterManagerProject.SettingsWindow("lblSizeSetting");
                userManageWindow.Show();
                this.needCloseWindowConfirm = false;
                CloseCurrentWindow();
            }
        }

        /// <summary>
        /// 点击运行参数设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiOperatorParam_Click(object sender, RoutedEventArgs e)
        {
            if (isSuperAdmin())
            {
                PrinterManagerProject.SettingsWindow userManageWindow = new PrinterManagerProject.SettingsWindow("lblParamSetting");
                userManageWindow.Show();
                this.needCloseWindowConfirm = false;
                CloseCurrentWindow();
            }
        }

        /// <summary>
        /// 点击药品信息同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiDrug_Click(object sender, RoutedEventArgs e)
        {
            PrinterManagerProject.SettingsWindow userManageWindow = new PrinterManagerProject.SettingsWindow("lblDrugSetting");
            userManageWindow.Show();
            this.needCloseWindowConfirm = false;
            CloseCurrentWindow();
        }

        /// <summary>
        /// 点击批次时间同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiBatch_Click(object sender, RoutedEventArgs e)
        {
            PrinterManagerProject.SettingsWindow userManageWindow = new PrinterManagerProject.SettingsWindow("lblTimeSetting");
            userManageWindow.Show();
            this.needCloseWindowConfirm = false;
            CloseCurrentWindow();
        }
        /// <summary>
        /// 点击用户管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiUserManager_Click(object sender, RoutedEventArgs e)
        {
            PrinterManagerProject.UserManage userManageWindow = new PrinterManagerProject.UserManage();
            userManageWindow.Show();
            this.needCloseWindowConfirm = false;
            CloseCurrentWindow();
        }
        /// <summary>
        /// 当前时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ShowCurrentTime(object sender, EventArgs e)
        {
            if (!this.ToString().Contains("LoginWindow"))
            {
                this.lblTime.Content = "当前时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss dddd");
            }
        }
        /// <summary>
        /// 点击关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if(window.GetType() == this.GetType())
                {
                    window.Close();
                }
            }
            Close();
        }
        /// <summary>
        /// 点击最小化按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        /// <summary>
        /// 点击修改密码按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miPrinterChangePassword_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ChangePassword changePasswordWindow = new ChangePassword();
            changePasswordWindow.Init(UserCache.Printer.ID);
            changePasswordWindow.Show();
            this.needCloseWindowConfirm = false;
            CloseCurrentWindow();
        }
        /// <summary>
        /// 点击修改密码按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miCheakerChangePassword_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ChangePassword changePasswordWindow = new ChangePassword();
            changePasswordWindow.Init(UserCache.Checker.ID);
            changePasswordWindow.Show();
            this.needCloseWindowConfirm = false;
            CloseCurrentWindow();
        }
        /// <summary>
        /// 点击注销按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miPrinterLogout_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var collections = Application.Current.Windows;
            foreach (Window window in collections)
            {
                BaseWindow win = window as BaseWindow;
                if (win != null)
                {
                    // 其他Window直接关闭
                    if (win.ToString().Contains("LoginWindow"))
                    {
                        win.Show();
                    }
                    else if (win != this)
                    {
                        // 设置不需要关闭页面提示
                        win.needCloseWindowConfirm = false;
                        win.Close();
                    }
                }
            }
            // 设置不需要关闭页面提示
            this.needCloseWindowConfirm = false;
            CloseCurrentWindow();
        }
        /// <summary>
        /// 点击退出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miExit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("确定要退出系统吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
            //关闭窗口
            if (result == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            }
        }
        /// <summary>
        /// 点击贴签系统菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miPrint_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!this.ToString().Contains("PrintWindow") && !this.ToString().Contains("LoginWindow"))
            {
                PrintWindow printWindow = new PrintWindow();
                printWindow.Show();
                this.needCloseWindowConfirm = false;
                CloseCurrentWindow();
            }
        }
        /// <summary>
        /// 点击综合查询菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miQuery_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!this.ToString().Contains("QueryWindow") && !this.ToString().Contains("LoginWindow"))
            {
                QueryWindow queryWindow = new QueryWindow();
                queryWindow.Show();
                this.needCloseWindowConfirm = false;
                CloseCurrentWindow();
            }
        }

        private void CloseCurrentWindow()
        {
            if (this.ToString().Contains("MainWindow"))
            {
                this.Hide();
            }
            else
            {
                this.Close();
            }
        }

        /// <summary>
        /// 实现窗体移动
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            // DragMove();

            base.OnMouseLeftButtonDown(e);
        }

        /// <summary>
        /// 是否超级管理员
        /// </summary>
        /// <returns></returns>
        protected bool isSuperAdmin()
        {
            return UserCache.Printer.user_name == "ydwl" && UserCache.Printer.password == "password01!" && UserCache.Checker.user_name == "ydwl" && UserCache.Checker.password == "password01!";
        }

        protected bool IsAdmin()
        {
            return UserCache.Printer.type_name == "管理员";
        }

        /// <summary>
        /// 设置菜单不可用
        /// </summary>
        protected void SetMenuDisabled()
        {
            // 用户菜单
            printerHeader.IsEnabled = false;
            checkerHeader.IsEnabled = false;
            // 退出按钮
            miExit.IsEnabled = false;
            // 关闭按钮
            CloseButton.IsEnabled = false;
            //设置菜单可用
            menu.IsEnabled = false;
        }
        protected void SetMenuEnabled()
        {
            // 用户菜单
            printerHeader.IsEnabled = true;
            checkerHeader.IsEnabled = true;
            // 退出按钮
            miExit.IsEnabled = true;
            // 关闭按钮
            CloseButton.IsEnabled = true;
            //设置菜单可用
            menu.IsEnabled = true;
        }



        public void BaseWindow_Closing(object sender, CancelEventArgs e)
        {

            if (needCloseWindowConfirm)
            {
                if (this.ToString().Contains("MainWindow"))
                {

                    MessageBoxResult result = MessageBox.Show("确定要退出系统吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    //关闭窗口
                    if (result == MessageBoxResult.Yes)
                    {
                        Environment.Exit(0);
                    }
                    return;
                }
                else
                {
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

                    //MessageBoxResult result = MessageBox.Show("确定关闭当前窗口吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    ////关闭窗口
                    //if (result == MessageBoxResult.Yes)
                    //{
                    //    // 打开主窗口
                    //    var collections = Application.Current.Windows;
                    //    foreach (Window window in collections)
                    //    {
                    //        BaseWindow win = window as BaseWindow;
                    //        if (win != null)
                    //        {
                    //            // 其他Window直接关闭
                    //            if (win.ToString().Contains("MainWindow"))
                    //            {
                    //                win.Show();
                    //            }
                    //        }
                    //    }

                    //    e.Cancel = false;
                    //}
                    ////不关闭窗口
                    //if (result == MessageBoxResult.No)
                    //    e.Cancel = true;
                }
            }
        }
    }
}