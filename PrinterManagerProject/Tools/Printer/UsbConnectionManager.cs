using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;
using Zebra.Sdk.Printer.Discovery;

namespace PrinterManagerProject.Tools
{
    public class UsbConnectionManager:IPrinterManager
    {
        public UsbConnectionManager()
        {

        }

        public override ConnectionA GetConnection()
        {
            DiscoveredUsbPrinter usbPrinter = null;
            List<DiscoveredUsbPrinter> printers = UsbDiscoverer.GetZebraUsbPrinters(new ZebraPrinterFilter());
            if (printers == null || printers.Count <= 0)
            {
                //MessageBox.Show("没有检测到打印机，请检查打印机是否开启！");
                myEventLog.LogInfo("没有检测到打印机，请检查打印机是否开启！");
                return null;
            }
            usbPrinter = printers[0];

            return new UsbConnection(usbPrinter.Address);
        }
    }
}
