using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrinterManagerProject.Tools
{
    /// <summary>
    /// PLC寄存器监听
    /// </summary>
    public class PLCReader
    {
        private PLCSerialPortUtils plcUtils;
        private Task task=null;
        private int Frequent=100;
        private string data;
        private bool isStop=false;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serialPortInterface">PLC</param>
        /// <param name="frequent">发送监听时间间隔（单位毫秒）</param>
        /// <param name="data">发送内容</param>
        public PLCReader(PLCSerialPortInterface serialPortInterface,int frequent,string data)
        {
            plcUtils = PLCSerialPortUtils.GetInstance(serialPortInterface);
            Frequent = frequent;
            this.data = data;
        }
        /// <summary>
        /// 开始监听
        /// </summary>
        public void Start()
        {
            task?.Dispose();
            isStop = false;
            myEventLog.LogInfo($"开始监听");

            task = Task.Run(() =>
            {
                while (isStop==false)
                {
                    plcUtils.SendData(data);
                    Thread.Sleep(Frequent);
                }
                myEventLog.LogInfo($"停止监听");

            });
        }
        /// <summary>
        /// 停止监听
        /// </summary>
        public void Stop()
        {
            isStop = true;
        }
    }


}
