using log4net;
using PrinterManagerProject.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrinterManagerProject
{
    public interface ScanerHandlerSerialPortInterface
    {
        /// <summary>
        /// 接收数据回调
        /// </summary>
        /// <param name="data"></param>
        void OnScannerHandlerDataReceived(string data);
        void OnScannerHandlerError(string msg);
        void OnScannerHandlerComplated();
    }

    public class ScanHandlerSerialPortUtils
    {
        private static ScanHandlerSerialPortUtils serialPortUtils;
        private static SerialPort sp = new SerialPort();

        private static ScanerHandlerSerialPortInterface mSerialPortInterface;

        private ScanHandlerSerialPortUtils() { }

        public static ScanHandlerSerialPortUtils GetInstance(ScanerHandlerSerialPortInterface serialPortInterface)
        {
            if (serialPortUtils == null)
            {
                serialPortUtils = new ScanHandlerSerialPortUtils();

                mSerialPortInterface = serialPortInterface;

                // 读取配置，并给串口通讯类做配置
                InitSerialPort();
            }
            return serialPortUtils;
        }

        /// <summary>
        /// 读取配置，并给串口通讯类做配置
        /// </summary>
        private static void InitSerialPort()
        {
            string COMName = ConfigurationManager.AppSettings.Get("ScanHandlerCOMName");

            sp.PortName = COMName; // 端口
            sp.BaudRate = 115200; // 波特率
            sp.DataBits = 8; // 数据位
            sp.StopBits = StopBits.One; // 1个停止位
            sp.Parity = Parity.None; // 校验位（奇偶性）

            sp.DataReceived += new SerialDataReceivedEventHandler(Com_DataReceived);
            sp.ReceivedBytesThreshold = 2; // 设置触发事件需要的缓存字节长度
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Com_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] ReDatas = new byte[sp.BytesToRead];
            sp.Read(ReDatas, 0, ReDatas.Length);//读取数据
            string result = Encoding.UTF8.GetString(ReDatas);
            
            new LogHelper().SerialPortLog($"接收到手持扫码枪：{result}");

            mSerialPortInterface.OnScannerHandlerDataReceived(result);
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        public bool SendData(string instructions)
        {
            //byte[] data = strToHexByte(instructions);

            if (sp.IsOpen)
            {
                try
                {
                    sp.Write(instructions);//发送数据

                    new LogHelper().SerialPortLog(string.Format("手持扫码枪发送:{0}", instructions));

                    myEventLog.LogInfo($"手持扫码枪发送:{instructions}");

                    return true;
                }
                catch (Exception ex)
                {
                    new LogHelper().ErrorLog( ex.Message);
                    myEventLog.LogError("向手持扫码枪发送数据出错。" +ex.Message,ex);
                }
                finally
                {
                }
            }
            return false;
        }

        /// <summary>
        /// 开启串口通讯
        /// </summary>
        public bool Open()
        {
            try
            {
                if (sp.IsOpen == false)
                {
                    sp.Open();
                }

                if (mSerialPortInterface != null)
                {
                    mSerialPortInterface.OnScannerHandlerComplated();
                }

                myEventLog.Log.Info($"成功打开手持扫码枪串口，{sp.PortName}。");

                return true;
            }
            catch (Exception ex)
            {
                if (mSerialPortInterface != null)
                {
                    mSerialPortInterface.OnScannerHandlerError(ex.Message);
                }
                new LogHelper().ErrorLog(ex.Message);
                myEventLog.LogError($"打开手持扫码枪串口出错{sp.PortName}。" + ex.Message, ex);
                return false;
            }
        }

        /// <summary>
        /// 关闭串口通讯
        /// </summary>
        public bool Close()
        {
            try
            {
                sp.Close();

                if (mSerialPortInterface != null)
                {
                    mSerialPortInterface.OnScannerHandlerComplated();
                }
                myEventLog.LogInfo($"成功关闭手持扫码枪串口，{sp.PortName}。");
                return true;
            }
            catch (Exception ex)
            {
                if (mSerialPortInterface != null)
                {
                    mSerialPortInterface.OnScannerHandlerError(ex.Message);
                }
                new LogHelper().ErrorLog(ex.Message);
                myEventLog.LogError($"关闭手持扫码枪串口出错，{sp.PortName}。" + ex.Message, ex);
                return false;
            }
        }
    }
}
