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
    public interface PLCSerialPortInterface
    {
        /// <summary>
        /// 接收数据回调
        /// </summary>
        /// <param name="data"></param>
        void OnPLCDataReceived(string data);
        
        void OnPLCError(string msg);
        
        void OnPLCComplated(int type);
    }

    /// <summary>
    /// 串口指令
    /// </summary>
    public class PLCSerialPortData
    {

        #region 发送-开关机数据

        /// <summary>
        /// 启动机器
        /// </summary>
        public const string MACHINE_START = "1Y";
        /// <summary>
        /// 停止机器
        /// </summary>
        public const string MACHINE_STOP = "1N";

        #endregion

        #region 发送-剔除信号

        /// <summary>
        /// 1号点
        /// 不是本组-剔除
        /// CCD1返回的结果和数据对比不存在
        /// </summary>
        public const string DOT1_OUT = "2N";

        /// <summary>
        /// 2号点
        /// 扫码枪扫描的结果和CCD2识别的结果一致
        /// 通过
        /// </summary>
        public const string DOT2_PASS = "4Y";

        /// <summary>
        /// 2号点
        /// 扫码枪扫描的结果和CCD2识别的结果不一致
        /// 剔除
        /// </summary>
        public const string DOT2_OUT = "4N";

        #endregion

        #region 接收-拍照指令

        /// <summary>
        /// CCD1光幕扫描，要通知CCD1拍照
        /// </summary>
        public const string CCD1_TACK_PICTURE = "81\r";

        /// <summary>
        /// CCD2光幕扫描，要通知CCD2拍照
        /// </summary>
        public const string CCD2_TACK_PICTURE = "84\r";

        #endregion

        #region 接收-打印指令

        ///// <summary>
        ///// 打印机开始打印
        ///// </summary>
        //public const string PRINT = "82\r";

        #endregion

        #region 接收-报错数据

        /// <summary>
        /// 错误
        /// </summary>
        public const string ERROR = "1Z\r";

        #endregion

        #region 接收-系统重置完成

        /// <summary>
        /// 错误
        /// </summary>
        public const string RESET_COMPLATE = "1R\r";

        #endregion

        #region 接收-过光幕

        /// <summary>
        /// 打印光幕
        /// </summary>
        public const string LIGHT_PASS = "82\r";

        #endregion

        #region 接收-超时停止

        /// <summary>
        /// 错误
        /// </summary>
        public const string OVER_TIME = "1O\r";

        #endregion

        #region 接收-缺少标签

        /// <summary>
        /// 错误
        /// </summary>
        public const string NO_PAPER = "2Z\r";

        #endregion

        #region 接收-缺少色带

        /// <summary>
        /// 错误
        /// </summary>
        public const string NO_COLOR = "3Z\r";

        #endregion

        #region 指令方法

        /// <summary>
        /// 根据CCD返回的命令
        /// 获取溶媒尺寸命令
        /// </summary>
        /// <param name="spec">毫升规格</param>
        /// <param name="type">瓶子规格</param>
        /// <returns>溶媒尺寸命令</returns>
        public static string GetSizeCmd(string spec, string type)
        {
            switch (spec)
            {
                //500ml
                case "B0":
                case "B4":
                case "B8":
                case "BC":
                case "BF":
                    //瓶装
                    if (type == "C0")
                    {
                        return "73";
                    }
                    else
                    { //袋装
                        return "76";
                    }
                //250ml
                case "B1":
                case "B5":
                case "B9":
                case "BD":
                    //瓶装
                    if (type == "C0")
                    {
                        return "72";
                    }
                    else
                    { //袋装
                        return "75";
                    }
                //100ml
                case "B2":
                case "B6":
                case "BA":
                case "BE":
                    //瓶装
                    if (type == "C0")
                    {
                        return "71";
                    }
                    else
                    { //袋装
                        return "74";
                    }
                //50ml
                case "B3":
                case "B7":
                case "BB":
                    //瓶装
                    if (type == "C0")
                    {
                        return "77";
                    }
                    else
                    { //袋装
                        return "78";
                    }
                default:
                    return "";
            }

        }

        #endregion
    }

    public class PLCSerialPortUtils
    {
        // 缓存收到的数据
        private static string buffer = "";
        // 锁定buffer
        private static object LockReceiveBuffer = new object();


        private static PLCSerialPortUtils serialPortUtils;
        private static SerialPort sp = new SerialPort();


        private static PLCSerialPortInterface mSerialPortInterface;

        public static int MAX_BUFFER_LEN = 5000;

        private PLCSerialPortUtils() { }

        public static PLCSerialPortUtils GetInstance(PLCSerialPortInterface serialPortInterface)
        {
            if (serialPortUtils == null)
            {
                serialPortUtils = new PLCSerialPortUtils();

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
            string COMName = ConfigurationManager.AppSettings.Get("PLCCOMName");

            sp.PortName = COMName; // 端口
            sp.BaudRate = 115200; // 波特率
            sp.DataBits = 8; // 数据位
            sp.StopBits = StopBits.One; // 1个停止位
            sp.Parity = Parity.None; // 校验位（奇偶性）
            sp.RtsEnable = true;
            sp.DtrEnable = true;

            sp.DataReceived += new SerialDataReceivedEventHandler(Com_DataReceived);
            sp.ReceivedBytesThreshold = PLCSerialPortData.ERROR.Length; // 设置触发事件需要的缓存字节长度
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Com_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //Task.Run(() => {
                //myEventLog.LogInfo($"PLC接收数据：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")}");
                byte[] ReDatas = new byte[sp.BytesToRead];
                sp.Read(ReDatas, 0, ReDatas.Length);//读取数据
            lock (LockReceiveBuffer)
            {
                var result = Encoding.UTF8.GetString(ReDatas);
                //myEventLog.LogInfo($"收到PLC信号：{result}。");

                buffer += Encoding.UTF8.GetString(ReDatas);
            }
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
                    if (instructions.Contains("WCS") || instructions.Contains("WDD"))
                    {
                        myEventLog.LogInfo($"发送给PLC:{instructions}");
                    }
                    var startTime = DateTime.Now;
                    sp.Write(instructions + "\r\n");//发送数据
                    if (instructions.Contains("WCS") || instructions.Contains("WDD"))
                    {
                        myEventLog.LogInfo($"数据发送成功");
                    }

                    //myEventLog.LogInfo($"发送给PLC花费时间:{(DateTime.Now - startTime).TotalMilliseconds}");
                    //new LogHelper().SerialPortLog($"发送给PLC:{instructions}");

                    //myEventLog.LogInfo($"发送给PLC:{instructions}");

                    return true;
                }
                catch (Exception ex)
                {
                    new LogHelper().ErrorLog(ex.Message);
                    myEventLog.LogError("向PLC串口发送数据出错，"+sp.PortName+"。"+ex.Message,ex);
                }
                finally
                {
                    Thread.Sleep(50);
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
                sp.Open();

                if (mSerialPortInterface != null)
                {
                    mSerialPortInterface.OnPLCComplated(1);
                    myEventLog.LogInfo($"成功打开PLC串口，{sp.PortName}。");
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
                                //myEventLog.LogInfo($"读取到PLC信号：{result}。");

                                buffer = buffer.Substring(endIndex + 1, buffer.Length - endIndex - 1);

                                mSerialPortInterface.OnPLCDataReceived(result);
                            }
                        }
                        // 读取信号间隔时间
                        Thread.Sleep(20);
                    }
                });
                return true;
            }
            catch (Exception ex)
            {
                if(mSerialPortInterface != null)
                {
                    mSerialPortInterface.OnPLCError(ex.Message);
                }
                new LogHelper().ErrorLog(ex.Message);
                myEventLog.LogError($"PLC串口打开失败，{sp.PortName}。"+ex.Message, ex);
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
                    mSerialPortInterface.OnPLCComplated(0);
                    myEventLog.LogInfo($"成功关闭PLC串口，{sp.PortName}。");
                }
                return true;
            }
            catch (Exception ex)
            {
                if (mSerialPortInterface != null)
                {
                    mSerialPortInterface.OnPLCError(ex.Message);
                }
                new LogHelper().ErrorLog(ex.Message);
                myEventLog.LogError($"PLC串口关闭出错，{sp.PortName}。" +ex.Message, ex);
                return false;
            }
        }
    }
}
