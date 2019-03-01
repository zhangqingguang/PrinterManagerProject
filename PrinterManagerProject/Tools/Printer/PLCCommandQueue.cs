using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace PrinterManagerProject.Tools
{
    /// <summary>
    /// 向PLC发送信号队列
    /// </summary>
    public class PLCCommandQueue
    {
        DispatcherTimer timer;
        public PLCCommandQueue(PLCSerialPortInterface serialPortInterface)
        {
            this.serialPortInterface = serialPortInterface;
            IsStart = false;
            queue = new ConcurrentQueue<string>();
            timer = new DispatcherTimer();
        }
        PLCSerialPortInterface serialPortInterface;
        public bool IsStart { get; set; }
        /// <summary>
        /// 线程安全的队列
        /// </summary>
        private ConcurrentQueue<string> queue { get; set; }
        private DateTime dateTime = DateTime.Now;
        public int Enqueue(string command)
        {
            if (IsStart == false)
            {
                return 0;
            }
            queue.Enqueue(command);
            return queue.Count;
        }

        public bool IsEmpty() {
            return queue.IsEmpty;
        }

        public void Start()
        {
            //if (IsStart == false)
            //{
            //    queue = new ConcurrentQueue<string>();
            //    timer = new DispatcherTimer();
            //    timer = new System.Windows.Threading.DispatcherTimer();
            //    // 当间隔时间过去时发生的事件
            //    timer.Tick += Timer_Tick; ; ;
            //    timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            //    timer.Start();
            //    IsStart = true;
            //}
            IsStart = true;
            Task.Run(() =>
            {
                while (IsStart || queue.IsEmpty==false)
                {
                    string command = "";
                    if (queue.TryDequeue(out command))
                    {
                        //myEventLog.LogInfo($"发送信号时间间隔：{(DateTime.Now - dateTime).TotalMilliseconds}");
                        //dateTime = DateTime.Now;
                        if (command.Contains("WDD0090100901"))
                        {
                            myEventLog.LogInfo($"正在发送内容：{command}");
                        }
                        if (command.Contains("00201"))
                        {
                            myEventLog.LogInfo($"正在发送内容：{command}");
                        }
                        if (command.Contains("00291"))
                        {
                            myEventLog.LogInfo($"正在发送内容：{command}");
                        }
                        PLCSerialPortUtils.GetInstance(serialPortInterface).SendData(command);
                        Thread.Sleep(25);
                    }
                    else
                    {
                        Thread.Sleep(5);
                    }
                }
            });
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //lock (this)
            //{
            //    myEventLog.LogInfo($"Timer执行");

            //    string command = "";
            //    if (queue.TryDequeue(out command))
            //    {
            //        PLCSerialPortUtils.GetInstance(serialPortInterface).SendData(command);
            //    }
            //}
        }

        public void Stop()
        {
            IsStart = false;
        }

    }
}
