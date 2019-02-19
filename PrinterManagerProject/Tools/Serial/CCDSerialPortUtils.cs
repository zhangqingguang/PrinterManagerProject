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
    public interface CCDSerialPortInterface
    {
        /// <summary>
        /// 接收数据回调
        /// </summary>
        /// <param name="data"></param>
        void OnCCDDataReceived(string data);

        void OnCCD1Error(string msg);
        void OnCCD2Error(string msg);

        void OnCCD1Complated();
        void OnCCD2Complated();
    }

    /// <summary>
    /// 串口指令
    /// </summary>
    public class CCDSerialPortData
    {
        #region 接收-无法识别

        /// <summary>
        /// CCD1识别错误
        /// </summary>
        public const string CCD1_ERROR = "FE A1 E0 00 00 00 00 EF";

        /// <summary>
        /// CCD2识别错误
        /// </summary>
        public const string CCD2_ERROR = "FE A3 E0 00 00 00 00 EF";

        #endregion

        #region 发送-拍照指令

        /// <summary>
        /// CCD1执行拍照
        /// </summary>
        public const string TAKE_PICTURE1 = "A1";

        /// <summary>
        /// CCD2执行拍照
        /// </summary>
        public const string TAKE_PICTURE2 = "A3";

        #endregion

        #region 基础数据
        
        /// <summary>
        /// 所有规格溶媒对应的指令
        /// </summary>
        public static Dictionary<string, string> specDic = new Dictionary<string, string>() { 
            {"B0", "0.9％氯化钠|500ml"},
            {"B1", "0.9％氯化钠|250ml"},
            {"B2", "0.9％氯化钠|100ml"},
            {"B3", "0.9％氯化钠|50ml" },
            {"B4", "5％葡萄糖|500ml"},
            {"B5", "5％葡萄糖|250ml"},
            {"B6", "5％葡萄糖|100ml"},
            {"B7", "5％葡萄糖|50ml" },
            {"B8", "10％葡萄糖|500ml"},
            {"B9", "10％葡萄糖|250ml"},
            {"BA", "10％葡萄糖|100ml"},
            {"BB", "10％葡萄糖|50ml" },
            {"BC", "葡萄糖氯化钠|500ml"},
            {"BD", "葡萄糖氯化钠|250ml"},
            {"BE", "葡萄糖氯化钠|100ml"},
            {"BF", "复方氯化钠|500ml"}
        };

        /// <summary>
        /// 袋装规格指令
        /// </summary>
        public static Dictionary<string, string> typeDic = new Dictionary<string, string>() { 
            {"C0", "非PVC双阀"},
            {"C1", "直立式袋"}
        };

        /// <summary>
        /// 获取规格和容量信息
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static string[] GetNameAndML(string cmd)
        {
            foreach (var item in specDic)
            {
                if (item.Key == cmd)
                {
                    return item.Value.Split('|');
                }
            }
            return null;
        }

        /// <summary>
        /// 获取药袋规格信息
        /// </summary>
        /// <returns></returns>
        public static string GetTypeValue(string cmd)
        {
            foreach (var item in typeDic)
            {
                if (item.Key == cmd)
                {
                    return item.Value;
                }
            }
            return null;
        }

        #endregion
    }

    public class CCDSerialPortUtils
    {
        private static CCDSerialPortUtils serialPortUtils;
        private static SerialPort sp = new SerialPort();

        private static CCDSerialPortInterface mSerialPortInterface;

        private CCDSerialPortUtils() { }

        public static CCDSerialPortUtils GetInstance(CCDSerialPortInterface serialPortInterface)
        {
            if (serialPortUtils == null)
            {
                serialPortUtils = new CCDSerialPortUtils();

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
            string COMName = ConfigurationManager.AppSettings.Get("CCDCOMName");

            sp.PortName = COMName; // 端口
            sp.BaudRate = 9600; // 波特率
            sp.DataBits = 8; // 数据位
            sp.StopBits = StopBits.One; // 1个停止位
            sp.Parity = Parity.None; // 校验位（奇偶性）

            sp.DataReceived += new SerialDataReceivedEventHandler(Com_DataReceived);
            sp.ReceivedBytesThreshold = CCDSerialPortData.CCD1_ERROR.Length; // 设置触发事件需要的缓存字节长度
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

            new LogHelper().SerialPortLog($"接收CCD：{result}");
            
            mSerialPortInterface.OnCCDDataReceived(result);
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
                    
                    new LogHelper().SerialPortLog($"CCD发送:{instructions}");
                    myEventLog.LogInfo($"发送到CCD：{instructions}");

                    return true;
                }
                catch (Exception ex)
                {
                    new LogHelper().ErrorLog(ex.Message);
                    myEventLog.LogError("向CCD发送数据出错，"+sp.PortName+"。"+ex.Message, ex);
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
                    mSerialPortInterface.OnCCD1Complated();
                    mSerialPortInterface.OnCCD2Complated();
                    myEventLog.LogInfo($"成功打开CCD1和CCD2串口，{sp.PortName}。");

                }
                return true;
            }
            catch (Exception ex)
            {
                if (mSerialPortInterface != null)
                {
                    mSerialPortInterface.OnCCD1Error(ex.Message);
                    mSerialPortInterface.OnCCD2Error(ex.Message);
                }
                new LogHelper().ErrorLog(ex.Message);
                myEventLog.LogError("CCD串口打开出错，"+sp.PortName+"。"+ex.Message, ex);
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
                    mSerialPortInterface.OnCCD1Complated();
                    mSerialPortInterface.OnCCD2Complated();
                    myEventLog.LogInfo($"成功关闭CCD1和CCD2串口，{sp.PortName}。");
                }
                return true;
            }
            catch (Exception ex)
            {
                if (mSerialPortInterface != null)
                {
                    mSerialPortInterface.OnCCD1Error(ex.Message);
                    mSerialPortInterface.OnCCD2Error(ex.Message);
                }
                new LogHelper().ErrorLog(ex.Message);
                myEventLog.LogError($"CCD串口关闭出错，{sp.PortName}。"+ex.Message, ex);
                return false;
            }
        }
    }
}
