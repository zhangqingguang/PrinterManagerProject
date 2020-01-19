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
        private static CurrentQueue currentQueue = CurrentQueue.None;
        private static PLCCommandQueue pLCCommandQueue;

        public static PLCCommandQueue GetInstance(PLCSerialPortInterface serialPortInterface)
        {
            if (pLCCommandQueue == null)
            {
                pLCCommandQueue = new PLCCommandQueue( serialPortInterface);
            }
            return pLCCommandQueue;
        }




        private static bool HasRead = true;
        private static object hasReadHelper = new object();
        DispatcherTimer timer;
        public PLCCommandQueue(PLCSerialPortInterface serialPortInterface)
        {
            this.serialPortInterface = serialPortInterface;
            IsStart = false;
            HasRead = true;
            queue = new ConcurrentQueue<string>();
            priority1Queue = new ConcurrentQueue<CCDSendData>();
            priority2Queue = new ConcurrentQueue<CCDSendData>();
            timer = new DispatcherTimer();
        }
        PLCSerialPortInterface serialPortInterface;
        public bool IsStart { get; set; }
        /// <summary>
        /// 线程安全的队列
        /// </summary>
        private ConcurrentQueue<string> queue { get; set; }
        private ConcurrentQueue<CCDSendData> priority1Queue { get; set; }
        private ConcurrentQueue<CCDSendData> priority2Queue { get; set; }
        public int EnqueueStop(string command)
        {
            string c = "";
            while (queue.IsEmpty == false)
            {
                queue.TryDequeue(out c);
            }
            queue.Enqueue(command);
            return queue.Count;
        }
        public int Enqueue(string command)
        {
            if (IsStart == false)
            {
                return 0;
            }

            //if(queue.Any(s=>s == command) == false)
            //{
            //    // 去除重复指令
                queue.Enqueue(command);
            //}
            return queue.Count;
        }
        /// <summary>
        /// CCD1 指令队列
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public int Enqueue1(CCDSendData command)
        {
            if (IsStart == false)
            {
                return 0;
            }

            priority1Queue.Enqueue(command);
            return priority1Queue.Count;
        }
        /// <summary>
        /// CCD2 指令队列
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public int Enqueue2(CCDSendData command)
        {
            if (IsStart == false)
            {
                return 0;
            }

            priority2Queue.Enqueue(command);
            return priority2Queue.Count;
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
                    if (priority2Queue.IsEmpty == false)
                    {
                        if(currentQueue == CurrentQueue.None || currentQueue == CurrentQueue.CCD2Queue)
                        {
                            CCDSendData prevCommand;
                            // 尝试取出一个
                            if (priority2Queue.TryPeek(out prevCommand))
                            {
                                currentQueue = CurrentQueue.CCD2Queue;
                                // 判断是否已经发送
                                if (prevCommand.IsSending == true)
                                {
                                    if (prevCommand.HasSend == false)
                                    {
                                        // 出队
                                        while (priority2Queue.TryDequeue(out prevCommand) == false)
                                        {

                                        }
                                        TimeWatcher.SendedCCD2Time = DateTime.Now;
                                        myEventLog.LogInfo($"收到CCD2返回信号");
                                        myEventLog.LogInfo($"开始发送指令信号到收到返回信号时间间隔：" + (TimeWatcher.SendedCCD2Time - TimeWatcher.SendCCD2Time).TotalMilliseconds);
                                        // 已经发送：调用回调函数
                                        prevCommand.OnSend();
                                        prevCommand.HasSend = true;
                                        currentQueue = CurrentQueue.None;
                                        continue;
                                    }
                                }
                                else
                                {
                                    lock (hasReadHelper)
                                    {
                                        HasRead = false;
                                    }

                                    prevCommand.IsSending = true;
                                    TimeWatcher.SendCCD2Time = DateTime.Now;
                                    myEventLog.LogInfo($"发送CCD2指令");
                                    myEventLog.LogInfo($"收到信号到开始发送指令信号时间间隔：" + (TimeWatcher.SendCCD2Time - TimeWatcher.Receive84Time).TotalMilliseconds);
                                    myEventLog.LogInfo($"识别出有效信号到开始发送指令信号时间间隔：" + (TimeWatcher.SendCCD2Time - TimeWatcher.ReceiveEffect84Time).TotalMilliseconds);
                                    myEventLog.LogInfo($"正在发送CCD2成功或失败：{prevCommand.Command}");
                                    PLCSerialPortUtils.GetInstance(serialPortInterface).SendData(prevCommand.Command);
                                    continue;
                                }
                            }
                        }
                    }
                    if (priority1Queue.IsEmpty == false)
                    {
                        if (currentQueue == CurrentQueue.None || currentQueue == CurrentQueue.CCD1Queue)
                        {
                            CCDSendData prevCommand;
                            // 尝试取出一个
                            if (priority1Queue.TryPeek(out prevCommand))
                            {
                                currentQueue = CurrentQueue.CCD1Queue;
                                // 判断是否已经发送
                                if (prevCommand.IsSending == true)
                                {
                                    if (prevCommand.HasSend == false)
                                    {
                                        // 出队
                                        while (priority1Queue.TryDequeue(out prevCommand) == false)
                                        {

                                        }
                                        // 已经发送：调用回调函数
                                        prevCommand.OnSend();
                                        prevCommand.HasSend = true;
                                        currentQueue = CurrentQueue.None;
                                        continue;
                                    }
                                }
                                else
                                {
                                    lock (hasReadHelper)
                                    {
                                        HasRead = false;
                                    }

                                    prevCommand.IsSending = true;
                                    myEventLog.LogInfo($"正在发送CCD1成功或失败：{prevCommand.Command}");
                                    PLCSerialPortUtils.GetInstance(serialPortInterface).SendData(prevCommand.Command);
                                    continue;
                                }
                            }
                        }
                    }
                    if (queue.IsEmpty)
                    {
                        continue;
                    }
                    string command = "";

                    if (currentQueue == CurrentQueue.None || currentQueue == CurrentQueue.OtherQueue)
                    {
                        if (queue.TryDequeue(out command))
                        {
                            lock (hasReadHelper)
                            {
                                HasRead = false;
                            }
                            if (command.Contains("WDD0090100901"))
                            {
                                myEventLog.LogInfo($"正在发送CCD1成功或失败：{command}");
                                //myEventLog.LogInfo($"发送信号时间间隔：CCD结果：{(DateTime.Now - prevSendTime).TotalMilliseconds}");
                            }
                            else
                            if (command.Contains("WDD0090000900"))
                            {
                                myEventLog.LogInfo($"正在发送CCD2成功或失败：{command}");
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
                            }
                            else
                            if (command.Contains("R00050"))
                            {
                                myEventLog.LogInfo($"发送启动传送带命令：{command}");
                                //myEventLog.LogInfo($"发送信号时间间隔：停止：{(DateTime.Now - prevSendTime).TotalMilliseconds}");
                            }
                            if (command.Contains("R00051"))
                            {
                                myEventLog.LogInfo($"发送停止传送带命令：{command}");
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
            myEventLog.LogInfo($"剩余指令数量：{queue.Count }");
            myEventLog.LogInfo($"剩余指令数量：{priority1Queue.Count}");
            myEventLog.LogInfo($"剩余指令数量：{priority2Queue.Count}");
            int times = 0;
            while ((queue.Count + priority1Queue.Count + priority2Queue.Count)!=0)
            {
                times++;
                Thread.Sleep(100);
                if (times > 10)
                {
                    break;
                }
            }

            myEventLog.LogInfo($"剩余指令执行完毕");
            IsStart = false;
        }

    }

    public enum CurrentQueue
    {
        CCD2Queue = 0,
        CCD1Queue = 1,
        OtherQueue = 2,
        None=100
    }
}
