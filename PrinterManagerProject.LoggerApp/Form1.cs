using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PrinterManagerProject.LoggerApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            bindDll();
        }
        #region 多个信号
        bool hasFindStart = false;
        bool hasFindEnd = false;

        DateTime? startTime = null;
        string startLog = "";
        string startTimeStr = "";
        DateTime? endTime = null;
        List<LogData> list = new List<LogData>();
        ConditionConfig startAction = new ConditionConfig()
        {
            ActionName = "",
            First = false,
            Condiiton1ActionName = "",
            Condiiton1Before = true,
            Condiiton2ActionName = "",
            Condiiton2Before = true
        };
        ConditionConfig endAction = new ConditionConfig()
        {
            ActionName = "",
            First = true,
            Condiiton1ActionName = "",
            Condiiton1Before = true,
            Condiiton2ActionName = "",
            Condiiton2Before = true
        };
        ConditionConfig singleAction = new ConditionConfig()
        {
            ActionName = "",
            First = false,
            Condiiton1ActionName = "",
            Condiiton1Before = true,
            Condiiton2ActionName = "",
            Condiiton2Before = true
        };
        private List<ComboxData> getActionList()
        {
            var comboxData = new List<ComboxData>();
            comboxData.Add(new ComboxData() { Value = "", Text = "请选择" });
            comboxData.Add(new ComboxData() { Value = "82： PLC接收82信号，开始打印", Text = "82： PLC接收82信号，开始打印" });
            comboxData.Add(new ComboxData() { Value = "83： PLC接收83信号，记录扫码信息", Text = "83： PLC接收83信号，记录扫码信息" });
            comboxData.Add(new ComboxData() { Value = "PLC接收有效84信号", Text = "84： PLC接收有效84信号" });
            comboxData.Add(new ComboxData() { Value = "正在发送CCD成功或失败：%01#WDD00900009003459", Text = "发送CCD2成功" });
            comboxData.Add(new ComboxData() { Value = "收到CCD2继续命令返回信号", Text = "CCD2命令返回" });
            return comboxData;
        }
        private void bindDll()
        {
            bindCombox(this.combox_start);
            bindCombox(this.combox_start_1);
            bindCombox(this.combox_start_2);

            bindCombox(this.combox_end);
            bindCombox(this.combox_end_1);
            bindCombox(this.combox_end_2);
            
            bindCombox(this.comboBox_single_action);
            bindCombox(this.comboBox_single_1);
        }
        private void bindCombox(System.Windows.Forms.ComboBox combox)
        {
            combox.DataSource = getActionList();
            combox.ValueMember = "Value";
            combox.DisplayMember = "Text";
        }


        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();     //显示选择文件对话框
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.tb_logfile.Text = openFileDialog1.FileName;          //显示文件路径
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            readLog();
        }
        private void readLog()
        {
            //this.combox_start_condition.SelectedIndex
            list.Clear();
            var allLines = File.ReadAllLines(this.tb_logfile.Text, Encoding.Default).ToList();
            var startEvent = combox_start.SelectedValue == null ? combox_start.Text : combox_start.SelectedValue.ToString();
            var endEvent = combox_end.SelectedValue == null ? combox_end.Text : combox_end.SelectedValue.ToString();
            foreach (var item in allLines)
            {
                if (item.Contains(startEvent))
                {
                    if (startAction.First)
                    {
                        if (hasFindStart == false)
                        {
                            hasFindStart = true;
                            getStartInfo(item);
                        }
                    }
                    else
                    {
                        getStartInfo(item);
                    }
                    continue;
                }
                if (item.Contains(endEvent))
                {
                    if (startTime.HasValue)
                    {
                        getEndInfo(item);
                    }

                    continue;
                }
            }
            BindSource(list);
        }

        private void BindSource(List<LogData> sources)
        {
            dataGridView1.DataSource = new BindingCollection<LogData>(sources);
        }

        #region 处理开始和结束事件
        private void getStartInfo(string log)
        {
            startTime = LogHelper.getTime(log);
            startTimeStr = LogHelper.getTimeStr(log);
            startLog = LogHelper.getLogInfo(log);
            endTime = null;
        }
        private void getEndInfo(string log)
        {
            endTime = LogHelper.getTime(log);
            list.Add(new LogData()
            {
                Interval = (endTime - startTime).Value.TotalMilliseconds,
                StartTime = startTimeStr,
                EndTime = LogHelper.getTimeStr(log),
                StartLog = startLog,
                EndLog = LogHelper.getLogInfo(log)
            });
            startTime = null;
            endTime = null;
            hasFindStart = false;
        }
        #endregion


        #region 事件下拉框切换事件
        private void combox_start_SelectedIndexChanged(object sender, EventArgs e)
        {
            startAction.ActionName = combox_start.SelectedValue == null ? combox_start.SelectedText : combox_start.SelectedValue.ToString();
        }

        private void combox_start_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            startAction.Condiiton1ActionName = combox_start_1.SelectedValue == null ? combox_start_1.SelectedText : combox_start_1.SelectedValue.ToString();
        }

        private void combox_start_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            startAction.Condiiton2ActionName = combox_start_2.SelectedValue == null ? combox_start_2.SelectedText : combox_start_2.SelectedValue.ToString();
        }

        private void combox_end_SelectedIndexChanged(object sender, EventArgs e)
        {
            endAction.ActionName = combox_end.SelectedValue == null ? combox_end.SelectedText : combox_end.SelectedValue.ToString();
        }

        private void combox_end_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            endAction.Condiiton1ActionName = combox_end_1.SelectedValue == null ? combox_end_1.SelectedText : combox_end_1.SelectedValue.ToString();
        }

        private void combox_end_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            endAction.Condiiton2ActionName = combox_end_2.SelectedValue == null ? combox_end_2.SelectedText : combox_end_2.SelectedValue.ToString();
        }

        private void comboBox_single_action_SelectedIndexChanged(object sender, EventArgs e)
        {
            singleAction.ActionName = comboBox_single_action.SelectedValue == null ? comboBox_single_action.SelectedText : comboBox_single_action.SelectedValue.ToString();
        }

        private void comboBox_single_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            singleAction.Condiiton1ActionName = comboBox_single_1.SelectedValue == null ? comboBox_single_1.SelectedText : comboBox_single_1.SelectedValue.ToString();
        }
        #endregion

        #region 事件RadioGroup切换事件
        private void combox_start_condition_SelectedIndexChanged(object sender, EventArgs e)
        {
            startAction.First = combox_start_condition.SelectedIndex == 0;
        }

        private void combox_start_condition1_SelectedIndexChanged(object sender, EventArgs e)
        {
            startAction.Condiiton1Before = combox_start_condition1.SelectedIndex == 0;

        }

        private void combox_start_condition2_SelectedIndexChanged(object sender, EventArgs e)
        {
            startAction.Condiiton1Before = combox_start_condition2.SelectedIndex == 0;
        }

        private void combox_end_condition_SelectedIndexChanged(object sender, EventArgs e)
        {
            endAction.First = combox_end_condition.SelectedIndex == 0;
        }

        private void combox_end_condition1_SelectedIndexChanged(object sender, EventArgs e)
        {
            endAction.Condiiton1Before = combox_end_condition1.SelectedIndex == 0;
        }

        private void combox_end_condition2_SelectedIndexChanged(object sender, EventArgs e)
        {
            endAction.Condiiton2Before = combox_end_condition2.SelectedIndex == 0;
        }

        private void combox_single_condition1_SelectedIndexChanged(object sender, EventArgs e)
        {
            singleAction.Condiiton1Before = combox_single_condition1.SelectedIndex == 0;
        }
        #endregion

        #region 重置条件
        private void button3_Click(object sender, EventArgs e)
        {
            combox_start.SelectedIndex = 0;
            combox_start_1.SelectedIndex = 0;
            combox_start_2.SelectedIndex = 0;
            combox_end.SelectedIndex = 0;
            combox_end_1.SelectedIndex = 0;
            combox_end_2.SelectedIndex = 0;

            combox_start_condition.SelectedIndex = 0;
            combox_start_condition1.SelectedIndex = 0;
            combox_start_condition2.SelectedIndex = 0;
            combox_end_condition.SelectedIndex = 0;
            combox_end_condition1.SelectedIndex = 0;
            combox_end_condition2.SelectedIndex = 0;
        }
        #endregion

        #endregion

        DateTime? singleTime;
        bool hasFindSingleAction = false;
        string currentSingleLog = null;
        private void button4_Click(object sender, EventArgs e)
        {
            readLog2();
        }
        private void readLog2()
        {
            //this.combox_start_condition.SelectedIndex
            singleList.Clear();
            var allLines = File.ReadAllLines(this.tb_logfile.Text, Encoding.Default).ToList();
            var startEvent = comboBox_single_action.SelectedValue == null ? comboBox_single_action.Text : comboBox_single_action.SelectedValue.ToString();
            foreach (var item in allLines)
            {
                if(string.IsNullOrEmpty(singleAction.Condiiton1ActionName) == false)
                {
                    if (item.Contains(singleAction.Condiiton1ActionName))
                    {
                        if(singleAction.Condiiton1Before == false)
                        {
                            // 最后一个
                            if (currentSingleLog != null)
                            {
                                addSingleLog(currentSingleLog);
                                currentSingleLog = null;
                            }
                        }
                        hasFindSingleAction = true;
                    }
                    if (item.Contains(singleAction.ActionName))
                    {
                        if(hasFindSingleAction == true)
                        {
                            addSingleLog(item);
                            hasFindSingleAction = false;
                        }
                    }
                    continue;
                }
                if (item.Contains(startEvent))
                {
                    getSingleInfo(item);
                    continue;
                }
            }
            BindSource2(singleList);
        }
        private void addSingleLog(string log)
        {
            if (singleList.Any())
            {
                var lastSingleData = singleList.LastOrDefault();

                singleList.Add(new LogData()
                {
                    Interval = (LogHelper.getTime(log) - LogHelper.str2Time(lastSingleData.EndTime)).TotalMilliseconds,
                    StartLog = lastSingleData.EndLog,
                    StartTime = lastSingleData.EndTime,
                    EndLog = LogHelper.getLogInfo(log),
                    EndTime = LogHelper.getTimeStr(log)
                });
            }
            else
            {
                singleList.Add(new LogData()
                {
                    Interval = 0,
                    EndLog = LogHelper.getLogInfo(log),
                    EndTime = LogHelper.getTimeStr(log)
                });
            }
        }
        private void BindSource2(List<LogData> sources)
        {
            dataGridView2.DataSource = new BindingCollection<LogData>(sources);
        }
        List<LogData> singleList = new List<LogData>();
        private void getSingleInfo(string log)
        {
            singleList.Add(new LogData
            {
                Interval = singleTime.HasValue == false ? 0 : (LogHelper.getTime(log) - singleTime).Value.TotalMilliseconds,
                StartTime = LogHelper.getTimeStr(log),
                StartLog = LogHelper.getLogInfo(log)
            });
            singleTime = LogHelper.getTime(log);
        }
    }
    public class LogData
    {
        public double Interval { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string StartLog { get; set; }
        public string EndLog { get; set; }
    }
    public class ComboxData
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }
}
