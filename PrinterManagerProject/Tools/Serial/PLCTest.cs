using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterManagerProject.Tools.Serial
{
    public class PLCTest: PLCSerialPortInterface
    {
        private PLCSerialPortUtils plcUtils;
        public PLCTest()
        {

            plcUtils = PLCSerialPortUtils.GetInstance(this);
            plcUtils.Open();
        }

        public void SendData(string data)
        {
            myEventLog.LogInfo($"PLC发送数据：{data}，{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")}");
            plcUtils.SendData(data);
        }
        public void OnPLCDataReceived(string data)
        {
            myEventLog.LogInfo($"PLC接收数据：{data}，{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")}");
        }

        public void OnPLCError(string msg)
        {
            myEventLog.LogInfo($"PLC出错：{msg}");
        }

        public void OnPLCComplated()
        {
        }
    }
}
