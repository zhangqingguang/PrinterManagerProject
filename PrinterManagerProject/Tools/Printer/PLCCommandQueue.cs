using PrinterManagerProject.Tools.Serial;
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
        private static bool HasRead = true;
        private static object hasReadHelper = new object();
        DispatcherTimer timer;
        public PLCCommandQueue(PLCSerialPortInterface serialPortInterface)
        {
            this.serialPortInterface = serialPortInterface;
            IsStart = false;
            queue = new ConcurrentQueue<string>();
            priorityQueue = new ConcurrentQueue<CCDSendData>();
            timer = new DispatcherTimer();
        }
        PLCSerialPortInterface serialPortInterface;
        public bool IsStart { get; set; }
        /// <summary>
        /// 线程安全的队列
        /// </summary>
        private ConcurrentQueue<string> queue { get; set; }
        private ConcurrentQueue<CCDSendData> priorityQueue { get; set; }
        public int Enqueue(string command)
        {
            if (IsStart == false)
            {
                return 0;
            }

            queue.Enqueue(command);
            return queue.Count;
        }
        public int Enqueue(CCDSendData command)
        {
            if (IsStart == false)
            {
                return 0;
            }

            priorityQueue.Enqueue(command);
            return priorityQueue.Count;
        }

        public  bool IsEmpty() {
            return queue.IsEmpty;
        }

        public static void SetHasRead()
        {
            lock (hasReadHelper)
            {
                HasRead = true;
            }
        }
        DateTime prevSendTime = DateTime.Now;

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
                while (IsStart)
                {
                    lock (hasReadHelper)
                    {
                        if (HasRead == false)
                        {
                            continue;
                        }
                    }
                    if (priorityQueue.IsEmpty == false)
                    {

                        CCDSendData prevCommand;
                        if (priorityQueue.TryDequeue(out prevCommand))
                        {
                            myEventLog.LogInfo($"正在发送CCD成功或失败：{prevCommand.Command}");
                            PLCSerialPortUtils.GetInstance(serialPortInterface).SendData(prevCommand.Command);
                            prevCommand.OnSend();

                            continue;
                        }
                    }
                    if (queue.IsEmpty)
                    {
                        continue;
                    }
                    string command = "";
                    if (queue.TryDequeue(out command))
                    {
                        lock (hasReadHelper)
                        {
                            HasRead = false;
                        }
                        if (command.Contains("WDD0090100901"))
                        {
                            myEventLog.LogInfo($"正在发送CCD成功或失败：{command}");
                            //myEventLog.LogInfo($"发送信号时间间隔：CCD结果：{(DateTime.Now - prevSendTime).TotalMilliseconds}");
                        }
                        else
                        if (command.Contains("00201"))
                        {
                            myEventLog.LogInfo($"正在发送开始打印命令：{command}");
                            //myEventLog.LogInfo($"发送信号时间间隔：发送：{(DateTime.Now - prevSendTime).TotalMilliseconds}");
                        }
                        else
                        if (command.Contains("00291"))
                        {
                            myEventLog.LogInfo($"正在发送停止打印命令：{command}");
                            //myEventLog.LogInfo($"发送信号时间间隔：停止：{(DateTime.Now - prevSendTime).TotalMilliseconds}");
                        }                        else
                        if (command.Contains("R00050"))
                        {
                            myEventLog.LogInfo($"发送启动传送带命令：{command}");
                            //myEventLog.LogInfo($"发送信号时间间隔：停止：{(DateTime.Now - prevSendTime).TotalMilliseconds}");
                        }
                        if (command.Contains("R00051"))
                        {
                            myEventLog.LogInfo($"发送停止传送单命令：{command}");
                            //myEventLog.LogInfo($"发送信号时间间隔：停止：{(DateTime.Now - prevSendTime).TotalMilliseconds}");
                        }
                        else
                        {
                            //myEventLog.LogInfo($"发送信号时间间隔：读取：{(DateTime.Now - prevSendTime).TotalMilliseconds}");
                        }
                        //prevSendTime = DateTime.Now;
                        PLCSerialPortUtils.GetInstance(serialPortInterface).SendData(command);
                        //Thread.Sleep(30);
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
