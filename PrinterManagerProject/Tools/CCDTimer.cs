using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PrinterManagerProject.Tools
{
    /// <summary>
    /// CCD超时计时器
    /// </summary>
    public interface ICCDTimer
    {
        void StopWait();
        void Start();
        void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e);
    }
    /// <summary>
    /// 监听CCD1超时计时器
    /// </summary>
    public class CCD1Timer : ICCDTimer
    {
        int currentCount=0;
        System.Timers.Timer timer;
        public delegate void FallEventHandler();
        public event FallEventHandler CCD1Expire;

        /// <summary>
        /// 停止等待拍照结果
        /// </summary>
        public void StopWait()
        {
            timer.Stop();
        }

        /// <summary>
        /// 开始等待
        /// </summary>
        public void Start()
        {
            //设置定时间隔(毫秒为单位)
            int interval = 100;
            timer = new System.Timers.Timer(interval);
            //设置执行一次（false）还是一直执行(true)
            timer.AutoReset = true;
            //设置是否执行System.Timers.Timer.Elapsed事件
            timer.Enabled = true;
            //绑定Elapsed事件
            timer.Elapsed += Timer_Elapsed;
        }

        /// <summary>
        /// 每隔100毫秒执行一次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            currentCount++;
            if (currentCount > 15)
            {
                timer.Stop();

                CCD1Expire.Invoke();
            }
        }
    }
    /// <summary>
    /// 监听CCD2超时计时器
    /// </summary>
    public class CCD2Timer : ICCDTimer
    {
        int currentCount = 0;
        System.Timers.Timer timer;
        public delegate void FallEventHandler();
        public event FallEventHandler CCDExpire;

        /// <summary>
        /// 停止等待拍照结果
        /// </summary>
        public void StopWait()
        {
            timer.Stop();
        }

        /// <summary>
        /// 开始等待
        /// </summary>
        public void Start()
        {
            //设置定时间隔(毫秒为单位)
            int interval = 100;
            timer = new System.Timers.Timer(interval);
            //设置执行一次（false）还是一直执行(true)
            timer.AutoReset = true;
            //设置是否执行System.Timers.Timer.Elapsed事件
            timer.Enabled = true;
            //绑定Elapsed事件
            timer.Elapsed += Timer_Elapsed;
        }

        /// <summary>
        /// 每隔100毫秒执行一次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            currentCount++;
            if (currentCount > 15)
            {
                timer.Stop();

                CCDExpire.Invoke();
            }
        }
    }
}
