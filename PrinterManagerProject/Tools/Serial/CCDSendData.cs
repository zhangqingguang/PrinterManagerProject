using PrinterManagerProject.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterManagerProject.Tools.Serial
{
    public class CCDSendData
    {
        /// <summary>
        /// 是否正在发送
        /// </summary>
        public bool IsSending = false;
        /// <summary>
        /// 是否已经发送
        /// </summary>
        public bool HasSend = false;
        /// <summary>
        /// 医嘱内容
        /// </summary>
        public tOrder order { get; set; }
        /// <summary>
        /// CCD1识别到的规格
        /// </summary>
        public string[] specData { get; set; }
        /// <summary>
        /// 发送到PLC的指令内容
        /// </summary>
        public string Command { get; set; }
        /// <summary>
        /// CCD识别到的结果
        /// </summary>
        public string CCDData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CurrentCommandData { get; internal set; }
        /// <summary>
        /// 发送到打印机的打印内容
        /// </summary>
        public string PrintCommand { get; internal set; }

        //1.声明关于事件的委托；
        public delegate void SendEventHandler(object sender, EventArgs e);
        //2.声明事件；   
        public event SendEventHandler Send;
        //3.编写引发事件的函数；
        public void OnSend()
        {
            if (this.Send != null)
            {
                this.Send(this, new EventArgs());   //发出警报
            }
        }

    }
}
