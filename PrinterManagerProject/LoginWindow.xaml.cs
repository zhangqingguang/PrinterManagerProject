using PrinterManagerProject.Tools;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Graphics;
using Zebra.Sdk.Printer;
using Zebra.Sdk.Printer.Discovery;
using System.Collections.Generic;
using PrinterManagerProject.Models;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Threading.Tasks;
using PrinterManagerProject.EF;
using PrinterManagerProject.EF.Bll;
using ZXing.PDF417;
using ZXing;

namespace PrinterManagerProject
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : BaseWindow
    {

        Connection connection = null;

        public LoginWindow()
        {
            InitializeComponent();

            //CCDSerialPortUtils.GetInstance(this).Open();
            //PLCSerialPortUtils.GetInstance(this).Open();


            #region 读取打印机状态

            if (ConnectionManager.CheckConnetionStatus()==false)
            {
                MessageBox.Show("数据库连接失败，请检查数据库服务是否开启！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


            if (PrintWindow.IsConnectDevices)
            {
                DiscoveredUsbPrinter usbPrinter = null;
                List<DiscoveredUsbPrinter> printers = UsbDiscoverer.GetZebraUsbPrinters(new ZebraPrinterFilter());
                if (printers == null || printers.Count <= 0)
                {
                    MessageBox.Show("没有检测到打印机，请检查打印机是否开启！");
                    return;
                }
                usbPrinter = printers[0];

                connection = new UsbConnection(usbPrinter.Address);
                try
                {
                    connection.Open();
                    ZebraPrinter printer = ZebraPrinterFactory.GetInstance(connection);

                    PrinterStatus printerStatus = printer.GetCurrentStatus();
                    if (printerStatus.isReadyToPrint)
                    {
                        Console.WriteLine("Ready To Print");
                    }
                    else if (printerStatus.isPaused)
                    {
                        Console.WriteLine("Cannot Print because the printer is paused.");
                    }
                    else if (printerStatus.isHeadOpen)
                    {
                        Console.WriteLine("Cannot Print because the printer head is open.");
                    }
                    else if (printerStatus.isPaperOut)
                    {
                        Console.WriteLine("Cannot Print because the paper is out.");
                    }
                    else
                    {
                        Console.WriteLine("Cannot Print.");
                    }
                }
                catch (ConnectionException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                catch (ZebraPrinterLanguageUnknownException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }

            #endregion

        }

        private int ConvertFontInt(double num)
        {
            double multiple = 2;
            return Convert.ToInt32(num * multiple);
        }
        private int ConvertInt(double num)
        {
            double multiple = 3;
            return Convert.ToInt32(num * multiple);
        }

        private void testPrint()
        {
            #region 打印

            try
            {
                connection.Open();
                ZebraPrinter printer = ZebraPrinterFactory.GetInstance(connection);

                PrintTemplateModel model = new PrintTemplateHelper().GetConfig();
                if (model == null)
                {
                    MessageBox.Show("打印模板参数获取失败！");
                    return;
                }


                string fontName = "SimSun";

                var paperWidth = model.PageWidth * 3;
                var paperHeight = model.PageHeight * 3;
                Bitmap image = new Bitmap(paperWidth, paperHeight);
                Graphics g = Graphics.FromImage(image);

                System.Drawing.Brush bush = new SolidBrush(System.Drawing.Color.Black);//填充的颜色

                try
                {
                    //消除锯齿
                    g.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.TextRenderingHint = TextRenderingHint.AntiAlias; //消除文字锯齿
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
                    g.DrawString("用量", new Font(fontName, ConvertFontInt(model.UseTitleFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.UseTitleFontX), ConvertInt(model.UseTitleFontY));
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

                    ZebraImageI imageI = ZebraImageFactory.GetImage(image);
                    // 控制打印宽度
                    printer.PrintImage(imageI, 0, 0, paperWidth, paperHeight, false);
                }
                finally
                {
                    g.Dispose();
                    image.Dispose();
                }
            }
            catch (ConnectionException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            catch (ZebraPrinterLanguageUnknownException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

            #endregion
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var userManager = new UserManager();

            var crzName = txtCZR.Text.Trim();
            var crzPassword = txtCZRPWD.Password.Trim();
            var shrName = txtSHR.Text.Trim();
            var shrPassword = txtCZRPWD.Password.Trim();
            if (string.IsNullOrEmpty(crzName))
            {
                MessageBox.Show("请输入操作员！");
                return;
            }
            if (string.IsNullOrEmpty(txtCZRPWD.Password.Trim()))
            {
                MessageBox.Show("请输入操作员密码！");
                return;
            }
            if (string.IsNullOrEmpty(txtSHR.Text.Trim()))
            {
                MessageBox.Show("请输入审核员！");
                return;
            }
            if (string.IsNullOrEmpty(txtCZRPWD.Password.Trim()))
            {
                MessageBox.Show("请输入审核员密码！");
                return;
            }
            var czrUser = userManager.FirstOrDefault(s => s.user_name == crzName && s.password == crzPassword);
            var fhrUser = userManager.FirstOrDefault(s => s.user_name == shrName && s.password == shrPassword);
            if (czrUser == null)
            {
                MessageBox.Show("操作员账户或密码不正确！");
                return;
            }
            else
            {
                if (czrUser.type_name != "操作员")
                {
                    MessageBox.Show($"{crzName}不是操作员！");
                    return;
                }
            }

            if (fhrUser == null)
            {
                MessageBox.Show("审核员账户或密码不正确！");
                return;
            }
            else
            {
                if (fhrUser.type_name != "审核员")
                {
                    MessageBox.Show($"{crzName}不是审核员！");
                    return;
                }
            }

            if (ConnectionManager.CheckPivasConnetionStatus() == false)
            {
                MessageBox.Show("Pivas 数据库连接失败，请检查数据库服务是否开启！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                UserCache.Printer = czrUser;
                UserCache.Checker = fhrUser;


                new LogHelper().Log("测试用户登录！");
                this.Hide();

                MainWindow window = new MainWindow();
                window.Show();
            }
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //CCDSerialPortUtils.GetInstance(this).Close();
            //PLCSerialPortUtils.GetInstance(this).Close();

            this.Close();
        }

        public void OnDataReceived(string data)
        {
            Console.WriteLine("===============================");
            Console.WriteLine(data);
            Console.WriteLine("===============================");
        }
    }
}
