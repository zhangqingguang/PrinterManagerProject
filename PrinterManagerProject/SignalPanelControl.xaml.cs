using PrinterManagerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrinterManagerProject
{
    /// <summary>
    /// SignalPanelControl.xaml 的交互逻辑
    /// </summary>
    public partial class SignalPanelControl : UserControl
    {
        public DeviceStatusModel StatusModel = new DeviceStatusModel();
        private readonly TaskScheduler _syncContextTaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        // 串口状态，灰色-失败
        SolidColorBrush errorColor = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#dddddd"));
        // 串口状态，绿色-成功
        SolidColorBrush complate = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#8cea00"));

        public SignalPanelControl()
        {
            InitializeComponent();

            lblCCD1.SetBinding(Label.ContentProperty, new Binding("CCD1Text") { Source = StatusModel });
            lblCCD2.SetBinding(Label.ContentProperty, new Binding("CCD2Text") { Source = StatusModel });
            lblControlSystem.SetBinding(Label.ContentProperty, new Binding("PlcText") { Source = StatusModel });
            lblScan1.SetBinding(Label.ContentProperty, new Binding("AutoScannerText") { Source = StatusModel });
            lblScan2.SetBinding(Label.ContentProperty, new Binding("HanderScannerText") { Source = StatusModel });
            lblDb.SetBinding(Label.ContentProperty, new Binding("DBText") { Source = StatusModel });
        }


        public void SetCCD1State(bool state)
        {
            Task.Factory.StartNew(() =>
            {
                elCCD1.Fill = (state ? complate : errorColor);
                StatusModel.CCD1Text = (state ? "CCD2连接成功" : "CCD2连接失败");

            }, new CancellationTokenSource().Token, TaskCreationOptions.None, _syncContextTaskScheduler);
        }
        public void SetCCD2State(bool state)
        {
            Task.Factory.StartNew(() =>
            {
                elCCD2.Fill = (state ? complate : errorColor);
                StatusModel.CCD2Text = (state ? "CCD2连接成功" : "CCD2连接失败");

            }, new CancellationTokenSource().Token, TaskCreationOptions.None, _syncContextTaskScheduler);
        }
        public void SetPlcState(bool state)
        {
            Task.Factory.StartNew(() =>
            {
                elControlSystem.Fill = (state ? complate : errorColor);
                StatusModel.PlcText = (state ? "控制系统连接成功" : "控制系统连接失败");

            }, new CancellationTokenSource().Token, TaskCreationOptions.None, _syncContextTaskScheduler);
        }
        public void SetAutoScannerState(bool state)
        {
            Task.Factory.StartNew(() =>
            {
                elScan1.Fill = (state ? complate : errorColor);
                StatusModel.AutoScannerText = (state ? "自动扫码枪连接成功" : "自动扫码枪连接失败");

            }, new CancellationTokenSource().Token, TaskCreationOptions.None, _syncContextTaskScheduler);
        }
        public void SetHanderScannerState(bool state)
        {
            Task.Factory.StartNew(() =>
            {
                elScan2.Fill = (state ? complate : errorColor);
                StatusModel.HanderScannerText = (state ? "手动扫码枪连接成功" : "手动扫码枪连接失败");

            }, new CancellationTokenSource().Token, TaskCreationOptions.None, _syncContextTaskScheduler);
        }
        public void SetDbState(bool state)
        {
            Task.Factory.StartNew(() =>
            {
                elDb.Fill = (state? complate : errorColor);
                StatusModel.DBText = (state? "数据库连接成功" : "数据库连接失败");

            }, new CancellationTokenSource().Token, TaskCreationOptions.None, _syncContextTaskScheduler);
        }
    }
}
