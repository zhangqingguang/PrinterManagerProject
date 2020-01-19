namespace PrinterManagerProject.LoggerApp
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tb_logfile = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.combox_start = new System.Windows.Forms.ComboBox();
            this.combox_end = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.combox_start_1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.combox_start_2 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.combox_end_2 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.combox_end_1 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.combox_start_condition2 = new DevExpress.XtraEditors.RadioGroup();
            this.combox_start_condition1 = new DevExpress.XtraEditors.RadioGroup();
            this.combox_start_condition = new DevExpress.XtraEditors.RadioGroup();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.combox_end_condition2 = new DevExpress.XtraEditors.RadioGroup();
            this.combox_end_condition1 = new DevExpress.XtraEditors.RadioGroup();
            this.combox_end_condition = new DevExpress.XtraEditors.RadioGroup();
            this.button3 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.comboBox_single_action = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.intervalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startLogDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endLogDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.logDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.logDataBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.combox_single_condition1 = new DevExpress.XtraEditors.RadioGroup();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox_single_1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.combox_start_condition2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.combox_start_condition1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.combox_start_condition.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.combox_end_condition2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.combox_end_condition1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.combox_end_condition.Properties)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logDataBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logDataBindingSource1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.combox_single_condition1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tb_logfile
            // 
            this.tb_logfile.Location = new System.Drawing.Point(12, 12);
            this.tb_logfile.Name = "tb_logfile";
            this.tb_logfile.ReadOnly = true;
            this.tb_logfile.Size = new System.Drawing.Size(694, 21);
            this.tb_logfile.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(713, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "选择日志文件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "开始事件：";
            // 
            // combox_start
            // 
            this.combox_start.FormattingEnabled = true;
            this.combox_start.Location = new System.Drawing.Point(84, 24);
            this.combox_start.Name = "combox_start";
            this.combox_start.Size = new System.Drawing.Size(121, 20);
            this.combox_start.TabIndex = 9;
            this.combox_start.SelectedIndexChanged += new System.EventHandler(this.combox_start_SelectedIndexChanged);
            // 
            // combox_end
            // 
            this.combox_end.FormattingEnabled = true;
            this.combox_end.Location = new System.Drawing.Point(89, 20);
            this.combox_end.Name = "combox_end";
            this.combox_end.Size = new System.Drawing.Size(121, 20);
            this.combox_end.TabIndex = 11;
            this.combox_end.SelectedIndexChanged += new System.EventHandler(this.combox_end_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "结束事件：";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(830, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "开始查询";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.intervalDataGridViewTextBoxColumn,
            this.startTimeDataGridViewTextBoxColumn,
            this.endTimeDataGridViewTextBoxColumn,
            this.startLogDataGridViewTextBoxColumn,
            this.endLogDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.logDataBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(6, 112);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1310, 487);
            this.dataGridView1.TabIndex = 13;
            // 
            // combox_start_1
            // 
            this.combox_start_1.FormattingEnabled = true;
            this.combox_start_1.Location = new System.Drawing.Point(84, 50);
            this.combox_start_1.Name = "combox_start_1";
            this.combox_start_1.Size = new System.Drawing.Size(121, 20);
            this.combox_start_1.TabIndex = 15;
            this.combox_start_1.SelectedIndexChanged += new System.EventHandler(this.combox_start_1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "事件：";
            // 
            // combox_start_2
            // 
            this.combox_start_2.FormattingEnabled = true;
            this.combox_start_2.Location = new System.Drawing.Point(84, 76);
            this.combox_start_2.Name = "combox_start_2";
            this.combox_start_2.Size = new System.Drawing.Size(121, 20);
            this.combox_start_2.TabIndex = 17;
            this.combox_start_2.SelectedIndexChanged += new System.EventHandler(this.combox_start_2_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "事件：";
            // 
            // combox_end_2
            // 
            this.combox_end_2.FormattingEnabled = true;
            this.combox_end_2.Location = new System.Drawing.Point(89, 72);
            this.combox_end_2.Name = "combox_end_2";
            this.combox_end_2.Size = new System.Drawing.Size(121, 20);
            this.combox_end_2.TabIndex = 28;
            this.combox_end_2.SelectedIndexChanged += new System.EventHandler(this.combox_end_2_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 27;
            this.label5.Text = "事件：";
            // 
            // combox_end_1
            // 
            this.combox_end_1.FormattingEnabled = true;
            this.combox_end_1.Location = new System.Drawing.Point(89, 46);
            this.combox_end_1.Name = "combox_end_1";
            this.combox_end_1.Size = new System.Drawing.Size(121, 20);
            this.combox_end_1.TabIndex = 26;
            this.combox_end_1.SelectedIndexChanged += new System.EventHandler(this.combox_end_1_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 25;
            this.label6.Text = "事件：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.combox_start_condition2);
            this.groupBox1.Controls.Add(this.combox_start_condition1);
            this.groupBox1.Controls.Add(this.combox_start_condition);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.combox_start);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.combox_start_1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.combox_start_2);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(377, 100);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "开始事件";
            // 
            // combox_start_condition2
            // 
            this.combox_start_condition2.EditValue = 0;
            this.combox_start_condition2.Location = new System.Drawing.Point(210, 75);
            this.combox_start_condition2.Name = "combox_start_condition2";
            this.combox_start_condition2.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.combox_start_condition2.Properties.Appearance.Options.UseBackColor = true;
            this.combox_start_condition2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.combox_start_condition2.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.combox_start_condition2.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "前"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "后")});
            this.combox_start_condition2.Size = new System.Drawing.Size(161, 23);
            this.combox_start_condition2.TabIndex = 37;
            this.combox_start_condition2.SelectedIndexChanged += new System.EventHandler(this.combox_start_condition2_SelectedIndexChanged);
            // 
            // combox_start_condition1
            // 
            this.combox_start_condition1.EditValue = 0;
            this.combox_start_condition1.Location = new System.Drawing.Point(210, 49);
            this.combox_start_condition1.Name = "combox_start_condition1";
            this.combox_start_condition1.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.combox_start_condition1.Properties.Appearance.Options.UseBackColor = true;
            this.combox_start_condition1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.combox_start_condition1.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.combox_start_condition1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "前"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "后")});
            this.combox_start_condition1.Size = new System.Drawing.Size(161, 23);
            this.combox_start_condition1.TabIndex = 36;
            this.combox_start_condition1.SelectedIndexChanged += new System.EventHandler(this.combox_start_condition1_SelectedIndexChanged);
            // 
            // combox_start_condition
            // 
            this.combox_start_condition.EditValue = 1;
            this.combox_start_condition.Location = new System.Drawing.Point(210, 23);
            this.combox_start_condition.Name = "combox_start_condition";
            this.combox_start_condition.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.combox_start_condition.Properties.Appearance.Options.UseBackColor = true;
            this.combox_start_condition.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.combox_start_condition.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.combox_start_condition.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "第一个"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "最后一个")});
            this.combox_start_condition.Size = new System.Drawing.Size(161, 23);
            this.combox_start_condition.TabIndex = 35;
            this.combox_start_condition.SelectedIndexChanged += new System.EventHandler(this.combox_start_condition_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.combox_end_condition2);
            this.groupBox2.Controls.Add(this.combox_end);
            this.groupBox2.Controls.Add(this.combox_end_condition1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.combox_end_condition);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.combox_end_1);
            this.groupBox2.Controls.Add(this.combox_end_2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(417, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(377, 100);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "结束事件";
            // 
            // combox_end_condition2
            // 
            this.combox_end_condition2.EditValue = 0;
            this.combox_end_condition2.Location = new System.Drawing.Point(215, 72);
            this.combox_end_condition2.Name = "combox_end_condition2";
            this.combox_end_condition2.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.combox_end_condition2.Properties.Appearance.Options.UseBackColor = true;
            this.combox_end_condition2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.combox_end_condition2.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.combox_end_condition2.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "前"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "后")});
            this.combox_end_condition2.Size = new System.Drawing.Size(161, 23);
            this.combox_end_condition2.TabIndex = 40;
            this.combox_end_condition2.SelectedIndexChanged += new System.EventHandler(this.combox_end_condition2_SelectedIndexChanged);
            // 
            // combox_end_condition1
            // 
            this.combox_end_condition1.EditValue = 0;
            this.combox_end_condition1.Location = new System.Drawing.Point(215, 46);
            this.combox_end_condition1.Name = "combox_end_condition1";
            this.combox_end_condition1.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.combox_end_condition1.Properties.Appearance.Options.UseBackColor = true;
            this.combox_end_condition1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.combox_end_condition1.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.combox_end_condition1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "前"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "后")});
            this.combox_end_condition1.Size = new System.Drawing.Size(161, 23);
            this.combox_end_condition1.TabIndex = 39;
            this.combox_end_condition1.SelectedIndexChanged += new System.EventHandler(this.combox_end_condition1_SelectedIndexChanged);
            // 
            // combox_end_condition
            // 
            this.combox_end_condition.EditValue = 0;
            this.combox_end_condition.Location = new System.Drawing.Point(215, 20);
            this.combox_end_condition.Name = "combox_end_condition";
            this.combox_end_condition.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.combox_end_condition.Properties.Appearance.Options.UseBackColor = true;
            this.combox_end_condition.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.combox_end_condition.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.combox_end_condition.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "第一个"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "最后一个")});
            this.combox_end_condition.Size = new System.Drawing.Size(161, 23);
            this.combox_end_condition.TabIndex = 38;
            this.combox_end_condition.SelectedIndexChanged += new System.EventHandler(this.combox_end_condition_SelectedIndexChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(830, 49);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 35;
            this.button3.Text = "重置条件";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 41);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1330, 631);
            this.tabControl1.TabIndex = 36;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1322, 605);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "两个信号时间间隔";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Controls.Add(this.button4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1322, 605);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "相同信号时间间隔";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // comboBox_single_action
            // 
            this.comboBox_single_action.FormattingEnabled = true;
            this.comboBox_single_action.Location = new System.Drawing.Point(83, 23);
            this.comboBox_single_action.Name = "comboBox_single_action";
            this.comboBox_single_action.Size = new System.Drawing.Size(121, 20);
            this.comboBox_single_action.TabIndex = 10;
            this.comboBox_single_action.SelectedIndexChanged += new System.EventHandler(this.comboBox_single_action_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "查询事件：";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(419, 6);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 12;
            this.button4.Text = "查询";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn4});
            this.dataGridView2.DataSource = this.logDataBindingSource1;
            this.dataGridView2.Location = new System.Drawing.Point(6, 98);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(1310, 448);
            this.dataGridView2.TabIndex = 14;
            // 
            // intervalDataGridViewTextBoxColumn
            // 
            this.intervalDataGridViewTextBoxColumn.DataPropertyName = "Interval";
            this.intervalDataGridViewTextBoxColumn.HeaderText = "间隔时间";
            this.intervalDataGridViewTextBoxColumn.Name = "intervalDataGridViewTextBoxColumn";
            // 
            // startTimeDataGridViewTextBoxColumn
            // 
            this.startTimeDataGridViewTextBoxColumn.DataPropertyName = "StartTime";
            this.startTimeDataGridViewTextBoxColumn.HeaderText = "开始时间";
            this.startTimeDataGridViewTextBoxColumn.Name = "startTimeDataGridViewTextBoxColumn";
            this.startTimeDataGridViewTextBoxColumn.Width = 200;
            // 
            // endTimeDataGridViewTextBoxColumn
            // 
            this.endTimeDataGridViewTextBoxColumn.DataPropertyName = "EndTime";
            this.endTimeDataGridViewTextBoxColumn.HeaderText = "结束时间";
            this.endTimeDataGridViewTextBoxColumn.Name = "endTimeDataGridViewTextBoxColumn";
            this.endTimeDataGridViewTextBoxColumn.Width = 200;
            // 
            // startLogDataGridViewTextBoxColumn
            // 
            this.startLogDataGridViewTextBoxColumn.DataPropertyName = "StartLog";
            this.startLogDataGridViewTextBoxColumn.HeaderText = "开始日志";
            this.startLogDataGridViewTextBoxColumn.Name = "startLogDataGridViewTextBoxColumn";
            this.startLogDataGridViewTextBoxColumn.Width = 200;
            // 
            // endLogDataGridViewTextBoxColumn
            // 
            this.endLogDataGridViewTextBoxColumn.DataPropertyName = "EndLog";
            this.endLogDataGridViewTextBoxColumn.HeaderText = "结束日志";
            this.endLogDataGridViewTextBoxColumn.Name = "endLogDataGridViewTextBoxColumn";
            this.endLogDataGridViewTextBoxColumn.Width = 200;
            // 
            // logDataBindingSource
            // 
            this.logDataBindingSource.DataSource = typeof(PrinterManagerProject.LoggerApp.LogData);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Interval";
            this.dataGridViewTextBoxColumn1.HeaderText = "间隔时间";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "StartTime";
            this.dataGridViewTextBoxColumn2.HeaderText = "开始时间";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "StartLog";
            this.dataGridViewTextBoxColumn4.HeaderText = "开始日志";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 200;
            // 
            // logDataBindingSource1
            // 
            this.logDataBindingSource1.DataSource = typeof(PrinterManagerProject.LoggerApp.LogData);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.combox_single_condition1);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.comboBox_single_1);
            this.groupBox3.Controls.Add(this.comboBox_single_action);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(383, 86);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "查询条件";
            // 
            // combox_single_condition1
            // 
            this.combox_single_condition1.EditValue = 0;
            this.combox_single_condition1.Location = new System.Drawing.Point(209, 49);
            this.combox_single_condition1.Name = "combox_single_condition1";
            this.combox_single_condition1.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.combox_single_condition1.Properties.Appearance.Options.UseBackColor = true;
            this.combox_single_condition1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.combox_single_condition1.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.combox_single_condition1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "前"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "后")});
            this.combox_single_condition1.Size = new System.Drawing.Size(161, 23);
            this.combox_single_condition1.TabIndex = 39;
            this.combox_single_condition1.SelectedIndexChanged += new System.EventHandler(this.combox_single_condition1_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 37;
            this.label8.Text = "事件：";
            // 
            // comboBox_single_1
            // 
            this.comboBox_single_1.FormattingEnabled = true;
            this.comboBox_single_1.Location = new System.Drawing.Point(83, 50);
            this.comboBox_single_1.Name = "comboBox_single_1";
            this.comboBox_single_1.Size = new System.Drawing.Size(121, 20);
            this.comboBox_single_1.TabIndex = 38;
            this.comboBox_single_1.SelectedIndexChanged += new System.EventHandler(this.comboBox_single_1_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 684);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tb_logfile);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.combox_start_condition2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.combox_start_condition1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.combox_start_condition.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.combox_end_condition2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.combox_end_condition1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.combox_end_condition.Properties)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logDataBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logDataBindingSource1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.combox_single_condition1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_logfile;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox combox_start;
        private System.Windows.Forms.ComboBox combox_end;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource logDataBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn intervalDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn startTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn endTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn startLogDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn endLogDataGridViewTextBoxColumn;
        private System.Windows.Forms.ComboBox combox_start_1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox combox_start_2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox combox_end_2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox combox_end_1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.RadioGroup combox_start_condition2;
        private DevExpress.XtraEditors.RadioGroup combox_start_condition1;
        private DevExpress.XtraEditors.RadioGroup combox_start_condition;
        private DevExpress.XtraEditors.RadioGroup combox_end_condition2;
        private DevExpress.XtraEditors.RadioGroup combox_end_condition1;
        private DevExpress.XtraEditors.RadioGroup combox_end_condition;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox_single_action;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.BindingSource logDataBindingSource1;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraEditors.RadioGroup combox_single_condition1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBox_single_1;
    }
}

