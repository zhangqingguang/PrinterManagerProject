using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrinterManagerProject.Tools
{
    public interface PressSerialPortInterface
    {
        void OnPressSerialReceived(string data);

        void OnPressSerialError(string msg);

        void OnPressSerialComplated(int type);
    }
    public class PressSerialPortUtils
    {
        private static object LockReceiveBuffer = new object();
        private static SerialPort sp = new SerialPort();
        
        private static PressSerialPortInterface mSerialPortInterface;
        private static PressSerialPortUtils pressSerialPortUtils;
        
        public static int press500Interval;
        public static int press250Interval;
        public static int press100Interval;

        // 缓存收到的数据
        private static string buffer = "";
        private static string ERROR = "1Z\r";
        
        public static PressSerialPortUtils GetInstance(PressSerialPortInterface serialPortInterface)
        {
            if (pressSerialPortUtils == null)
            {
                pressSerialPortUtils = new PressSerialPortUtils();

                mSerialPortInterface = serialPortInterface;

                // 读取配置，并给串口通讯类做配置
                InitSerialPort();
            }
            return pressSerialPortUtils;
        }

        /// <summary>
        /// 读取配置，并给串口通讯类做配置
        /// </summary>
        private static void InitSerialPort()
        {
            string PressCOMName = ConfigurationManager.AppSettings.Get("PressCOMName");
            string PressRate = ConfigurationManager.AppSettings.Get("PressRate");

            string Press500Interval = ConfigurationManager.AppSettings.Get("Press500Interval");
            string Press250Interval = ConfigurationManager.AppSettings.Get("Press250Interval");
            string Press100Interval = ConfigurationManager.AppSettings.Get("Press100Interval");

            press500Interval = int.Parse(Press500Interval);
            press250Interval = int.Parse(Press250Interval);
            press100Interval = int.Parse(Press100Interval);

            sp.PortName = PressCOMName; // 端口
            sp.BaudRate = int.Parse(PressRate); // 波特率
            sp.DataBits = 8; // 数据位
            sp.StopBits = StopBits.One; // 1个停止位
            sp.Parity = Parity.None; // 校验位（奇偶性）
            sp.RtsEnable = true;
            sp.DtrEnable = true;

            sp.DataReceived += new SerialDataReceivedEventHandler(Com_DataReceived);
            sp.ReceivedBytesThreshold = ERROR.Length; // 设置触发事件需要的缓存字节长度
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
            // 收到信号后，设置抚标记可写
            lock (LockReceiveBuffer)
            {
                var result = Encoding.UTF8.GetString(ReDatas);

                TimeWatcher.Receive84Time = DateTime.Now;
                myEventLog.LogInfo($"收到抚标机信号：{result}；{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}。");
            }
        }
        /// <summary>
        /// 根据规格发送数据
        /// </summary>
        /// <param name="sender">spec</param>
        public void SendData(string spec)
        {
            myEventLog.LogInfo($"准备向抚标机发送规格：{spec}；{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}。");
            byte[] Buffer = new byte[15];
            Buffer[0] = 0x5A;
            Buffer[1] = 0xA9;
            Buffer[2] = 0x16;
            switch (spec)
            {
                case "100ml":
                    Buffer[4] = 0xD0;
                    break;
                case "250ml":
                    Buffer[4] = 0xC0;
                    break;
                case "500ml":
                    Buffer[4] = 0xA0;
                    break;
                default:
                    return;
            }
            SendData(Buffer);
        }
        /// <summary>
        /// 向抚标机发送数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private bool SendData(byte[] buffer)
        {
            //byte[] data = strToHexByte(instructions);
            if (sp.IsOpen)
            {
                try
                {
                    string sendData = byteToHexStr(buffer);
                    new LogHelper().SerialPortLog($"向抚标机发送:{sendData}");
                    myEventLog.LogInfo($"发送到抚标机：{sendData}");

                    sp.Write(buffer,0, buffer.Length);//发送数据

                    return true;
                }
                catch (Exception ex)
                {
                    new LogHelper().ErrorLog(ex.Message);
                    myEventLog.LogError("向抚标记串口发送数据出错，" + sp.PortName + "。" + ex.Message, ex);
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
                    mSerialPortInterface.OnPressSerialComplated(1);
                    myEventLog.LogInfo($"成功打开抚标记串口，{sp.PortName}。");
                }
                Task.Run(() => {
                    //string bufferStr = "";
                    //int frameLen = 1;
                    while (sp.IsOpen)
                    {
                        lock (LockReceiveBuffer)
                        {
                            int endIndex = buffer.IndexOf('\r');
                            if (endIndex > 0)
                            {
                                var result = buffer.Substring(0, endIndex);
                                //myEventLog.LogInfo($"读取到抚标记信号：{result}。");

                                buffer = buffer.Substring(endIndex + 1, buffer.Length - endIndex - 1);

                                mSerialPortInterface.OnPressSerialReceived(result);
                            }
                        }
                        // 读取信号间隔时间
                        Thread.Sleep(5);
                    }
                });
                //将电机电源开启
                SendStartPowerDown();
                SendStartPressDown();
                //将电机复位到零点
                SendResetZeroData();
                return true;
            }
            catch (Exception ex)
            {
                if (mSerialPortInterface != null)
                {
                    mSerialPortInterface.OnPressSerialError(ex.Message);
                }
                new LogHelper().ErrorLog(ex.Message);
                myEventLog.LogError($"抚标记串口打开失败，{sp.PortName}。" + ex.Message, ex);
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
                    mSerialPortInterface.OnPressSerialComplated(0);
                    myEventLog.LogInfo($"成功关闭抚标记串口，{sp.PortName}。");
                }
                //将电机电源关闭
                SendClosePressDown();
                SendClosePowerDown();
                return true;
            }
            catch (Exception ex)
            {
                if (mSerialPortInterface != null)
                {
                    mSerialPortInterface.OnPressSerialError(ex.Message);
                }
                new LogHelper().ErrorLog(ex.Message);
                myEventLog.LogError($"抚标记串口关闭出错，{sp.PortName}。" + ex.Message, ex);
                return false;
            }
        }



        /// <summary>
        /// 开启控制板电源
        /// </summary>
        /// <returns></returns>
        private bool SendStartPowerDown()
        {
            byte[] Buffer = new byte[15];
            Buffer[0] = 0x5A;
            Buffer[1] = 0xA9;
            Buffer[2] = 0x03;
            if (SendData(Buffer))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 开启按下控制电源
        /// </summary>
        /// <returns></returns>
        private bool SendStartPressDown()
        {
            byte[] Buffer = new byte[15];
            Buffer[0] = 0x5A;
            Buffer[1] = 0xA9;
            Buffer[2] = 0x01;
            if (SendData(Buffer))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 复位零点位置
        /// </summary>
        /// <returns></returns>
        private bool SendResetZeroData()
        {
            byte[] Buffer = new byte[15];
            Buffer[0] = 0x5A;
            Buffer[1] = 0xA9;
            Buffer[2] = 0x17;

            if (SendData(Buffer))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 关闭按下控制电源
        /// </summary>
        /// <returns></returns>
        private bool SendClosePressDown()
        {
            byte[] Buffer = new byte[15];
            Buffer[0] = 0x5A;
            Buffer[1] = 0xA9;
            Buffer[2] = 0x02;

            if (SendData(Buffer))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 关闭控制板电源
        /// </summary>
        /// <returns></returns>
        private bool SendClosePowerDown()
        {
            byte[] Buffer = new byte[15];
            Buffer[0] = 0x5A;
            Buffer[1] = 0xA9;
            Buffer[2] = 0x04;

            if (SendData(Buffer))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// byte[] 转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }
    }
}
