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
        private int Frequent=1000;
        private string data;
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
        }
        /// <summary>
        /// 开始监听
        /// </summary>
        public void Start()
        {
            task?.Dispose();

            task = Task.Run(() =>
            {
                plcUtils.SendData(data);
                Thread.Sleep(Frequent);
            });
        }
        /// <summary>
        /// 停止监听
        /// </summary>
        public void Stop()
        {
            task.Dispose();
        }
    }


}
