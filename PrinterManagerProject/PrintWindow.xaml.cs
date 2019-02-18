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
using System.Threading.Tasks;

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

        private string currentBatch;
        private string currentDate;

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

            // 检测打印机中是否有多余的打印内容，导致标签打印错位
            //CheckLabelsRemainingInBatch();
        }

        #region 事件响应
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
                    if (row == null)
                    {
                        continue;
                    }

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
                //MessageBoxResult result = MessageBox.Show("确定是退出贴签系统吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
                ////关闭窗口
                //if (result == MessageBoxResult.Yes)
                //{

                //    e.Cancel = false;
                //}
                ////不关闭窗口
                //if (result == MessageBoxResult.No)
                //    e.Cancel = true;


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

            // 清空队列
            queue = new List<DrugsQueueModel>();

            if (tabMain.SelectedIndex == 0)
            {
                var printer = GetPrinter();
                var status = printer.GetCurrentStatus();
                if (status.labelsRemainingInBatch > 0)
                {
                    // 打印机中有打印任务的，重置打印机
                    printer.Reset();
                    zPrinter = null;
                }

                // 先关再开
                PLCSerialPortUtils plcUtils = PLCSerialPortUtils.GetInstance(this);
                plcUtils.SendData(PLCSerialPortData.MACHINE_STOP);
                plcUtils.SendData(PLCSerialPortData.MACHINE_START);

                btnPrint.IsEnabled = false;
                btnStopPrint.IsEnabled = true;

                // 更新打印模板
                GetPrinterModelConfig();

            }
            else
            {
                MessageBox.Show("当前标签页面不能自动打印！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ButtonStopPrint_Click(object sender, RoutedEventArgs e)
        {
            PLCSerialPortUtils.GetInstance(this).SendData(PLCSerialPortData.MACHINE_STOP);

            ResetPrinter();

            btnPrint.IsEnabled = true;
            btnStopPrint.IsEnabled = false;


        }

        private void Use_date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            currentDate = this.use_date.SelectedDate?.ToString("yyyy-MM-dd");
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

            if (this.cb_batch.SelectedValue != null)
            {
                currentBatch = this.cb_batch.SelectedValue.ToString();
            }

            //  根据tab选择要操作的数据源
            List<Model.ListAllModel> currentSourceList = null;
            switch (tabMain.SelectedIndex)
            {
                case 1:

                    // 绑定显示的列表
                    handlerPrintCurrentList = handlerPrintList.FindAll(m => m.batch == this.cb_batch.SelectedValue + "").ToList();
                    dgvGroupNoAutoList.EnableRowVirtualization = false;
                    dgvGroupNoAutoList.DataContext = handlerPrintCurrentList;
                    dgvGroupNoAutoList.ItemsSource = handlerPrintCurrentList;

                    // 修改数据背景色
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action<DataGrid>(DgvListStatus), dgvGroupNoAutoList);

                    BindDgvs(handlerPrintList);
                    break;
                case 0:
                default:

                    // 绑定显示的列表
                    autoPrintCurrentList = autoPrintList.FindAll(m => m.batch == this.cb_batch.SelectedValue + "").ToList();
                    dgvGroupDetailList.EnableRowVirtualization = false;
                    dgvGroupDetailList.DataContext = autoPrintCurrentList;
                    dgvGroupDetailList.ItemsSource = autoPrintCurrentList;

                    // 修改数据背景色
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action<DataGrid>(DgvListStatus), dgvGroupDetailList);

                    BindDgvs(autoPrintList);
                    break;
            }

        }
        /// <summary>
        /// 绑定列表数据
        /// </summary>
        /// <param name="dataSource"></param>
        private void BindDgvs(List<Model.ListAllModel> dataSource)
        {
            if (dataSource.Count <= 0)
            {
                return;
            }
            //绑定右（溶媒统计）列表
            var solventlist = dataSource.GroupBy(m => new { m.drug_name, m.drug_spec }).
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
            var deptList = dataSource.GroupBy(m => new { m.departmengt_name, m.department_code }).Select(a => new { dept_name = a.Key.departmengt_name, dept_code = a.Key.department_code }).ToList();
            deptList.Insert(0, new { dept_name = "全部", dept_code = "0" });
            this.cb_dept.DisplayMemberPath = "dept_name";
            this.cb_dept.SelectedValuePath = "dept_code";
            this.cb_dept.ItemsSource = deptList;

            // 绑定药品分类
            var drugCategoryList = dataSource.GroupBy(m => new { m.ydrug_class_name }).Select(a => new { class_name = a.Key.ydrug_class_name }).ToList();
            drugCategoryList.Insert(0, new { class_name = "全部" });
            this.cb_drug_category.DisplayMemberPath = "class_name";
            this.cb_drug_category.SelectedValuePath = "class_name";
            this.cb_drug_category.ItemsSource = drugCategoryList;

            // 绑定主药
            var drugList = dataSource.GroupBy(m => new { m.ydrug_name, m.ydrug_spec }).Select(a => new { ydrug_name = string.Format("{0}({1})", a.Key.ydrug_name, a.Key.ydrug_spec), ydrug_id = string.Format("{0}|{1}", a.Key.ydrug_name, a.Key.ydrug_spec) }).ToList();
            drugList.Insert(0, new { ydrug_name = "全部", ydrug_id = "" });
            this.cb_drug.DisplayMemberPath = "ydrug_name";
            this.cb_drug.SelectedValuePath = "ydrug_id";
            this.cb_drug.ItemsSource = drugList;
        }

        #endregion

        #region 读取打印机状态

        /// <summary>
        /// 初始化打印机并检测状态
        /// </summary>
        private void InitPrinter()
        {

            string errorMsg = "";
            try
            {

                PrinterStatus printerStatus = GetPrinterStatus(null);
                if(printerStatus == null)
                {
                    return;
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
                else if(printerStatus.isPaused)
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
                if (string.IsNullOrEmpty(errorMsg) == false)
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

        /// <summary>
        /// 尝试打开打印机连接
        /// </summary>
        /// <returns></returns>
        private bool TryOpenPrinterConnection()
        {
            try
            {
                var startTime = DateTime.Now;
                if (connection == null)
                {
                    DiscoveredUsbPrinter usbPrinter = null;
                    List<DiscoveredUsbPrinter> printers = UsbDiscoverer.GetZebraUsbPrinters(new ZebraPrinterFilter());
                    if (printers == null || printers.Count <= 0)
                    {
                        MessageBox.Show("没有检测到打印机，请检查打印机是否开启！");
                        myEventLog.LogInfo("没有检测到打印机，请检查打印机是否开启！");
                        return false;
                    }
                    usbPrinter = printers[0];

                    connection = new UsbConnection(usbPrinter.Address);
                }
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
                myEventLog.LogError("打开打印机连接出错！",ex);
                return false;
            }
        }

        ZebraPrinter zPrinter = null;
        /// <summary>
        /// 获取ZebraPrinter
        /// </summary>
        /// <returns></returns>
        private ZebraPrinter GetPrinter()
        {
            //if (TryOpenPrinterConnection())
            //{
            //    return ZebraPrinterFactory.GetInstance(connection);
            //}
            //return null;
            if (TryOpenPrinterConnection())
            {
                if (zPrinter!=null && zPrinter.Connection.Connected == false)
                {
                    myEventLog.LogInfo("Printer连接失败，重置Printer！");
                    zPrinter = null;
                }
                if (zPrinter == null)
                {
                    zPrinter = ZebraPrinterFactory.GetInstance(connection);

                }
            }
            return zPrinter;
        }

        /// <summary>
        /// 获取打印机状态
        /// </summary>
        /// <returns></returns>
        private PrinterStatus GetPrinterStatus(ZebraPrinter printer)
        {
            if(printer == null)
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
        /// 获取打印机缓冲区中未打印标签数量
        /// </summary>
        /// <returns></returns>
        private int GetLabelsRemainingInBatch(ZebraPrinter printer)
        {
            var status = GetPrinterStatus(printer);
            if (status != null)
            {
                return status.labelsRemainingInBatch;
            }
            return 0;
        }

        /// <summary>
        /// 打印机中是否有多余的打印任务
        /// </summary>
        bool PrinterHasExtraContent = false;
        /// <summary>
        /// 检查打印机中是否有多余打印任务
        /// </summary>
        /// <returns></returns>
        private void CheckLabelsRemainingInBatch()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    CheckPrintHasExtraContent();

                    Thread.Sleep(10000);
                }
            });
        }
        private void CheckLabelsRemainingInBatchOnce()
        {
            Task.Run(() =>
            {
                CheckPrintHasExtraContent();
            });
        }

        private void CheckPrintHasExtraContent()
        {
            try
            {
                var printer = GetPrinter();
                if (printer != null)
                {
                    var labelsCount = GetLabelsRemainingInBatch(printer);
                    PrinterHasExtraContent = labelsCount > queue.Count(s => s.GoToScan == false);

                    myEventLog.LogInfo($"检测打印机是否有多余打印任务：{(PrinterHasExtraContent ? "有" : "无")},打印任务：{labelsCount}，未贴签：{queue.Count(s => s.GoToScan == false)}");
                }
                else
                {
                    myEventLog.LogInfo("检测打印机是否有多余打印任务：打印机连接未开启");
                }
            }
            catch (Exception)
            {
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
            double multiple = printMultiple;
            return Convert.ToInt32(num * multiple);
        }
        int printMultiple = 3;
        /// <summary>
        /// 生成打印内容并推送的打印机
        /// </summary>
        private void Print(ZebraPrinter printer, List<Model.Print_ymodel> drugs, ref Model.ListAllModel drug, ref StringBuilder b)
        {
            #region 打印

            try
            {
                string fontName = "SimSun";

                var paperWidth = model.PageWidth * printMultiple;
                var paperHeight = model.PageHeight * printMultiple;

                var startTime = DateTime.Now;
                Bitmap image = new Bitmap(paperWidth, paperHeight);
                Graphics g = Graphics.FromImage(image);

                System.Drawing.Brush bush = new SolidBrush(System.Drawing.Color.Black);//填充的颜色

                try
                {
                    //消除锯齿
                    g.SmoothingMode = SmoothingMode.Default;  //使绘图质量最高，即消除锯齿
                    //g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    //g.CompositingQuality = CompositingQuality.HighSpeed;
                    //g.TextRenderingHint = TextRenderingHint.AntiAlias; //消除文字锯齿
                    g.PageUnit = GraphicsUnit.Pixel;
                    //清空图片背景颜色
                    g.Clear(System.Drawing.Color.White);

                    // 生成条形码数据
                    if (string.IsNullOrEmpty(drug.QRcode))
                    {
                        drug.QRcode = Guid.NewGuid().ToString();
                        drug.QRcode = drug.QRcode.Substring(drug.QRcode.Length - 21, 20);
                    }
                    myEventLog.LogInfo($"生成条形码：{drug.QRcode}");

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

                    myEventLog.LogInfo($"打印标签二维码内容：{drug.QRcode}");
                    Bitmap bmp = pdf417Writer.Write(drug.QRcode);

                    myEventLog.LogInfo($"标签尺寸：配置 {model.BarCodeWidth}/{model.BarCodeHeight}");
                    myEventLog.LogInfo($"标签尺寸：转换 {ConvertInt(model.BarCodeWidth)}/{ConvertInt(model.BarCodeHeight)}");

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

                    #region 药品列表
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
                    #endregion

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
                    myEventLog.LogInfo($"图片尺寸:{imageI.Width}×{imageI.Height}");

                    myEventLog.LogInfo($"画图花费时间:{(DateTime.Now - startTime).TotalMilliseconds}");
                    // 控制打印宽度
                    printer.PrintImage(imageI, 0, 0, paperWidth, paperHeight, false);

                }
                catch (Exception e)
                {
                    myEventLog.LogError(e.Message, e);
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
                    myEventLog.LogError(e.Message, e);
                new LogHelper().PrinterLog(e.Message);
                Console.WriteLine(e.ToString());
            }
            catch (ZebraPrinterLanguageUnknownException e)
            {
                    myEventLog.LogError(e.Message, e);
                new LogHelper().PrinterLog(e.Message);
                Console.WriteLine(e.ToString());
            }
            catch (IOException e)
            {
                    myEventLog.LogError(e.Message, e);
                new LogHelper().PrinterLog(e.Message);
                Console.WriteLine(e.ToString());
            }
            finally
            {
            }

            #endregion
        }
        int cuont = 0;
        private void commandPrint()
        {
            try
            {
                var printer = GetPrinter();

                var command = @"^XAA@N,25,25,B:CYRILLIC.FNT^FO100,20^FS
^FDThis is a test.^FS
^A@N,50,50^FO200,40^FS
^FDThis string uses the B:Cyrillic.FNT^FS
^XZ";
                command = @"^XA
^FO100,100
^BQN,2,10
^FDMM,AAC-42^FS
^XZ";
                command = @"^XA
^LH0,0
^FO203,203
^ABN,30,30
^FD中国^FS
^XZ";
                command = @"^XA^FO50,50^AQN,50,50,E:wryh.FNT^FD中国^FS^XZ";
                command = @"^XA^FO50,50^AQN,50,50^FD中国^FS^XZ";
                //command = @"^XA^WD*:*.FNT*^XZ";
                //if (cuont == 0)
                //{

                //    command = @"^XA^WD*:*.FNT^XZ";
                //}
                //else if(cuont == 1)
                //{
                //    command = @"^XA^CWW,E:wryh.FNT^XZ";

                //}
                //else if (cuont == 2)
                //{
                //    command = @"^XA^WD*:*.FNT*^XZ";

                //}
                //else if(cuont == 3)
                //{
                //    command = @"^XA^FO50,50^AQN,50,50^FD中国^FS^XZ";
                //}
                //if (cuont == 0)
                //{

                //    command = @"^XA^FO50,50^AEN,50,50,E:000.FNT^FD中国^FS^XZ";
                //}
                //else if (cuont == 1)
                //{
                //    command = @"^XA^FO50,50^AEN,50,50,E:AGE001.FNT^FD中国^FS^XZ";

                //}
                //else
                //{
                //    command = @"^XA^FO50,50^AEN,50,50,Z:S.FNT^FDChina^FS^XZ";

                //}
                command = @"^XA^FO50,50^AEN,50,50,E:000.FNT^FD中国^FS^XZ";
                cuont++;
                printer.SendCommand(command);
            }
            catch (Exception)
            {
            }

        }
        /// <summary>
        /// 重置打印机
        /// </summary>
        private void ResetPrinter()
        {
            #region 重置打印机

            try
            {
                var printer = GetPrinter();
                var status = printer.GetCurrentStatus();
                if (status.labelsRemainingInBatch > 0)
                {
                    // 打印机中有打印任务的，重置打印机
                    printer.Reset();
                    zPrinter = null;
                    connection = null;
                }
            }
            catch (ConnectionException e)
            {
                    myEventLog.LogError(e.Message, e);
                new LogHelper().PrinterLog(e.Message);
                Console.WriteLine(e.ToString());
            }
            catch (ZebraPrinterLanguageUnknownException e)
            {
                    myEventLog.LogError(e.Message, e);
                new LogHelper().PrinterLog(e.Message);
                Console.WriteLine(e.ToString());
            }
            catch (IOException e)
            {
                    myEventLog.LogError(e.Message, e);
                new LogHelper().PrinterLog(e.Message);
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if(connection!=null)
                connection.Close();
            }

            #endregion
        }

        #endregion

        #region CCD1超时处理
        CCD1Timer ccd1Timer;
        int ccd1ErrorCount = 0;
        Object ccd1LockHelper = new Object();

        /// <summary>
        /// CCD1命令过期
        /// </summary>
        private void Ccd1Timer_CCD1Expire()
        {
            ccd1ErrorCount = -1;
            myEventLog.LogInfo($"CCD1超时");
            PLCSerialPortUtils.GetInstance(this).SendData(PLCSerialPortData.DOT1_OUT);
        }
        /// <summary>
        /// 发送CCD1拍照命令，并开始CCD1超时计时
        /// </summary>
        private void CCD1TakePicture()
        {
            CCD1StopWait();

            ccd1Timer = new CCD1Timer();
            ccd1Timer.CCD1Expire += Ccd1Timer_CCD1Expire;
            ccd1Timer.Start();
            myEventLog.LogInfo($"CCD1开始计时");
            CCDSerialPortUtils.GetInstance(this).SendData(CCDSerialPortData.TAKE_PICTURE1);
        }
        /// <summary>
        /// 停止CCD1计时
        /// </summary>
        private void CCD1StopWait()
        {
            if (ccd1Timer != null)
            {
                lock (ccd1LockHelper)
                {
                    ccd1Timer.StopWait();
                    myEventLog.LogInfo($"CCD1停止计时");
                    ccd1Timer = null;
                }
            }
        }
        #endregion

        #region CCD2超时处理
        CCD2Timer ccd2Timer;
        int ccd2ErrorCount = 0;
        Object ccd2LockHelper = new Object();

        /// <summary>
        /// CCD2命令过期
        /// </summary>
        private void Ccd2Timer_CCD2Expire()
        {
            ccd2ErrorCount = -1;
            myEventLog.LogInfo($"CCD2超时");
            PLCSerialPortUtils.GetInstance(this).SendData(PLCSerialPortData.DOT2_OUT);
            RemoveCCD2Error();
        }
        /// <summary>
        /// 发送CCD2拍照命令，并开始CCD2超时计时
        /// </summary>
        private void CCD2TakePicture()
        {
            CCD2StopWait();

            ccd2Timer = new CCD2Timer();
            ccd2Timer.CCDExpire += Ccd2Timer_CCD2Expire;
            ccd2Timer.Start();
            myEventLog.LogInfo($"CCD2开始计时");
            CCDSerialPortUtils.GetInstance(this).SendData(CCDSerialPortData.TAKE_PICTURE2);
        }
        /// <summary>
        /// 停止CCD2计时
        /// </summary>
        private void CCD2StopWait()
        {
            if (ccd2Timer != null)
            {
                lock (ccd2LockHelper)
                {
                    ccd2Timer.StopWait();
                    myEventLog.LogInfo($"CCD2停止计时");
                    ccd2Timer = null;
                }
            }
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
                myEventLog.LogInfo($"接收CCD：{data}");
                // CCD1识别失败

#warning 取消CCD1判断出错
                if (data == CCDSerialPortData.CCD1_ERROR + "123")
                {
                    lock (ccd1LockHelper)
                    {
                        CCD1StopWait();
                        if (ccd1ErrorCount == -1)
                        {
                            // CCD1已超时，不再处理
                            myEventLog.LogInfo($"接收CCD：{data}，CCD1已超时，不再处理");
                            return;
                        }

                        // 这里设置显示串口正常
                        Dispatcher.Invoke(() =>
                        {
                            Ellipse ellipse = spcViewPanel.FindName("elCCD1") as Ellipse;
                            ellipse.Fill = complate;
                            Label label = spcViewPanel.FindName("lblCCD1") as Label;
                            label.Content = "CCD1通讯正常";
                        });

                        if (ccd1ErrorCount < MaxTryPhotographTimes - 1)
                        {
                            ccd1ErrorCount++;
                            //1#拍照
                            Thread.Sleep(10);

                            // 重新拍照
                            CCD1TakePicture();
                            myEventLog.LogInfo($"CCD1第{ccd1ErrorCount}次识别失败，重新拍照");
                            //model.Count++;
                        }
                        else
                        {
                            ccd1ErrorCount++;
                            myEventLog.LogInfo($"CCD1第{ccd1ErrorCount}次识别失败，从CCD1剔除");
                            // 从1#位剔除药袋
                            PLCSerialPortUtils.GetInstance(this).SendData(PLCSerialPortData.DOT1_OUT);
                        }
                    }
                }
                // CCD2识别失败
#warning 取消CCD2判断出错
                else if (data == CCDSerialPortData.CCD2_ERROR+"123")
                {
                    lock (ccd2LockHelper)
                    {

                        CCD2StopWait();
                        if (ccd2ErrorCount == -1)
                        {
                            // CCD2已超时，不再处理
                            myEventLog.LogInfo($"接收CCD：{data}，CCD2已超时，不再处理");
                            return;
                        }

                        // 这里设置显示串口正常
                        Dispatcher.Invoke(() =>
                        {
                            Ellipse ellipse = spcViewPanel.FindName("elCCD2") as Ellipse;
                            ellipse.Fill = complate;
                            Label label = spcViewPanel.FindName("lblCCD2") as Label;
                            label.Content = "CCD2通讯正常";
                        });

                        // CCD2处理的是最先入队的
                        lock (queueHelper)
                        {
                            DrugsQueueModel model = queue.FirstOrDefault();
                            if (model != null)
                            {
                                if (string.IsNullOrEmpty(model.ScanData) == false)
                                {
                                    if (ccd2ErrorCount < model.CCD1TakePhotoCount - 1)
                                    {
                                        ccd2ErrorCount++;
                                        //2#拍照
                                        Thread.Sleep(10);
                                        CCD2TakePicture();
                                        myEventLog.LogInfo($"CCD2第{ccd2ErrorCount}次识别失败，重新拍照");

                                        return;

                                    }
                                    else
                                    {
                                        myEventLog.LogInfo($"CCD2拍照次数{ccd2ErrorCount}超过CCD1拍照次数{model.CCD1TakePhotoCount}，从CCD2剔除");
                                    }
                                }
                                else
                                {
                                    myEventLog.LogInfo($"扫码枪未识别到二维码[{model.QRData}]，从CCD2剔除");
                                }
                            }
                            else
                            {
                                myEventLog.LogInfo($"队列中没有液体，CCD2不拍照，从CCD2剔除");
                            }
                        }

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

                    int ccd1Count = 0;
                    if (datas[1] == "A1")
                    {
                        // CCD1拍照次数
                        ccd1Count = ccd1ErrorCount;
                        // CCD1识别成功，停止CCD1计时
                        CCD1StopWait();
                    }
                    else
                    {
                        // CCD1识别成功，停止CCD1计时
                        CCD2StopWait();
                    }

                    b.AppendLine(string.Format("{0}：分割完成", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                    // 数据正确
                    if (datas[0] == "FE" && datas[datas.Length - 1] == "EF")
                    {
                        b.AppendLine(string.Format("{0}：对比指令正确性完成", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                        // CCD1的反馈
                        if (datas[1] == "A1")
                        {
                            //string mlCmd1 = PLCSerialPortData.GetSizeCmd(datas[2], datas[3]);

                            //myEventLog.LogInfo($"向PLC发送液体大小命令：{mlCmd1}");
                            //// 发送指令，调整PLC药袋大小以及打印机高度
                            //PLCSerialPortUtils.GetInstance(this).SendData(mlCmd1);
                            //return;
#warning 取消品规和批号
                            //datas[2] = "B9"; // 250ml 葡萄糖
                            datas[2] = "BA"; // 100ml 葡萄糖
                            datas[3] = "C0";

                            myEventLog.LogInfo("收到CCD1识别成功命令");

                            string[] spec = CCDSerialPortData.GetNameAndML(datas[2]); // 规格和毫升数
                            string type = CCDSerialPortData.GetTypeValue(datas[3]); // 药袋种类

                            myEventLog.LogInfo($"CCD1品规:{spec[0]}-{spec[1]}");
                            Model.ListAllModel currentDrug = GetModelBySpec(spec);

                            if (currentDrug != null)
                            {
                                myEventLog.LogInfo($"CCD1医嘱:{currentDrug.Id}");

                                // 获取水和药品信息
                                AotuDataListBll bll = new AotuDataListBll();
                                List<Model.Print_ymodel> drugs = bll.getPrint_y(currentDrug.Id);
                                if (drugs != null && drugs.Count > 0)
                                {
                                    myEventLog.LogInfo($"药品数量:{drugs.Count}");

                                    string mlCmd = PLCSerialPortData.GetSizeCmd(datas[2], datas[3]);

                                    myEventLog.LogInfo($"向PLC发送液体大小命令：{mlCmd}");
                                    var startTime = DateTime.Now;
                                    var printer = GetPrinter();
                                    myEventLog.LogInfo($"获取Printer花费时间:{(DateTime.Now - startTime).TotalMilliseconds}");
                                    startTime = DateTime.Now;
                                    // 发送指令，调整PLC药袋大小以及打印机高度
                                    if (PLCSerialPortUtils.GetInstance(this).SendData(mlCmd))
                                    {
                                        myEventLog.LogInfo($"CCD1命令发送成功花费时间:{(DateTime.Now - startTime).TotalMilliseconds}");
                                        startTime = DateTime.Now;


                                        //插入到队列中

                                        AddQueue(currentDrug, data, spec[0], spec[1], ccd1Count);
                                        myEventLog.LogInfo($"插入队列花费时间:{(DateTime.Now - startTime).TotalMilliseconds}");
                                        startTime = DateTime.Now;
                                        //Print(printer, drugs, ref currentDrug, ref b);
                                        //commandPrint();
                                        myEventLog.LogInfo($"发送打印内容花费时间:{(DateTime.Now - startTime).TotalMilliseconds}");
                                        //CheckLabelsRemainingInBatchOnce();

                                        //if (PrinterHasExtraContent)
                                        //{
                                        //    myEventLog.LogInfo($"打印机中有额外的打印任务，不发送打印内容");
                                        //}
                                        //else
                                        //{
                                        //    startTime = DateTime.Now;
                                        //    Print(printer, drugs, ref currentDrug, ref b);
                                        //    myEventLog.LogInfo($"发送打印内容花费时间:{(DateTime.Now - startTime).TotalMilliseconds}");
                                        //}

                                        b.AppendLine(string.Format("{0}：打印过程完成",
                                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                                    }
                                    else
                                    {
                                        new LogHelper().PrinterLog($"向PLC发送打印尺寸命令失败");
                                        myEventLog.LogInfo($"向PLC发送打印尺寸命令失败");

                                        // 入队空信息，占位
                                        AddQueue(null, null, null, null, ccd1Count);
                                    }
                                }
                                else
                                {
                                    // 从1#位剔除（不是本组）的药袋
                                    myEventLog.LogInfo($"CCD1失败， 没有药品信息");
                                    AddQueue(null, data, spec[0], spec[1], ccd1Count);
                                    PLCSerialPortUtils.GetInstance(this).SendData(PLCSerialPortData.DOT1_OUT);
                                }
                            }
                            else
                            {
                                // 从1#位剔除（不是本组）的药袋
                                myEventLog.LogInfo($"CCD1失败， 未找到当前品规（{spec[0]}-{spec[1]}）的带贴签液体");
                                PLCSerialPortUtils.GetInstance(this).SendData(PLCSerialPortData.DOT1_OUT);
                            }

                            new LogHelper().PrinterLog(b.ToString());
                        }
                        // CCD2的反馈
                        else if (datas[1] == "A3")
                        {
                            //myEventLog.LogInfo("收到CCD2识别成功命令");
                            //PLCSerialPortUtils.GetInstance(this).SendData(PLCSerialPortData.DOT2_PASS);
                            //Success();
                            //return;

#warning 取消品规
                            datas[2] = "BA";
                            string[] spec = CCDSerialPortData.GetNameAndML(datas[2]); // 规格和毫升数
                            string batchNumber = datas[4] + datas[5] + datas[6]; //批号
                            myEventLog.LogInfo($"CCD2识别到液体规格：{spec[0]}-{spec[1]}");
                            myEventLog.LogInfo($"CCD2识别到批号：{batchNumber}");



                            // 对比数据源信息和扫描出来的信息是否正确，如果正确则发送指令调整机器，否则1#位剔除
                            // 同时要对比摄像头反馈的 医嘱编号 信息，确认正确性

                            bool success = true;
                            DrugsQueueModel model = queue.FirstOrDefault();
                            if (model != null)
                            {
                                // 二维码损坏，无效，漏扫，药品和规格不匹配，毫升数不匹配， 剔除
                                if (string.IsNullOrEmpty(model.ScanData) || model.ScanData != model.QRData || !model.Drug.drug_name.Contains(spec[0]) || !model.Drug.drug_spec.Contains(spec[1]))
                                {
                                    if (string.IsNullOrEmpty(model.ScanData))
                                    {
                                        myEventLog.LogInfo($"CCD2失败，未扫描到二维码{model.ScanData}");
                                    }
                                    else
                                    if (model.ScanData!=model.QRData)
                                    {
                                        myEventLog.LogInfo($"CCD2失败，扫描到的二维码和液体二维码不一致{model.QRData}，{model.ScanData}");
                                    }

                                    if (!model.Drug.drug_name.Contains(spec[0]))
                                    {
                                        myEventLog.LogInfo($"CCD2失败，溶媒名称不匹配{model.Drug.drug_name}，{spec[0]}-{spec[1]}");
                                    }
                                    if (!model.Drug.drug_spec.Contains(spec[1]))
                                    {
                                        myEventLog.LogInfo($"CCD2失败，溶媒规格不匹配{model.Drug.drug_spec}，{spec[1]}");
                                    }
                                    success = false;
                                }
                                if (success == true)
                                {
                                    // 修改到数据库，修改失败则判为失败
                                    AotuDataListBll bll = new AotuDataListBll();
                                    if (bll.update_status(model.Drug.Id, model.Drug.QRcode))
                                    {
                                        myEventLog.LogInfo($"更新到数据库：ID={model.Drug.Id}");
                                        // 2#位通过
                                        PLCSerialPortUtils.GetInstance(this).SendData(PLCSerialPortData.DOT2_PASS);

                                        countModel.AllCount++;
                                        countModel.AutoCount++;
                                        SaveCount();

                                        // 处理到数据源
                                        Model.ListAllModel autoPrintModel = autoPrintList.Find(m => m.Id == model.Drug.Id);
                                        if (autoPrintModel != null)
                                        {
                                            myEventLog.LogInfo($"更新列表：ID={autoPrintModel.Id}");
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
                                            //Dispatcher.Invoke(() =>
                                            //{
                                            //    dgvGroupDetailList.ItemsSource = null;
                                            //    dgvGroupDetailList.ItemsSource = autoPrintCurrentList;
                                            //});
                                            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action<DataGrid>(DgvListStatus), dgvGroupDetailList);

                                            // --- 设置为CCD2识别通过的状态 ---
                                            Success();

                                            myEventLog.LogInfo($"数据回写成功：Id={autoPrintCurrentModel.Id},QRCode={model.Drug.QRcode},DrugId={model.Drug.Id}");
                                            myEventLog.LogInfo($"CCD2成功");
                                        }
                                        else
                                        {
                                            success = false;
                                            myEventLog.LogInfo($"数据回写失败：QRCode={model.Drug.QRcode},DrugId={model.Drug.Id}");
                                        }
                                    }
                                    else // 数据库操作失败
                                    {
                                        // --- 删除对比失败的信息 ---
                                        myEventLog.LogInfo($"数据回写失败：QRCode={model.Drug.QRcode},DrugId={model.Drug.Id}");
                                        myEventLog.LogInfo($"CCD2失败 数据回写失败");
                                    }
                                }
                            }
                            if(success == false)
                            {
                                // 从2#位剔除信息对比失败的药袋
                                PLCSerialPortUtils.GetInstance(this).SendData(PLCSerialPortData.DOT2_OUT);

                                // 记录到异常列表
                                
                                exceptionList.Add(model.Drug);

                                // 显示界面效果
                                //Dispatcher.Invoke(() =>
                                //{
                                //    dgvGroupCheckRejectsList.ItemsSource = exceptionList;
                                //});
                                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action<DataGrid>(DgvListStatus), dgvGroupCheckRejectsList);

                                // --- 删除对比失败的信息 ---
                                RemoveCCD2Valid();
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
                    myEventLog.LogError(ex.Message, ex);
                new LogHelper().ErrorLog($"处理接收CCD数据出错。"+ex.Message);
            }
        }
        /// <summary>
        /// 根据品规挑选一个液体，入队
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        private Model.ListAllModel GetModelBySpec(string[] spec)
        {
            // 通过扫描出来的信息对比数据源，查找匹配的数据，如果查询到则发指令调整机器大小，否则1#位剔除
                var queueIds = queue.Select(s => s.Drug.Id).ToList();
            if (autoPrintCurrentList.Any()==false)
            {
                var batch = currentBatch ?? this.cb_batch.SelectedValue.ToString();
                autoPrintCurrentList = autoPrintList.FindAll(m => m.batch == batch).ToList();
            }
            return autoPrintCurrentList.Find(m => queueIds.Contains(m.Id) == false && string.IsNullOrEmpty(m.QRcode) && m.drug_name.Contains(spec[0]) && m.drug_spec.Contains(spec[1]));
        }

        /// <summary>
        /// PLC 数据接收
        /// </summary>
        /// <param name="data"></param>
        public void OnPLCDataReceived(string data)
        {
            try
            {
                myEventLog.LogInfo($"接收到PLC：{data}");
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
                    // 停50毫秒稳定液体
                    Thread.Sleep(50);

                    // 开始拍照
                    lock (ccd1LockHelper)
                    {
                        ccd1ErrorCount = 0;
                    }
                    CCD1TakePicture();
                }
                // 接收指令触发2#拍照
                else if (data == PLCSerialPortData.CCD2_TACK_PICTURE)
                {
                    // 停50毫秒稳定液体
                    Thread.Sleep(50);
                    // 开始拍照
                    lock (ccd2LockHelper)
                    {
                        ccd2ErrorCount = 0;
                    }
                    CCD2TakePicture();
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
                    myEventLog.LogError("处理接收PLC数据出错。"+ex.Message, ex);
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
                myEventLog.LogInfo($"接收扫码枪：{data}");
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
                    myEventLog.LogError("处理接收到自动扫码枪数据出错。" + ex.Message, ex);
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
                myEventLog.LogInfo($"接收到手持扫码枪：{data}");
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
                    myEventLog.LogError("处理接收到手动扫码枪数据出错。"+ex.Message, ex);
                new LogHelper().ErrorLog(ex.Message);
            }
        }

        #endregion

        #region 队列方法 - 先进先出原则

        object queueHelper = new object();

        /// <summary>
        /// 流程全部完成
        /// 从队列中推出
        /// </summary>
        private void Success()
        {
            lock (queueHelper)
                {
                // 判断是否有队列数据
                if (queue.Count > 0)
                {
                    queue.RemoveAt(queue.Count - 1);
                }
            }
        }

        /// <summary>
        /// 推入队列
        /// 写入扫描信息和数据库信息
        /// 并重置CCD计数器
        /// </summary>
        /// <param name="drug">当前液体信息（携带二维码信息）</param>
        /// <param name="cmd">CCD1命令内容</param>
        /// <param name="spec">规格</param>
        /// <param name="ml">液体毫升数</param>
        /// <param name="ccd1TakePhotoTimes">CCD1拍照次数</param>
        private void AddQueue(Model.ListAllModel drug, string cmd, string spec, string ml,int ccd1TakePhotoTimes)
        {
            // 保存溶媒信息到队列
            DrugsQueueModel qModel = new DrugsQueueModel();
            if(drug != null)
            {
                if (string.IsNullOrEmpty(drug.QRcode))
                {
                    drug.QRcode = Guid.NewGuid().ToString();
                    drug.QRcode = drug.QRcode.Substring(drug.QRcode.Length - 21, 20);
                }
                // 保存识别数据
                qModel.CMD = cmd;
                qModel.Drug = drug;
                qModel.Spec = spec;
                qModel.ML = ml;
                qModel.CCD1TakePhotoCount = ccd1TakePhotoTimes;
                qModel.QRData = drug.QRcode;

                myEventLog.LogInfo($"将液体信息插入队列:Id={qModel.Drug.Id}，Code={qModel.QRData}");
            }
            else
            {
                myEventLog.LogInfo($"将空液体信息插入队列");
            }
            // 添加到队列
            lock (queueHelper)
            {
                queue.Add(qModel);
            }
        }

        /// <summary>
        /// 删除队列中第一个数据
        /// </summary>
        private void RemoveCCD1Error()
        {
            // 判断是否有队列数据
            lock (queueHelper)
            {
                if (queue.Count > 0)
                {
                    queue.RemoveAt(0);
                }
            }
        }

        /// <summary>
        /// CCD2错误时删除对象
        /// </summary>
        private void RemoveCCD2Error()
        {
            // 判断是否有队列数据
            lock (queueHelper)
            {
                if (queue.Count > 0)
                {
                    queue.First().QRData = null;
                    queue.RemoveAt(0);
                }
            }
        }

        /// <summary>
        /// CCD2信息对比失败时删除对象
        /// </summary>
        private void RemoveCCD2Valid()
        {
            // 判断是否有队列数据
            lock (queueHelper)
            {
                if (queue.Count > 0)
                {
                    queue.First().QRData = null;
                    queue.RemoveAt(0);
                }
            }
        }

        /// <summary>
        /// 记录扫码枪扫描的二维码信息
        /// </summary>
        /// <param name="result"></param>
        private void SetScanResult(string result)
        {
            // 找到第一条刚过光幕的数据，赋值
            lock (queueHelper)
            {
                DrugsQueueModel model = queue.Find(m => m.QRData == result);
                if (model != null)
                {
                    model.ScanData = result;
                    model.GoToScan = true;
                    myEventLog.LogInfo($"修改液体扫描到的二维码：{result}");
                }
                else
                {
                    myEventLog.LogInfo($"未找到二维码对应的液体");

                }
            }
        }


        /// <summary>
        /// 记录通过光幕，马上要扫描
        /// 保证扫描前不能有新的物料通过光幕
        /// </summary>
        private void GoToScan()
        {
            // 找到最后一个没有记录的进行记录
            lock (queueHelper)
            {
                DrugsQueueModel model = queue.Find(m => !m.GoToScan);
                if (model != null)
                {
                    model.GoToScan = true;
                }
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
