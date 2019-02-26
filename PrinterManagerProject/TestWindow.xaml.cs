using PrinterManagerProject.Models;
using PrinterManagerProject.Tools;
using PrinterManagerProject.Tools.Serial;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PrinterManagerProject.EF;
using PrinterManagerProject.EF.Models;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Graphics;
using Zebra.Sdk.Printer;
using Zebra.Sdk.Printer.Discovery;
using ZXing;
using ZXing.PDF417;

namespace PrinterManagerProject
{
    /// <summary>
    /// TestWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();

            // ViewBarCode();
            //ViewCard();
            //commandPrint();
            //PLCCommandTest();
            //ViewCard();
            commandPrint();
        }

        private void PLCCommandTest()
        {
            PLCTest plcTest = new PLCTest();
            plcTest.SendData("%01#RDD0090000901**\r");
        }

        private void ViewBarCode()
        {
            PrintTemplateModel model = new PrintTemplateHelper().GetConfig();
            if (model == null)
            {
                MessageBox.Show("打印模板参数获取失败！");
                return;
            }

            var paperWidth = model.PageWidth * printMultiple;
            var paperHeight = model.PageHeight * printMultiple;
            Bitmap image = new Bitmap(paperWidth, paperHeight);
            Graphics g = Graphics.FromImage(image);

            System.Drawing.Brush bush = new SolidBrush(System.Drawing.Color.Black);//填充的颜色

            try
            {
                //消除锯齿
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.PageUnit = GraphicsUnit.Pixel;
                //清空图片背景颜色
                g.Clear(System.Drawing.Color.White);

                var code = "1231125-102-23";

                #region PDF417
                PDF417EncodingOptions pdf_options = new PDF417EncodingOptions
                {
                    Margin = 0,
                    DisableECI = true,
                    CharacterSet = "UTF-8",
                    Width = ConvertInt(model.BarCodeWidth),
                    Height = ConvertInt(model.BarCodeHeight),
                    PureBarcode = false
                };
                var pdf417Writer = new ZXing.BarcodeWriter();
                pdf417Writer.Format = BarcodeFormat.PDF_417;
                pdf417Writer.Options = pdf_options;
                #endregion

                Bitmap bmp = pdf417Writer.Write(code);

                // Bitmap bmp = CreateImg.GenerateBitmap(code, 5, 5);
                g.DrawImage(bmp, ConvertInt(model.BarCodeX), ConvertInt(model.BarCodeY), ConvertInt(model.BarCodeWidth), ConvertInt(model.BarCodeHeight));

                IntPtr myImagePtr = image.GetHbitmap();     //创建GDI对象，返回指针
                BitmapSource imgsource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(myImagePtr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());  //创建imgSource

                DeleteObject(myImagePtr);

                imgTest.Source = imgsource;


                
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }

        }

        private void commandPrint()
        {
            UsbConnection connection = null;
            try
            {
                DiscoveredUsbPrinter usbPrinter = null;
                List<DiscoveredUsbPrinter> printers = UsbDiscoverer.GetZebraUsbPrinters(new ZebraPrinterFilter());
                if (printers == null || printers.Count <= 0)
                {
                    MessageBox.Show("没有检测到打印机，请检查打印机是否开启！");
                    myEventLog.LogInfo("没有检测到打印机，请检查打印机是否开启！");
                    return;
                }
                usbPrinter = printers[0];

                connection = new UsbConnection(usbPrinter.Address);

                connection.Open();
                var printer = ZebraPrinterFactory.GetInstance(connection);

                var startTime = DateTime.Now;
                var command = GetCommand();
                Console.WriteLine($"生成打印命令花费时间：{(DateTime.Now - startTime).TotalMilliseconds}ms");
                startTime = DateTime.Now;

                printer.SendCommand(command);
                Console.WriteLine($"打印内容发送成功！花费时间：{(DateTime.Now - startTime).TotalMilliseconds}ms");
            }
            catch (Exception e)
            {
                connection.Close();
            }
            
        }

        private string GetCommand()
        {
            PrintTemplateModel model = new PrintTemplateHelper().GetConfig();

            StringBuilder sb = new StringBuilder();
            var orderManager = new OrderManager();
            var order = orderManager.FirstOrDefault(s => true);
            List<PrintDrugModel> drugs = orderManager.GetPrintDrugs(order.Id);


            //            var command = $@"^XA
            //^CI17
            //^A@N,60,60,E:000.FNT^F8^FD1一二三四五六七八九十This is a test.^FS
            //^XZ";
            sb.Append("^XA");
            sb.Append("^CWJ,E:001.FNT^FS");
            //sb.Append("^A@N,60,60,E:000.FNT^F8^FD1一二三四五六七八九十This is a test.^FS");
            sb.Append(GetBarCodeCommand(order.barcode,model.BarCodeX,model.BarCodeY));
            //g.DrawImage(bmp, ConvertInt(model.BarCodeX), ConvertInt(model.BarCodeY), ConvertInt(model.BarCodeWidth), ConvertInt(model.BarCodeHeight));
            sb.Append(GetLabelCommand(order.patient_name, model.AreaFontSize, model.AreaFontX, model.AreaFontY));
            sb.Append(GetLabelCommand(order.use_date, model.DateFontSize, model.DateFontX, model.DateFontY));
            sb.Append(GetLabelCommand("1/1", model.PageNumFontSize, model.PageNumFontX, model.PageNumFontY));
            sb.Append(GetLabelCommand("——————————————————————————", model.DrugsTitleFontSize, 0, model.SplitY-5));
            sb.Append(GetLabelCommand(order.group_num, model.DoctorAdviceFontSize, model.DoctorAdviceFontX, model.DoctorAdviceFontY));
            sb.Append(GetLabelCommand(order.bed_number, model.BedFontSize, model.BedFontX, model.BedFontY));
            sb.Append(GetLabelCommand(order.patient_name, model.PatientFontSize, model.PatientFontX, model.PatientFontY));
            sb.Append(GetLabelCommand("男", model.GenderFontSize, model.GenderFontX, model.GenderFontY));
            sb.Append(GetLabelCommand(order.batch, model.BatchNumberFontSize, model.BatchNumberFontX, model.BatchNumberFontY));
            sb.Append(GetLabelCommand(order.zone?.ToString(), model.SerialNumberFontSize, model.SerialNumberFontX, model.SerialNumberFontY));
            //g.DrawLine(new System.Drawing.Pen(bush), new System.Drawing.Point(ConvertInt(model.Split2X), ConvertInt(model.Split2Y)), new System.Drawing.Point(ConvertInt(model.Split2X + model.Split2Width), ConvertInt(model.Split2Y)));
            sb.Append(GetLabelCommand("——————————————————————————", model.DrugsTitleFontSize, 0, model.Split2Y - 5));
            sb.Append(GetLabelCommand("药品名称", model.DrugsTitleFontSize, model.DrugsTitleFontX, model.DrugsTitleFontY));
            sb.Append(GetLabelCommand("数量", model.UseTitleFontSize, model.UseTitleFontX, model.UseTitleFontY));

            //g.DrawLine(new System.Drawing.Pen(bush), new System.Drawing.Point(ConvertInt(model.SplitX), ConvertInt(model.SplitY)), new System.Drawing.Point(ConvertInt(model.SplitX + model.SplitWidth), ConvertInt(model.SplitY)));
            //int x = model.DrugsContentFontX;
            //int y = model.DrugsContentFontY;
            //int u_x = model.UseValueFontX;
            //int u_y = model.UseValueFontY;
            var margin = 10;
            var height = model.DrugsContentFontY + margin;
            var paperWidth = model.PageWidth ;
            var paperHeight = model.PageHeight ;

            #region 药品列表
            // 药品信息
            for (int i = 0; i < drugs.Count; i++)
            {
                int fontHeight = model.DrugsContentFontSize;
                // 药名
                sb.Append(GetLabelCommand(drugs[i].drug_name, model.DrugsContentFontSize, model.DrugsContentFontX, height));
                //数量
                sb.Append(GetLabelCommand(drugs[i].use_count.TrimEnd('0'), model.DrugsContentFontSize, model.PageWidth-80, height));

                // 只修改Y轴，向下平铺
                height += model.DrugsContentFontSize + margin;
            }
            #endregion

            sb.Append(GetLabelCommand($"处方医生：{order.doctor_name}", model.DoctorFontSize, model.DoctorFontX, model.DoctorFontY));
            sb.Append(GetLabelCommand($"备注：{order.pass_remark}", model.RemarkFontSize, model.RemarkFontX, model.RemarkFontY));
            sb.Append(GetLabelCommand($"滴速：{order.ml_speed}   {order.usage_name}   {order.use_frequency}({order.use_time})", model.SpeedFontSize, model.SpeedFontX, model.SpeedFontY));

            //g.DrawLine(new System.Drawing.Pen(bush), new System.Drawing.Point(ConvertInt(model.Split3X), ConvertInt(model.Split3Y)), new System.Drawing.Point(ConvertInt(model.Split3X + model.Split3Width), ConvertInt(model.Split3Y)));

            sb.Append(GetLabelCommand("——————————————————————————", model.DrugsTitleFontSize, 0, model.Split3Y - 5));
            sb.Append(GetLabelCommand($"审核：{order.checker}", model.ExamineFontSize, model.ExamineFontX, model.ExamineFontY));
            sb.Append(GetLabelCommand($"复审：", model.ReviewFontSize, model.ReviewFontX, model.ReviewFontY));
            sb.Append(GetLabelCommand($"排药：{order.deliveryer}", model.SortFontSize, model.SortFontX, model.SortFontY));
            sb.Append(GetLabelCommand($"配液：{order.config_person}", model.DispensingFontSize, model.DispensingFontX, model.DispensingFontY));
            sb.Append(GetLabelCommand($"配液：___时___分", model.DispensingDateFontSize, model.DispensingDateFontX, model.DispensingDateFontY));

            sb.Append("^XZ");

            return sb.ToString();
        }

        private string GetLabelCommand(string content,int fontSize,int x,int y)
        {
            x = x * 3;
            y = y * 3;
            int width =Convert.ToInt32( fontSize * 2.5);
            int height = Convert.ToInt32(fontSize * 2.5);
            string fontName = "E:000.FNT";
            //return $"^A@N,{height},{width},{fontName}^F8^FD{content}^FS";
            return $"^FO{x},{y}^AJN,{width},{height}^CI17^F8^FD{content}^FS";
        }

        private string GetBarCodeCommand(string content,int x,int y)
        {
            x = x * 3;
            y = y * 3;

            return $@"
^By3,3
^FO{x},{y},^B7N,7,4,4,13,N
^FDZebraZebraZebraZebraZebraZebra
^FS";
        }


        private ZebraPrinter printer = null;
        IPrinterManager printerManager = new UsbConnectionManager();

        int printMultiple = 3;
        private void ViewCard()
        {

            if (printer == null)
            {
                printer = printerManager.GetPrinter();
            }

            PrintTemplateModel model = new PrintTemplateHelper().GetConfig();
            if (model == null)
            {
                MessageBox.Show("打印模板参数获取失败！");
                return;
            }

            string fontName = "SimSun";

            var startTime = DateTime.Now;
            var paperWidth = model.PageWidth * printMultiple;
            var paperHeight = model.PageHeight * printMultiple;
            Bitmap image = new Bitmap(paperWidth, paperHeight);
            Graphics g = Graphics.FromImage(image);

            System.Drawing.Brush bush = new SolidBrush(System.Drawing.Color.Black);//填充的颜色

            try
            {
                //消除锯齿
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.PageUnit = GraphicsUnit.Pixel;
                //清空图片背景颜色
                g.Clear(System.Drawing.Color.White);

                var code = "1231125-102-23";
                #region PDF417
                PDF417EncodingOptions pdf_options = new PDF417EncodingOptions
                {
                    Margin = 0,
                    DisableECI = true,
                    CharacterSet = "UTF-8",
                    Width = ConvertInt(model.BarCodeWidth),
                    Height = ConvertInt(model.BarCodeHeight),
                    PureBarcode = false
                };
                var pdf417Writer = new ZXing.BarcodeWriter();
                pdf417Writer.Format = BarcodeFormat.PDF_417;
                pdf417Writer.Options = pdf_options;
                #endregion

                Bitmap bmp = pdf417Writer.Write(code);
                g.DrawImage(bmp, ConvertInt(model.BarCodeX), ConvertInt(model.BarCodeY), ConvertInt(model.BarCodeWidth), ConvertInt(model.BarCodeHeight));

                g.DrawString("普通外科二病区", new Font(fontName, ConvertFontInt(model.AreaFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.AreaFontX), ConvertInt(model.AreaFontY));
                g.DrawString("2018-11-12补", new Font(fontName, ConvertFontInt(model.DateFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.DateFontX), ConvertInt(model.DateFontY));
                g.DrawString("1/1", new Font(fontName, ConvertFontInt(model.PageNumFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.PageNumFontX), ConvertInt(model.PageNumFontY));

                g.DrawLine(new System.Drawing.Pen(bush), new System.Drawing.Point(ConvertInt(model.SplitX), ConvertInt(model.SplitY)), new System.Drawing.Point(ConvertInt(model.SplitX + model.SplitWidth), ConvertInt(model.SplitY)));

                g.DrawString(code, new Font(fontName, ConvertFontInt(model.DoctorAdviceFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.DoctorAdviceFontX), ConvertInt(model.DoctorAdviceFontY));
                g.DrawString("31床", new Font(fontName, ConvertFontInt(model.BedFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.BedFontX), ConvertInt(model.BedFontY));
                g.DrawString("史新平", new Font(fontName, ConvertFontInt(model.PatientFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.PatientFontX), ConvertInt(model.PatientFontY));
                g.DrawString("男", new Font(fontName, ConvertFontInt(model.GenderFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.GenderFontX), ConvertInt(model.GenderFontY));
                g.DrawString("1批", new Font(fontName, ConvertFontInt(model.BatchNumberFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.BatchNumberFontX), ConvertInt(model.BatchNumberFontY));
                g.DrawString("[C-4]-1", new Font(fontName, ConvertFontInt(model.SerialNumberFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.SerialNumberFontX), ConvertInt(model.SerialNumberFontY));

                g.DrawLine(new System.Drawing.Pen(bush), new System.Drawing.Point(ConvertInt(model.Split2X), ConvertInt(model.Split2Y)), new System.Drawing.Point(ConvertInt(model.Split2X + model.Split2Width), ConvertInt(model.Split2Y)));

                g.DrawString("药品名称", new Font(fontName, ConvertFontInt(model.DrugsTitleFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.DrugsTitleFontX), ConvertInt(model.DrugsTitleFontY));
                g.DrawString("5%葡萄糖注射液[100ml](非PVC双阀)", new Font(fontName, ConvertFontInt(model.DrugsContentFontSize)), bush, ConvertInt(model.DrugsContentFontX), ConvertInt(model.DrugsContentFontY));
                g.DrawString("数量", new Font(fontName, ConvertFontInt(model.UseTitleFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.UseTitleFontX), ConvertInt(model.UseTitleFontY));
                g.DrawString("100ml", new Font(fontName, ConvertFontInt(model.UseValueFontSize)), bush, ConvertInt(model.UseValueFontX), ConvertInt(model.UseValueFontY));
                g.DrawString("处方医生：张慧", new Font(fontName, ConvertFontInt(model.DoctorFontSize)), bush, ConvertInt(model.DoctorFontX), ConvertInt(model.DoctorFontY));
                g.DrawString("备注：", new Font(fontName, ConvertFontInt(model.RemarkFontSize)), bush, ConvertInt(model.RemarkFontX), ConvertInt(model.RemarkFontY));
                g.DrawString("滴速：40滴/分   静脉续滴   qd(8点)", new Font(fontName, ConvertFontInt(model.SpeedFontSize)), bush, ConvertInt(model.SpeedFontX), ConvertInt(model.SpeedFontY));

                g.DrawLine(new System.Drawing.Pen(bush), new System.Drawing.Point(ConvertInt(model.Split3X), ConvertInt(model.Split3Y)), new System.Drawing.Point(ConvertInt(model.Split3X + model.Split3Width), ConvertInt(model.Split3Y)));

                g.DrawString("审核：焦媛媛", new Font(fontName, ConvertFontInt(model.ExamineFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.ExamineFontX), ConvertInt(model.ExamineFontY));
                g.DrawString("复审：", new Font(fontName, ConvertFontInt(model.ReviewFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.ReviewFontX), ConvertInt(model.ReviewFontY));
                g.DrawString("排药：牛捷", new Font(fontName, ConvertFontInt(model.SortFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.SortFontX), ConvertInt(model.SortFontY));
                g.DrawString("配液：", new Font(fontName, ConvertFontInt(model.DispensingFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.DispensingFontX), ConvertInt(model.DispensingFontY));
                g.DrawString("配液：___时___分", new Font(fontName, ConvertFontInt(model.DispensingDateFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.DispensingDateFontX), ConvertInt(model.DispensingDateFontY));


                //image.Save(AppDomain.CurrentDomain.BaseDirectory + @"Config\test" + DateTime.Now.Millisecond + ".jpg");

                //IntPtr myImagePtr = image.GetHbitmap();     //创建GDI对象，返回指针
                //BitmapSource imgsource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(myImagePtr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());  //创建imgSource

                //DeleteObject(myImagePtr);

                //imgTest.Source = imgsource;

                //var fileName = Guid.NewGuid().ToString();
                //var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Config", fileName + ".jpg");
                //var pathTarget = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", fileName + "-bak.jpg");
                //image.Save(path);

                //ImageHelper.KiSaveAsJPEG(image,pathTarget, 20);

                ZebraImageI imageI = ZebraImageFactory.GetImage(image);
                myEventLog.LogInfo($"画图花费时间:{(DateTime.Now - startTime).TotalMilliseconds}");
                startTime = DateTime.Now;
                printer.PrintImage(imageI, 0, 0, paperWidth, paperHeight, false);
                myEventLog.LogInfo($"发送打印内容花费时间:{(DateTime.Now - startTime).TotalMilliseconds}");

            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        private int ConvertFontInt(double num)
        {
            double multiple = 2;
            return Convert.ToInt32(num * multiple);
        }
        private int ConvertInt(double num)
        {
            double multiple = printMultiple;
            return Convert.ToInt32(num * multiple);
        }


        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Owner?.Show();
        }
    }
}
