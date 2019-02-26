using PrinterManagerProject.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PrinterManagerProject.Tools;
using System.Threading;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;
using System.Windows.Shapes;
using System.Text;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using PrinterManagerProject.EF;
using PrinterManagerProject.EF.Models;
using PrinterManagerProject.EF.Bll;

namespace PrinterManagerProject
{
    /// <summary>
    /// PrintWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PrintWindow : BaseWindow, ScanerHandlerSerialPortInterface, ScannerSerialPortInterface, PLCSerialPortInterface, CCDSerialPortInterface
    {
        /// <summary>
        /// 是否要连接设备
        /// </summary>
        public static bool IsConnectDevices = true;
        private List<OrderQueueModel> queue = new List<OrderQueueModel>();
        private Connection connection = null;
        private ZebraPrinter printer = null;
        private SummaryCountModel countModel = new SummaryCountModel();
        private WarningManager warningManager = new WarningManager();
        private PrintTemplateManager printTemplateManager = new PrintTemplateManager();


        #region 列表数据对象
        /// <summary>
        /// 当前批次全部数据
        /// </summary>
        private ObservableCollection<tOrder> autoPrintList = new ObservableCollection<tOrder>();
        // 汇总表
        private ObservableCollection<SolventModel> autoPrintSummaryList = new ObservableCollection<SolventModel>();
        // 自动显示贴签
        private ObservableCollection<tOrder> autoPrintCurrentList = new ObservableCollection<tOrder>();
        // 手动贴签
        private ObservableCollection<tOrder> handlerPrintList = new ObservableCollection<tOrder>();
        // 自动显示贴签
        private ObservableCollection<tOrder> handlerPrintCurrentList = new ObservableCollection<tOrder>();
        // 异常列表
        private ObservableCollection<tOrder> exceptionList = new ObservableCollection<tOrder>();
        #endregion



        #region PLC使用属性
        /// <summary>
        /// CCD1是否正在识别
        /// </summary>
        private bool ccd1IsBusy = false;
        /// <summary>
        /// CCD2是否正在识别
        /// </summary>
        private bool ccd2IsBusy = false;
        /// <summary>
        /// 光幕指令
        /// </summary>
        private PLCReader lightListener;
        /// <summary>
        /// 警告指令
        /// </summary>
        //private PLCReader warningListener;
        /// <summary>
        /// 扫码枪光幕
        /// </summary>
        //private PLCReader scannerLightListener;
        //private PLCReader ccd2LightListener;
        //private PLCReader printLightListener; 
        #endregion

            /// <summary>
            /// CCD连接状态
            /// </summary>
        private static bool CCDConnected = false;
        /// <summary>
        /// PLC连接状态
        /// </summary>
        private static bool PCLConnected = false;
        /// <summary>
        /// 自动扫码枪连接状态
        /// </summary>
        private static bool AutoScannerConnected = false;
        /// <summary>
        /// 手动扫码枪连接状态
        /// </summary>
        private static bool HanderScannerConnected = false;


        private OrderManager orderManager = new OrderManager();

        private bool FormLoading = true;

        IPrinterManager printerManager = new UsbConnectionManager();

        public PrintWindow()
        {
            InitializeComponent();
            base.Loaded += PrintWindow_Loaded;


        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            //if (ConnectionManager.CheckPivasConnetionStatus() == false)
            //{
            //    MessageBox.Show("Pivas数据库连接失败，请检查数据库服务是否开启！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return;
            //}
            if (ConnectionManager.CheckConnetionStatus() == false)
            {
                MessageBox.Show("本地数据库连接失败，请检查数据库服务是否开启！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            InitDate();
            InitBatch();

            // 检测打印机中是否有多余的打印内容，导致标签打印错位
            //CheckLabelsRemainingInBatch();
            FormLoading = false;

            Dispatcher.Invoke(() =>
            {
                this.printerLabel.Content = UserCache.Printer.true_name;
                this.checkerLabel.Content = UserCache.Checker.true_name;
            });

            //LoadData();
        }

        private void PrintWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsConnectDevices)
            {
                // 开启串口
                PLCSerialPortUtils.GetInstance(this).Open();
                CCDSerialPortUtils.GetInstance(this).Open();
                ScannerSerialPortUtils.GetInstance(this).Open();
                ScanHandlerSerialPortUtils.GetInstance(this).Open();
                
                // 检查数据库状态
                if (!CheckDBConnection())
                {
                    return;
                }
                if (printTemplateManager.GetPrintConfig() == null)
                {
                    MessageBox.Show("打印模板参数获取失败！");
                    return;
                }
            }

        }

        #region 初始化
        /// <summary>
        /// 初始化时间
        /// </summary>
        private void InitDate()
        {
            this.use_date.SelectedDate = DateTime.Now.Date;
        }
        /// <summary>
        /// 初始化批次
        /// </summary>
        private void InitBatch()
        {
            tBatch tfv = new tBatch();
            var list = new BatchManager().GetAll();
            list.Insert(0, new tBatch() { batch = "", batch_name = "请选择" });
            this.cb_batch.DisplayMemberPath = "batch_name";
            this.cb_batch.SelectedValuePath = "batch";
            this.cb_batch.ItemsSource = list;

            //绑定批次调整后事件
            this.cb_batch.SelectionChanged += Cb_batch_SelectionChanged;
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
            SendCCD1Out();
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
            ccd1StartTime = DateTime.Now;
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
            SendCCD2Out();
            RemoveCCD2();
        }
        /// <summary>
        /// 发送CCD2拍照命令，并开始CCD2超时计时
        /// </summary>
        private void CCD2TakePicture()
        {
            CCD2StopWait();

            ccd2Timer = new CCD2Timer();
            ccd2Timer.CCDExpire += Ccd2Timer_CCD2Expire;
            ccd2StartTime = DateTime.Now;
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

        private void SendCCD1Out()
        {

            //PLCSerialPortUtils.GetInstance(this).SendData(PLCSerialPortData.DOT1_OUT);
            PLCSerialPortUtils.GetInstance(this).SendData("%01#WDD0090100901324E**");  // 1N:CCD1剔除
            myEventLog.LogInfo($"发送CCD1剔除命令");
            SetCCD1IsNotBusy();
        }

        private void SetCCD1IsNotBusy()
        {
            Task.Run(() =>
            {
                Thread.Sleep(AppConfig.FreeCCDBusyState);
                ccd1IsBusy = false;
            });
        }
        private void SendCCD2Out()
        {

            //PLCSerialPortUtils.GetInstance(this).SendData(PLCSerialPortData.DOT2_OUT);DOT2_PASS
            PLCSerialPortUtils.GetInstance(this).SendData("%01#WDD0090000900344E**"); //4N：CCD2剔除
            myEventLog.LogInfo($"发送CCD2剔除命令");
            SetCCD2IsNotBusy();
        }

        private void SetCCD2IsNotBusy()
        {
            Task.Run(() =>
            {
                Thread.Sleep(AppConfig.FreeCCDBusyState);
                ccd2IsBusy = false;
            });
        }
        #region 接收串口信息

        DateTime ccd1StartTime = DateTime.Now;
        DateTime ccd2StartTime = DateTime.Now;


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
                if (data == CCDSerialPortData.CCD1_ERROR)
                {
                    lock (ccd1LockHelper)
                    {
                        myEventLog.LogInfo($"CCD1拍照花费时间:{(DateTime.Now - ccd1StartTime).TotalMilliseconds}");
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

                        if (ccd1ErrorCount < AppConfig.CCD1TakePhotoMaxTimes - 1)
                        {
                            ccd1ErrorCount++;
                            //1#拍照
                            Thread.Sleep(AppConfig.CcdTakePhotoSleepTime);

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
                            SendCCD1Out();
                        }
                    }
                }
                // CCD2识别失败
#warning 取消CCD2判断出错
                else if (data == CCDSerialPortData.CCD2_ERROR)
                {
                    lock (ccd2LockHelper)
                    {

                        myEventLog.LogInfo($"CCD2拍照花费时间:{(DateTime.Now - ccd2StartTime).TotalMilliseconds}");
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

                        lock (queueHelper)
                        {
                            // CCD2是流程最后一步，取出第一个
                            OrderQueueModel model = queue.FirstOrDefault();
                            if (model != null)
                            {
                                if (string.IsNullOrEmpty(model.ScanData) == false)
                                {
                                    ccd2ErrorCount++;
                                    myEventLog.LogInfo($"Index:{model?.Index},CCD2第{ccd2ErrorCount}次识别失败，重新拍照");

                                    if (ccd2ErrorCount < model.CCD1TakePhotoCount+1)
                                    {
                                        //2#拍照
                                        Thread.Sleep(AppConfig.CcdTakePhotoSleepTime);
                                        CCD2TakePicture();

                                        return;
                                    }
                                    else
                                    {
                                        //myEventLog.LogInfo($"Index:{model?.Index},CCD2拍照次数{ccd2ErrorCount}超过CCD1拍照次数{model.CCD1TakePhotoCount}，从CCD2剔除");
                                        myEventLog.LogInfo($"Index:{model?.Index},CCD2拍照次数{ccd2ErrorCount}超过{ccd2ErrorCount}次，从CCD2剔除");
                                    }
                                }
                                else
                                {
                                    myEventLog.LogInfo($"Index:{model?.Index},扫码枪未识别到二维码[{model.QRData}]，从CCD2剔除");
                                }
                            }
                            else
                            {
                                myEventLog.LogInfo($"Index:{model?.Index},队列中没有液体，CCD2不拍照，从CCD2剔除");
                            }
                        }

                        // 从2#位剔除药袋
                        SendCCD2Out();
                        // 删除队列
                        RemoveCCD2();
                    }
                }
                // CCD1识别结果处理，且不是错误信息
                else if (data.Length == CCDSerialPortData.CCD1_ERROR.Length)
                {

                    #region 设置串口状态
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
                    #endregion


                    string[] datas = data.Split(' ');

                    #region 过滤拍照超时，记录CCD1拍照次数，停止CCD超时计时
                    if (datas[1] == "A1")
                    {
                        myEventLog.LogInfo($"CCD1拍照花费时间:{(DateTime.Now - ccd1StartTime).TotalMilliseconds}");
                        if (ccd1ErrorCount != -1)
                        {
                            // CCD1识别成功，停止CCD1计时
                            CCD1StopWait();
                        }
                        else
                        {
                            myEventLog.LogInfo("收到CCD1识别成功命令超时，不再处理");
                            return;
                        }
                    }
                    else
                    {
                        myEventLog.LogInfo($"CCD2拍照花费时间:{(DateTime.Now - ccd2StartTime).TotalMilliseconds}");
                        if (ccd2ErrorCount != -1)
                        {
                            // CCD1识别成功，停止CCD1计时
                            CCD2StopWait();
                        }
                        else
                        {
                            myEventLog.LogInfo("收到CCD2识别成功命令超时，不再处理");
                            return;
                        }
                    }
                    #endregion

                    StringBuilder b = new StringBuilder();

                    b.AppendLine(string.Format("{0}：开始处理", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));

                    b.AppendLine(string.Format("{0}：分割完成", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                    // 数据正确
                    if (datas[0] == "FE" && datas[datas.Length - 1] == "EF")
                    {
                        b.AppendLine(string.Format("{0}：对比指令正确性完成", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                        // CCD1的反馈
                        if (datas[1] == "A1")
                        {
                            #region 处理CCD1数据
                            //string mlCmd1 = PLCSerialPortData.GetSizeCmd(datas[2], datas[3]);

                            //myEventLog.LogInfo($"向PLC发送液体大小命令：{mlCmd1}");
                            //// 发送指令，调整PLC药袋大小以及打印机高度
                            //PLCSerialPortUtils.GetInstance(this).SendData(mlCmd1);
                            //return;
#warning 取消品规和批号
                            //datas[2] = "B9"; // 250ml 葡萄糖
                            //datas[2] = "BA"; // 100ml 葡萄糖
                            //datas[3] = "C0";

                            myEventLog.LogInfo("收到CCD1识别成功命令");

                            string[] spec = CCDSerialPortData.GetNameAndML(datas[2]); // 规格和毫升数
                            string type = CCDSerialPortData.GetTypeValue(datas[3]); // 药袋种类

                            myEventLog.LogInfo($"CCD1品规:{spec[0]}-{spec[1]}");
                            tOrder currentOrder = GetModelBySpec(spec);

                            if (currentOrder != null)
                            {
                                myEventLog.LogInfo($"CCD1医嘱:{currentOrder.Id}");

                                // 获取水和药品信息

                                List<PrintDrugModel> drugs = orderManager.GetPrintDrugs(currentOrder.Id);
                                //if (drugs != null && drugs.Count > 0)
                                //{
                                myEventLog.LogInfo($"药品数量:{drugs.Count}");

                                string mlCmd = PLCSerialPortData.GetSizeCmd(datas[2], datas[3]);

                                myEventLog.LogInfo($"向PLC发送液体大小命令：{mlCmd}");
                                var startTime = DateTime.Now;
                                var printer = printerManager.GetPrinter();
                                myEventLog.LogInfo($"获取Printer花费时间:{(DateTime.Now - startTime).TotalMilliseconds}");
                                startTime = DateTime.Now;
                                // 发送指令，调整PLC药袋大小以及打印机高度
                                //if (PLCSerialPortUtils.GetInstance(this).SendData(mlCmd))
                                var printCommand = printTemplateManager.GetPrintCommand(currentOrder);
                                myEventLog.LogInfo($"生成打印命令花费时间:{(DateTime.Now - startTime).TotalMilliseconds}");
                                Thread.Sleep(AppConfig.Ccd1SuccessSleepTime);
                                myEventLog.LogInfo($"发送成功指令等待时间:{AppConfig.Ccd1SuccessSleepTime}");
                                startTime = DateTime.Now;

                                myEventLog.LogInfo($"发送CCD1继续命令");
                                var currentCmdData = $"3{mlCmd.ToCharArray()[0]}3{mlCmd.ToCharArray()[1]}";
                                var specCmdData = GetSepcData(currentCmdData);

                                if (PLCSerialPortUtils.GetInstance(this).SendData($"%01#WDD0090100903{specCmdData}**")) // 71-78
                                {
                                    myEventLog.LogInfo($"CCD1命令发送成功花费时间:{(DateTime.Now - startTime).TotalMilliseconds}");
                                    startTime = DateTime.Now;

                                    printer.SendCommand(printCommand);
                                    myEventLog.LogInfo($"发送打印内容花费时间:{(DateTime.Now - startTime).TotalMilliseconds}");

                                    SetCCD1IsNotBusy();
                                    startTime = DateTime.Now;

                                    //插入到队列中
                                    AddQueue(currentOrder, data, spec[0], spec[1], currentCmdData);
                                    myEventLog.LogInfo($"插入队列花费时间:{(DateTime.Now - startTime).TotalMilliseconds}");
                                    b.AppendLine(string.Format("{0}：打印过程完成",
                                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                                }
                                else
                                {
                                    new LogHelper().PrinterLog($"向PLC发送打印尺寸命令失败");
                                    myEventLog.LogInfo($"向PLC发送打印尺寸命令失败");

                                    // 入队空信息，占位
                                    AddQueue(null, null, null, null,"");
                                }

                                //}
                                //else
                                //{
                                //    // 从1#位剔除（不是本组）的药袋
                                //    myEventLog.LogInfo($"CCD1失败， 没有药品信息");
                                //    AddQueue(null, data, spec[0], spec[1]);
                                //    SendCCD1Out();

                                //}
                            }
                            else
                            {
                                // 从1#位剔除（不是本组）的药袋
                                myEventLog.LogInfo($"CCD1失败， 未找到当前品规（{spec[0]}-{spec[1]}）的未贴签液体");
                                SendCCD1Out();
                            }

                            new LogHelper().PrinterLog(b.ToString());
                            #endregion
                        }
                        // CCD2的反馈
                        else if (datas[1] == "A3")
                        {
                            #region 处理CCD2数据

                            //myEventLog.LogInfo("收到CCD2识别成功命令");
                            //PLCSerialPortUtils.GetInstance(this).SendData(PLCSerialPortData.DOT2_PASS);
                            //Success();
                            //return;

#warning 取消品规
                            //datas[2] = "BA";
                            string[] spec = CCDSerialPortData.GetNameAndML(datas[2]); // 规格和毫升数
                            string batchNumber = datas[4] + datas[5] + datas[6]; //批号
                            myEventLog.LogInfo($"CCD2识别到液体规格：{spec[0]}-{spec[1]}");
                            myEventLog.LogInfo($"CCD2识别到批号：{batchNumber}");



                            // 对比数据源信息和扫描出来的信息是否正确，如果正确则发送指令调整机器，否则1#位剔除
                            // 同时要对比摄像头反馈的 医嘱编号 信息，确认正确性

                            bool success = true;
                            // CCD2是流程最后一步，取出第一个
                            OrderQueueModel model = queue.FirstOrDefault();
                            if (success == true && model != null && model.Drug != null)
                            {

                                    // 二维码损坏，无效，漏扫，药品和规格不匹配，毫升数不匹配， 剔除
                                    if (string.IsNullOrEmpty(model.ScanData) || model.ScanData != model.QRData || !model.Drug.drug_name.Contains(spec[0]) || !model.Drug.drug_spec.Contains(spec[1]))
                                    {
                                        if (string.IsNullOrEmpty(model.ScanData))
                                        {
                                            myEventLog.LogInfo($"Index:{model?.Index},CCD2失败，未扫描到二维码{model.ScanData}");
                                            warningManager.AddWarning(model.Drug, spec[0], spec[1], WarningStateEnum.TagUnRecognition, UserCache.Printer.ID, UserCache.Printer.true_name, UserCache.Checker.ID, UserCache.Checker.true_name);
                                        }
                                        else
                                        if (model.ScanData != model.QRData)
                                        {
                                            myEventLog.LogInfo($"Index:{model?.Index},CCD2失败，扫描到的二维码和液体二维码不一致{model.QRData}，{model.ScanData}");
                                            warningManager.AddWarning(model.Drug, spec[0], spec[1], WarningStateEnum.QRCodeMismatch, UserCache.Printer.ID, UserCache.Printer.true_name, UserCache.Checker.ID, UserCache.Checker.true_name);
                                        }

                                        if (!model.Drug.drug_name.Contains(spec[0]))
                                        {
                                            myEventLog.LogInfo($"Index:{model?.Index},CCD2失败，溶媒名称不匹配{model.Drug.drug_name}，{spec[0]}-{spec[1]}");
                                            warningManager.AddWarning(model.Drug, spec[0], spec[1], WarningStateEnum.DrugMismatch, UserCache.Printer.ID, UserCache.Printer.true_name, UserCache.Checker.ID, UserCache.Checker.true_name);
                                        }
                                        else if (!model.Drug.drug_spec.Contains(spec[1]))
                                        {
                                            myEventLog.LogInfo($"Index:{model?.Index},CCD2失败，溶媒规格不匹配{model.Drug.drug_spec}，{spec[1]}");
                                            warningManager.AddWarning(model.Drug, spec[0], spec[1], WarningStateEnum.DrugMismatch, UserCache.Printer.ID, UserCache.Printer.true_name, UserCache.Checker.ID, UserCache.Checker.true_name);
                                        }
                                        success = false;
                                    }
                                if (success == true)
                                {
                                    // 修改到数据库，修改失败则判为失败
                                    bool updateDataSuccess = false;
                                    try
                                    {
                                        orderManager.PrintSuccess(model.Drug.Id, PrintModelEnum.Auto, batchNumber, UserCache.Printer.ID, UserCache.Printer.true_name, UserCache.Checker.ID, UserCache.Checker.true_name);
                                        updateDataSuccess = true;
                                    }
                                    catch (Exception ex)
                                    {
                                        myEventLog.LogError($"更新到数据库出错",ex);
                                    }
                                    if (updateDataSuccess)
                                    {
                                        myEventLog.LogInfo($"Index:{model?.Index},更新到数据库：ID={model.Drug.Id}");
                                        countModel.AutoCount++;
                                        countModel.PrintedTotalCount++;
                                        countModel.NotPrintTotalCount--;
                                        UpdateSummaryLabel();

                                        // 处理到数据源
                                        tOrder autoPrintModel = autoPrintList.FirstOrDefault(m => m.Id == model.Drug.Id);
                                        // 2#位通过
                                        myEventLog.LogInfo($"Index:{model?.Index},发送CCD2继续命令");
                                        PLCSerialPortUtils.GetInstance(this).SendData("%01#WDD00900009003459**");  //1Y:CCD2继续
                                        SetCCD2IsNotBusy();

                                        // 回写数据
                                        PrintSuccess(autoPrintModel, batchNumber);
                                        // --- 设置为CCD2识别通过的状态 ---
                                        Success();

                                        myEventLog.LogInfo($"Index:{model?.Index},数据回写成功：Id={autoPrintModel.Id},QRCode={model.Drug.barcode},DrugId={model.Drug.Id}");
                                        myEventLog.LogInfo($"Index:{model?.Index},CCD2成功");
                                        //if (autoPrintModel != null)
                                        //{
                                        //}
                                        //else
                                        //{
                                        //    success = false;
                                        //    myEventLog.LogInfo($"数据回写失败：QRCode={model.Drug.barcode},DrugId={model.Drug.Id}");
                                        //}
                                    }
                                    else // 数据库操作失败
                                    {
                                        // --- 删除对比失败的信息 ---
                                        myEventLog.LogInfo($"Index:{model?.Index},数据回写失败：QRCode={model.Drug.barcode},DrugId={model.Drug.Id}");
                                        myEventLog.LogInfo($"Index:{model?.Index},CCD2失败 数据回写失败");
                                        warningManager.AddWarning(model.Drug, spec[0], spec[1], WarningStateEnum.DataUpdateFailed, UserCache.Printer.ID, UserCache.Printer.true_name, UserCache.Checker.ID, UserCache.Checker.true_name);
                                        success = false;
                                    }
                                }
                            }
                            else
                            {
                                myEventLog.LogInfo($"CCD2失败，队列中无数据");
                                success = false;
                                //warningManager.AddWarning(model.Drug, spec[0], spec[1], WarningStateEnum.NotInQueue, UserCache.Printer.ID, UserCache.Printer.true_name, UserCache.Checker.ID, UserCache.Checker.true_name);
                            }
                            if (success == false)
                            {
                                // 从2#位剔除信息对比失败的药袋
                                SendCCD2Out();

                                // --- 删除对比失败的信息 ---
                                RemoveCCD2();
                            }
                        } 
                        #endregion
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
                new LogHelper().ErrorLog($"处理接收CCD数据出错。" + ex.Message);
            }
        }
        /// <summary>
        /// 拼接CCD1成功发送的命令
        /// </summary>
        /// <param name="specCmd"></param>
        /// <returns></returns>
        private string GetSepcData(string newCmd)
        {
            /***
             * 901:拨板成功指令+挡板移动指令
             * 902:缓存下一个要打印液体的规格
             * 903:打印机高低+打印纸出纸时间，过打印光幕一段时间后，会将902的值拷贝到903
             */
            // 901
            string specCmd = newCmd;
            lock (queueHelper)
            {
                foreach (var item in queue)
                {
                    myEventLog.LogInfo($"Index:{item.Index}，Cmd：{item.SpecCmd}");
                }
                var cmd = queue.Where(s => s.PrinterLightScan == false).Select(s => s.SpecCmd).FirstOrDefault();
                if(cmd==null)
                {
                    // 902
                    specCmd += newCmd;
                    //903
                    specCmd += "0000";
                }else
                {
                    // 902
                    specCmd += cmd;
                    // 903
                    specCmd += newCmd;
                }
            }
            return specCmd;
        }

        /// <summary>
        /// 根据品规挑选一个液体，入队
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        private tOrder GetModelBySpec(string[] spec)
        {
            // 通过扫描出来的信息对比数据源，查找匹配的数据，如果查询到则发指令调整机器大小，否则1#位剔除
            var queueIds = queue.Where(s => s.Drug != null).Select(s => s.Drug.Id).ToList();
            return autoPrintList.Where(m => queueIds.Contains(m.Id) == false && (m.printing_status == PrintStatusEnum.NotPrint || m.printing_status == null) && m.drug_name.ToLower().Contains(spec[0].ToLower()) && m.drug_spec.ToLower().Contains(spec[1].ToLower()))
                // 按照主药进行打印
                .OrderBy(s=>s.ydrug_name).FirstOrDefault();
            //            if (autoPrintCurrentList.Any()==false)
            //            {
            //#warning 调用线程无法访问此对象
            //                var batch = "01";
            //                autoPrintCurrentList = autoPrintList.FindAll(m => m.batch == batch).ToList();
            //            }
            //return autoPrintCurrentList.Find(m => queueIds.Contains(m.Id) == false && m.drug_name.Contains(spec[0]) && m.drug_spec.Contains(spec[1]));
        }
        //int scannerLightCount = 0;
        //object scannerLightCountHelper = new object();

        bool IsWarningShowing = false;

        /// <summary>
        /// PLC 数据接收
        /// </summary>
        /// <param name="data"></param>
        public void OnPLCDataReceived(string data)
        {
            try
            {
                if (data.StartsWith("%01!") || data.Contains("!") || data.StartsWith("%")==false)
                {
                    // 异常信号
                    //myEventLog.LogInfo($"收到PLC异常信号:{data}。");
                }
                else if (data.Contains("$RC"))
                {
                    if (data.Length == "%01#RC12345678**".Length)
                    {
                        //myEventLog.LogInfo($"PLC接收报警信号");
                        var dataArray = data.ToCharArray();
                        // 异常状态结果
                        var warning = dataArray[6];
                        var blockWarning = dataArray[7];
                        var printCardOutOfWarning = dataArray[8];
                        //var colorTapeOutOfWarning = dataArray[9];
                        var emptyWarning = dataArray[10];
                        var ccd1Status = dataArray[11];
                        var printStatus = dataArray[12];
                        var ccd2Status = dataArray[13];
                        var scannerStatus = dataArray[9];

                        if (ccd1Status == '1' && ccd1IsBusy == false)
                        {
                            if (CanStartConveryorBelt())
                            {
                                // 收到光幕信号，队列中液体数量小于队列中液体最大数，且未打印液体数量小于队列中允许的最大未打印数量，继续拍照

                                prevCCD1Time = DateTime.Now;
                                if (AppConfig.MaxQueueCount != 0 && queue.Count() >= AppConfig.MaxQueueCount)
                                {
                                    myEventLog.LogInfo($"PLC接收81信号，队列数量超过{AppConfig.MaxQueueCount}，不识别，剔除");

                                    SendCCD1Out();
                                    return;
                                }
                                myEventLog.LogInfo($"PLC接收81信号，CCD1开始拍照");

                                myEventLog.LogInfo($"PLC接收数据：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")}");

                                ccd1IsBusy = true;

                                // 停一段时间稳定液体
                                Thread.Sleep(AppConfig.CcdTakePhotoSleepTime);

                                // 开始拍照
                                lock (ccd1LockHelper)
                                {
                                    ccd1ErrorCount = 0;
                                }
                                CCD1TakePicture();
                            }
                        }

                        if (ccd2Status == '1' && ccd2IsBusy == false)
                        {
                            prevCCD2Time = DateTime.Now;
                            myEventLog.LogInfo($"PLC接收84信号，CCD2开始拍照");
                            ccd2IsBusy = true;

                            // 停一段时间稳定液体
                            Thread.Sleep(AppConfig.CcdTakePhotoSleepTime);
                            // 开始拍照
                            lock (ccd2LockHelper)
                            {
                                ccd2ErrorCount = 0;
                            }
                            CCD2TakePicture();
                        }

                        if (printStatus == '1')
                        {
                            myEventLog.LogInfo($"PLC接收82信号，开始打印");
                            GoToPrinterLight();
                        }

                        if(scannerStatus == '1')
                        {
                            GoToScannerLight();
                        }

                        if (IsWarningShowing == false &&(warning == '1' || blockWarning == '1'))
                        {
                            IsWarningShowing = true;
                            MessageBox.Show("设备有卡药，请处理后再进行工作！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                            IsWarningShowing = false;
                            //printerManager.ResetPrinter();
                        }
                        else
                        if (IsWarningShowing == false && printCardOutOfWarning == '1')
                        {
                            IsWarningShowing = true;
                            MessageBox.Show("设备的打印机缺纸，请处理后再进行工作！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                            IsWarningShowing = false;
                            //printerManager.ResetPrinter();
                        }
                        //else
                        //if (IsWarningShowing == false && colorTapeOutOfWarning == '1')
                        //{
                        //    IsWarningShowing = true;
                        //    MessageBox.Show("设备的打印机缺少色带，请处理后再进行工作！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        //    //printerManager.ResetPrinter();
                        //    IsWarningShowing = false;
                        //}
                        else
                        if (IsWarningShowing == false && emptyWarning == '1')
                        {
                            // 长时间未方药，停机
                            //printerManager.ResetPrinter();
                        }

                        if (warning == '1'
                            || blockWarning == '1'
                            || printCardOutOfWarning == '1'
                            || printCardOutOfWarning == '1'
                            //|| colorTapeOutOfWarning == '1'
                            || emptyWarning == '1')
                        {
                            StopPrint();
                        }

                    }
                }
                else if (data.Contains("$RD") && data.Length == "%01$RD000016".Length)
                {
                    //// 读取扫码枪光幕计数器指令返回结果
                    //// 16进制数字
                    //var str = data.Substring(6, 4);
                    //// 0、1是低位，2、3是高位，转换成10进制数字
                    //int count = Convert.ToInt32(data.Substring(8, 2)+data.Substring(6, 2), 16);
                    //GoToScannerLight();

                }
                else if (data.Contains("$WC"))
                {
                    // 写开始/停止指令返回结果
                }
                else if (data.Contains("$WD"))
                {
                    // 写CCD1和CCD2指令返回结果
                    myEventLog.LogInfo($"收到剔除或继续返回信号:{data}。");
                }
            }
            catch (Exception ex)
            {
                myEventLog.LogError("处理接收PLC数据出错。" + ex.Message, ex);
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
                Dispatcher.Invoke(() =>
                {
                    if (tabMain.SelectedIndex != 2)
                    {
                        MessageBox.Show("不在“补打签队列”标签页面无法使用手持扫码枪！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    else
                    {
                        // 这里处理扫码枪扫码结果
                        GetDurgInfoByScanner(data);
                    }
                });

                // 这里设置显示串口通讯
                Dispatcher.Invoke(() =>
                {
                    Ellipse ellipse = spcViewPanel.FindName("elScan2") as Ellipse;
                    ellipse.Fill = errorColor;
                    Label label = spcViewPanel.FindName("lblScan2") as Label;
                    label.Content = "扫描系统2通讯正常";
                });

            }
            catch (Exception ex)
            {
                myEventLog.LogError("处理接收到手动扫码枪数据出错。" + ex.Message, ex);
                new LogHelper().ErrorLog(ex.Message);
            }
        }

        #endregion


        #region 队列方法 - 先进先出原则
        /// <summary>
        /// 插入队列的顺序号
        /// </summary>
        int queueIndex = 1;


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
                    // CCD2是流程最后一步，取出第一个
                    var qModel = queue.FirstOrDefault();
                    myEventLog.LogInfo($"Index:{qModel.Index},从队列中删除一项，Id={qModel.Drug.Id}，Code={qModel.QRData}");
                
                    queue.Remove(qModel);

                    if (CanStartConveryorBelt())
                    {
                        // 出队后，队列中液体数量小于队列中液体最大数，且未打印液体数量小于队列中允许的最大未打印数量，第一个传送带继续
                        myEventLog.LogInfo($"开启传送带，队列数量：{queue.Count()}/{AppConfig.MaxQueueCount}，未打印数量：{queue.Count(s => s.PrinterLightScan == false)}/{AppConfig.MaxNotPrintQueueCount}");
                        StartConveryorBelt();
                    }
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
        private void AddQueue(tOrder drug, string cmd, string spec, string ml,string specCmd)
        {
            // 保存溶媒信息到队列
            OrderQueueModel qModel = new OrderQueueModel();
            if (drug != null)
            {
                if (string.IsNullOrEmpty(drug.barcode))
                {
                    drug.barcode = Guid.NewGuid().ToString();
                    drug.barcode = drug.barcode.Substring(drug.barcode.Length - 21, 20);
                }
                // 保存识别数据
                qModel.CMD = cmd;
                qModel.Drug = drug;
                qModel.Spec = spec;
                qModel.ML = ml;
                qModel.CCD1TakePhotoCount = ccd1ErrorCount+1;
                qModel.QRData = drug.barcode;
                qModel.PrinterLightScan = false;
                qModel.ScannerLightScan = false;
                qModel.Index = queueIndex++;
                qModel.SpecCmd = specCmd;

                myEventLog.LogInfo($"将Index:{qModel.Index},液体信息插入队列:Id={qModel.Drug.Id}，Code={qModel.QRData}");
            }
            else
            {
                //qModel.Drug = new tOrder();
                qModel.Index = queueIndex++;
                myEventLog.LogInfo($"Index:{qModel.Index},将空液体信息插入队列");
            }
            // 添加到队列
            lock (queueHelper)
            {
                // 入队时添加到队尾
                queue.Add(qModel);
                if(CanStopConveryorBelt())
                {
                    // 入队后，队列中液体数量大于队列中液体最大数，或未打印液体数量大于队列中允许的最大未打印数量，停止第一个传送带
                    myEventLog.LogInfo($"停止传送带，队列数量：{queue.Count()}/{AppConfig.MaxQueueCount}，未打印数量：{queue.Count(s => s.PrinterLightScan == false)}/{AppConfig.MaxNotPrintQueueCount}");
                    StopConveryorBelt();
                }
            }

        }
        /// <summary>
        /// CCD2错误时删除对象
        /// </summary>
        private void RemoveCCD2()
        {
            // 判断是否有队列数据
            lock (queueHelper)
            {
                if (queue.Count > 0)
                {
                    // CCD2是流程最后一步，取出第一个
                    var qModel = queue.FirstOrDefault();
                    myEventLog.LogInfo($"Index:{qModel.Index},从队列中删除一项，Id={qModel.Drug.Id}，Code={qModel.QRData}");
                    queue.Remove(qModel);

                    if (CanStopConveryorBelt())
                    {
                        myEventLog.LogInfo($"开启传送带，队列数量：{queue.Count()}/{AppConfig.MaxQueueCount}，未打印数量：{queue.Count(s => s.PrinterLightScan == false)}/{AppConfig.MaxNotPrintQueueCount}");
                        StartConveryorBelt();
                    }
                }
            }
            SetCCD2IsNotBusy();
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
                OrderQueueModel model = queue.Find(m => m.QRData == result);
                if (model != null)
                {
                    model.ScanData = result;
                    model.PrinterLightScan = true;
                    myEventLog.LogInfo($"修改液体扫描到的二维码：{result}");
                }
                else
                {
                    myEventLog.LogInfo($"未找到二维码对应的液体");
                }
            }
        }


        /// <summary>
        /// 打印机光幕
        /// </summary>
        private void GoToPrinterLight()
        {
            if ((DateTime.Now - prevPrinterLightTime).TotalMilliseconds < AppConfig.LightTimeInterval)
            {
                myEventLog.LogInfo($"PLC接收82信号，信号重复，不做处理");
                prevPrinterLightTime = DateTime.Now;
                return;
            }
            prevPrinterLightTime = DateTime.Now;
            // 找到最后一个没有记录的进行记录
            lock (queueHelper)
            {
                OrderQueueModel model = queue.Find(m => !m.PrinterLightScan);
                if (model != null)
                {
                    model.PrinterLightScan = true;
                    myEventLog.LogInfo($"过打印机光幕，记录过光幕状态");
                    if (CanStartConveryorBelt())
                    {
                        // 打印过之后，判断是否可以可以启动
                        StartConveryorBelt();
                    }
                }
                else
                {
                    myEventLog.LogInfo($"过打印机光幕，队列中未找到打印液体");
                }
            }
        }
        DateTime prevScannerLightTime = DateTime.Now;
        DateTime prevPrinterLightTime = DateTime.Now;
        DateTime prevCCD1Time = DateTime.Now;
        DateTime prevCCD2Time = DateTime.Now;
        /// <summary>
        /// 扫码枪光幕
        /// </summary>
        private void GoToScannerLight()
        {
            if ((DateTime.Now- prevScannerLightTime).TotalMilliseconds < AppConfig.LightTimeInterval)
            {
                prevScannerLightTime = DateTime.Now;
                return;
            }
            prevScannerLightTime = DateTime.Now;
            lock (queueHelper)
            {
                if (queue.Any() == false)
                {
                    myEventLog.LogInfo($"队列中无数据，不修改扫码枪计数！");
                    return;
                }
                foreach (var item in queue)
                {
                    if(item.PrinterLightScan == false)
                    {
                        item.PrinterLightScan = true;
                        myEventLog.LogInfo($"Index:{item.Index},过扫码枪光幕，识别到未记录82信号，修改收到82信号，");
                    }

                    if (CanStartConveryorBelt())
                    {
                        StartConveryorBelt();
                    }

                    if(item.ScannerLightScan == false)
                    {
                        item.ScannerLightScan = true;
                        myEventLog.LogInfo($"Index:{item.Index},过扫码枪光幕，记录过光幕状态，");
                        break;
                    }
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
                CCDConnected = false;
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
                CCDConnected = true;
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
                CCDConnected = false;
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
                CCDConnected = true;
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
            PCLConnected = false;
            // 这里设置显示串口连接失败
            Dispatcher.Invoke(() =>
            {
                Ellipse ellipse = spcViewPanel.FindName("elControlSystem") as Ellipse;
                ellipse.Fill = errorColor;
                Label label = spcViewPanel.FindName("lblControlSystem") as Label;
                label.Content = "控制系统连接失败";
            });
        }
        public void OnPLCComplated(int type)
        {
            if (type == 1)
            {
                // 连接成功
                CreatePLCReader();
                PCLConnected = true;
            }
            else
            {
                PCLConnected = false;
                lightListener?.Stop();
                //scannerLightListener.Stop();
            }

            // 这里设置显示串口连接成功
            Dispatcher.Invoke(() =>
            {
                Ellipse ellipse = spcViewPanel.FindName("elControlSystem") as Ellipse;
                ellipse.Fill = complate;
                Label label = spcViewPanel.FindName("lblControlSystem") as Label;
                label.Content = "控制系统连接成功";
            });
        }

        private void CreatePLCReader()
        {
            lightListener = new PLCReader(this, AppConfig.LightReaderIntervalTime, "%01#RCP8R0090R0095R0096R0007R0098R0170R0171R0173**");
            //warningListener = new PLCReader(this, AppConfig.WarningReaderIntervalTime, "%01#RCP5R0090R0095R0096R0097R0098**");
            //scannerLightListener = new PLCReader(this, AppConfig.LightReaderIntervalTime, "%01#RDD0071400714**");
        }

        public void OnScannerError(string msg)
        {
            AutoScannerConnected = false;
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
            AutoScannerConnected = true;
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
            HanderScannerConnected = false;
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
            HanderScannerConnected = true;
            // 这里设置显示串口连接成功
            Dispatcher.Invoke(() =>
            {
                Ellipse ellipse = spcViewPanel.FindName("elScan2") as Ellipse;
                ellipse.Fill = complate;
                Label label = spcViewPanel.FindName("lblScan2") as Label;
                label.Content = "扫描系统2连接成功";
            });
        }

        /// <summary>
        /// 贴签完成，修改列表中内容和颜色
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sBatchs">药品批号</param>
        private void PrintSuccess(tOrder model, string sBatchs)
        {
#warning 可能有线程出错Bug
            myEventLog.LogInfo($"更新列表：ID={model.Id}");
            model.printing_time = DateTime.Now;
            model.sbatches = sBatchs; ;
            model.printing_model = PrintModelEnum.Auto;
            model.printing_status = PrintStatusEnum.Success;
            model.barcode = DateTime.Now.ToString("HH:mm:ss");
            model.SetPropertyChanged("printing_time");
            model.SetPropertyChanged("sbatches");
            model.SetPropertyChanged("printing_model");
            model.SetPropertyChanged("printing_status");

            myEventLog.LogInfo($"更新列表内容");

            // 绑定药品汇总列表
            BindDurgSummary(autoPrintList);
            // 绑定统计数字
            GetSummaryLabels();
        }
        #endregion

        #region 数据库链接检查

        /// <summary>
        /// 链接数据库读取一个信息
        /// </summary>
        private bool CheckDBConnection()
        {
            if (ConnectionManager.CheckConnetionStatus() == false)
            {
                // 这里设置显示串口正常
                Dispatcher.Invoke(() =>
                {
                    Ellipse ellipse = spcViewPanel.FindName("elDb") as Ellipse;
                    ellipse.Fill = errorColor;
                    Label label = spcViewPanel.FindName("lblDb") as Label;
                    label.Content = "数据库连接失败";
                });

                MessageBox.Show("数据库连接失败，请检查数据库服务是否开启！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else
            {
                // 这里设置显示串口正常
                Dispatcher.Invoke(() =>
                {
                    Ellipse ellipse = spcViewPanel.FindName("elDb") as Ellipse;
                    ellipse.Fill = complate;
                    Label label = spcViewPanel.FindName("lblDb") as Label;
                    label.Content = "数据库连接正常";
                });
                return true;
            }
        }

        #endregion

        #region 数据源加载绑定
        /// <summary>
        /// 以日期和批次改变时获取的医嘱数据
        /// </summary>
        private ObservableCollection<tOrder> datasource = new ObservableCollection<tOrder>();

        private bool ddlBinding = false;
        /// <summary>
        /// 
        /// </summary>
        public void LoadData()
        {
            if (FormLoading)
            {
                return;
            }

            if (CheckDBConnection() == false)
            {
                return;
            }

            if (use_date.SelectedDate.HasValue == false)
            {
                MessageBox.Show("请先选择用药日期！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var batch = cb_batch.SelectedValue.ToString();
            if (string.IsNullOrEmpty(batch))
            {
                // 选中请选择时，清空数据
                datasource = new ObservableCollection<tOrder>();
            }
            else
            {
                datasource = string.IsNullOrEmpty(batch) ? new ObservableCollection<tOrder>() : orderManager.GetAllOrderByDateTime(use_date.SelectedDate.Value, batch);
            }
            autoPrintList = datasource;

            ddlBinding = true;

            // 绑定药品分类下拉框
            BindDrugType(autoPrintList);
            // 绑定科室下拉框
            BindDepartment(autoPrintList);
            // 绑定主药下拉框
            BindMainDrug(autoPrintList);

            ddlBinding = false;

            BindDgvs();
        }

        /// <summary>
        /// 绑定列表
        /// </summary>
        private void BindDgvs()
        {
            if (ddlBinding)
            {
                return;
            }
            FilterOrder();
            // 绑定右边溶媒汇总列表
            BindDurgSummary(autoPrintList);
            // 绑定待贴签列表
            BindAllList(autoPrintList);
            // 绑定统计数字
            GetSummaryLabels();
            //autoPrintList.Where(s => s.printing_model == PrintModelEnum.Manual)
            //BindManualList(autoPrintList.Where(s=>s.printing_model== PrintModelEnum.Manual));
            BindWarning();
        }

        /// <summary>
        /// 根据科室、药品分组、药品分类
        /// </summary>
        private void FilterOrder()
        {
            var hasCondition = false;
            var query = datasource.AsQueryable();
            // 科室
            if (cb_dept.SelectedValue != null && string.IsNullOrEmpty(cb_dept.SelectedValue.ToString())==false)
            {
                query= query.Where(s => s.department_code == cb_dept.SelectedValue.ToString());
                hasCondition = true;
            }
            // 主药分组
            if (cb_drug.SelectedValue != null && string.IsNullOrEmpty(cb_drug.SelectedValue.ToString()) == false)
            {
                query = query.Where(s => s.ydrug_id == cb_drug.SelectedValue.ToString());
                hasCondition = true;
            }
            // 药品分类
            if (cb_drug_category.SelectedValue != null && string.IsNullOrEmpty(cb_drug_category.SelectedValue.ToString()) == false)
            {
                query = query.Where(s => s.ydrug_class_name == cb_drug_category.SelectedValue.ToString());
                hasCondition = true;
            }
            if (hasCondition)
            {
                autoPrintList = new ObservableCollection<tOrder>();

                foreach (var order in query.ToList())
                {
                    autoPrintList.Add(order);
                }
            }
            else
            {
                autoPrintList = datasource;
            }
        }

        /// <summary>
        /// 绑定异常信息
        /// </summary>
        private void BindWarning()
        {
            var date = use_date.SelectedDate?.ToString("yyyy-MM-dd");
            var batch = cb_batch.SelectedValue?.ToString() ?? "";
            var dept = cb_dept.SelectedValue?.ToString() ?? "";
            var drugClass = cb_drug_category.SelectedValue?.ToString() ?? "";
            var mainDrug = cb_drug.SelectedValue?.ToString() ?? "";
            
            var list = warningManager.GetWarning(date, batch, dept, drugClass, mainDrug);

            Dispatcher.Invoke(() =>
            {
                dgv_PrintError.ItemsSource = list;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        private void GetSummaryLabels()
        {
            countModel.TotalCount = autoPrintList.Count();
            //var autoPrintTotalCount = autoPrintList.Count();
            //var manualPrintTotalCount = autoPrintList.Count();

            countModel.PrintedTotalCount = autoPrintList.Count(s => s.printing_status.HasValue);
            countModel.AutoCount = autoPrintList.Count(s => s.printing_status.HasValue && s.printing_model.HasValue && s.printing_model.Value == PrintModelEnum.Auto);
            countModel.ManualCount = autoPrintList.Count(s => s.printing_status.HasValue && s.printing_model.HasValue && s.printing_model.Value == PrintModelEnum.Manual);

            countModel.NotPrintTotalCount = autoPrintList.Count(s => s.printing_status.HasValue == false || s.printing_status.Value == PrintStatusEnum.NotPrint);
            //var autoNotPrintedCount = autoPrintList.Count();
            //var manualNotPrintedCount = autoPrintList.Count();

            UpdateSummaryLabel();
        }

        /// <summary>
        /// 绑定统计数字
        /// </summary>
        private void UpdateSummaryLabel()
        {

            Dispatcher.Invoke(() =>
            {
                spcViewPanel.lblTotalNumber.Content = countModel.TotalCount;
                //spcViewPanel.lbl_aotu1.Content = autoPrintTotalCount;
                //spcViewPanel.lbl_manual1.Content = manualPrintTotalCount;

                spcViewPanel.lblComplated.Content = countModel.PrintedTotalCount;
                spcViewPanel.lbl_aotu2.Content = countModel.AutoCount;
                spcViewPanel.lbl_manual2.Content = countModel.ManualCount;

                spcViewPanel.lblUncomplate.Content = countModel.NotPrintTotalCount;
                //spcViewPanel.lbl_aotu3.Content = autoNotPrintedCount;
                //spcViewPanel.lbl_manual3.Content = manualNotPrintedCount;
            });
        }

        /// <summary>
        /// 绑定右边溶媒统计
        /// </summary>
        /// <param name="dataSource"></param>
        private void BindDurgSummary(ObservableCollection<tOrder> dataSource)
        {
            //绑定右（溶媒统计）列表
            var solventlist = dataSource.GroupBy(m => new { m.drug_name, m.drug_spec }).
                Select(a => new SolventModel()
                {
                    SolventName = a.Key.drug_name,
                    Spec = a.Key.drug_spec,
                    Number = a.Count(),
                    MarkNumber = a.Count() - a.Count(m => m.printing_status.HasValue ==false || m.printing_status == PrintStatusEnum.NotPrint)
                }).ToList();

            autoPrintSummaryList = new ObservableCollection<SolventModel>();
            foreach (var item in solventlist)
            {
                autoPrintSummaryList.Add(item);
            }

            Dispatcher.Invoke(() =>
            {
                this.dgvGroupDetailRightList.ItemsSource = null;
                this.dgvGroupDetailRightList.ItemsSource = autoPrintSummaryList;
                this.dgvGroupDetailRightList.CanUserSortColumns = false;

                // 绑定右侧总统计信息
                int number = solventlist.Sum(m => m.Number);
                lblNumber.Content = number;
                int markNumber = solventlist.Sum(m => m.MarkNumber);
                lblMarkNumber.Content = markNumber;
            });
        }

        #region 绑定下拉框
        /// <summary>
        /// 绑定药品分类下拉框
        /// </summary>
        /// <param name="dataSource"></param>
        private void BindDrugType(ObservableCollection<tOrder> dataSource)
        {
            // 绑定药品分类
            var drugCategoryList = dataSource
                .GroupBy(m => new { m.ydrug_class_name })
                .Select(a => new { class_name = a.Key.ydrug_class_name, ydrug_id= a.Key.ydrug_class_name })
                .ToList()
                .Select(a => new { class_name = a.class_name.Trim(), ydrug_id = a.ydrug_id.Trim() })
                .Distinct()
                .ToList();
            drugCategoryList.Insert(0, new { class_name = "全部", ydrug_id =""});

            Dispatcher.Invoke(() =>
            {
                this.cb_drug_category.DisplayMemberPath = "class_name";
                this.cb_drug_category.SelectedValuePath = "ydrug_id";
                this.cb_drug_category.ItemsSource = drugCategoryList;
                this.cb_drug_category.SelectedIndex = 0;
            });
        }
        /// <summary>
        /// 绑定科室下拉框
        /// </summary>
        /// <param name="dataSource"></param>
        private void BindDepartment(ObservableCollection<tOrder> dataSource)
        {
            // 绑定科室
            var deptList = dataSource.GroupBy(m => new { m.departmengt_name, m.department_code }).Select(a => new { dept_name = a.Key.departmengt_name, dept_code = a.Key.department_code }).ToList();
            deptList.Insert(0, new { dept_name = "全部", dept_code = "" });

            Dispatcher.Invoke(() =>
            {
                this.cb_dept.DisplayMemberPath = "dept_name";
                this.cb_dept.SelectedValuePath = "dept_code";
                this.cb_dept.ItemsSource = deptList;
                this.cb_dept.SelectedIndex = 0;
            });
        }
        /// <summary>
        /// 绑定主药下拉框
        /// </summary>
        /// <param name="dataSource"></param>
        private void BindMainDrug(ObservableCollection<tOrder> dataSource)
        {
            // 绑定主药
            var drugIds = dataSource.Select(s => s.ydrug_id).Distinct().ToList();
            DrugManager drugManager = new DrugManager();
            var drugList = drugManager.GetAll(s => drugIds.Contains(s.drug_code))
                .OrderBy(s => s.drug_name)
                .ThenBy(s => s.drug_form)
                .ThenBy(s => s.drug_spec)
                .Select(s => new
                {
                    ydrug_id = s.drug_code,
                    ydrug_name = s.drug_name + " " + s.drug_spec + " " + s.drug_form
                }).ToList();
            //var drugList = dataSource.GroupBy(m => new { m.ydrug_name, m.ydrug_spec }).Select(a => new { ydrug_name = string.Format("{0}({1})", a.Key.ydrug_name, a.Key.ydrug_spec), ydrug_id = string.Format("{0}|{1}", a.Key.ydrug_name, a.Key.ydrug_spec) }).ToList();
            drugList.Insert(0, new { ydrug_id = "", ydrug_name = "全部" });

            Dispatcher.Invoke(() =>
            {
                this.cb_drug.DisplayMemberPath = "ydrug_name";
                this.cb_drug.SelectedValuePath = "ydrug_id";
                this.cb_drug.ItemsSource = drugList;
                this.cb_drug.SelectedIndex = 0;
            });
        } 
        #endregion

        /// <summary>
        /// 待贴签列表绑定
        /// </summary>
        /// <param name="dataSource"></param>
        private void BindAllList(ObservableCollection<tOrder> dataSource)
        {

            Dispatcher.Invoke(() =>
            {
                this.dgv_AllPrint.ItemsSource = dataSource;
            });
        }
        private void BindManualList(ObservableCollection<tOrder> dataSource)
        {

            Dispatcher.Invoke(() =>
            {
                this.dgv_ManualPrint.ItemsSource = dataSource;
            });
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

        #region 事件响应

        private void BaseWindow_Closing(object sender, CancelEventArgs e)
        {
            if (needCloseWindowConfirm)
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
            }
        }


        #region 开始打印/暂停打印事件
        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {

            // 检查本地数据库连接状态
            if (CheckDBConnection() == false)
            {
                return;
            }

            if (ConnectionManager.CheckPivasConnetionStatus() == false)
            {
                MessageBox.Show("Pivas数据库连接失败，请检查数据库服务是否开启！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (CCDConnected == false)
            {
                MessageBox.Show("未检测到软件【药袋检测控制系统】，请先启动！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (PCLConnected == false)
            {
                MessageBox.Show("控制系统连接失败，请检查！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (AutoScannerConnected == false)
            {
                MessageBox.Show("自动扫码枪连接失败，请检查！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 清空队列
            queue = new List<OrderQueueModel>();

            if (tabMain.SelectedIndex == 0)
            {
                if(string.IsNullOrEmpty(cb_batch.SelectedValue?.ToString() ?? ""))
                {
                    MessageBox.Show("请选择批次！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (autoPrintList.Any() == false)
                {
                    MessageBox.Show("当前批次无待贴标签液体！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (autoPrintList.Count(s=>s.printing_status.HasValue == false || s.printing_status == PrintStatusEnum.NotPrint)==0)
                {
                    MessageBox.Show("当前批次以完成贴签，无待打印液体！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                Task.Run(() =>
                {

                    Dispatcher.Invoke(() =>
                    {
                        loadMask.LoadingText = "正在检测打印机...";
                        loadMask.Visibility = Visibility.Visible;
                    });
                    if (IsConnectDevices == false || CheckPrinterStatus())
                    {
                        Dispatcher.Invoke(() =>
                        {
                            loadMask.Visibility = Visibility.Hidden;
                        });
                        var printer = printerManager.GetPrinter();
                        var status = printer.GetCurrentStatus();
                        if (status.labelsRemainingInBatch > 0)
                        {
                            myEventLog.LogInfo("开始时，打印机中有打印任务，重置打印机！");
                            // 打印机中有打印任务的，重置打印机
                            printerManager.ResetPrinter();
                            MessageBox.Show("打印机正在启动，请稍后再试！");
                            return;
                        }
                        // 先关再开
                        PLCSerialPortUtils plcUtils = PLCSerialPortUtils.GetInstance(this);
                        //plcUtils.SendData(PLCSerialPortData.MACHINE_STOP);
                        //plcUtils.SendData(PLCSerialPortData.MACHINE_START);
                        plcUtils.SendData("%01#WCSR00291**"); // 停止打印
                        myEventLog.LogInfo($"发送停止命令");

                        plcUtils.SendData("%01#WCSR00201**"); // 开始打印
                        myEventLog.LogInfo($"发送开始命令");

                        if (lightListener == null)
                        {
                            CreatePLCReader();
                        }

                        lightListener?.Start();

                        StartUpdateControlState();

                        SetCCD1IsNotBusy();
                        SetCCD2IsNotBusy();
                    }
                    else
                    {
                        Dispatcher.Invoke(() =>
                        {
                            loadMask.Visibility = Visibility.Hidden;
                        });
                        MessageBox.Show("无法连接到打印机，请检查打印机是否开启！");
                    }
                });
                //scannerLightListener.Start();
                // 这里设置显示串口正常
            }
            else
            {
                MessageBox.Show("当前标签页面不能自动打印！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        /// <summary>
        /// 检查打印机状态
        /// </summary>
        /// <returns></returns>
        private bool CheckPrinterStatus()
        {
            var printerConnected = true;
            try
            {
                var tryConnectionTimes = 0;

                while (printerManager.TryOpenPrinterConnection() == false)
                {
                    tryConnectionTimes++;
                    if (tryConnectionTimes >= 5)
                    {
                        printerConnected = false;
                        break;
                    }

                    Thread.Sleep(1000);
                }
                tryConnectionTimes = 0;

                while (printerConnected == true && printerManager.GetPrinter() == null)
                {
                    tryConnectionTimes++;
                    if (tryConnectionTimes >= 5)
                    {
                        printerConnected = false;
                        break;
                    }
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                myEventLog.LogError("尝试连接打印机出错", ex);
            }
            finally
            {
            }

            return printerConnected;
        }

        private void ButtonStopPrint_Click(object sender, RoutedEventArgs e)
        {
            StopPrint();
        }

        private void StopPrint()
        {
            StopUpdateControlState();
            if (IsConnectDevices)
            {
                lightListener?.Stop();
                //scannerLightListener.Stop();
                //PLCSerialPortUtils.GetInstance(this).SendData(PLCSerialPortData.MACHINE_STOP);
                PLCSerialPortUtils.GetInstance(this).SendData("%01#WCSR00291**"); // 停止打印
                myEventLog.LogInfo($"发送停止命令");

                Task.Run(() =>
                {
                    try
                    {

                        var status = printerManager.GetPrinterStatus();
                        if (status.labelsRemainingInBatch > 0)
                        {
                            printerManager.ResetPrinter();
                            myEventLog.LogInfo($"停止时，打印机中有打印任务，重置打印机！");
                        }
                    }
                    catch (Exception ex)
                    {
                        myEventLog.LogError($"停止打印时，检测打印机任务数出错！", ex);

                    }
                });
            }
        }

        private void StopUpdateControlState()
        {

            // 这里设置显示串口正常
            Dispatcher.Invoke(() =>
            {
                btnPrint.IsEnabled = true;
                btnStopPrint.IsEnabled = false;
                cb_drug.IsEnabled = true;
                cb_drug_category.IsEnabled = true;
                cb_dept.IsEnabled = true;
                cb_batch.IsEnabled = true;
                use_date.IsEnabled = true;
                SetMenuEnabled();
            });
        }
        private void StartUpdateControlState()
        {

            // 这里设置显示串口正常
            Dispatcher.Invoke(() =>
            {
                btnPrint.IsEnabled = false;
                btnStopPrint.IsEnabled = true;
                cb_drug.IsEnabled = false;
                cb_drug_category.IsEnabled = false;
                cb_dept.IsEnabled = false;
                cb_batch.IsEnabled = false;
                use_date.IsEnabled = false;
                SetMenuDisabled();
            });
        }

        #endregion

        #region 数据绑定事件
        /// <summary>
        /// 同步医嘱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectionManager.CheckPivasConnetionStatus() == false)
            {
                MessageBox.Show("Pivas数据库连接失败，请检查数据库服务是否开启！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (CheckDBConnection() == false)
            {
                return;
            }
            if (this.use_date.SelectedDate.HasValue == false)
            {
                MessageBox.Show("请选择用药日期！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (this.use_date.SelectedDate.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("只能选择今天和今天后的日期！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                myEventLog.LogInfo("开始同步医嘱");
                this.btnUpdate.IsEnabled = false;
                new DataSync().SyncOrder(this.use_date.SelectedDate.Value);
                myEventLog.LogInfo("医嘱同步完成");
                MessageBox.Show("从Pivas同步医嘱完成！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception exception)
            {
                myEventLog.LogError(exception.Message, exception);
                MessageBox.Show("从Pivas同步医嘱出错！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                this.btnUpdate.IsEnabled = true;
            }

            LoadData();
        }
        /// <summary>
        /// 日期选择框发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Use_date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //绑定批次调整后事件
            this.cb_batch.SelectedIndex = 0;

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
            LoadData();
        }

        /// <summary>
        /// 科室下拉框发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cb_dept_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BindDgvs();
        }
        /// <summary>
        /// 药品分类下拉框发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cb_drug_category_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BindDgvs();
        }
        /// <summary>
        /// 主药下拉框发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cb_drug_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BindDgvs();
        } 
        #endregion

        #endregion


        #region 前端传送带控制
        /// <summary>
        /// 前端传送带是否正在运行
        /// </summary>
        private bool IsConveryorBeltRunning = true;
        /// <summary>
        /// 启动传送带
        /// </summary>
        private void StartConveryorBelt()
        {
            if (IsConveryorBeltRunning)
            {
                return;
            }
            // 启动传送带
            myEventLog.LogInfo($"发送启动传送带命令");
            PLCSerialPortUtils.GetInstance(this).SendData("%01#WCSR00050**");
        }
        /// <summary>
        /// 停止传送带
        /// </summary>
        private void StopConveryorBelt()
        {
            if (IsConveryorBeltRunning)
            {
                myEventLog.LogInfo($"发送停止传送单命令");
                // 停止传送带
                PLCSerialPortUtils.GetInstance(this).SendData("%01#WCSR00051**");
            }
        }
        /// <summary>
        /// 判断是否应该开始送料传送带
        /// </summary>
        /// <returns></returns>
        private bool CanStartConveryorBelt()
        {
            return queue.Count(s => s.PrinterLightScan == false) < AppConfig.MaxNotPrintQueueCount && queue.Count() < AppConfig.MaxQueueCount;
        }
        /// <summary>
        /// 判断是否需要停止送料传送带
        /// </summary>
        /// <returns></returns>
        private bool CanStopConveryorBelt()
        {
            return queue.Count(s => s.PrinterLightScan == false) >= AppConfig.MaxNotPrintQueueCount || queue.Count() >= AppConfig.MaxQueueCount;
        }
        #endregion
    }

}
