﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;

namespace PrinterManagerProject.Tools
{
    public abstract class IPrinterManager
    {
        private ConnectionA connection = null;
        private ZebraPrinter printer = null;
        public abstract ConnectionA GetConnection();
        public bool TryOpenPrinterConnection()
        {
            try
            {
                if(connection == null)
                {
                    connection = GetConnection();
                }
                var startTime = DateTime.Now;
                if (connection.Connected == false)
                {
                    connection.Open();
                    myEventLog.LogInfo($"打开打印机连接花费时间:{(DateTime.Now - startTime).TotalMilliseconds}");
                    myEventLog.LogInfo("成功打开打印机连接！");
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("与打印机连接出错！");
                myEventLog.LogError("打开打印机连接出错！", ex);
                return false;
            }
        }
        /// <summary>
        /// 获取ZebraPrinter
        /// </summary>
        /// <returns></returns>
        public ZebraPrinter GetPrinter()
        {
            if (TryOpenPrinterConnection())
            {
                if (printer != null && printer.Connection.Connected == false)
                {
                    myEventLog.LogInfo("Printer连接失败，重置Printer！");
                    printer = null;
                }
                if (printer == null)
                {
                    printer = ZebraPrinterFactory.GetInstance(connection);

                }
            }
            return printer;
        }
        /// <summary>
        /// 获取打印机状态
        /// </summary>
        /// <returns></returns>
        public PrinterStatus GetPrinterStatus(ZebraPrinter printer)
        {
            if (printer == null)
            {
                printer = GetPrinter();
            }

            if (printer != null)
            {
                return printer.GetCurrentStatus();
            }
            return null;
        }
        /// <summary>
        /// 初始化打印机并检测状态
        /// </summary>
        public string InitPrinter()
        {

            string errorMsg = "";
            try
            {

                PrinterStatus printerStatus = GetPrinterStatus(null);
                if (printerStatus == null)
                {
                    return "打印机连接失败，请检查打印机是否连接到上位机";
                }

                if (printerStatus.isReadyToPrint)
                {
                    Console.WriteLine("Ready To Print");
                    myEventLog.LogInfo("打印机准备完毕！");
                    System.Console.WriteLine("打印机准备完毕！");
                }
                else if (printerStatus.isHeadOpen)
                {
                    errorMsg = "打印机头已打开，请检查打印机状态！";
                    myEventLog.Log.Warn("打印机头已打开，请检查打印机状态！");
                    Console.WriteLine("Cannot Print because the printer head is open.");
                }
                else if (printerStatus.isPaperOut)
                {
                    errorMsg = "纸张用完，请检查打印机是否有纸！";
                    myEventLog.Log.Warn("纸张用完，请检查打印机是否有纸！");
                    Console.WriteLine("Cannot Print because the paper is out.");
                }
                else if (printerStatus.isPaused)
                {
                    errorMsg = "打印机已暂停，请检查打印机状态！";
                    myEventLog.Log.Warn("打印机已暂停，请检查打印机状态！");
                    Console.WriteLine("Cannot Print because the printer is paused.");
                }
                else
                {
                    Console.WriteLine("Cannot Print.");
                }
            }
            catch (ConnectionException e)
            {
                errorMsg = "打印机连接失败，请检查是否连接到上位机！";
                Console.WriteLine(e.ToString());
            }
            catch (ZebraPrinterLanguageUnknownException e)
            {
                errorMsg = "打印机设置出错，请检查打印机！";
                Console.WriteLine(e.ToString());
            }
            finally
            {
                connection.Close();
            }
            return errorMsg;
        }
        /// <summary>
        /// 获取打印机缓冲区中未打印标签数量
        /// </summary>
        /// <returns></returns>
        public int GetLabelsRemainingInBatch(ZebraPrinter printer)
        {
            var status = GetPrinterStatus(printer);
            if (status != null)
            {
                return status.labelsRemainingInBatch;
            }
            return 0;
        }
    }
}