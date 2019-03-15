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
using System.Windows.Threading;
using System.Collections.Concurrent;
using PrinterManagerProject.Tools.Serial;

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
        private ConcurrentQueue<OrderQueueModel> queue = new ConcurrentQueue<OrderQueueModel>();
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
        #endregion
        private readonly TaskScheduler _syncContextTaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        #region 设备连接状态

        /// <summary>
        /// 上一次检测CCD连接状态
        /// </summary>
        private static bool PrevCCDConnected = false;
        /// <summary>
        /// 上一次检测PLC连接状态
        /// </summary>
        private static bool PrevPCLConnected = false;
        /// <summary>
        /// 上一次检测自动扫码枪连接状态
        /// </summary>
        private static bool PrevAutoScannerConnected = false;
        /// <summary>
        /// 上一次检测手动扫码枪连接状态
        /// </summary>
        private static bool PrevHanderScannerConnected = false;

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
        #endregion


        private OrderManager orderManager = new OrderManager();

        private bool FormLoading = true;

        IPrinterManager printerManager = new UsbConnectionManager();

        DateTime QueueIsEmptyTime = DateTime.Now;

        /// <summary>
        /// 保存最后一个CCD1状态
        /// </summary>
        private static bool LastCCD1IsSuccess = true;
        /// <summary>
        /// 保存最后一个CCD2状态
        /// </summary>
        private static bool LastCCD2IsSuccess = true;
        /// <summary>
        /// 监听长时间未放药计时器
        /// </summary>
        private DispatcherTimer QueueIsEmptyStopTimer;
        private DispatcherTimer PlcReaderTimer;

        /// <summary>
        /// 卡药检测Timer
        /// </summary>
        private DispatcherTimer BlockDetectictTimer;
        private object queueHelper = new object();

        PLCCommandQueue plcCommandSendQueueHelper;

        public PrintWindow()
        {
            needCloseWindowConfirm = true;
            InitializeComponent();
            base.Loaded += PrintWindow_Loaded;
            plcCommandSendQueueHelper = PLCCommandQueue.GetInstance(this);
            Task.Run(()=> { scanDeviceState(); });

            //Task.Factory.StartNew(() =>
            //{
            //    var i = 1;
            //    while (i<4)
            //    {
            //        spcViewPanel.SetCCD2State(i % 2 + 1);
            //        i++;
            //        Thread.Sleep(2000);
            //    }
            //}, new CancellationTokenSource().Token, TaskCreationOptions.None, _syncContextTaskScheduler);
        }

        private void scanDeviceState()
        {
            while (true)
            {
                if(PrevCCDConnected!= CCDConnected)
                {
                    spcViewPanel.SetCCD2State(CCDConnected);
                    spcViewPanel.SetCCD1State(CCDConnected);
                }
                //if (PrevPCLConnected != PCLConnected)
                //{
                //    spcViewPanel.SetPlcState(PCLConnected);
                //}
                //spcViewPanel.SetDbState(CheckDBConnection);
                if (PrevPCLConnected != PCLConnected)
                {
                    spcViewPanel.SetPlcState(PCLConnected);
                }
                if (PrevAutoScannerConnected != AutoScannerConnected)
                {
                    spcViewPanel.SetAutoScannerState(AutoScannerConnected);
                }
                if (PrevHanderScannerConnected != HanderScannerConnected)
                {
                    spcViewPanel.SetHanderScannerState(HanderScannerConnected);
                }
                Thread.Sleep(1000);
            }
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

            try
            {
                new DrugManager().SyncDrug();
            }
            catch (Exception ex)
            {
                myEventLog.LogError("同步医嘱信息出错",ex);
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

                plcCommandSendQueueHelper = PLCCommandQueue.GetInstance(this);
                plcCommandSendQueueHelper.Start();



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

        /// <summary>
        /// 发送上一个CCD1成功的时间
        /// </summary>
        DateTime preCCD1SuccessTime = DateTime.Now;
        /// <summary>
        /// 发送CCD1成功命令
        /// </summary>
        /// <param name="cmdData"></param>
        /// <returns></returns>
        private bool SendCCD1Success(string cmdData)
        {

            CCDSendData sendData = new CCDSendData();
            sendData.Command = $"%01#WDD0090100901{cmdData}**";
            sendData.Send += SendCCD1Success_Callback;
            plcCommandSendQueueHelper.Enqueue(sendData);
            //int queueCount = plcCommandSendQueueHelper.Enqueue($"%01#WDD0090100901{cmdData}**");
            return true;
        }

        private void SendCCD1Success_Callback(object sender, EventArgs e)
        {
            Thread.Sleep(AppConfig.Ccd1SuccessSleepTime);
            myEventLog.LogInfo($"发送指令等待时间:{AppConfig.Ccd1SuccessSleepTime}");
            ccd1SuccessTime = DateTime.Now;

            myEventLog.LogInfo($"发送CCD1继续命令");
        }

        /// <summary>
        /// 发送CCD2成功命令
        /// </summary>
        /// <returns></returns>
        private bool SendCCD2Success()
        {
            try
            {

                CCDSendData sendData = new CCDSendData();
                sendData.Command = "%01#WDD00900009003459**";
                sendData.Send += SendCCD2Success_Callback; ;
                plcCommandSendQueueHelper.Enqueue(sendData);
                //plcCommandSendQueueHelper.Enqueue("%01#WDD00900009003459**");
                return true;
            }
            catch (Exception ex)
            {
                myEventLog.LogError($"发送CCD2继续命令出错：" + ex.Message, ex);
                return false;
            }
        }

        private void SendCCD2Success_Callback(object sender, EventArgs e)
        {
            prevCCD2IsSuccess = true;
            myEventLog.LogInfo($"发送CCD2继续命令");
            SetCCD2IsNotBusy();
        }

        private static int IndexOf81AfterCCD1IsNotBusy = 0;
        /// <summary>
        /// 发送CCD1失败命令
        /// </summary>
        private void SendCCD1Out()
        {
            CCDSendData sendData = new CCDSendData();
            sendData.Command = $"%01#WDD0090100901324E**";
            sendData.Send += SendCCD1Out_Callback; ;
            plcCommandSendQueueHelper.Enqueue(sendData);

            //plcCommandSendQueueHelper.Enqueue("%01#WDD0090100901324E**");  // 1N:CCD1剔除
            //plcCommandSendQueueHelper.Enqueue("%01#WDD0090100901324E**");  // 1N:CCD1剔除
        }

        private void SendCCD1Out_Callback(object sender, EventArgs e)
        {
            myEventLog.LogInfo($"发送CCD1剔除命令");
            SetCCD1IsNotBusy();
        }

        DateTime ccd2EndTime = DateTime.Now;
        /// <summary>
        /// 发送CCD2失败命令
        /// </summary>
        private void SendCCD2Out()
        {
            CCDSendData sendData = new CCDSendData();
            sendData.Command = "%01#WDD0090000900344E**";
            sendData.Send += SendCCD2Out_Callback; 
            plcCommandSendQueueHelper.Enqueue(sendData);
            //plcCommandSendQueueHelper.Enqueue("%01#WDD0090000900344E**"); //4N：CCD2剔除
        }

        private void SendCCD2Out_Callback(object sender, EventArgs e)
        {
            prevCCD2IsSuccess = false;
            myEventLog.LogInfo($"发送CCD2剔除命令");
            SetCCD2IsNotBusy();
        }

        /// <summary>
        /// 设置CCD1空闲
        /// </summary>
        private void SetCCD1IsNotBusy()
        {
            Task.Run(() =>
            {
                Thread.Sleep(AppConfig.FreeCCDBusyState);
                ccd1IsBusy = false;
                IndexOf81AfterCCD1IsNotBusy = 0;
            });
        }
        bool prevCCD2IsSuccess = true;
        /// <summary>
        /// 设置CCD2空闲
        /// </summary>
        private void SetCCD2IsNotBusy()
        {
            Task.Run(() =>
            {
                Thread.Sleep(AppConfig.FreeCCDBusyState);
                ccd2IsBusy = false;
                ccd2EndTime = DateTime.Now;
            });
        }
        #region 接收串口信息

        DateTime ccd1StartTime = DateTime.Now;
        DateTime ccd1SuccessTime = DateTime.Now;
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


                        if (ccd1ErrorCount < AppConfig.CCD1TakePhotoMaxTimes - 1)
                        {
                            ccd1ErrorCount++;
                            Task.Run(() =>
                            {
                                //1#拍照
                                Thread.Sleep(AppConfig.CcdTakePhotoSleepTime);
                                // 重新拍照
                                CCD1TakePicture();
                                myEventLog.LogInfo($"CCD1第{ccd1ErrorCount}次识别失败，重新拍照");
                                //model.Count++;
                            });
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
                        if (CheckQueueError())
                        {
                            // 从2#位剔除药袋
                            SendCCD2Out();
                            // 删除队列
                            RemoveCCD2();
                            return;
                        }


                        // CCD2是流程最后一步，取出第一个


                        OrderQueueModel model = queue.FirstOrDefault();
                        if (model != null)
                        {
                            if (string.IsNullOrEmpty(model.ScanData) == false)
                            {
                                ccd2ErrorCount++;
                                myEventLog.LogInfo($"Index:{model?.Index},CCD2第{ccd2ErrorCount}次识别失败，重新拍照");

                                if (ccd2ErrorCount < model.CCD1TakePhotoCount + 1)
                                {
                                    Task.Run(() =>
                                    {
                                        //2#拍照
                                        Thread.Sleep(AppConfig.CcdTakePhotoSleepTime);
                                        CCD2TakePicture();
                                    });
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

                        // 从2#位剔除药袋
                        SendCCD2Out();
                        // 删除队列
                        RemoveCCD2();
                    }
                }
                // CCD1识别结果处理，且不是错误信息
                else if (data.Length == CCDSerialPortData.CCD1_ERROR.Length)
                {
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
                    // 数据正确
                    if (datas[0] == "FE" && datas[datas.Length - 1] == "EF")
                    {
                        // CCD1的反馈
                        if (datas[1] == "A1")
                        {
                            #region 处理CCD1数据

                            myEventLog.LogInfo("收到CCD1识别成功命令");

                            string[] spec = CCDSerialPortData.GetNameAndML(datas[2]); // 规格和毫升数
                            string type = CCDSerialPortData.GetTypeValue(datas[3]); // 药袋种类

                            myEventLog.LogInfo($"CCD1品规:{spec[0]}-{spec[1]}");
                            tOrder currentOrder = GetModelBySpec(spec);

                            if (currentOrder != null)
                            {
                                //myEventLog.LogInfo($"CCD1医嘱:{currentOrder.Id}");

                                // 获取水和药品信息

                                List<PrintDrugModel> drugs = orderManager.GetPrintDrugs(currentOrder.Id);
                                //if (drugs != null && drugs.Count > 0)
                                //{
                                //myEventLog.LogInfo($"药品数量:{drugs.Count}");

                                string mlCmd = PLCSerialPortData.GetSizeCmd(datas[2], datas[3]);

                                var printCommand = "";
                                var startTime = DateTime.Now;
                                bool success = false;
                                //ZebraPrinter printer = null;
                                try
                                {
                                    printer = printerManager.GetPrinter();
                                    //myEventLog.LogInfo($"获取Printer花费时间:{(DateTime.Now - startTime).TotalMilliseconds}");
                                    startTime = DateTime.Now;
                                    printCommand = printTemplateManager.GetPrintCommand(currentOrder);
                                    //myEventLog.LogInfo($"生成打印命令花费时间:{(DateTime.Now - startTime).TotalMilliseconds}");
                                    success = true;
                                }
                                catch (Exception ex)
                                {
                                    myEventLog.LogError($"获取Printer出错:{ex.Message}", ex);
                                    success = false;
                                }
                                if (success)
                                {
                                    // 发送指令，调整PLC药袋大小以及打印机高度

                                    //var specCmdData = GetSepcData(currentCmdData);
                                    var currentCmdData = $"3{mlCmd.ToCharArray()[0]}3{mlCmd.ToCharArray()[1]}";
                                    //myEventLog.LogInfo($"发送CCD1继续命令");

                                    //Task.Run(() =>
                                    //{
                                        
                                        while ((DateTime.Now - ccd1SuccessTime).TotalMilliseconds < AppConfig.TowMedicionMinInterval)
                                        {
                                            // 保证从CCD1发出去的时间间隔固定在AppConfig.TowMedicionMinInterval
                                            Thread.Sleep(20);
                                        }


                                    startTime = DateTime.Now;
                                    //printer.SendCommand(printCommand);
                                    printer.SendCommand(printCommand);
                                    myEventLog.LogInfo($"发送打印内容花费时间:{(DateTime.Now - startTime).TotalMilliseconds}");

                                    CCDSendData sendData = new CCDSendData();
                                    sendData.Command = $"%01#WDD0090100901{currentCmdData}**";
                                    sendData.order = currentOrder;
                                    sendData.specData = spec;
                                    sendData.CCDData = data;
                                    sendData.CurrentCommandData = currentCmdData;
                                    sendData.PrintCommand = printCommand;
                                    sendData.Send += SendCCD1Command_Send;

                                    plcCommandSendQueueHelper.Enqueue(sendData);

                                        //int queueCount = plcCommandSendQueueHelper.Enqueue($"%01#WDD0090100901{currentCmdData}**");
                                        //myEventLog.LogInfo($"距离上次发送时间间隔:{(DateTime.Now - ccd1SuccessTime).TotalMilliseconds}");
                                        //ccd1SuccessTime = DateTime.Now;
                                        //startTime = DateTime.Now;
                                        ////myEventLog.LogInfo($"发送CCD1继续命令花费时间:{(DateTime.Now - startTime).TotalMilliseconds}");
                                        //startTime = DateTime.Now;

                                    ////printer.SendCommand(printCommand);
                                    //printer.SendCommand(printCommand);
                                    ////myEventLog.LogInfo($"发送打印内容花费时间:{(DateTime.Now - startTime).TotalMilliseconds}");

                                    //SetCCD1IsNotBusy();
                                    //startTime = DateTime.Now;

                                    ////插入到队列中
                                    //AddQueue(currentOrder, data, spec[0], spec[1], currentCmdData, ccd1SuccessTime);
                                    ////myEventLog.LogInfo($"插入队列花费时间:{(DateTime.Now - startTime).TotalMilliseconds}");
                                    //});
                                }
                                else
                                {
                                    myEventLog.LogInfo($"获取Printer或生成打印内容指令失败");
                                    SendCCD1Out();
                                }
                            }
                            else
                            {
                                // 从1#位剔除（不是本组）的药袋
                                myEventLog.LogInfo($"CCD1失败， 未找到当前品规（{spec[0]}-{spec[1]}）的未贴签液体");
                                SendCCD1Out();
                            }

                            #endregion
                        }
                        // CCD2的反馈
                        else if (datas[1] == "A3")
                        {
                            #region 处理CCD2数据

                            string[] spec = CCDSerialPortData.GetNameAndML(datas[2]); // 规格和毫升数
                            string batchNumber = datas[4] + datas[5] + datas[6]; //批号
                            myEventLog.LogInfo($"CCD2识别到液体规格：{spec[0]}-{spec[1]}");
                            //myEventLog.LogInfo($"CCD2识别到批号：{batchNumber}");



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

                                        ClearQueue();
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
                                if (success)
                                {
                                    success = SetCCD2IsSuccess(spec, batchNumber, success, model);
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
        /// CCD1成功指令发送成功回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendCCD1Command_Send(object sender, EventArgs e)
        {
            var model = sender as CCDSendData;
            myEventLog.LogInfo($"距离上次发送时间间隔:{(DateTime.Now - ccd1SuccessTime).TotalMilliseconds}");
            ccd1SuccessTime = DateTime.Now;
            //var startTime = DateTime.Now;
            //myEventLog.LogInfo($"发送CCD1继续命令花费时间:{(DateTime.Now - startTime).TotalMilliseconds}");


            SetCCD1IsNotBusy();
            //startTime = DateTime.Now;

            //插入到队列中
            AddQueue(model.order, model.CCDData, model.specData[0], model.specData[1], model.CurrentCommandData, ccd1SuccessTime);
            //myEventLog.LogInfo($"插入队列花费时间:{(DateTime.Now - startTime).TotalMilliseconds}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <param name="batchNumber"></param>
        /// <param name="success"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool SetCCD2IsSuccess(string[] spec, string batchNumber, bool success, OrderQueueModel model)
        {
            if (success == true)
            {
                // 修改到数据库，修改失败则判为失败
                bool updateDataSuccess = false;
                try
                {
                    // 检查是否错位，错位则剔除，否则继续
                    //if (CheckQueueAddRemoveCCD2())
                    //{
                    //    return;
                    //}

                    if (CheckQueueError())
                    {

                        myEventLog.LogInfo($"Index:{model?.Index},检测到队列错位，未更新到数据库：ID={model.Drug.Id}");
                        return false;  // 检测到标签错位时已经发送CCD2出错命令
                        //return false;
                    }
                    var ccd2startTime = DateTime.Now;
                    orderManager.PrintSuccess(model.Drug.Id, PrintModelEnum.Auto, batchNumber, UserCache.Printer.ID, UserCache.Printer.true_name, UserCache.Checker.ID, UserCache.Checker.true_name);

                    //myEventLog.LogInfo($"CCD2更新到数据库花费时间:{(DateTime.Now - ccd2startTime).TotalMilliseconds}");

                    myEventLog.LogInfo($"Index:{model?.Index},更新到数据库：Id={model.Drug.Id},BarCode={model.Drug.barcode},GroupNum={model.Drug.group_num}");

                    SendCCD2Success();
                    // --- 设置为CCD2识别通过的状态 ---
                    Success();

                    updateDataSuccess = true;

                }
                catch (Exception ex)
                {
                    myEventLog.LogError($"Index：{model.Index}，更新到数据库出错", ex);
                }
                if (updateDataSuccess)
                {


                    //myEventLog.LogInfo($"Index:{model?.Index},更新到数据库：ID={model.Drug.Id}");
                    countModel.AutoCount++;
                    countModel.PrintedTotalCount++;
                    countModel.NotPrintTotalCount--;
                    UpdateSummaryLabel();

                    // 处理到数据源
                    tOrder autoPrintModel = autoPrintList.FirstOrDefault(m => m.Id == model.Drug.Id);

                    // 回写数据
                    PrintSuccess(autoPrintModel, batchNumber);

                    //myEventLog.LogInfo($"Index:{model?.Index},数据回写成功：Id={autoPrintModel.Id},BarCode={model.Drug.barcode},DrugId={model.Drug.Id}");
                    //myEventLog.LogInfo($"Index:{model?.Index},CCD2成功");
                    //if (autoPrintModel != null)
                    //{
                    //}
                    //else
                    //{
                    //    success = false;
                    //    myEventLog.LogInfo($"数据回写失败：BarCode={model.Drug.barcode},DrugId={model.Drug.Id}");
                    //}
                }
                else // 数据库操作失败
                {
                    // --- 删除对比失败的信息 ---
                    //myEventLog.LogInfo($"Index:{model?.Index},数据回写失败：BarCode={model.Drug.barcode},DrugId={model.Drug.Id}");
                    myEventLog.LogInfo($"Index:{model?.Index},CCD2失败 数据回写失败");
                    warningManager.AddWarning(model.Drug, spec[0], spec[1], WarningStateEnum.DataUpdateFailed, UserCache.Printer.ID, UserCache.Printer.true_name, UserCache.Checker.ID, UserCache.Checker.true_name);
                    success = false;
                }
            }

            return success;
        }

        /// <summary>
        /// 检查当前液体是否处于错误队列中，是则直接从CCD2剔除
        /// </summary>
        private bool CheckQueueError()
        {
            if (DateTime.Now < ccd2IsErrorBefore)
            {
                myEventLog.LogInfo($"队列已错位，剔除当前液体");

                return true;
            }
            return false;
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
        }

        bool IsWarningShowing = false;
        //DateTime prePLCTime = DateTime.Now;
        //DateTime preReadPLCTime = DateTime.Now;
        /// <summary>
        /// PLC 数据接收
        /// </summary>
        /// <param name="data"></param>
        public void OnPLCDataReceived(string data)
        {
            try
            {
                //var plcIntervalTickets = (DateTime.Now - prePLCTime).TotalMilliseconds;
                //prePLCTime = DateTime.Now;
                //myEventLog.LogInfo($"两个PLC信号间隔时间:{plcIntervalTickets}。");
                if (data.StartsWith("%01!") || data.Contains("!") || data.StartsWith("%")==false)
                {
                    // 异常信号
                    myEventLog.LogInfo($"收到PLC异常信号:{data}。");
                }
                else if (data.Contains("$RC"))
                {
                    //var plcReadIntervalTickets = (DateTime.Now - preReadPLCTime).TotalMilliseconds;
                    //preReadPLCTime = DateTime.Now;
                    //myEventLog.LogInfo($"两个读取PLC信号间隔时间:{plcReadIntervalTickets}。");
                    if (data.Length == "%01#RC12345678**".Length)
                    {
                        //myEventLog.LogInfo($"PLC接收报警信号");
                        var dataArray = data.ToCharArray();
                        // 异常状态结果
                        var warning = dataArray[6];
                        var blockWarning = dataArray[7];
                        var printCardOutOfWarning = dataArray[8];
                        //var colorTapeOutOfWarning = dataArray[9];
                        var scannerStatus = dataArray[9];
                        var emptyWarning = dataArray[10];
                        var ccd1Status = dataArray[11];
                        var printStatus = dataArray[12];
                        var ccd2Status = dataArray[13];

                        if (ccd2Status == '1')
                        {
                            var intervalTickets = (DateTime.Now - prevCCD2Time).TotalMilliseconds;
                            //myEventLog.LogInfo($"84信号间隔时间：{intervalTickets}");
                            var isLightSignalEffective = intervalTickets > AppConfig.LightTimeInterval; // CCD2最小间隔时间=CCD2拨成功后等待时间+从拨成功位置走到初始值位置
                            prevCCD2Time = DateTime.Now;
                            if (isLightSignalEffective && ccd2IsBusy == false)
                            {
                                myEventLog.LogInfo($"84： PLC接收84信号，CCD2开始拍照");

                                ccd2IsBusy = true;
                                //GoToCCD2();
                                #region 处理CCD2信号

                                if (CheckQueueError() == false)
                                {

                                    var item = queue.FirstOrDefault(s => s.CCD2LightScan == false && (DateTime.Now - s.EnqueueTime).TotalMilliseconds > AppConfig.EnqueueToCCD2LightMinTime);
                                    if (item == null)
                                    {
                                        myEventLog.LogInfo($"队列中无数据，直接剔除");
                                        SendCCD2Out();
                                        ccd2IsBusy = false;
                                    }
                                    else
                                    {

                                        if ((DateTime.Now - item.EnqueueTime).TotalMilliseconds < AppConfig.EnqueueToCCD2LightMinTime)
                                        {
                                            // 收到CCD2信号时间小于光幕信号到CCD2最小时间，异常信号
                                            //myEventLog.LogInfo($"Index：{item.Index}，84： PLC接收有效84信号，CCD2开始拍照");
                                            myEventLog.LogInfo($"Index：{item.Index}，入队到CCD2时间小于{AppConfig.ScannerToCCD2LightMinTime}，无效信号");
                                            ccd2IsBusy = false;
                                        }
                                        else
                                        {
                                            if (item.CCD2LightScan)
                                            {
                                                myEventLog.LogInfo($"Index：{item.Index}，84： PLC接收无效84信号，当前项已在CCD2拍照，无效信号");
                                                ccd2IsBusy = true;
                                            }
                                            else
                                            {
                                                myEventLog.LogInfo($"Index：{item.Index}，84： PLC接收有效84信号，CCD2开始拍照");
                                                item.CCD2Time = DateTime.Now;
                                                //myEventLog.LogInfo($"Index：{item.Index}，记录CCD2时间：{item.CCD2Time.ToString("yyyy-MM-dd HH:mm:ss fff")}");
                                                item.CCD2LightScan = true;
                                                if (string.IsNullOrEmpty(item.ScanData) || item.ScanData != item.Drug.barcode)
                                                {
                                                    if (string.IsNullOrEmpty(item.ScanData))
                                                    {
                                                        myEventLog.LogInfo($"Index：{item.Index}，扫码枪未扫描到二维码，直接剔除");
                                                    }
                                                    else
                                                    if (item.ScanData != item.Drug.barcode)
                                                    {
                                                        myEventLog.LogInfo($"Index：{item.Index}，扫码枪扫描到二维码与液体的二维码不一致，直接剔除");
                                                    }
                                                    SendCCD2Out();
                                                    RemoveCCD2();
                                                }
                                                else
                                                {

                                                    if (AppConfig.CCD2IsEnabled)
                                                    {
                                                        ccd2IsBusy = true;
                                                        Task.Run(() =>
                                                        {
                                                            // 停一段时间稳定液体
                                                            Thread.Sleep(AppConfig.CcdTakePhotoSleepTime);
                                                            // 开始拍照
                                                            lock (ccd2LockHelper)
                                                            {
                                                                ccd2ErrorCount = 0;
                                                            }
                                                            CCD2TakePicture();
                                                        });
                                                    }
                                                    else
                                                    {
                                                        bool success = true;
                                                        OrderQueueModel model;
                                                        if (queue.TryPeek(out model))
                                                        {
                                                            myEventLog.LogInfo($"Index：{item.Index}，从队列中读出数据");

                                                            string[] spec = new string[] { "", "" };
                                                            success = SetCCD2IsSuccess(spec, "", success, model);

                                                            if (success == false)
                                                            {
                                                                // 从2#位剔除信息对比失败的药袋
                                                                SendCCD2Out();
                                                                // --- 删除对比失败的信息 ---
                                                                RemoveCCD2();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            myEventLog.LogInfo($"队列中无数据-未更新到数据库");
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    // 队列错位时，直接剔除
                                    SendCCD2Out();
                                    ccd2IsBusy = false;
                                    return;
                                } 
                                #endregion
                            }
                        }

                        if (ccd1Status == '1')
                        {
                            var intervalTickets = (DateTime.Now - prevCCD1Time).TotalMilliseconds;
                            //myEventLog.LogInfo($"81信号间隔时间：{intervalTickets}");
                            var isLightSignalEffective = intervalTickets > AppConfig.LightTimeInterval; // CCD1信号最小间隔时间=CCD1拨成功后等待时间+从拨成功位置走到初始值位置
                            if (isLightSignalEffective == false)
                            {
                                prevCCD1Time = DateTime.Now;
                                if (AppConfig.IsStopOnCCD1ResultDelayed)
                                {
                                    // 时间间隔小于AppConfig.LightTimeInterval，
                                    if (ccd1IsBusy == false && IndexOf81AfterCCD1IsNotBusy == AppConfig.Waite81SignalTimesOnCCD1ResultDelayed)
                                    {
                                        StopPrint();
                                        myEventLog.LogInfo($"设备可能有卡药：CCD1未剔除成功");
                                        MessageBox.Show("设备可能有卡药，请处理后再进行工作");
                                    }
                                    else
                                    {
                                        IndexOf81AfterCCD1IsNotBusy++;
                                    }
                                }
                            }
                            else
                            if (isLightSignalEffective && ccd1IsBusy == false)
                            {
                                if ((DateTime.Now - preCCD1SuccessTime).TotalMilliseconds > AppConfig.TowMedicionMinInterval)
                                {

                                    if (CanStartConveryorBelt())
                                    {
                                        StartConveryorBelt();
                                    }
                                    preCCD1SuccessTime = DateTime.Now;
                                    prevCCD1Time = DateTime.Now;
                                    GoToCCD1();
                                }
                                else
                                {
                                    if (CanStartConveryorBelt())
                                    {
                                        StartConveryorBelt();
                                    }
                                }
                            }
                        }


                        if (printStatus == '1')
                        {
                            var intervalTickets = (DateTime.Now - prevPrinterLightTime).TotalMilliseconds;
                            //myEventLog.LogInfo($"82信号间隔时间：{intervalTickets}");
                            var isLightSignalEffective = intervalTickets > AppConfig.LightTimeInterval; // 打印机光幕最小间隔时间=两个读取信号的长间隔时间
                            prevPrinterLightTime = DateTime.Now;
                            if (isLightSignalEffective)
                            {

                                Task.Run(() =>
                                {
                                    GoToPrinterLight();
                                });
                            }
                        }

                        if (scannerStatus == '1')
                        {
                            var intervalTickets = (DateTime.Now - prevScannerLightTime).TotalMilliseconds;
                            //myEventLog.LogInfo($"83信号间隔时间：{intervalTickets}");
                            var isLightSignalEffective = intervalTickets > AppConfig.LightTimeInterval; //扫码枪光幕最小间隔时间=两个读取信号的长间隔时间
                            prevScannerLightTime = DateTime.Now;
                            if (isLightSignalEffective)
                            {
                                //Task.Run(() =>
                                //{
                                    GoToScannerLight();
                                //});
                            }
                        }

                        if(printCardOutOfWarning == '1')
                        {
                            if(IsWarningShowing == false)
                            {
                                IsWarningShowing = true;
                                MessageBox.Show("设备的打印机缺纸，请处理后再进行工作！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                                StopPrint();
                            }
                        }

                        //if (warning == '1'
                        //                            //|| blockWarning == '1'
                        //                            || printCardOutOfWarning == '1'
                        //                            //|| printCardOutOfWarning == '1'
                        //                            //|| colorTapeOutOfWarning == '1'
                        //                            || emptyWarning == '1')
                        //{
                        //    StopPrint();
                        //}

                        //if (IsWarningShowing == false && printCardOutOfWarning == '1')
                        //{
                        //    IsWarningShowing = true;
                        //    MessageBox.Show("设备的打印机缺纸，请处理后再进行工作！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        //    //IsWarningShowing = false;
                        //    //printerManager.ResetPrinter();
                        //}
                        ////else
                        ////if (IsWarningShowing == false && colorTapeOutOfWarning == '1')
                        ////{
                        ////    IsWarningShowing = true;
                        ////    MessageBox.Show("设备的打印机缺少色带，请处理后再进行工作！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        ////    //printerManager.ResetPrinter();
                        ////    IsWarningShowing = false;
                        ////}
                        //else
                        //if (IsWarningShowing == false && emptyWarning == '1')
                        //{
                        //    // 长时间未方药，停机
                        //    //printerManager.ResetPrinter();
                        //}
                        //else
                        //if (IsWarningShowing == false && (warning == '1' || blockWarning == '1'))
                        //{
                        //    IsWarningShowing = true;
                        //    myEventLog.LogInfo($"设备有卡药：PLC报警");
                        //    MessageBox.Show("设备有卡药，请处理后再进行工作！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        //    //IsWarningShowing = false;
                        //    //printerManager.ResetPrinter();
                        //}

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

        private void GoToCCD1()
        {
            ccd1IsBusy = true;
            if (CanStartConveryorBelt())
            {
                StartConveryorBelt();
            }

            Task.Run(() =>
            {
                // 收到光幕信号，队列中液体数量小于队列中液体最大数，且未打印液体数量小于队列中允许的最大未打印数量，继续拍照

                ccd1IsBusy = true;
                if (AppConfig.MaxQueueCount != 0 && queue.Count() >= AppConfig.MaxQueueCount)
                {
                    myEventLog.LogInfo($"Index：{queueIndex+1}，81： PLC接收81信号，队列数量超过{AppConfig.MaxQueueCount}，不识别，剔除");

                    SendCCD1Out();
                    ccd1IsBusy = false;
                }
                else
                {

                    myEventLog.LogInfo($"Index：{queueIndex+1}，PLC接收81信号，CCD1开始拍照");

                    //myEventLog.LogInfo($"Index：{queueIndex+1}，PLC接收数据：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")}");

                    // 停一段时间稳定液体
                    Thread.Sleep(AppConfig.CcdTakePhotoSleepTime);

                    // 开始拍照
                    lock (ccd1LockHelper)
                    {
                        ccd1ErrorCount = 0;
                    }
                    CCD1TakePicture();
                }
            });
        }

        private void GoToCCD2()
        {
            myEventLog.LogInfo($"84： PLC疑似接收84信");
            //SendCCD2Success();
            //Success();
            //return;
        }

                string prevScanCode = "";
        DateTime prevScanCodeTime = DateTime.Now;
        /// <summary>
        /// 扫码枪扫码结果
        /// </summary>
        /// <param name="data"></param>
        public void OnScannerDataReceived(string data)
        {
            try
            {
                if(data == prevScanCode && (DateTime.Now - prevScanCodeTime).TotalMilliseconds<2000)
                {
                    myEventLog.LogInfo($"接收重复扫码枪：{data}，不处理");
                    return;
                }

                prevScanCode = data;
                prevScanCodeTime = DateTime.Now;

                myEventLog.LogInfo($"接收扫码枪：{data}");
                // 这里设置显示串口通讯

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



        /// <summary>
        /// 流程全部完成
        /// 从队列中推出
        /// </summary>
        private void Success()
        {
            // 判断是否有队列数据
            if (queue.IsEmpty == false)
            {
                // CCD2是流程最后一步，取出第一个
                OrderQueueModel qModel;
                if (queue.TryDequeue(out qModel))
                {
                    PrintQueueTimes(qModel);
                    myEventLog.LogInfo($"Index:{qModel.Index},从队列中删除一项，Id={qModel.Drug.Id}，Code={qModel.QRData}，GroupNum={qModel.Drug?.group_num}，BarCode={qModel.Drug.barcode}");
                }
                else
                {
                    myEventLog.LogInfo($"队列数量： {queue.Count}！");
                }

                if (queue.IsEmpty)
                {
                    QueueIsEmptyTime = DateTime.Now;
                }

                myEventLog.LogInfo($"队列数量： {queue.Count}！");
                if (CanStartConveryorBelt())
                {
                    //myEventLog.LogInfo($"开启传送带，队列数量：{queue.Count()}/{AppConfig.MaxQueueCount}，未打印数量：{queue.Count(s => s.PrinterLightScan == false)}/{AppConfig.MaxNotPrintQueueCount}");
                    StartConveryorBelt();
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
        private void AddQueue(tOrder drug, string cmd, string spec, string ml,string specCmd,DateTime ccd1SuccessTime)
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
                qModel.CCD2LightScan = false;
                qModel.Index = queueIndex++;
                qModel.SpecCmd = specCmd;
                qModel.EnqueueTime = ccd1SuccessTime;
                //myEventLog.LogInfo($"记录入队时间：{qModel.EnqueueTime.ToString("yyyy-MM-dd HH:mm:ss fff")}");

                myEventLog.LogInfo($"将Index:{qModel.Index},液体信息插入队列:Id={qModel.Drug.Id}，Code={qModel.QRData}，GroupNum={qModel.Drug.group_num}，BarCode={qModel.Drug.barcode}");
            }
            else
            {
                //qModel.Drug = new tOrder();
                qModel.Index = queueIndex++;
                myEventLog.LogInfo($"Index:{qModel.Index},将空液体信息插入队列");
            }
            // 添加到队列
                // 入队时添加到队尾
                queue.Enqueue(qModel);
                QueueIsEmptyTime = DateTime.Now;
                myEventLog.LogInfo($"队列数量： {queue.Count}！");
                if(CanStopConveryorBelt())
                {
                    // 入队后，队列中液体数量大于队列中液体最大数，或未打印液体数量大于队列中允许的最大未打印数量，停止第一个传送带
                    //myEventLog.LogInfo($"停止传送带，队列数量：{queue.Count()}/{AppConfig.MaxQueueCount}，未打印数量：{queue.Count(s => s.PrinterLightScan == false)}/{AppConfig.MaxNotPrintQueueCount}");
                    StopConveryorBelt();
                }

        }

        private void PrintQueueTimes(OrderQueueModel model)
        {
            //var now = DateTime.Now;
            //myEventLog.LogInfo($"CCD1-打印光幕时间： {(model.PrintLightTime - model.EnqueueTime).TotalMilliseconds}！");
            //myEventLog.LogInfo($"CCD1-扫码枪光幕时间： {(model.ScannerLightTime - model.EnqueueTime).TotalMilliseconds}！");
            //myEventLog.LogInfo($"CCD1-CCD2光幕时间： {(model.CCD2Time - model.EnqueueTime).TotalMilliseconds}！");
            //myEventLog.LogInfo($"CCD1-完成时间： {(now - model.EnqueueTime).TotalMilliseconds}！");

            //myEventLog.LogInfo($"打印光幕时间-扫码枪光幕时间： {(model.ScannerLightTime - model.PrintLightTime).TotalMilliseconds}！");
            //myEventLog.LogInfo($"打印光幕时间-CCD2光幕时间： {(model.CCD2Time - model.PrintLightTime).TotalMilliseconds}！");
            //myEventLog.LogInfo($"打印光幕时间-完成时间： {(now - model.PrintLightTime).TotalMilliseconds}！");

            //myEventLog.LogInfo($"扫码枪光幕时间-CCD2光幕时间： {(model.CCD2Time - model.ScannerLightTime).TotalMilliseconds}！");
            //myEventLog.LogInfo($"扫码枪光幕时间-完成时间： {(now - model.ScannerLightTime).TotalMilliseconds}！");

            //myEventLog.LogInfo($"CCD2光幕时间-完成时间： {(now - model.CCD2Time).TotalMilliseconds}！");
        }
        /// <summary>
        /// CCD2错误时删除对象
        /// </summary>
        private void RemoveCCD2()
        {
            // 判断是否有队列数据
                if (queue.IsEmpty==false)
                {
                    // CCD2是流程最后一步，取出第一个
                    OrderQueueModel qModel ;
                    if(queue.TryDequeue(out qModel))
                    {
                        PrintQueueTimes(qModel);
                        myEventLog.LogInfo($"Index:{qModel.Index},从队列中删除一项，Id={qModel.Drug.Id}，Code={qModel.QRData}，GroupNum={qModel.Drug?.group_num}，BarCode={qModel.Drug.barcode}");
                    }

                    myEventLog.LogInfo($"队列数量： {queue.Count}！");

                    if (queue.IsEmpty)
                    {
                        QueueIsEmptyTime = DateTime.Now;
                    }

                    if (CanStartConveryorBelt())
                    {
                        //myEventLog.LogInfo($"开启传送带，队列数量：{queue.Count()}/{AppConfig.MaxQueueCount}，未打印数量：{queue.Count(s => s.PrinterLightScan == false)}/{AppConfig.MaxNotPrintQueueCount}");
                        StartConveryorBelt();
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
            //myEventLog.LogInfo($"扫描到二维码:{result}");
            //OrderQueueModel model = queue.LastOrDefault(m => m.ScannerLightScan == true && string.IsNullOrEmpty(m.ScanData));
            var model = queue.LastOrDefault(m=>m.ScannerLightScan == true); // 最后一个记录扫码枪光幕的
            if(model != null)
            {
                if (string.IsNullOrEmpty(model.ScanData) == false)
                {
                    // 如果最后一个已经记录二维码，将model=null，尝试匹配下一个Model
                    model = null;
                }
            }
            if(model == null)
            {
                // 尝试寻找第一个没有记录扫码枪光幕的（这里认为不会同时出现未记录扫码枪光幕且未扫到二维码的情况）
                model = queue.FirstOrDefault(m => m.ScannerLightScan == false);
            }

            if (model != null)
            {
                if(model.QRData.Length * 2==result.Length && model.QRData+model.QRData == result)
                {
                    result = model.QRData;
                }

                if (model.QRData == result)
                {
                    model.ScanData = result;
                    model.ScannerLightScan = true;
                    //model.PrinterLightScan = true;
                    myEventLog.LogInfo($"Index:{model.Index}，BarCode:{model.Drug.barcode}，修改液体扫描到的二维码：{result}");
                }
                else
                {
                    ClearQueue();
                }
            }
            else
            {
                myEventLog.LogInfo($"未找到记录光幕的液体");
            }
        }


        /// <summary>
        /// 打印机光幕
        /// </summary>
        private void GoToPrinterLight()
        {
            myEventLog.LogInfo($"82： PLC接收82信号，开始打印");
            // 找到最后一个没有记录的进行记录
            OrderQueueModel model = queue.FirstOrDefault(m => !m.PrinterLightScan);
            if (model != null)
            {
//#warning 需要统计从入队到打印机收到打印信号的最小时间间隔
                //var printLightMinTime = 1000;   // 最小时间=入队到CCD1推到传送带时间+(CCD1到打印机光幕移动距离)/传送带移动速度=（48-22）/43=600
                if ((DateTime.Now - model.EnqueueTime).TotalMilliseconds > AppConfig.EnqueueToPrintLightMinTime)
                {
                    //myEventLog.LogInfo($"82： PLC接收有效82信号，开始打印");
                    model.PrintLightTime = DateTime.Now;
                    //myEventLog.LogInfo($"Index：{model.Index}，GroupNum：{model.Drug.group_num}，BarCode={model.Drug.barcode}，记录打印机光幕时间：{model.PrintLightTime.ToString("yyyy-MM-dd HH:mm:ss fff")}");
                    model.PrinterLightScan = true;
                    //myEventLog.LogInfo($"过打印机光幕，记录过光幕状态");
                    if (CanStartConveryorBelt())
                    {
                        // 打印过之后，判断是否可以可以启动
                        StartConveryorBelt();
                    }
                }
                else
                {
                    myEventLog.LogInfo($"Index：{model.Index}，GroupNum：{model.Drug.group_num}，BarCode={model.Drug.barcode}，当前信号从入队列到收到打印机光幕信号小于{AppConfig.EnqueueToPrintLightMinTime}，无效信号");
                }
            }
            else
            {
                myEventLog.LogInfo($"过打印机光幕，队列中未找到打印液体，无效信号");
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
            if (queue.IsEmpty)
            {
                myEventLog.LogInfo($"队列中无数据，不修改扫码枪计数！");
            }
            else
            {
                myEventLog.LogInfo($"83： PLC接收83信号，记录扫码信息");

                //var item = queue.FirstOrDefault(s => s.PrinterLightScan && s.ScannerLightScan == false && (DateTime.Now - s.PrintLightTime).TotalMilliseconds > AppConfig.PrintToScannerLightMinTime);
                //var item = queue.FirstOrDefault(s => s.PrinterLightScan && s.ScannerLightScan == false && (DateTime.Now - s.PrintLightTime).TotalMilliseconds > AppConfig.PrintToScannerLightMinTime);
                //var item = queue.FirstOrDefault(s => s.ScannerLightScan == false && (DateTime.Now - s.PrintLightTime).TotalMilliseconds > AppConfig.PrintToScannerLightMinTime);
                var item = queue.FirstOrDefault(s => s.ScannerLightScan == false);
                if (item != null)
                {
                    //var enqueueToScannerMinTime = 2700;// 最小时间=入队到CCD1推到传送带时间+(入队到扫码枪光幕移动距离)/传送带移动速度=（85-22）/43=1650
                    //var printerToScannerMinTime = 1200; // 最小时间=入队到CCD1推到传送带时间+(CCD1到扫码枪光幕移动距离)/传送带移动速度=（36-22）/43=581
                    var now = DateTime.Now;

                    if ((now - item.EnqueueTime).TotalMilliseconds > AppConfig.EnqueueToScannerLightMinTime)
                    {
                        if (item.PrinterLightScan == true && (now - item.PrintLightTime).TotalMilliseconds < AppConfig.PrintToScannerLightMinTime)
                        {
                            myEventLog.LogInfo($"Index：{item.Index}，GroupNum：{item.Drug.group_num}，BarCode={item.Drug.barcode}，过扫码枪光幕，小于从打印机光幕到扫码枪光幕的最小时间，无效信号");
                        }
                        else
                        {
                            item.ScannerLightScan = true;
                            item.ScannerLightTime = DateTime.Now;
                            myEventLog.LogInfo($"Index：{item.Index}，GroupNum：{item.Drug.group_num}，BarCode={item.Drug.barcode}，记录扫码枪光幕时间：{item.ScannerLightTime.ToString("yyyy-MM-dd HH:mm:ss fff")}");
                            //myEventLog.LogInfo($"83： PLC接收有效83信号，记录扫码信息");
                        }
                    }
                    else
                    {
                        myEventLog.LogInfo($"Index：{item.Index}，GroupNum：{item.Drug.group_num}，BarCode={item.Drug.barcode}，过扫码枪光幕，小于从入队到扫码枪光幕的最小时间，无效信号");
                    }
                }
                else
                {
                    myEventLog.LogInfo($"过扫码枪光幕，队列中未找到未过光幕液体，无效信号");
                }
            }
        }

        #endregion

        #region 串口链接检查


        public void OnCCD1Error(string msg)
        {
                CCDConnected = false;
        }
        public void OnCCD1Complated()
        {
                CCDConnected = true;
        }

        public void OnCCD2Error(string msg)
        {
                CCDConnected = false;
        }

        public void OnCCD2Complated()
        {
        }


        public void OnPLCError(string msg)
        {
            PCLConnected = false;
        }
        public void OnPLCComplated(int type)
        {
            if (type == 1)
            {
                PCLConnected = true;
            }
            else
            {
                PCLConnected = false;
                PlcReaderTimer?.Stop();
            }
        }


        public void OnScannerError(string msg)
        {
            AutoScannerConnected = false;
        }
        public void OnScannerComplated()
        {
            AutoScannerConnected = true;
        }


        public void OnScannerHandlerError(string msg)
        {
            HanderScannerConnected = false;
        }
        public void OnScannerHandlerComplated()
        {
            HanderScannerConnected = true;
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

            //myEventLog.LogInfo($"更新列表内容");

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
                spcViewPanel.SetDbState(false);

                MessageBox.Show("数据库连接失败，请检查数据库服务是否开启！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else
            {
                // 这里设置显示串口正常
                spcViewPanel.SetDbState(true);
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
            //BindWarning();
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
            var batch = cb_batch.SelectedValue.ToString();
            // 绑定主药
            var list = string.IsNullOrEmpty(batch) ? new List<tDrug>() : new DrugManager().GetMainDrugListForPrintWindow(use_date.SelectedDate.Value, batch);
            list.Insert(0, new tDrug() { drug_code = "", drug_name = "全部" });

            Dispatcher.Invoke(() =>
            {
                this.cb_drug.DisplayMemberPath = "drug_name";
                this.cb_drug.SelectedValuePath = "drug_code";
                this.cb_drug.ItemsSource = list;
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
            StopPrint();

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


        #region 开始打印/暂停打印事件
        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {

            // 检查本地数据库连接状态
            if (CheckDBConnection() == false)
            {
                return;
            }

            // 打印期间不需要连接Pivas
            //if (ConnectionManager.CheckPivasConnetionStatus() == false)
            //{
            //    MessageBox.Show("Pivas数据库连接失败，请检查数据库服务是否开启！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return;
            //}

            // 尝试开启串口，已开启的不重新开启
            PLCSerialPortUtils.GetInstance(this).Open();

            CCDSerialPortUtils.GetInstance(this).Open();
            ScannerSerialPortUtils.GetInstance(this).Open();
            ScanHandlerSerialPortUtils.GetInstance(this).Open();

            plcCommandSendQueueHelper = PLCCommandQueue.GetInstance(this);
            plcCommandSendQueueHelper.Start();

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


            if (tabMain.SelectedIndex == 0)
            {
                if (string.IsNullOrEmpty(cb_batch.SelectedValue?.ToString() ?? ""))
                {
                    MessageBox.Show("请选择批次！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (autoPrintList.Any() == false)
                {
                    MessageBox.Show("当前批次无待贴标签液体！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (autoPrintList.Count(s => s.printing_status.HasValue == false || s.printing_status == PrintStatusEnum.NotPrint) == 0)
                {
                    MessageBox.Show("当前批次以完成贴签，无待打印液体！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                Task.Run(() => {
                    int times = 60;
                    while (times > 0)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            loadMask.LoadingText = "正在检测打印机" + times;
                        });
                        times--;
                        Thread.Sleep(1000);
                    }
                });


                // 轮询监听长时间未放药，停止打印
                QueueIsEmptyTime = DateTime.Now;
                QueueIsEmptyStopTimer = new DispatcherTimer();
                QueueIsEmptyStopTimer.Tick += QueueIsEmptyStopTimer_Tick; ;
                QueueIsEmptyStopTimer.Interval = new TimeSpan(0, 0, 0, 5);
                QueueIsEmptyStopTimer.Start();

                if (AppConfig.IsEnabledCCD2ExpireDetectict || AppConfig.IsEnabledScannerLightExpireDetectict || AppConfig.IsEnabledPrinterLightExpireDetectict || AppConfig.BeforeCCD2BlockDetectictIsEnabled)
                {
                    // 打印机前档板卡药计时器
                    BlockDetectictTimer = new DispatcherTimer();
                    BlockDetectictTimer.Tick += BeforePrintBlockTimer_Tick; ;
                    BlockDetectictTimer.Interval = new TimeSpan(0, 0, 0, 0, AppConfig.BlockDetectictInterval);
                    BlockDetectictTimer.Start();
                }

                // 轮询获取光幕状态和异常信息
                PlcReaderTimer = new DispatcherTimer();
                PlcReaderTimer.Tick += PlcReaderTimer_Tick; ; ;
                PlcReaderTimer.Interval = new TimeSpan(0, 0, 0, 0, AppConfig.LightReaderIntervalTime);
                PlcReaderTimer.Start();

                Task.Run(() =>
                {
                    // 连接打印机Loading，尝试打开打印机连接，判断打印机状态是否需要重置，重置后goto到打印机Loading，获取打印机状态5次仍失败的，提示获取打印机状态失败
                    try
                    {
                        Dispatcher.Invoke(() =>
                        {
                            loadMask.Visibility = Visibility.Visible;
                        });
                        if (IsConnectDevices == false || CheckPrinterStatus())
                        {
                            queueIndex = 0;

                            // 清空队列
                            //myEventLog.LogInfo($"队列数量： {queue.Count}！");
                            queue = new ConcurrentQueue<OrderQueueModel>();
                            myEventLog.LogInfo($"清空队列！");

                            // 清除打印机中的打印内容
                            printerManager.ClearAll();

                            // 先关再开
                            plcCommandSendQueueHelper.Enqueue("%01#WCSR00291**"); // 停止打印
                            // 命令间隔时间太短会导致无法启动
                            Thread.Sleep(50);
                            myEventLog.LogInfo($"发送停止命令");

                            plcCommandSendQueueHelper.Enqueue("%01#WCSR00201**"); // 开始打印
                            myEventLog.LogInfo($"发送开始命令");


                            SetCCD1IsNotBusy();
                            SetCCD2IsNotBusy();
                            IsWarningShowing = false;

                            StartUpdateControlState();

                        }
                        else
                        {
                            MessageBox.Show("无法连接到打印机，请检查打印机是否开启！");
                        }
                    }
                    catch (Exception ex) {
                        myEventLog.LogError("开始打印出错：" + ex.Message, ex);
                        MessageBox.Show("启动失败，请重试！");
                        StopPrint();
                    }
                    finally
                    {
                        Dispatcher.Invoke(() =>
                        {
                            loadMask.Visibility = Visibility.Hidden;
                        });
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
        /// 检测打印机前档板是否有卡药（液体是否在要求时间内过打印机光幕）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BeforePrintBlockTimer_Tick(object sender, EventArgs e)
        {
            if (IsWarningShowing == false && AppConfig.BeforeCCD2BlockDetectictIsEnabled)
            {
                // 没过打印机，且入队时间超过
                if (queue.Count(s => s.CCD2LightScan == false && s.ScannerLightScan == true && (DateTime.Now - s.ScannerLightTime).TotalMilliseconds > AppConfig.BeforeCCD2BlockAfterScannerLightTimes)>AppConfig.BeforeCCD2MaxCount)
                {
                    IsWarningShowing = true;
                    StopPrint();
                    myEventLog.LogInfo($"CCD2前等待数量超过{AppConfig.BeforeCCD2MaxCount}，提示卡药，暂停打印");
                    MessageBox.Show("设备可能有卡药，请处理后再进行工作！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            if (IsWarningShowing == false && AppConfig.IsEnabledPrinterLightExpireDetectict)
            {
                var item = queue.FirstOrDefault(s => s.PrinterLightScan == false && (DateTime.Now - s.EnqueueTime).TotalMilliseconds > AppConfig.PrinterLightExpireDetectictTimes);
                if (item!=null)
                {
                    // 插入队列到一段时间后未收到打印机光幕信号，提示卡药
                    IsWarningShowing = true;
                    StopPrint();
                    myEventLog.LogInfo($"Index：{item.Index}，收到打印机光幕信号超时{AppConfig.PrinterLightExpireDetectictTimes}，提示卡药，暂停打印.{item.EnqueueTime.ToString("yyyy-MM-dd HH:mm:ss fff")}");
                    MessageBox.Show("设备可能有卡药，请处理后再进行工作！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            if (IsWarningShowing == false && AppConfig.IsEnabledScannerLightExpireDetectict)
            {
                var item = queue.FirstOrDefault(s => s.ScannerLightScan == false && s.PrinterLightScan == true && (DateTime.Now - s.PrintLightTime).TotalMilliseconds > AppConfig.ScannerLightExpireDetectictTimes);
                if (item!=null)
                {
                    // 收到打印机光幕信号一段时间后未收到扫码枪光幕信号，提示卡药
                    IsWarningShowing = true;
                    StopPrint();
                    myEventLog.LogInfo($"Index：{item.Index}，收到扫码枪光幕信号超时{AppConfig.ScannerLightExpireDetectictTimes}，提示卡药，暂停打印.{item.PrintLightTime.ToString("yyyy-MM-dd HH:mm:ss fff")}");
                    MessageBox.Show("设备可能有卡药，请处理后再进行工作！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            if (IsWarningShowing == false && AppConfig.IsEnabledCCD2ExpireDetectict)
            {
                var item = queue.FirstOrDefault(s => s.CCD2LightScan == false && s.ScannerLightScan == true && (DateTime.Now - s.ScannerLightTime).TotalMilliseconds > AppConfig.CCD2ExpireDetectictTimes);
                if (item!=null)
                {
                    // 收到打印机光幕信号一段时间后未收到扫码枪光幕信号，提示卡药
                    IsWarningShowing = true;
                    StopPrint();
                    myEventLog.LogInfo($"Index：{item.Index}，收到CCD2信号超时{AppConfig.CCD2ExpireDetectictTimes}，提示卡药，暂停打印.{item.ScannerLightTime.ToString("yyyy-MM-dd HH:mm:ss fff")}");
                    MessageBox.Show("设备可能有卡药，请处理后再进行工作！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        /// <summary>
        /// PLC读光幕和报警信号计时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlcReaderTimer_Tick(object sender, EventArgs e)
        {
            plcCommandSendQueueHelper.Enqueue("%01#RCP8R0090R0095R0096R0007R0098R0170R0009R0173**");
        }

        /// <summary>
        /// 长时间未放药Timer处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueueIsEmptyStopTimer_Tick(object sender, EventArgs e)
        {
            if (QueueIsEmptyTime.AddMilliseconds(AppConfig.QueueIsEmptyStopTime) < DateTime.Now)
            {
                // 当队列为空的时间超过设置时间，停止打印
                myEventLog.LogInfo($"长时间未放药，停机");

                StopPrint();

                MessageBox.Show("长时间未放药，自动停机！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }

        /// <summary>
        /// 检查打印机状态
        /// </summary>
        /// <returns></returns>
        private bool CheckPrinterStatus()
        {
            // 基本上40秒内打印机能启动成功
            var tryConnectionTimes = 40;
            var printerConnected = true;
            try
            {

                while (printerManager.TryOpenPrinterConnection() == false)
                {
                    tryConnectionTimes--;
                    if (tryConnectionTimes == 0)
                    {
                        printerConnected = false;
                        break;
                    }

                    Thread.Sleep(1000);
                }

                while (printerConnected == true && printerManager.GetPrinter() == null)
                {
                    tryConnectionTimes--;
                    if (tryConnectionTimes ==5)
                    {
                        printerConnected = false;
                        break;
                    }
                    Thread.Sleep(1000);
                }
                Dispatcher.Invoke(() =>
                {
                    loadMask.LoadingText = $"已成功连接打印机！";
                });
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
            myEventLog.LogInfo($"队列数量： {queue.Count}！");

            StopUpdateControlState();
            if (IsConnectDevices)
            {
                QueueIsEmptyStopTimer?.Stop();
                PlcReaderTimer?.Stop();
                plcCommandSendQueueHelper.Enqueue("%01#WCSR00291**"); // 停止打印
                myEventLog.LogInfo($"发送停止命令");

                // 等上一段时间，命令发送完毕再停止发送命令
                Thread.Sleep(200);
                plcCommandSendQueueHelper.Stop();

                printerManager.ClearAll();
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
            if (this.cb_batch == null || string.IsNullOrEmpty(this.cb_batch.SelectedValue.ToString()))
            {
                MessageBox.Show("请选择批次！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                myEventLog.LogInfo("开始同步医嘱");
                this.btnUpdate.IsEnabled = false;
                new DataSync().SyncOrder(this.use_date.SelectedDate.Value,this.cb_batch.SelectedValue.ToString());
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
        private static bool IsConveryorBeltRunning = true;
        /// <summary>
        /// 启动传送带
        /// </summary>
        private void StartConveryorBelt()
        {
            //if (IsConveryorBeltRunning)
            //{
            //    return;
            //}
            if (IsQueueError == true)
            {
                return;
            }
            IsConveryorBeltRunning = true;
            // 启动传送带
            //myEventLog.LogInfo($"发送启动传送带命令");
            plcCommandSendQueueHelper.Enqueue("%01#WCSR00050**");
        }
        /// <summary>
        /// 停止传送带
        /// </summary>
        private void StopConveryorBelt()
        {
            if (IsConveryorBeltRunning)
            {
                //myEventLog.LogInfo($"发送停止传送单命令");
                // 停止传送带
                plcCommandSendQueueHelper.Enqueue("%01#WCSR00051**");

                IsConveryorBeltRunning = false;
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

        /// <summary>
        /// 检查出队列错位，在这个时间之前的CCD2判断都是错的
        /// </summary>
        DateTime ccd2IsErrorBefore = DateTime.Now;
        bool IsQueueError = false;
        /// <summary>
        /// 队列错位时，清除队列
        /// </summary>
        private void ClearQueue()
        {
            /***
             * 发现打印内容与扫描到的二维码不一致，清空队列
             * 1、给PLC发送清空队列指令
             * 2、停止前端传送带
             * 3、清空队列
             */
            Task.Run(()=> {
                IsQueueError = true;
                myEventLog.LogInfo($"检测到标签错位，开始执行清空队列流程");
                // 从现在开始，8000毫秒内的液体都会被CCD2删除
                ccd2IsErrorBefore = DateTime.Now.AddMilliseconds(8000);
                myEventLog.LogInfo($"检测到标签错位，第一次清空打印机内容");
                printerManager.ClearAll();
                myEventLog.LogInfo($"检测到标签错位，停止传送带");
                StopConveryorBelt();
                OrderQueueModel item;
                myEventLog.LogInfo($"检测到标签错位，第一次清空队列:{queue.Count()}个");
                while (queue.TryDequeue(out item))
                {

                }
                Thread.Sleep(3000);
                // 再清一次队列，把刚加进去的数据清除
                myEventLog.LogInfo($"检测到标签错位，第二次清空打印机内容");
                printerManager.ClearAll();
                myEventLog.LogInfo($"检测到标签错位，第二次清空队列:{queue.Count()}个");
                while (queue.TryDequeue(out item))
                {

                }
                // 保证所有液体都过了打印机，重置PLC的队列
                myEventLog.LogInfo($"检测到标签错位，发送清空PLC队列命令");
                plcCommandSendQueueHelper.Enqueue($"%01#WCSR00011**");
                IsQueueError = false;
                Thread.Sleep(500);

                // 清空打印机的内容
                myEventLog.LogInfo($"检测到标签错位，清空打印机内容");
                printerManager.ClearAll();
                // 取消重置PLC队列状态
                myEventLog.LogInfo($"检测到标签错位，发送重置PLC队列命令");
                plcCommandSendQueueHelper.Enqueue($"%01#WCSR00010**");
                // 传送带继续
                StartConveryorBelt();
                myEventLog.LogInfo($"检测到标签错位，结束清空队列流程");
            });
        }
    }

}
