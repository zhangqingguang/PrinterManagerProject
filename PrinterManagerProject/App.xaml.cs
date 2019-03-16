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

namespace PrinterManagerProject
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            System.Threading.Thread.Sleep(3000);
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
    }
}
