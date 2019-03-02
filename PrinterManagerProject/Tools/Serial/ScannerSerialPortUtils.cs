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
    public interface ScannerSerialPortInterface
    {
        /// <summary>
        /// 接收数据回调
        /// </summary>
        /// <param name="data"></param>
        void OnScannerDataReceived(string data);
        void OnScannerError(string msg);
        void OnScannerComplated();
    }

    public class ScannerSerialPortUtils
    {
        private static ScannerSerialPortUtils serialPortUtils;
        private static SerialPort sp = new SerialPort();

        private static ScannerSerialPortInterface mSerialPortInterface;

        private ScannerSerialPortUtils() { }

        public static ScannerSerialPortUtils GetInstance(ScannerSerialPortInterface serialPortInterface)
        {
            if (serialPortUtils == null)
            {
                serialPortUtils = new ScannerSerialPortUtils();

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
            string COMName = ConfigurationManager.AppSettings.Get("ScanCOMName");

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

            new LogHelper().SerialPortLog($"扫码枪接收:{result}");

            mSerialPortInterface.OnScannerDataReceived(result);
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

                    new LogHelper().SerialPortLog($"扫码枪发送:{instructions}");

                    myEventLog.LogInfo($"扫码枪发送:{instructions}");

                    return true;
                }
                catch (Exception ex)
                {
                    new LogHelper().ErrorLog(ex.Message);
                    myEventLog.LogError($"向自动扫码枪发送数据出错，{sp.PortName}。" + ex.Message, ex);
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
                if (sp.IsOpen)
                {
                    return true;
                }
                sp.Open();

                if (mSerialPortInterface != null)
                {
                    myEventLog.LogInfo($"成功打开自动扫码枪串口，{sp.PortName}。");
                    mSerialPortInterface.OnScannerComplated();
                }
                return true;
            }
            catch (Exception ex)
            {
                if (mSerialPortInterface != null)
                {
                    mSerialPortInterface.OnScannerError(ex.Message);
                }
                new LogHelper().ErrorLog(ex.Message);
                myEventLog.LogError($"打开自动扫码枪串口出错，{sp.PortName}。" + ex.Message, ex);
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
                    mSerialPortInterface.OnScannerComplated();
                    myEventLog.LogInfo($"成功关闭自动扫码枪串口，{sp.PortName}。");
                }
                return true;
            }
            catch (Exception ex)
            {
                if (mSerialPortInterface != null)
                {
                    mSerialPortInterface.OnScannerError(ex.Message);
                }
                new LogHelper().ErrorLog(ex.Message);
                myEventLog.LogError($"关闭自动扫码枪串口出错，{sp.PortName}。" + ex.Message, ex);
                return false;
            }
        }
    }
}
