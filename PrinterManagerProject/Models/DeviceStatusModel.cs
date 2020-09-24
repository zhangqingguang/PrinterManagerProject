using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PrinterManagerProject.Models
{

    public class DeviceStatusModel : DependencyObject
    {
        public string CCD1Text
        {
            get { return (string)GetValue(CCD1TextProperty); }
            set { SetValue(CCD1TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CCD1Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CCD1TextProperty =
            DependencyProperty.Register("CCD1Text", typeof(string), typeof(DeviceStatusModel), new PropertyMetadata("CCD1连接中..."));



        public int CCD1State
        {
            get { return (int)GetValue(CCD1StateProperty); }
            set { SetValue(CCD1StateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CCD1State.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CCD1StateProperty =
            DependencyProperty.Register("CCD1State", typeof(int), typeof(DeviceStatusModel), new PropertyMetadata(0));




        public string CCD2Text
        {
            get { return (string)GetValue(CCD2TextProperty); }
            set { SetValue(CCD2TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CCD2Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CCD2TextProperty =
            DependencyProperty.Register("CCD2Text", typeof(string), typeof(DeviceStatusModel), new PropertyMetadata("CCD2连接中..."));



        public int CCD2State
        {
            get { return (int)GetValue(CCD2StateProperty); }
            set { SetValue(CCD2StateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CCD2State.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CCD2StateProperty =
            DependencyProperty.Register("CCD2State", typeof(int), typeof(DeviceStatusModel), new PropertyMetadata(0));




        public string HanderScannerText
        {
            get { return (string)GetValue(HanderScannerTextProperty); }
            set { SetValue(HanderScannerTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HanderScannerText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HanderScannerTextProperty =
            DependencyProperty.Register("HanderScannerText", typeof(string), typeof(DeviceStatusModel), new PropertyMetadata("自动扫码枪连接中..."));



        public int HanderScannerState
        {
            get { return (int)GetValue(HanderScannerStateProperty); }
            set { SetValue(HanderScannerStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HanderScannerState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HanderScannerStateProperty =
            DependencyProperty.Register("HanderScannerState", typeof(int), typeof(DeviceStatusModel), new PropertyMetadata(0));



        public string AutoScannerText
        {
            get { return (string)GetValue(AutoScannerTextProperty); }
            set { SetValue(AutoScannerTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AutoScannerText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoScannerTextProperty =
            DependencyProperty.Register("AutoScannerText", typeof(string), typeof(DeviceStatusModel), new PropertyMetadata("手动扫码枪连接中..."));



        public int AutoScannerState
        {
            get { return (int)GetValue(AutoScannerStateProperty); }
            set { SetValue(AutoScannerStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AutoScannerState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoScannerStateProperty =
            DependencyProperty.Register("AutoScannerState", typeof(int), typeof(DeviceStatusModel), new PropertyMetadata(0));



        public string DBText
        {
            get { return (string)GetValue(DBTextProperty); }
            set { SetValue(DBTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DBText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DBTextProperty =
            DependencyProperty.Register("DBText", typeof(string), typeof(DeviceStatusModel), new PropertyMetadata("数据库连接中"));



        public int DBState
        {
            get { return (int)GetValue(DBStateProperty); }
            set { SetValue(DBStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DBState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DBStateProperty =
            DependencyProperty.Register("DBState", typeof(int), typeof(DeviceStatusModel), new PropertyMetadata(0));




        public string PlcText
        {
            get { return (string)GetValue(PlcTextProperty); }
            set { SetValue(PlcTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlcText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlcTextProperty =
            DependencyProperty.Register("PlcText", typeof(string), typeof(DeviceStatusModel), new PropertyMetadata("控制系统连接中..."));




        public int PlcState
        {
            get { return (int)GetValue(PlcStateProperty); }
            set { SetValue(PlcStateProperty, value); }
        }



        // Using a DependencyProperty as the backing store for PlcState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlcStateProperty =
            DependencyProperty.Register("PlcState", typeof(int), typeof(DeviceStatusModel), new PropertyMetadata(0));
        public string ControlSerialStateText
        {
            get { return (string)GetValue(ControlSerialStateTextProperty); }
            set { SetValue(ControlSerialStateTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ControlSerialStateText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ControlSerialStateTextProperty =
            DependencyProperty.Register("ControlSerialStateText", typeof(string), typeof(DeviceStatusModel), new PropertyMetadata("控制串口连接中..."));




        public int ControlSerialState
        {
            get { return (int)GetValue(ControlSerialStateProperty); }
            set { SetValue(ControlSerialStateProperty, value); }
        }



        // Using a DependencyProperty as the backing store for ControlSerialState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ControlSerialStateProperty =
            DependencyProperty.Register("ControlSerialState", typeof(int), typeof(DeviceStatusModel), new PropertyMetadata(0));


        public string SerialStateText
        {
            get { return (string)GetValue(SerialStateTextProperty); }
            set { SetValue(SerialStateTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SerialStateText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SerialStateTextProperty =
            DependencyProperty.Register("SerialStateText", typeof(string), typeof(DeviceStatusModel), new PropertyMetadata("传感器串口连接中..."));




        public int SerialState
        {
            get { return (int)GetValue(SerialStateProperty); }
            set { SetValue(SerialStateProperty, value); }
        }



        // Using a DependencyProperty as the backing store for SerialState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SerialStateProperty =
            DependencyProperty.Register("SerialState", typeof(int), typeof(DeviceStatusModel), new PropertyMetadata(0));


        public BindingExpressionBase SetBinding(DependencyProperty dp, BindingBase binding)
        {
            return BindingOperations.SetBinding(this, dp, binding);
        }
    }
}
