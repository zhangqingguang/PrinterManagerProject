using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

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
        private MenuItem miChangePassword;
        private MenuItem miLogout;
        private MenuItem miExit;
        private MenuItem miPrint;
        private MenuItem miQuery;

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
                    // 初始化控件
                    lblTime = metroWindowTemplate.FindName("lblTime", this) as Label;
                    miChangePassword = metroWindowTemplate.FindName("miChangePassword", this) as MenuItem;
                    miLogout = metroWindowTemplate.FindName("miLogout", this) as MenuItem;
                    miExit = metroWindowTemplate.FindName("miExit", this) as MenuItem;
                    miPrint = metroWindowTemplate.FindName("miPrint", this) as MenuItem;
                    miQuery = metroWindowTemplate.FindName("miQuery", this) as MenuItem;

                    // 事件绑定
                    miChangePassword.Click += miChangePassword_Click;
                    miLogout.Click += miLogout_Click;
                    miExit.Click += miExit_Click;
                    miPrint.Click += miPrint_Click;
                    miQuery.Click += miQuery_Click;

                    dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                    // 当间隔时间过去时发生的事件
                    dispatcherTimer.Tick += new EventHandler(ShowCurrentTime);
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
                    dispatcherTimer.Start();
                }
            }
        }

        public void ShowCurrentTime(object sender, EventArgs e)
        {
            if (!this.ToString().Contains("LoginWindow"))
            {
                this.lblTime.Content = "当前时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss dddd");
            }
        }

        private void CloseButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }

        private void MinButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void miChangePassword_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Show("Comming soon!");
        }

        private void miLogout_Click(object sender, System.Windows.RoutedEventArgs e)
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
            this.Close();
        }

        private void miExit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("确定要退出系统吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
            //关闭窗口
            if (result == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            }
        }

        private void miPrint_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!this.ToString().Contains("PrintWindow") && !this.ToString().Contains("LoginWindow"))
            {
                PrintWindow printWindow = new PrintWindow();
                printWindow.Show();
                this.needCloseWindowConfirm = false;
                this.Close();
            }
        }

        private void miQuery_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!this.ToString().Contains("QueryWindow") && !this.ToString().Contains("LoginWindow"))
            {
                QueryWindow queryWindow = new QueryWindow();
                queryWindow.Show();
                this.needCloseWindowConfirm = false;
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
    }
}
