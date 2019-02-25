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
    public class DriveConnectionManager : IPrinterManager
    {
        public DriveConnectionManager()
        {

        }

        public override ConnectionA GetConnection()
        {
            DiscoveredPrinterDriver driverPrinter = null;
            List<DiscoveredPrinterDriver> printers = UsbDiscoverer.GetZebraDriverPrinters();
            if (printers == null || printers.Count <= 0)
            {
                //MessageBox.Show("没有检测到打印机，请检查打印机是否开启！");
                myEventLog.LogInfo("没有检测到打印机，请检查打印机是否开启！");
                return null;
            }
            driverPrinter = printers[0];

            var connection = new DriverPrinterConnection(driverPrinter.Address);
            connection.Open();
            try
            {
                ZebraPrinterFactory.GetInstance(connection);

            }
            catch (Exception ex)
            {

            }
            try
            {
                ZebraPrinterFactory.GetLinkOsPrinter(connection);
            }
            catch (Exception ex)
            {

            }



            return connection;
        }
    }
}
