using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using PrinterManagerProject.EF;
using System.Windows.Threading;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PrinterManagerProject
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        System.Threading.Mutex mutex;
        protected override void OnStartup(StartupEventArgs e)
        {
            this.Startup += App_Startup;
            base.OnStartup(e);
            MapperConfig.Config();

            this.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);

            //非UI线程未捕获异常处理事件(例如自己创建的一个子线程)
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            //Task线程内未捕获异常处理事件
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            //处理完后，我们需要将Handler=true表示已此异常已处理过
            myEventLog.LogError("捕获到全局异常：" + e.Exception.Message, e.Exception);
            MessageBox.Show("程序出现异常，请联系支持");
            e.Handled = true;
        }

        //非UI线程未捕获异常处理事件(例如自己创建的一个子线程)
        //如果UI线程异常DispatcherUnhandledException未注册，则如果发生了UI线程未处理异常也会触发此异常事件
        //此机制的异常捕获后应用程序会直接终止。没有像DispatcherUnhandledException事件中的Handler=true的处理方式，可以通过比如Dispatcher.Invoke将子线程异常丢在UI主线程异常处理机制中处理
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            if (ex != null)
            {
                // string msg = String.Format("{0}\n\n{1}", ex.Message, ex.StackTrace);//异常信息 和 调用堆栈信息
                //  MessageBox.Show(msg, "非UI线程异常");
                myEventLog.Log.Error("非UI线程异常", ex);
            }
        }

        //Task线程内未捕获异常处理事件
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            //    string msg = String.Format("{0}\n\n{1}", ex.Message, ex.StackTrace);
            // MessageBox.Show(msg, "Task异常");
            myEventLog.Log.Error("Task异常", ex);
            e.SetObserved();

        }
        #region 设置软件不能重复打开


        private void App_Startup(object sender, StartupEventArgs e)
        {
            bool ret;
            mutex = new System.Threading.Mutex(true, "PrinterManagerProject", out ret);

            if (!ret)
            {

                Process process = RunningInstance();
                if (process != null)
                {
                    //MessageBox.Show("应用程序已经在运行中。。。");
                    HandleRunningInstance(process);
                    //System.Threading.Thread.Sleep(1000);  
                    System.Environment.Exit(1);
                }
            }
        }
        /// <summary>  
        /// 该函数设置由不同线程产生的窗口的显示状态  
        /// </summary>  
        /// <param name="hWnd">窗口句柄</param>  
        /// <param name="cmdShow">指定窗口如何显示。查看允许值列表，请查阅ShowWlndow函数的说明部分</param>  
        /// <returns>如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零</returns>  
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        /// <summary>  
        ///  该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。  
        ///  系统给创建前台窗口的线程分配的权限稍高于其他线程。   
        /// </summary>  
        /// <param name="hWnd">将被激活并被调入前台的窗口句柄</param>  
        /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零</returns>  
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int SW_SHOWNOMAL = 1;

        private static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, SW_SHOWNOMAL);   //显示  
            SetForegroundWindow(instance.MainWindowHandle); //当到最前端  
        }
        public static Process RunningInstance()
        {
            Process currentProcess = Process.GetCurrentProcess();
            Process[] Processes = Process.GetProcessesByName(currentProcess.ProcessName);
            foreach (Process process in Processes)
            {
                if (process.Id != currentProcess.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == currentProcess.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }
        #endregion
    }
}
