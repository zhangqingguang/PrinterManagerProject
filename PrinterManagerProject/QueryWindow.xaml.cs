using PrinterManagerProject.EF;
using PrinterManagerProject.EF.Bll;
using PrinterManagerProject.EF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace PrinterManagerProject
{
    /// <summary>
    /// QueryWindow.xaml 的交互逻辑
    /// </summary>
    public partial class QueryWindow : BaseWindow
    {
        ObservableCollection<tOrder> list = new ObservableCollection<tOrder>();
        public QueryWindow()
        {
            InitializeComponent();

            base.Loaded += QueryWindow_Loaded;
        }

        private void QueryWindow_Loaded(object sender, RoutedEventArgs e)
        {
            BindBatch();
            BindDate();
            SearchData();
        }


        #region 初始化
        private void BindDate()
        {
            this.dp_useDate.SelectedDate = DateTime.Now;
        }
        private void BindBatch()
        {
            var batchs = new BatchManager().GetAll();
            batchs.Insert(0, new tBatch() { batch_name = "全部", batch = "" });
            this.cb_batch.ItemsSource = batchs;
            this.cb_batch.DisplayMemberPath = "batch_name";
            this.cb_batch.SelectedValuePath = "batch";
            this.cb_batch.SelectedIndex = 0;

        } 
        #endregion

        public void GetData()
        {
            list = new OrderManager().GetAllOrderByDateTime(this.dp_useDate.SelectedDate.Value, this.cb_batch.SelectedValue?.ToString());
        }

        private void BindData()
        {
            this.dgv_list.ItemsSource = list;


            // 绑定科室下拉框
            BindDepartment();
            // 绑定主药下拉框
            BindMainDrug();
        }
        private void SearchData()
        {
            if (dp_useDate.SelectedDate.HasValue == false)
            {
                MessageBox.Show("请选择用药日期");
            }
            GetData();
            BindData();
        }

        /// <summary>
        /// 绑定科室下拉框
        /// </summary>
        /// <param name="dataSource"></param>
        private void BindDepartment()
        {
            // 绑定科室
            var deptList = list.GroupBy(m => new { m.departmengt_name, m.department_code }).Select(a => new { dept_name = a.Key.departmengt_name, dept_code = a.Key.department_code }).ToList();
            deptList.Insert(0, new { dept_name = "全部", dept_code = "" });

            this.cb_dept.DisplayMemberPath = "dept_name";
            this.cb_dept.SelectedValuePath = "dept_code";
            this.cb_dept.ItemsSource = deptList;
            this.cb_dept.SelectedIndex = 0;
        }
        /// <summary>
        /// 绑定主药下拉框
        /// </summary>
        /// <param name="dataSource"></param>
        private void BindMainDrug()
        {
            // 绑定主药
            var drugIds = list.Select(s => s.ydrug_id).Distinct().ToList();
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

            this.cb_drug.DisplayMemberPath = "ydrug_name";
            this.cb_drug.SelectedValuePath = "ydrug_id";
            this.cb_drug.ItemsSource = drugList;
            this.cb_drug.SelectedIndex = 0;
        }


        #region 事件

        private void BaseWindow_Closing(object sender, CancelEventArgs e)
        {
            if (needCloseWindowConfirm)
            {
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
        #endregion

        private void Cb_dept_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterData();

        }

        private void Cb_drug_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterData();

        }

        private void Cb_Printer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterData();

        }

        private void Cb_PrintStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterData();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterData();

        }

        private void FilterData()
        {
            var query = list.AsQueryable();
            if (cb_dept.SelectedIndex != 0 && cb_dept.SelectedValue!=null)
            {
                query = query.Where(s => s.department_code == cb_dept.SelectedValue.ToString());
            }
            if (cb_drug.SelectedIndex != 0 && cb_drug.SelectedValue != null)
            {
                query = query.Where(s => s.ydrug_name == cb_drug.SelectedValue.ToString());
            }
            if (cb_Printer.SelectedIndex != 0 && cb_Printer.SelectedValue != null)
            {
                query = query.Where(s => s.PrintUserId == Convert.ToUInt32(cb_Printer.SelectedValue));
            }
            if (cb_PrintStatus.SelectedIndex != 0 && cb_PrintStatus.SelectedValue != null)
            {
                if(cb_PrintStatus.SelectedItem is ComboBoxItem status)
                {
                    if (status.DataContext.ToString() == PrintStatusEnum.NotPrint.GetHashCode().ToString())
                    {
                        query = query.Where(s => s.printing_status.HasValue == false || s.printing_status == PrintStatusEnum.NotPrint);
                    }
                    else if (status.DataContext.ToString() == PrintStatusEnum.Success.GetHashCode().ToString())
                    { 
                        query = query.Where(s => s.printing_status.HasValue == true && s.printing_status == PrintStatusEnum.Success);
                    }
                }
            }
            if (tb_groupNum!=null && string.IsNullOrEmpty(tb_groupNum.Text) == false)
            {
                query = query.Where(s => s.group_num == tb_groupNum.Text);
            }
            if (tb_key != null && string.IsNullOrEmpty(tb_key.Text) == false)
            {
                query = query.Where(s => s.patient_name.Contains(tb_key.Text) || s.bed_number.Contains(tb_key.Text));
            }
            var filterList = query.ToList();
            var datasource = new ObservableCollection<tOrder>();
            foreach (var item in filterList)
            {
                datasource.Add(item);
            }
            this.dgv_list.ItemsSource = null;
            this.dgv_list.ItemsSource = datasource;
        }

        private void Dp_useDate_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            SearchData();
        }

        private void Cb_batch_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            SearchData();
        }

        private void Tb_key_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterData();
        }

        private void Tb_groupNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FilterData();
        }
    }
}
