using PrinterManagerProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PrinterManagerProject.Tools;
using System.Threading;
using Zebra.Sdk.Printer.Discovery;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;
using System.Drawing;
using Zebra.Sdk.Graphics;
using System.IO;
using ZXing.PDF417;
using ZXing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Shapes;
using System.Text;
using PrinterManagerProject.BLL;
using System.Linq;
using System.Data;
using log4net;

namespace PrinterManagerProject
{
    /// <summary>
    /// PrintWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PrintWindow : BaseWindow, ScanerHandlerSerialPortInterface, ScannerSerialPortInterface, PLCSerialPortInterface, CCDSerialPortInterface
    {
        private const int MaxTryPhotographTimes = 3;

        private List<DrugsQueueModel> queue = new List<DrugsQueueModel>();
        private PrintTemplateModel model = null;
        private Connection connection = null;
        private ZebraPrinter printer = null;
        private HistoryModel countModel = null;
        
        // 自动贴签
        private List<PrinterManagerProject.Model.ListAllModel> autoPrintList = new List<Model.ListAllModel>();
        // 自动显示贴签
        private List<PrinterManagerProject.Model.ListAllModel> autoPrintCurrentList = new List<Model.ListAllModel>();
        // 手动贴签
        private List<PrinterManagerProject.Model.ListAllModel> handlerPrintList = new List<Model.ListAllModel>();
        // 自动显示贴签
        private List<PrinterManagerProject.Model.ListAllModel> handlerPrintCurrentList = new List<Model.ListAllModel>();
        // 异常列表
        private List<PrinterManagerProject.Model.ListAllModel> exceptionList = new List<Model.ListAllModel>();

        public PrintWindow()
        {
            InitializeComponent();

            // 开启串口
            PLCSerialPortUtils.GetInstance(this).Open();
            CCDSerialPortUtils.GetInstance(this).Open();
            ScannerSerialPortUtils.GetInstance(this).Open();
            ScanHandlerSerialPortUtils.GetInstance(this).Open();
            
            // 检查打印机状态
            InitPrinter();
            
            // 检查数据库状态
            if (!CheckDBConnection())
            {
                return;
            }

            if (!GetPrinterModelConfig())
                return;

            #region 绑定批次

            BLL.tBatch_for_View tfv = new BLL.tBatch_for_View();
            List<Model.tBatch_for_View> list = tfv.GetList();
            list.Insert(0, new Model.tBatch_for_View() { batch = "0", batch_name = "请选择" });
            this.cb_batch.DisplayMemberPath = "batch_name";
            this.cb_batch.SelectedValuePath = "batch";
            this.cb_batch.ItemsSource = list;

            #endregion

            //绑定批次调整后事件
            this.cb_batch.SelectionChanged += Cb_batch_SelectionChanged;

            this.use_date.SelectedDate = DateTime.Now.Date;

            GetCount();
        }

        /// <summary>
        /// 根据状态修改DataGridRow的颜色
        /// 调用方法
        /// Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action(DgvGroupDetailListStatus));
        /// </summary>
        /// <param name="dg"></param>
        private void DgvListStatus(DataGrid dg)
        {
            if (dg != null)
            {
                SolidColorBrush notComplate = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFdddddd")); // 未完成的-灰色
                SolidColorBrush complated = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFcae8fc")); // 完成的--浅蓝色
                SolidColorBrush exception = new SolidColorBrush(System.Windows.Media.Color.FromRgb(96, 126, 234)); // 异常的-蓝色

                for (int i = 0; i < dg.Items.Count; i++)
                {
                    var row = dg.ItemContainerGenerator.ContainerFromItem(dg.Items[i]) as DataGridRow;

                    Model.ListAllModel model = dg.Items[i] as Model.ListAllModel;
                    if (model != null)
                    {
                        switch (model.printing_status)
                        {
                            case 0:
                                row.Background = notComplate;
                                break;
                            case 1:
                                row.Background = complated;
                                break;
                        }
                    }
                }
            }
        }

        private void BaseWindow_Closing(object sender, CancelEventArgs e)
        {
            if (needCloseWindowConfirm)
            {
                MessageBoxResult result = MessageBox.Show("确定是退出贴签系统吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
                //关闭窗口
                if (result == MessageBoxResult.Yes)
                {
                    // 关闭PLC
                    PLCSerialPortUtils plcUtils = PLCSerialPortUtils.GetInstance(this);
                    plcUtils.SendData(PLCSerialPortData.MACHINE_STOP);
                    // 关闭串口
                    PLCSerialPortUtils.GetInstance(this).Close();
                    CCDSerialPortUtils.GetInstance(this).Close();
                    ScannerSerialPortUtils.GetInstance(this).Close();
                    ScanHandlerSerialPortUtils.GetInstance(this).Close();

                    // 打开主窗口
                    var collections = Application.Current.Windows;
                    foreach (Window window in collections)
                    {
                        BaseWindow win = window as BaseWindow;
                        if (win != null)
                        {
                            // 其他Window直接关闭
                            if (win.ToString().Contains("MainWindow"))
                            {
                                win.Show();
                            }
                        }
                    }

                    e.Cancel = false;
                }
                //不关闭窗口
                if (result == MessageBoxResult.No)
                    e.Cancel = true;
            }
        }

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            ////var model = autoPrintCurrentList[0];
            ////model.printing_status = 1;

            //var model = autoPrintCurrentList[0];
            //autoPrintCurrentList.Remove(model);

            //// 显示界面效果
            //Dispatcher.Invoke(() =>
            //{
            //    dgvGroupDetailList.ItemsSource = null;
            //    dgvGroupDetailList.ItemsSource = autoPrintCurrentList;
            //});
            //Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action<DataGrid>(DgvListStatus), dgvGroupDetailList);
            //return;

            if (tabMain.SelectedIndex == 0)
            {
                // 先关再开
                PLCSerialPortUtils plcUtils = PLCSerialPortUtils.GetInstance(this);
                plcUtils.SendData(PLCSerialPortData.MACHINE_STOP);
                plcUtils.SendData(PLCSerialPortData.MACHINE_START);

                btnPrint.IsEnabled = false;
                btnStopPrint.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("当前标签页面不能自动打印！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ButtonStopPrint_Click(object sender, RoutedEventArgs e)
        {
            PLCSerialPortUtils.GetInstance(this).SendData(PLCSerialPortData.MACHINE_STOP);

            btnPrint.IsEnabled = true;
            btnStopPrint.IsEnabled = false;
        }

        private void Use_date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //绑定批次调整后事件
            this.cb_batch.SelectionChanged -= Cb_batch_SelectionChanged;
            this.cb_batch.SelectedIndex = 0;
            this.cb_batch.SelectionChanged += Cb_batch_SelectionChanged;

            this.dgvGroupDetailList.ItemsSource = null;
            this.cb_dept.ItemsSource = null;
            this.cb_drug.ItemsSource = null;
            this.cb_drug_category.ItemsSource = null;

            // 清空右侧总统计信息
            lblNumber.Content = 0;
            lblMarkNumber.Content = 0;

            LoadData();
        }

        /// <summary>
        /// 筛选数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cb_batch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!CheckDBConnection())
            {
                MessageBox.Show("数据库连接失败，请检查数据库服务是否开启！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //  根据tab选择要操作的数据源
            List<Model.ListAllModel> currentSourceList = null;
            switch (tabMain.SelectedIndex)
            {
                case 1:
                    currentSourceList = handlerPrintList;

                    // 绑定显示的列表
                    handlerPrintCurrentList = currentSourceList.FindAll(m => m.batch == this.cb_batch.SelectedValue + "").ToList();
                    dgvGroupNoAutoList.EnableRowVirtualization = false;
                    dgvGroupNoAutoList.DataContext = handlerPrintCurrentList;
                    dgvGroupNoAutoList.ItemsSource = handlerPrintCurrentList;

                    // 修改数据背景色
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action<DataGrid>(DgvListStatus), dgvGroupNoAutoList);
                    break;
                case 0:
                default:
                    currentSourceList = autoPrintList;

                    // 绑定显示的列表
                    autoPrintCurrentList = currentSourceList.FindAll(m => m.batch == this.cb_batch.SelectedValue + "").ToList();
                    dgvGroupDetailList.EnableRowVirtualization = false;
                    dgvGroupDetailList.DataContext = autoPrintCurrentList;
                    dgvGroupDetailList.ItemsSource = autoPrintCurrentList;

                    // 修改数据背景色
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action<DataGrid>(DgvListStatus), dgvGroupDetailList);
                    break;
            }

            if (currentSourceList.Count <= 0)
            {
                return;
            }

            //绑定右（溶媒统计）列表
            var solventlist = currentSourceList.GroupBy(m => new { m.drug_name, m.drug_spec }).
                Select(a => new SolventModel()
                {
                    SolventName = a.Key.drug_name,
                    Spec = a.Key.drug_spec,
                    Number = a.Count(),
                    MarkNumber = a.Count() - a.Count(m => m.printing_status == 0)
                }).ToList();
            this.dgvGroupDetailRightList.ItemsSource = solventlist;
            this.dgvGroupDetailRightList.CanUserSortColumns = false;
            // 绑定右侧总统计信息
            int number = solventlist.Sum(m => m.Number);
            lblNumber.Content = number;
            int markNumber = solventlist.Sum(m => m.MarkNumber);
            lblMarkNumber.Content = markNumber;

            // 绑定科室
            var deptList = currentSourceList.GroupBy(m => new { m.departmengt_name, m.department_code }).Select(a => new { dept_name = a.Key.departmengt_name, dept_code = a.Key.department_code }).ToList();
            deptList.Insert(0, new { dept_name = "全部", dept_code = "0" });
            this.cb_dept.DisplayMemberPath = "dept_name";
            this.cb_dept.SelectedValuePath = "dept_code";
            this.cb_dept.ItemsSource = deptList;

            // 绑定药品分类
            var drugCategoryList = currentSourceList.GroupBy(m => new { m.ydrug_class_name }).Select(a => new { class_name = a.Key.ydrug_class_name }).ToList();
            drugCategoryList.Insert(0, new { class_name = "全部" });
            this.cb_drug_category.DisplayMemberPath = "class_name";
            this.cb_drug_category.SelectedValuePath = "class_name";
            this.cb_drug_category.ItemsSource = drugCategoryList;

            // 绑定主药
            var drugList = currentSourceList.GroupBy(m => new { m.ydrug_name, m.ydrug_spec }).Select(a => new { ydrug_name = string.Format("{0}({1})", a.Key.ydrug_name, a.Key.ydrug_spec), ydrug_id = string.Format("{0}|{1}", a.Key.ydrug_name, a.Key.ydrug_spec) }).ToList();
            drugList.Insert(0, new { ydrug_name = "全部", ydrug_id = "" });
            this.cb_drug.DisplayMemberPath = "ydrug_name";
            this.cb_drug.SelectedValuePath = "ydrug_id";
            this.cb_drug.ItemsSource = drugList;
        }


        #region 读取打印机状态

        /// <summary>
        /// 初始化打印机并检测状态
        /// </summary>
        private void InitPrinter()
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

            bool hasError = false;
            string errorMsg = "";
            try
            {
                connection.Open();
                printer = ZebraPrinterFactory.GetInstance(connection);

                PrinterStatus printerStatus = printer.GetCurrentStatus();
                if (printerStatus.isReadyToPrint)
                {
                    Console.WriteLine("Ready To Print");
                    myEventLog.LogWarn("打印机准备完毕！");
                    System.Console.WriteLine("打印机准备完毕！");
                }
                else if (printerStatus.isHeadOpen)
                {
                    hasError = true;
                    errorMsg = "打印机头已打开，请检查打印机状态！";
                    myEventLog.Log.Warn("打印机头已打开，请检查打印机状态！");
                    Console.WriteLine("Cannot Print because the printer head is open.");
                }
                else if (printerStatus.isPaperOut)
                {
                    hasError = true;
                    errorMsg = "纸张用完，请检查打印机是否有纸！";
                    myEventLog.Log.Warn("纸张用完，请检查打印机是否有纸！");
                    Console.WriteLine("Cannot Print because the paper is out.");
                }
                else if(printerStatus.isPaused)
                {
                    hasError = true;
                    errorMsg = "打印机已暂停，请检查打印机状态！";
                    myEventLog.Log.Warn("打印机已暂停，请检查打印机状态！");
                    Console.WriteLine("Cannot Print because the printer is paused.");
                }
                else 
                {
                    hasError = true;
                    Console.WriteLine("Cannot Print.");
                }
            }
            catch (ConnectionException e)
            {
                hasError = true;
                errorMsg = "打印机连接失败，请检查是否连接到上位机！";
                Console.WriteLine(e.ToString());
            }
            catch (ZebraPrinterLanguageUnknownException e)
            {
                hasError = true;
                errorMsg = "打印机设置出错，请检查打印机！";
                Console.WriteLine(e.ToString());
            }
            finally
            {
                connection.Close();
                if (hasError)
                {
                    // 这里设置显示串口正常
                    MessageBoxResult result = MessageBox.Show(errorMsg, "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        InitPrinter();
                    }
                }
            }
        }

        #endregion

        #region 获取标签打印模板

        /// <summary>
        /// 获取标签打印模板
        /// </summary>
        private bool GetPrinterModelConfig()
        {
            model = new PrintTemplateHelper().GetConfig();
            if (model == null)
            {
                MessageBox.Show("打印模板参数获取失败！");
                return false;
            }
            return true;
        }

        #endregion

        #region 打印方法

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

        /// <summary>
        /// 生成打印内容并推送的打印机
        /// </summary>
        private void Print(List<Model.Print_ymodel> drugs, ref Model.ListAllModel drug, ref StringBuilder b)
        {
            #region 打印

            try
            {
                connection.Open();
                printer = ZebraPrinterFactory.GetInstance(connection);
                b.AppendLine(string.Format("连接打印机完成：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));


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

                    myEventLog.Log.Info($"打印标签二维码内容：{drug.QRcode}");
                    Bitmap bmp = pdf417Writer.Write(drug.QRcode);
                    g.DrawImage(bmp, ConvertInt(model.BarCodeX), ConvertInt(model.BarCodeY), ConvertInt(model.BarCodeWidth), ConvertInt(model.BarCodeHeight));

                    g.DrawString(drug.patient_name, new Font(fontName, ConvertFontInt(model.AreaFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.AreaFontX), ConvertInt(model.AreaFontY));
                    g.DrawString(drug.use_date, new Font(fontName, ConvertFontInt(model.DateFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.DateFontX), ConvertInt(model.DateFontY));
                    g.DrawString("1/1", new Font(fontName, ConvertFontInt(model.PageNumFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.PageNumFontX), ConvertInt(model.PageNumFontY));

                    g.DrawLine(new System.Drawing.Pen(bush), new System.Drawing.Point(ConvertInt(model.SplitX), ConvertInt(model.SplitY)), new System.Drawing.Point(ConvertInt(model.SplitX + model.SplitWidth), ConvertInt(model.SplitY)));

                    g.DrawString(drug.group_num, new Font(fontName, ConvertFontInt(model.DoctorAdviceFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.DoctorAdviceFontX), ConvertInt(model.DoctorAdviceFontY));
                    g.DrawString(drug.bed_number, new Font(fontName, ConvertFontInt(model.BedFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.BedFontX), ConvertInt(model.BedFontY));
                    g.DrawString(drug.patient_name, new Font(fontName, ConvertFontInt(model.PatientFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.PatientFontX), ConvertInt(model.PatientFontY));
                    g.DrawString("男", new Font(fontName, ConvertFontInt(model.GenderFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.GenderFontX), ConvertInt(model.GenderFontY));
                    g.DrawString(string.Format("{0}批", drug.batch), new Font(fontName, ConvertFontInt(model.BatchNumberFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.BatchNumberFontX), ConvertInt(model.BatchNumberFontY));
                    g.DrawString(string.Format("[{0}]", drug.zone), new Font(fontName, ConvertFontInt(model.SerialNumberFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.SerialNumberFontX), ConvertInt(model.SerialNumberFontY));

                    g.DrawLine(new System.Drawing.Pen(bush), new System.Drawing.Point(ConvertInt(model.Split2X), ConvertInt(model.Split2Y)), new System.Drawing.Point(ConvertInt(model.Split2X + model.Split2Width), ConvertInt(model.Split2Y)));

                    g.DrawString("药品名称", new Font(fontName, ConvertFontInt(model.DrugsTitleFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.DrugsTitleFontX), ConvertInt(model.DrugsTitleFontY));
                    g.DrawString("用量", new Font(fontName, ConvertFontInt(model.UseTitleFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.UseTitleFontX), ConvertInt(model.UseTitleFontY));

                    int x = ConvertInt(model.DrugsContentFontX);
                    int y = ConvertInt(model.DrugsContentFontY);
                    int u_x = ConvertInt(model.UseValueFontX);
                    int u_y = ConvertInt(model.UseValueFontY);

                    // 药品信息
                    for (int i = 0; i < drugs.Count; i++)
                    {
                        int fontHeight = ConvertFontInt(model.DrugsContentFontSize);
                        int margin = 10;
                        float width = paperWidth - ConvertInt(80);
                        float height = fontHeight;
                        int row = (int)Math.Ceiling(height * drug.drug_name.Length / width);
                        for (int j = 0; j < row; j++)
                        {
                            height += j * fontHeight + margin;
                        }

                        // 药名
                        RectangleF rectangle = new RectangleF(x, y, width, height);
                        Font font = new Font(fontName, ConvertFontInt(model.DrugsContentFontSize));
                        StringFormat sf = new StringFormat();
                        //sf.Alignment = StringAlignment.Near;
                        //sf.LineAlignment = StringAlignment.Near;
                        g.DrawString(drugs[i].drug_name, font, bush, rectangle, sf);

                        //g.DrawString(drug.drug_name, new Font(fontName, ConvertFontInt(model.DrugsContentFontSize)), bush, x, y);
                        //用量
                        g.DrawString(drugs[i].use_count, new Font(fontName, ConvertFontInt(model.UseValueFontSize)), bush, u_x, u_y);

                        // 只修改Y轴，向下平铺
                        y += (int)height + margin;
                        u_y += (int)height + margin;
                    }

                    g.DrawString(string.Format("处方医生：{0}", drug.doctor_name), new Font(fontName, ConvertFontInt(model.DoctorFontSize)), bush, ConvertInt(model.DoctorFontX), ConvertInt(model.DoctorFontY));
                    g.DrawString(string.Format("备注：{0}", drug.pass_remark), new Font(fontName, ConvertFontInt(model.RemarkFontSize)), bush, ConvertInt(model.RemarkFontX), ConvertInt(model.RemarkFontY));
                    g.DrawString(string.Format("滴速：{0}   {1}   qd(8点)", drug.ml_speed, drug.usage_name), new Font(fontName, ConvertFontInt(model.SpeedFontSize)), bush, ConvertInt(model.SpeedFontX), ConvertInt(model.SpeedFontY));

                    g.DrawLine(new System.Drawing.Pen(bush), new System.Drawing.Point(ConvertInt(model.Split3X), ConvertInt(model.Split3Y)), new System.Drawing.Point(ConvertInt(model.Split3X + model.Split3Width), ConvertInt(model.Split3Y)));

                    g.DrawString(string.Format("审核：{0}", drug.checker), new Font(fontName, ConvertFontInt(model.ExamineFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.ExamineFontX), ConvertInt(model.ExamineFontY));
                    g.DrawString(string.Format("复审：{0}", ""), new Font(fontName, ConvertFontInt(model.ReviewFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.ReviewFontX), ConvertInt(model.ReviewFontY));
                    g.DrawString(string.Format("排药：{0}", drug.deliveryer), new Font(fontName, ConvertFontInt(model.SortFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.SortFontX), ConvertInt(model.SortFontY));
                    g.DrawString(string.Format("配液：{0}", drug.config_person), new Font(fontName, ConvertFontInt(model.DispensingFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.DispensingFontX), ConvertInt(model.DispensingFontY));
                    g.DrawString("配液：___时___分", new Font(fontName, ConvertFontInt(model.DispensingDateFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.DispensingDateFontX), ConvertInt(model.DispensingDateFontY));

                    ZebraImageI imageI = ZebraImageFactory.GetImage(image);


                    b.AppendLine(string.Format("生成标签完成：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));

                    // 控制打印宽度
                    printer.PrintImage(imageI, 0, 0, paperWidth, paperHeight, false);
                }
                catch (Exception e)
                {
                    myEventLog.Log.Error(e.Message, e);
                    new LogHelper().PrinterLog(e.Message);
                }
                finally
                {
                    g.Dispose();
                    image.Dispose();
                }
            }
            catch (ConnectionException e)
            {
                    myEventLog.Log.Error(e.Message, e);
                new LogHelper().PrinterLog(e.Message);
                Console.WriteLine(e.ToString());
            }
            catch (ZebraPrinterLanguageUnknownException e)
            {
                    myEventLog.Log.Error(e.Message, e);
                new LogHelper().PrinterLog(e.Message);
                Console.WriteLine(e.ToString());
            }
            catch (IOException e)
            {
                    myEventLog.Log.Error(e.Message, e);
                new LogHelper().PrinterLog(e.Message);
                Console.WriteLine(e.ToString());
            }
            finally
            {
                connection.Close();
            }

            #endregion
        }

        /// <summary>
        /// 重置打印机
        /// </summary>
        private void ResetPrinter()
        {
            #region 重置打印机

            try
            {
                connection.Open();
                ZebraPrinter printer = ZebraPrinterFactory.GetInstance(connection);
                printer.Reset();
            }
            catch (ConnectionException e)
            {
                    myEventLog.Log.Error(e.Message, e);
                new LogHelper().PrinterLog(e.Message);
                Console.WriteLine(e.ToString());
            }
            catch (ZebraPrinterLanguageUnknownException e)
            {
                    myEventLog.Log.Error(e.Message, e);
                new LogHelper().PrinterLog(e.Message);
                Console.WriteLine(e.ToString());
            }
            catch (IOException e)
            {
                    myEventLog.Log.Error(e.Message, e);
                new LogHelper().PrinterLog(e.Message);
                Console.WriteLine(e.ToString());
            }
            finally
            {
                connection.Close();
            }

            #endregion
        }

        #endregion

        #region 接收串口信息

        /// <summary>
        /// CCD 接收串口信息并处理
        /// </summary>
        /// <param name="data">收到的数据</param>
        public void OnCCDDataReceived(string data)
        {
            try
            {
                myEventLog.Log.Info($"接收CCD：{data}");
                // CCD1识别失败
                if (data == CCDSerialPortData.CCD1_ERROR)
                {
                    // 这里设置显示串口正常
                    Dispatcher.Invoke(() =>
                    {
                        Ellipse ellipse = spcViewPanel.FindName("elCCD1") as Ellipse;
                        ellipse.Fill = complate;
                        Label label = spcViewPanel.FindName("lblCCD1") as Label;
                        label.Content = "CCD1通讯正常";
                    });

                    DrugsQueueModel model = queue.Find(m => m.Drug == null);
                    if (model != null && model.Count < MaxTryPhotographTimes-1)
                    {
                        //1#拍照
                        Thread.Sleep(10);
                        CCDSerialPortUtils.GetInstance(this).SendData(CCDSerialPortData.TAKE_PICTURE1);
                        model.Count++;
                    }
                    else
                    {
                        // 从1#位剔除药袋
                        PLCSerialPortUtils.GetInstance(this).SendData(PLCSerialPortData.DOT1_OUT);

                        // 删除队列
                        RemoveCCD1Error();
                    }
                }
                // CCD2识别失败
                else if (data == CCDSerialPortData.CCD2_ERROR)
                {
                    // 这里设置显示串口正常
                    Dispatcher.Invoke(() =>
                    {
                        Ellipse ellipse = spcViewPanel.FindName("elCCD2") as Ellipse;
                        ellipse.Fill = complate;
                        Label label = spcViewPanel.FindName("lblCCD2") as Label;
                        label.Content = "CCD2通讯正常";
                    });

                    DrugsQueueModel model = queue.FindLast(m => m.Drug != null);
                    if (model != null && model.Count < MaxTryPhotographTimes-1)
                    {
                        //2#拍照
                        Thread.Sleep(10);
                        CCDSerialPortUtils.GetInstance(this).SendData(CCDSerialPortData.TAKE_PICTURE2);
                        model.Count++;
                    }
                    else
                    {
                        // 从2#位剔除药袋
                        PLCSerialPortUtils.GetInstance(this).SendData(PLCSerialPortData.DOT2_OUT);

                        // 删除队列
                        RemoveCCD2Error();
                    }
                }
                // CCD1识别结果处理，且不是错误信息
                else if (data.Length == CCDSerialPortData.CCD1_ERROR.Length)
                {
                    // 这里设置显示串口正常
                    Dispatcher.Invoke(() =>
                    {
                        Ellipse ellipse1 = spcViewPanel.FindName("elCCD1") as Ellipse;
                        ellipse1.Fill = complate;
                        Label label1 = spcViewPanel.FindName("lblCCD1") as Label;
                        label1.Content = "CCD1通讯正常";

                        Ellipse ellipse2 = spcViewPanel.FindName("elCCD2") as Ellipse;
                        ellipse2.Fill = complate;
                        Label label2 = spcViewPanel.FindName("lblCCD2") as Label;
                        label2.Content = "CCD2通讯正常";
                    });

                    StringBuilder b = new StringBuilder();

                    b.AppendLine(string.Format("{0}：开始处理", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));

                    string[] datas = data.Split(' ');
                    b.AppendLine(string.Format("{0}：分割完成", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                    // 数据正确
                    if (datas[0] == "FE" && datas[datas.Length - 1] == "EF")
                    {
                        b.AppendLine(string.Format("{0}：对比指令正确性完成", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                        // CCD1的反馈
                        if (datas[1] == "A1")
                        {
                            b.AppendLine(string.Format("{0}：对比位置信息完成", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));

                            string[] spec = CCDSerialPortData.GetNameAndML(datas[2]); // 规格和毫升数
                            string type = CCDSerialPortData.GetTypeValue(datas[3]); // 药袋种类

                            b.AppendLine(string.Format("{0}：规格和药带信息获取完成", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));

                            // 通过扫描出来的信息对比数据源，查找匹配的数据，如果查询到则发指令调整机器大小，否则1#位剔除
                            Model.ListAllModel currentDrug = autoPrintCurrentList.Find(m => m.drug_name.Contains(spec[0]) && m.drug_spec.Contains(spec[1]));
                            b.AppendLine(string.Format("{0}：获取药品信息：{1}-{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), spec[0], spec[1]));

                            if (currentDrug != null)
                            {
                                string mlCmd = PLCSerialPortData.GetSizeCmd(datas[2], datas[3]);
                                b.AppendLine(string.Format("{0}：获取指令完成", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                                // 发送指令，调整PLC药袋大小以及打印机高度
                                PLCSerialPortUtils.GetInstance(this).SendData(mlCmd);
                                b.AppendLine(string.Format("{0}：发送指令完成", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));

                                // 获取水和药品信息
                                AotuDataListBll bll = new AotuDataListBll();
                                List<Model.Print_ymodel> drugs = bll.getPrint_y(currentDrug.Id);
                                if (drugs != null && drugs.Count > 0)
                                {
                                    b.AppendLine(string.Format("{0}：获取药品列表完成", drugs.Count));

                                    // 生成条形码数据
                                    currentDrug.QRcode = Guid.NewGuid().ToString();

                                    myEventLog.Log.Info($"生成条形码：{currentDrug.QRcode}");

                                    // 向打印机推送打印内容
                                    Print(drugs, ref currentDrug, ref b);
                                    b.AppendLine(string.Format("{0}：打印过程完成", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));

                                    WriteQueueDrug(currentDrug, data, spec[0], spec[1]);
                                }
                                else
                                {
                                    // 从1#位剔除（不是本组）的药袋
                                    myEventLog.Log.Info("CCD1失败， 没有药品信息");
                                    PLCSerialPortUtils.GetInstance(this).SendData(PLCSerialPortData.DOT1_OUT);

                                }
                            }
                            else
                            {
                                // 从1#位剔除（不是本组）的药袋
                                    myEventLog.Log.Info("CCD1失败， 没有未提前液体信息");
                                PLCSerialPortUtils.GetInstance(this).SendData(PLCSerialPortData.DOT1_OUT);
                            }

                            new LogHelper().PrinterLog(b.ToString());
                            myEventLog.Log.Info(b.ToString());
                        }
                        // CCD2的反馈
                        else if (datas[1] == "A3")
                        {
                            string[] spec = CCDSerialPortData.GetNameAndML(datas[2]); // 规格和毫升数
                            string batchNumber = datas[4] + datas[5] + datas[6]; //批号

                            // 对比数据源信息和扫描出来的信息是否正确，如果正确则发送指令调整机器，否则1#位剔除
                            // 同时要对比摄像头反馈的 医嘱编号 信息，确认正确性
                            DrugsQueueModel model = queue.FindLast(m => m.Drug != null);
                            if (model != null)
                            {
                                // 二维码损坏，无效，漏扫，药品和规格不匹配，毫升数不匹配， 剔除
                                if (string.IsNullOrEmpty(model.ScanData)==false || model.ScanData != model.QRData || !model.Drug.drug_name.Contains(spec[0]) || !model.Drug.drug_spec.Contains(spec[1]))
                                {
                                    if (string.IsNullOrEmpty(model.ScanData))
                                    {
                                        myEventLog.Log.Info($"CCD2失败，未扫描到二维码{model.ScanData}");
                                    }
                                    else
                                    if (model.ScanData!=model.QRData)
                                    {
                                        myEventLog.Log.Info($"CCD2失败，扫描到的二维码和液体二维码不一致{model.QRData}，{model.ScanData}");
                                    }

                                    if (!model.Drug.drug_name.Contains(spec[0]))
                                    {
                                        myEventLog.Log.Info($"CCD2失败，溶媒名称不匹配{model.Drug.drug_name}，{spec[0]}");
                                    }

                                    if (!model.Drug.drug_spec.Contains(spec[1]))
                                    {
                                        myEventLog.Log.Info($"CCD2失败，溶媒规格不匹配{model.Drug.drug_spec}，{spec[1]}");
                                    }

                                    // 从2#位剔除信息对比失败的药袋
                                    PLCSerialPortUtils.GetInstance(this).SendData(PLCSerialPortData.DOT2_OUT);

                                    // 记录到异常列表
                                    exceptionList.Add(model.Drug);
                                    autoPrintCurrentList.Remove(model.Drug);
                                    autoPrintList.Remove(model.Drug);

                                    // 显示界面效果
                                    Dispatcher.Invoke(() =>
                                    {
                                        dgvGroupDetailList.ItemsSource = null;
                                        dgvGroupDetailList.ItemsSource = autoPrintCurrentList;
                                    });
                                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action<DataGrid>(DgvListStatus), dgvGroupDetailList);

                                    // --- 删除对比失败的信息 ---
                                    RemoveCCD2Valid();
                                    return;
                                }

                                // 修改到数据库，修改失败则判为失败
                                AotuDataListBll bll = new AotuDataListBll();
                                if (bll.update_status(model.Drug.Id, model.Drug.QRcode))
                                {
                                    // 2#位通过
                                    PLCSerialPortUtils.GetInstance(this).SendData(PLCSerialPortData.DOT2_PASS);

                                    countModel.AllCount++;
                                    countModel.AutoCount++;
                                    SaveCount();

                                    // 处理到数据源
                                    Model.ListAllModel autoPrintModel = autoPrintList.Find(m => m.Id == model.Drug.Id);
                                    // 回写数据
                                    autoPrintModel.sbatches = batchNumber;
                                    autoPrintModel.printing_time = DateTime.Now;
                                    autoPrintModel.printing_model = 0;
                                    autoPrintModel.printing_status = 1;
                                    autoPrintModel.QRcode = model.Drug.QRcode;

                                    Model.ListAllModel autoPrintCurrentModel = autoPrintCurrentList.Find(m => m.Id == model.Drug.Id);
                                    // 回写数据
                                    autoPrintCurrentModel.sbatches = batchNumber;
                                    autoPrintCurrentModel.printing_time = DateTime.Now;
                                    autoPrintCurrentModel.printing_model = 0;
                                    autoPrintCurrentModel.printing_status = 1;
                                    autoPrintCurrentModel.QRcode = model.Drug.QRcode;

                                    // 显示界面效果
                                    Dispatcher.Invoke(() =>
                                    {
                                        dgvGroupDetailList.ItemsSource = null;
                                        dgvGroupDetailList.ItemsSource = autoPrintCurrentList;
                                    });
                                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action<DataGrid>(DgvListStatus), dgvGroupDetailList);

                                    // --- 设置为CCD2识别通过的状态 ---
                                    Success();

                                    myEventLog.Log.Info($"数据回写成功：Id={autoPrintCurrentModel.Id},QRCode={model.Drug.QRcode},DrugId={model.Drug.Id}");
                                    myEventLog.Log.Info($"CCD2成功");
                                }
                                else // 数据库操作失败
                                {
                                    // 记录到异常列表
                                    exceptionList.Add(model.Drug);
                                    autoPrintCurrentList.Remove(model.Drug);
                                    autoPrintList.Remove(model.Drug);

                                    // 显示界面效果
                                    Dispatcher.Invoke(() =>
                                    {
                                        dgvGroupDetailList.ItemsSource = null;
                                        dgvGroupDetailList.ItemsSource = autoPrintCurrentList;
                                    });
                                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action<DataGrid>(DgvListStatus), dgvGroupDetailList);

                                    // --- 删除对比失败的信息 ---
                                    RemoveCCD2Valid();
                                    myEventLog.Log.Info($"数据回写失败：QRCode={model.Drug.QRcode},DrugId={model.Drug.Id}");
                                    myEventLog.Log.Info($"CCD2失败 数据回写失败");
                                }
                            }
                            else
                            {
                                // 从2#位剔除信息对比失败的药袋
                                PLCSerialPortUtils.GetInstance(this).SendData(PLCSerialPortData.DOT2_OUT);

                                #warning 没有药品信息，不应该记录异常
                                // 记录到异常列表
                                //exceptionList.Add(model.Drug);
                                //autoPrintCurrentList.Remove(model.Drug);
                                //autoPrintList.Remove(model.Drug);

                                // 显示界面效果
                                Dispatcher.Invoke(() =>
                                {
                                    dgvGroupDetailList.ItemsSource = null;
                                    dgvGroupDetailList.ItemsSource = autoPrintCurrentList;
                                });
                                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action<DataGrid>(DgvListStatus), dgvGroupDetailList);

                                // --- 删除对比失败的信息 ---
                                RemoveCCD2Valid();

                                myEventLog.Log.Info($"CCD2失败 队列中未找到液体");
                            }
                        }
                    }
                    // 数据错误
                    else
                    {
                        // 暂不处理
                    }
                }
            }
            catch (Exception ex)
            {
                    myEventLog.Log.Error(ex.Message, ex);
                new LogHelper().ErrorLog($"处理接收CCD数据出错。"+ex.Message);
            }
        }

        /// <summary>
        /// PLC 数据接收
        /// </summary>
        /// <param name="data"></param>
        public void OnPLCDataReceived(string data)
        {
            try
            {
                myEventLog.Log.Info($"接收到PLC：{data}");
                // PLC 报错
                if (data == PLCSerialPortData.ERROR)
                {
                    Dispatcher.Invoke(() =>
                    {
                        btnPrint.IsEnabled = true;
                        btnStopPrint.IsEnabled = false;
                    });

                    ResetPrinter();
                }
                // PLC 超时停机
                else if (data == PLCSerialPortData.OVER_TIME)
                {
                    Dispatcher.Invoke(() =>
                    {
                        btnPrint.IsEnabled = true;
                        btnStopPrint.IsEnabled = false;
                    });

                    ResetPrinter();
                }
                // PLC 缺纸
                else if (data == PLCSerialPortData.NO_PAPER)
                {
                    Dispatcher.Invoke(() =>
                    {
                        btnPrint.IsEnabled = true;
                        btnStopPrint.IsEnabled = false;
                        MessageBox.Show("设备的打印机缺纸，请处理后再进行工作！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    });

                    ResetPrinter();
                }
                // PLC 缺色带
                else if (data == PLCSerialPortData.NO_COLOR)
                {
                    Dispatcher.Invoke(() =>
                    {
                        btnPrint.IsEnabled = true;
                        btnStopPrint.IsEnabled = false;
                        MessageBox.Show("设备的打印机缺少色带，请处理后再进行工作！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    });

                    ResetPrinter();
                }
                // 接收指令触发1#拍照
                else if (data == PLCSerialPortData.CCD1_TACK_PICTURE)
                {
                    // --- 加入队列 ---
                    AddQueue();

                    // 停50毫秒稳定液体
                    Thread.Sleep(50);
                    CCDSerialPortUtils.GetInstance(this).SendData(CCDSerialPortData.TAKE_PICTURE1);
                }
                // 接收指令触发2#拍照
                else if (data == PLCSerialPortData.CCD2_TACK_PICTURE)
                {
                    // 停50毫秒稳定液体
                    Thread.Sleep(50);
                    CCDSerialPortUtils.GetInstance(this).SendData(CCDSerialPortData.TAKE_PICTURE2);
                }
                // 过光幕
                else if (data == PLCSerialPortData.LIGHT_PASS)
                {
                    GoToScan();
                }
                // 重置完成
                else if (data == PLCSerialPortData.RESET_COMPLATE)
                {
                    // 这里设置显示串口正常
                    Dispatcher.Invoke(() =>
                    {
                        Ellipse ellipse = spcViewPanel.FindName("elControlSystem") as Ellipse;
                        ellipse.Fill = complate;
                        Label label = spcViewPanel.FindName("lblControlSystem") as Label;
                        label.Content = "控制系统通讯正常";
                    });
                }
            }
            catch (Exception ex)
            {
                    myEventLog.Log.Error("处理接收PLC数据出错。"+ex.Message, ex);
                new LogHelper().ErrorLog(ex.Message);
            }
        }

        /// <summary>
        /// 扫码枪扫码结果
        /// </summary>
        /// <param name="data"></param>
        public void OnScannerDataReceived(string data)
        {
            try
            {

                myEventLog.Log.Info($"接收扫码枪：{data}");
                // 这里设置显示串口通讯
                Dispatcher.Invoke(() =>
                {
                    Ellipse ellipse = spcViewPanel.FindName("elScan1") as Ellipse;
                    ellipse.Fill = errorColor;
                    Label label = spcViewPanel.FindName("lblScan1") as Label;
                    label.Content = "扫描系统1通讯正常";
                });

                SetScanResult(data);
            }
            catch (Exception ex)
            {
                    myEventLog.Log.Error("处理接收到自动扫码枪数据出错。" + ex.Message, ex);
                new LogHelper().ErrorLog(ex.Message);
            }
        }

        /// <summary>
        /// 手持扫码枪扫描结果
        /// </summary>
        /// <param name="data"></param>
        public void OnScannerHandlerDataReceived(string data)
        {
            try
            {
                myEventLog.Log.Info($"接收到手持扫码枪：{data}");
                if (tabMain.SelectedIndex != 2)
                {
                    Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show("不在“补打签队列”标签页面无法使用手持扫码枪！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    });
                    return;
                }

                // 这里设置显示串口通讯
                Dispatcher.Invoke(() =>
                {
                    Ellipse ellipse = spcViewPanel.FindName("elScan2") as Ellipse;
                    ellipse.Fill = errorColor;
                    Label label = spcViewPanel.FindName("lblScan2") as Label;
                    label.Content = "扫描系统2通讯正常";
                });

                // 这里处理扫码枪扫码结果
                GetDurgInfoByScanner(data);
            }
            catch (Exception ex)
            {
                    myEventLog.Log.Error("处理接收到手动扫码枪数据出错。"+ex.Message, ex);
                new LogHelper().ErrorLog(ex.Message);
            }
        }

        #endregion

        #region 队列方法 - 先进先出原则

        /// <summary>
        /// 流程全部完成
        /// 从队列中推出
        /// </summary>
        private void Success()
        {
            // 判断是否有队列数据
            if (queue.Count > 0)
            {
                queue.RemoveAt(queue.Count - 1);
            }
        }

        /// <summary>
        /// 推入一个
        /// 
        /// 没有药品信息
        /// </summary>
        private void AddQueue()
        {
            // 保存溶媒信息到队列
            DrugsQueueModel qModel = new DrugsQueueModel();
            // 添加到队列
            queue.Add(qModel);
        }

        /// <summary>
        /// 获取进入的第一个料
        /// 写入扫描信息和数据库信息
        /// 并重置CCD计数器
        /// </summary>
        private void WriteQueueDrug(Model.ListAllModel drug, string cmd, string spec, string ml)
        {
            if (queue.Count > 0)
            {
                // 从队列获取没有药品信息的数据
                DrugsQueueModel qModel = queue.Find(m => m.Drug == null);

                if (qModel != null)
                {
                    // 保存识别数据
                    qModel.CMD = cmd;
                    qModel.Drug = drug;
                    qModel.Spec = spec;
                    qModel.ML = ml;
                    qModel.Count = 0;
                    qModel.QRData = drug.QRcode;

                    myEventLog.Log.Info($"打印完成，修改队列中的液体信息");
                }
            }
        }

        /// <summary>
        /// 删除队列中第一个数据
        /// </summary>
        private void RemoveCCD1Error()
        {
            // 判断是否有队列数据
            if (queue.Count > 0)
            {
                queue.RemoveAt(0);
            }
        }

        /// <summary>
        /// CCD2错误时删除对象
        /// </summary>
        private void RemoveCCD2Error()
        {
            // 判断是否有队列数据
            if (queue.Count > 0)
            {
                queue.RemoveAt(queue.Count - 1);
            }
        }

        /// <summary>
        /// CCD2信息对比失败时删除对象
        /// </summary>
        private void RemoveCCD2Valid()
        {
            // 判断是否有队列数据
            if (queue.Count > 0)
            {
                queue.RemoveAt(queue.Count - 1);
            }
        }

        /// <summary>
        /// 记录扫码枪扫描的二维码信息
        /// </summary>
        /// <param name="result"></param>
        private void SetScanResult(string result)
        {
            // 找到第一条刚过光幕的数据，赋值
            DrugsQueueModel model = queue.Find(m => m.GoToScan);
            if (model != null)
            {
                model.ScanData = result;
            }
        }


        /// <summary>
        /// 记录通过光幕，马上要扫描
        /// 保证扫描前不能有新的物料通过光幕
        /// </summary>
        private void GoToScan()
        {
            // 找到最后一个没有记录的进行记录
            DrugsQueueModel model = queue.FindLast(m => !m.GoToScan);
            if (model != null)
            {
                model.GoToScan = true;
            }
        }

        #endregion

        #region 串口链接检查

        // 串口状态，灰色-失败
        SolidColorBrush errorColor = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#dddddd"));
        // 串口状态，绿色-成功
        SolidColorBrush complate = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#8cea00"));

        public void OnCCD1Error(string msg)
        {
            // 这里设置显示串口连接失败
            Dispatcher.Invoke(() =>
            {
                Ellipse ellipse = spcViewPanel.FindName("elCCD1") as Ellipse;
                ellipse.Fill = errorColor;
                Label label = spcViewPanel.FindName("lblCCD1") as Label;
                label.Content = "CCD1连接失败";
            });
        }
        public void OnCCD1Complated()
        {
            // 这里设置显示串口连接成功
            Dispatcher.Invoke(() =>
            {
                Ellipse ellipse = spcViewPanel.FindName("elCCD1") as Ellipse;
                ellipse.Fill = complate;
                Label label = spcViewPanel.FindName("lblCCD1") as Label;
                label.Content = "CCD1连接成功";
            });

            // 发送测试指令

        }


        public void OnCCD2Error(string msg)
        {
            // 这里设置显示串口连接失败
            Dispatcher.Invoke(() =>
            {
                Ellipse ellipse = spcViewPanel.FindName("elCCD2") as Ellipse;
                ellipse.Fill = errorColor;
                Label label = spcViewPanel.FindName("lblCCD2") as Label;
                label.Content = "CCD2连接失败";
            });
        }
        public void OnCCD2Complated()
        {
            // 这里设置显示串口连接成功
            Dispatcher.Invoke(() =>
            {
                Ellipse ellipse = spcViewPanel.FindName("elCCD2") as Ellipse;
                ellipse.Fill = complate;
                Label label = spcViewPanel.FindName("lblCCD2") as Label;
                label.Content = "CCD2连接成功";
            });
        }


        public void OnPLCError(string msg)
        {
            // 这里设置显示串口连接失败
            Dispatcher.Invoke(() =>
            {
                Ellipse ellipse = spcViewPanel.FindName("elControlSystem") as Ellipse;
                ellipse.Fill = errorColor;
                Label label = spcViewPanel.FindName("lblControlSystem") as Label;
                label.Content = "控制系统连接失败";
            });
        }
        public void OnPLCComplated()
        {
            // 这里设置显示串口连接成功
            Dispatcher.Invoke(() =>
            {
                Ellipse ellipse = spcViewPanel.FindName("elControlSystem") as Ellipse;
                ellipse.Fill = complate;
                Label label = spcViewPanel.FindName("lblControlSystem") as Label;
                label.Content = "控制系统连接成功";
            });
        }


        public void OnScannerError(string msg)
        {
            // 这里设置显示串口连接失败
            Dispatcher.Invoke(() =>
            {
                Ellipse ellipse = spcViewPanel.FindName("elScan1") as Ellipse;
                ellipse.Fill = errorColor;
                Label label = spcViewPanel.FindName("lblScan1") as Label;
                label.Content = "扫描系统1连接失败";
            });
        }
        public void OnScannerComplated()
        {
            // 这里设置显示串口连接成功
            Dispatcher.Invoke(() =>
            {
                Ellipse ellipse = spcViewPanel.FindName("elScan1") as Ellipse;
                ellipse.Fill = complate;
                Label label = spcViewPanel.FindName("lblScan1") as Label;
                label.Content = "扫描系统1连接成功";
            });
        }


        public void OnScannerHandlerError(string msg)
        {
            // 这里设置显示串口连接失败
            Dispatcher.Invoke(() =>
            {
                Ellipse ellipse = spcViewPanel.FindName("elScan2") as Ellipse;
                ellipse.Fill = errorColor;
                Label label = spcViewPanel.FindName("lblScan2") as Label;
                label.Content = "扫描系统2连接失败";
            });
        }
        public void OnScannerHandlerComplated()
        {
            // 这里设置显示串口连接成功
            Dispatcher.Invoke(() =>
            {
                Ellipse ellipse = spcViewPanel.FindName("elScan2") as Ellipse;
                ellipse.Fill = complate;
                Label label = spcViewPanel.FindName("lblScan2") as Label;
                label.Content = "扫描系统2连接成功";
            });
        }

        #endregion

        #region 数据库链接检查

        /// <summary>
        /// 链接数据库读取一个信息
        /// </summary>
        private bool CheckDBConnection()
        {
            try
            {
                BLL.tBatch_for_View bll = new tBatch_for_View();
                int count = bll.GetRecordCount("");

                // 这里设置显示串口正常
                Dispatcher.Invoke(() =>
                {
                    Ellipse ellipse = spcViewPanel.FindName("elDb") as Ellipse;
                    ellipse.Fill = complate;
                    Label label = spcViewPanel.FindName("lblDb") as Label;
                    label.Content = "数据库链接正常";
                });
                return true;
            }
            catch (Exception)
            {
                // 这里设置显示串口正常
                Dispatcher.Invoke(() =>
                {
                    Ellipse ellipse = spcViewPanel.FindName("elDb") as Ellipse;
                    ellipse.Fill = errorColor;
                    Label label = spcViewPanel.FindName("lblDb") as Label;
                    label.Content = "数据库链接失败";
                });
                return false;
            }
        }

        #endregion

        #region 数据源加载绑定
        /// <summary>
        /// 
        /// </summary>

        public void LoadData()
        {
            if (!CheckDBConnection())
            {
                MessageBox.Show("数据库连接失败，请检查数据库服务是否开启！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //获取数据
            string strdate = Convert.ToDateTime(use_date.SelectedDate).ToString("yyyy-MM-dd");

            BLL.AotuDataListBll alb = new AotuDataListBll();
            autoPrintList = alb.getlist(strdate, "");


            int autoCount = autoPrintList.Count(m => m.printing_status == 0);
            int handlerCount = handlerPrintList.Count(m => m.printing_status == 0);
            // 未贴签总量
            Label lblUncomplate = spcViewPanel.FindName("lblUncomplate") as Label;
            lblUncomplate.Content = autoCount + handlerCount;
            Label lbl_aotu3 = spcViewPanel.FindName("lbl_aotu3") as Label;
            lbl_aotu3.Content = autoCount;
            Label lbl_manual3 = spcViewPanel.FindName("lbl_manual3") as Label;
            lbl_manual3.Content = handlerCount;
        }

        #endregion

        #region 扫描枪1扫描信息处理 - 补打签队列页面

        /// <summary>
        /// 通过扫描枪扫描到的数据从数据库中读取对应的数据
        /// 显示在 补打签 界面上
        /// </summary>
        private void GetDurgInfoByScanner(string resultCode)
        {

        }

        #endregion

        #region 总数配置

        private void GetCount()
        {
            countModel = new HistoryHelper().GetHistory();

            Label lblTotalNumber = spcViewPanel.FindName("lblTotalNumber") as Label;
            lblTotalNumber.Content = countModel.AllCount;

            Label lbl_aotu1 = spcViewPanel.FindName("lbl_aotu1") as Label;
            lbl_aotu1.Content = countModel.AutoCount;

            Label lbl_manual1 = spcViewPanel.FindName("lbl_manual1") as Label;
            lbl_manual1.Content = countModel.HandlerCount;

        }

        private void SaveCount()
        {
            new HistoryHelper().SaveConfig(countModel);
        }

        #endregion
    }

}
