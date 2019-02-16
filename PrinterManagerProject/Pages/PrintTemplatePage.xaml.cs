using Newtonsoft.Json;
using PrinterManagerProject.Models;
using PrinterManagerProject.Tools;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrinterManagerProject.Pages
{
    /// <summary>
    /// PrintTemplatePage.xaml 的交互逻辑
    /// </summary>
    public partial class PrintTemplatePage : Page
    {

        public PrintTemplatePage()
        {
            InitializeComponent();

            foreach (var item in canvasMain.Children)
            {
                Border element = item as Border;
                element.MouseDown += new MouseButtonEventHandler(Border_MouseDown);
            }

            canvasMain.MouseDown += new MouseButtonEventHandler(Canvas_MouseDown);
            canvasMain.MouseUp += new MouseButtonEventHandler(Canvas_MouseUp);
            canvasMain.MouseMove += new MouseEventHandler(Canvas_MouseMove);

            //WaitingBox.Show(() => {
            LoadConfig();
            //});
        }

        #region 事件处理

        private bool isDragging;
        private Point oldPoint;
        private Border draggableElement;

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            draggableElement = sender as Border;
            if (draggableElement != null)
            {
                draggableElement.BorderThickness = new Thickness(1);
                draggableElement.BorderBrush = Brushes.DodgerBlue;

                SetBorderThickness(draggableElement);
                SetLabelColor(draggableElement);
            }
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            oldPoint = e.GetPosition(null);
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && draggableElement != null)
            {
                double elementX = (double)draggableElement.GetValue(Canvas.LeftProperty);
                double elementY = (double)draggableElement.GetValue(Canvas.TopProperty);
                double xPos = e.GetPosition(null).X - oldPoint.X + elementX;
                double yPos = e.GetPosition(null).Y - oldPoint.Y + elementY;

                if (elementX < 0)
                    xPos = 0;
                if (elementX > canvasMain.Width - draggableElement.ActualWidth)
                    xPos = canvasMain.Width - draggableElement.ActualWidth;
                if (elementY < 0)
                    yPos = 0;
                if (elementY > canvasMain.Height - draggableElement.ActualHeight)
                    yPos = canvasMain.Height - draggableElement.ActualHeight;

                draggableElement.SetValue(Canvas.LeftProperty, xPos);
                draggableElement.SetValue(Canvas.TopProperty, yPos);

                oldPoint = e.GetPosition(null);
            }
        }

        /// <summary>
        /// 点击表单提示文字
        /// 显示对应预览对象边框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SolidColorBrush color = Brushes.Black;
            labelArea.Foreground = color;
            labelDate.Foreground = color;
            labelPageNum.Foreground = color;
            labelSplit.Foreground = color;
            labelBed.Foreground = color;
            labelPatient.Foreground = color;
            labelGender.Foreground = color;
            labelBatchNumber.Foreground = color;
            labelSerialNumber.Foreground = color;
            labelSplit2.Foreground = color;
            labelDrugsTitle.Foreground = color;
            labelDrugsContent.Foreground = color;
            labelUseTitle.Foreground = color;
            labelUseValue.Foreground = color;
            labelDoctor.Foreground = color;
            labelRemark.Foreground = color;
            labelSpeed.Foreground = color;
            labelSplit3.Foreground = color;
            labelExamine.Foreground = color;
            labelReview.Foreground = color;
            labelSort.Foreground = color;
            labelDispensing.Foreground = color;
            labelDispensingDate.Foreground = color;
            labelBarCodeWidth.Foreground = color;
            labelBarCodeHeight.Foreground = color;
            labelBarCodeX.Foreground = color;
            labelBarCodeY.Foreground = color;
            labelDoctorAdvice.Foreground = color;

            color = Brushes.DodgerBlue;
            Border border = null;
            Control label = sender as Control;
            if (label != null)
            {
                switch (label.Name)
                {
                    case "labelArea":
                        border = borderArea;
                        break;
                    case "labelDate":
                        border = borderDate;
                        break;
                    case "labelPageNum":
                        border = borderPageNum;
                        break;
                    case "labelSplit":
                        border = borderSplit;
                        break;
                    case "labelDoctorAdvice":
                        border = borderDoctorAdvice;
                        break;
                    case "labelBed":
                        border = borderBed;
                        break;
                    case "labelPatient":
                        border = borderPatient;
                        break;
                    case "labelGender":
                        border = borderGender;
                        break;
                    case "labelBatchNumber":
                        border = borderBatchNumber;
                        break;
                    case "labelSerialNumber":
                        border = borderSerialNumber;
                        break;
                    case "labelSplit2":
                        border = borderSplit2;
                        break;
                    case "labelDrugsTitle":
                        border = borderDrugsTitle;
                        break;
                    case "labelDrugsContent":
                        border = borderDrugsContent;
                        break;
                    case "labelUseTitle":
                        border = borderUseTitle;
                        break;
                    case "labelUseValue":
                        border = borderUseValue;
                        break;
                    case "labelDoctor":
                        border = borderDoctor;
                        break;
                    case "labelRemark":
                        border = borderRemark;
                        break;
                    case "labelSpeed":
                        border = borderSpeed;
                        break;
                    case "labelSplit3":
                        border = borderSplit3;
                        break;
                    case "labelExamine":
                        border = borderExamine;
                        break;
                    case "labelReview":
                        border = borderReview;
                        break;
                    case "labelSort":
                        border = borderSort;
                        break;
                    case "labelDispensing":
                        border = borderDispensing;
                        break;
                    case "labelDispensingDate":
                        border = borderDispensingDate;
                        break;
                    case "labelBarCodeWidth":
                    case "labelBarCodeHeight":
                    case "labelBarCodeX":
                    case "labelBarCodeY":
                        border = borderBarCode;
                        break;
                }

                label.Foreground = color;
            }

            SetBorderThickness(border);
        }

        #endregion

        #region 私有帮助方法

        /// <summary>
        /// 设置对象边框
        /// </summary>
        /// <param name="border"></param>
        private void SetBorderThickness(Border border)
        {
            foreach (var item in canvasMain.Children)
            {
                Border element = item as Border;

                if (element.Equals(border))
                {
                    border.BorderThickness = new Thickness(1);
                    border.BorderBrush = Brushes.DodgerBlue;
                }
                else
                {
                    element.BorderThickness = new Thickness(0);
                }
            }
        }

        /// <summary>
        /// 设置Lable颜色
        /// </summary>
        /// <param name="border"></param>
        private void SetLabelColor(Border border)
        {
            SolidColorBrush color = Brushes.Black;
            labelArea.Foreground = color;
            labelDate.Foreground = color;
            labelPageNum.Foreground = color;
            labelSplit.Foreground = color;
            labelBed.Foreground = color;
            labelPatient.Foreground = color;
            labelGender.Foreground = color;
            labelBatchNumber.Foreground = color;
            labelSerialNumber.Foreground = color;
            labelSplit2.Foreground = color;
            labelDrugsTitle.Foreground = color;
            labelDrugsContent.Foreground = color;
            labelUseTitle.Foreground = color;
            labelUseValue.Foreground = color;
            labelDoctor.Foreground = color;
            labelRemark.Foreground = color;
            labelSpeed.Foreground = color;
            labelSplit3.Foreground = color;
            labelExamine.Foreground = color;
            labelReview.Foreground = color;
            labelSort.Foreground = color;
            labelDispensing.Foreground = color;
            labelDispensingDate.Foreground = color;
            labelBarCodeWidth.Foreground = color;
            labelBarCodeHeight.Foreground = color;
            labelBarCodeX.Foreground = color;
            labelBarCodeY.Foreground = color;
            labelDoctorAdvice.Foreground = color;

            color = Brushes.DodgerBlue;
            if (border != null)
            {
                switch (border.Name)
                {
                    case "borderArea":
                        labelArea.Foreground = color;
                        break;
                    case "borderDate":
                        labelDate.Foreground = color;
                        break;
                    case "borderPageNum":
                        labelPageNum.Foreground = color;
                        break;
                    case "borderSplit":
                        labelSplit.Foreground = color;
                        break;
                    case "borderDoctorAdvice":
                        labelDoctorAdvice.Foreground = color;
                        break;
                    case "borderBed":
                        labelBed.Foreground = color;
                        break;
                    case "borderPatient":
                        labelPatient.Foreground = color;
                        break;
                    case "borderGender":
                        labelGender.Foreground = color;
                        break;
                    case "borderBatchNumber":
                        labelBatchNumber.Foreground = color;
                        break;
                    case "borderSerialNumber":
                        labelSerialNumber.Foreground = color;
                        break;
                    case "borderSplit2":
                        labelSplit2.Foreground = color;
                        break;
                    case "borderDrugsTitle":
                        labelDrugsTitle.Foreground = color;
                        break;
                    case "borderDrugsContent":
                        labelDrugsContent.Foreground = color;
                        break;
                    case "borderUseTitle":
                        labelUseTitle.Foreground = color;
                        break;
                    case "borderUseValue":
                        labelUseValue.Foreground = color;
                        break;
                    case "borderDoctor":
                        labelDoctor.Foreground = color;
                        break;
                    case "borderRemark":
                        labelRemark.Foreground = color;
                        break;
                    case "borderSpeed":
                        labelSpeed.Foreground = color;
                        break;
                    case "borderSplit3":
                        labelSplit3.Foreground = color;
                        break;
                    case "borderExamine":
                        labelExamine.Foreground = color;
                        break;
                    case "borderReview":
                        labelReview.Foreground = color;
                        break;
                    case "borderSort":
                        labelSort.Foreground = color;
                        break;
                    case "borderDispensing":
                        labelDispensing.Foreground = color;
                        break;
                    case "borderDispensingDate":
                        labelDispensingDate.Foreground = color;
                        break;
                    case "borderBarCode":
                        labelBarCodeWidth.Foreground = color;
                        labelBarCodeHeight.Foreground = color;
                        labelBarCodeX.Foreground = color;
                        labelBarCodeY.Foreground = color;
                        break;
                }
            }
        }

        #endregion

        #region 设置配置信息

        private void LoadConfig()
        {
            PrintTemplateModel model = new PrintTemplateHelper().GetConfig();
            if (model == null)
            {
                MessageBox.Show("获取参数失败！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            /// <summary>
            /// 页面宽度
            /// </summary>
            txtPageWidth.Text = model.PageWidth + "";
            /// <summary>
            /// 页面高度
            /// </summary>
            txtPageHeight.Text = model.PageHeight + "";

            /// <summary>
            /// 病区文字
            /// </summary>
            sliderAreaFontSize.Value = model.AreaFontSize;
            /// <summary>
            /// 病区文字X坐标
            /// </summary>
            txtAreaFontX.Text = model.AreaFontX + "";
            /// <summary>
            /// 病区文字Y坐标
            /// </summary>
            txtAreaFontY.Text = model.AreaFontY + "";

            /// <summary>
            /// 日期文字
            /// </summary>
            sliderDateFontSize.Value = model.DateFontSize;
            /// <summary>
            /// 日期文字X坐标
            /// </summary>
            txtDateFontX.Text = model.DateFontX + "";
            /// <summary>
            /// 日期文字Y坐标
            /// </summary>
            txtDateFontY.Text = model.DateFontY + "";

            /// <summary>
            /// 页码文字
            /// </summary>
            sliderPageNumFontSize.Value = model.PageNumFontSize;
            /// <summary>
            /// 页码文字X坐标
            /// </summary>
            txtPageNumFontX.Text = model.PageNumFontX + "";
            /// <summary>
            /// 页码文字Y坐标
            /// </summary>
            txtPageNumFontY.Text = model.PageNumFontY + "";

            /// <summary>
            /// 第一条分割线宽度
            /// </summary>
            sliderSplitWidth.Value = model.SplitWidth;
            /// <summary>
            /// 第一条分割线X坐标
            /// </summary>
            txtSplitX.Text = model.SplitX + "";
            /// <summary>
            /// 第一条分割线Y坐标
            /// </summary>
            txtSplitY.Text = model.SplitY + "";

            /// <summary>
            /// 医嘱号文字
            /// </summary>
            sliderDoctorAdviceFontSize.Value = model.DoctorAdviceFontSize;
            /// <summary>
            /// 医嘱号文字X坐标
            /// </summary>
            txtDoctorAdviceFontX.Text = model.DoctorAdviceFontX + "";
            /// <summary>
            /// 医嘱号文字Y坐标
            /// </summary>
            txtDoctorAdviceFontY.Text = model.DoctorAdviceFontY + "";

            /// <summary>
            /// 床位文字
            /// </summary>
            sliderBedFontSize.Value = model.BedFontSize;
            /// <summary>
            /// 床位文字X坐标
            /// </summary>
            txtBedFontX.Text = model.BedFontX + "";
            /// <summary>
            /// 床位文字Y坐标
            /// </summary>
            txtBedFontY.Text = model.BedFontY + "";

            /// <summary>
            /// 患者名称文字
            /// </summary>
            sliderPageNumFontSize.Value = model.PatientFontSize;
            /// <summary>
            /// 患者名称文字X坐标
            /// </summary>
            txtPatientFontX.Text = model.PatientFontX + "";
            /// <summary>
            /// 患者名称文字Y坐标
            /// </summary>
            txtPatientFontY.Text = model.PatientFontY + "";

            /// <summary>
            /// 性别文字
            /// </summary>
            sliderGenderFontSize.Value = model.GenderFontSize;
            /// <summary>
            /// 性别文字X坐标
            /// </summary>
            txtGenderFontX.Text = model.GenderFontX + "";
            /// <summary>
            /// 性别文字Y坐标
            /// </summary>
            txtGenderFontY.Text = model.GenderFontY + "";

            /// <summary>
            /// 批号文字
            /// </summary>
            sliderBatchNumberFontSize.Value = model.BatchNumberFontSize;
            /// <summary>
            /// 批号文字X坐标
            /// </summary>
            txtBatchNumberFontX.Text = model.BatchNumberFontX + "";
            /// <summary>
            /// 批号文字Y坐标
            /// </summary>
            txtBatchNumberFontY.Text = model.BatchNumberFontY + "";

            /// <summary>
            /// 编号文字
            /// </summary>
            sliderSerialNumberFontSize.Value = model.SerialNumberFontSize;
            /// <summary>
            /// 编号文字X坐标
            /// </summary>
            txtSerialNumberFontX.Text = model.SerialNumberFontX + "";
            /// <summary>
            /// 编号文字Y坐标
            /// </summary>
            txtSerialNumberFontY.Text = model.SerialNumberFontY + "";

            /// <summary>
            /// 第二条分割线宽度
            /// </summary>
            sliderSplit2Width.Value = model.Split2Width;
            /// <summary>
            /// 第二条分割线X坐标
            /// </summary>
            txtSplit2X.Text = model.Split2X + "";
            /// <summary>
            /// 第二条分割线Y坐标
            /// </summary>
            txtSplit2Y.Text = model.Split2Y + "";

            /// <summary>
            /// 药名标题文字
            /// </summary>
            sliderDrugsTitleFontSize.Value = model.DrugsTitleFontSize;
            /// <summary>
            /// 药名标题文字X坐标
            /// </summary>
            txtDrugsTitleFontX.Text = model.DrugsTitleFontX + "";
            /// <summary>
            /// 药名标题文字Y坐标
            /// </summary>
            txtDrugsTitleFontY.Text = model.DrugsTitleFontY + "";

            /// <summary>
            /// 药品名称文字
            /// </summary>
            sliderDrugsContentFontSize.Value = model.DrugsContentFontSize;
            /// <summary>
            /// 药品名称文字X坐标
            /// </summary>
            txtDrugsContentFontX.Text = model.DrugsContentFontX + "";
            /// <summary>
            /// 药品名称文字Y坐标
            /// </summary>
            txtDrugsContentFontY.Text = model.DrugsContentFontY + "";

            /// <summary>
            /// 用量标题文字
            /// </summary>
            sliderUseTitleFontSize.Value = model.UseTitleFontSize;
            /// <summary>
            /// 用量标题文字X坐标
            /// </summary>
            txtUseTitleFontX.Text = model.UseTitleFontX + "";
            /// <summary>
            /// 用量标题文字Y坐标
            /// </summary>
            txtUseTitleFontY.Text = model.UseTitleFontY + "";

            /// <summary>
            /// 用量值文字
            /// </summary>
            sliderUseValueFontSize.Value = model.UseValueFontSize;
            /// <summary>
            /// 用量值文字X坐标
            /// </summary>
            txtUseValueFontX.Text = model.UseValueFontX + "";
            /// <summary>
            /// 用量值文字Y坐标
            /// </summary>
            txtUseValueFontY.Text = model.UseValueFontY + "";

            /// <summary>
            /// 处方医生文字
            /// </summary>
            sliderDoctorAdviceFontSize.Value = model.DoctorFontSize;
            /// <summary>
            /// 处方医生文字
            /// </summary>X坐标
            txtDoctorFontX.Text = model.DoctorFontX + "";
            /// <summary>
            /// 处方医生文字Y坐标
            /// </summary>
            txtDoctorFontY.Text = model.DoctorFontY + "";

            /// <summary>
            /// 备注文字
            /// </summary>
            sliderRemarkFontSize.Value = model.RemarkFontSize;
            /// <summary>
            /// 备注文字X坐标
            /// </summary>
            txtRemarkFontX.Text = model.RemarkFontX + "";
            /// <summary>
            /// 备注文字Y坐标
            /// </summary>
            txtRemarkFontY.Text = model.RemarkFontY + "";

            /// <summary>
            /// 滴速文字
            /// </summary>
            sliderSortFontSize.Value = model.SpeedFontSize;
            /// <summary>
            /// 滴速文字X坐标
            /// </summary>
            txtSpeedFontX.Text = model.SpeedFontX + "";
            /// <summary>
            /// 滴速文字Y坐标
            /// </summary>
            txtSpeedFontY.Text = model.SpeedFontY + "";

            /// <summary>
            /// 第三条分割线宽度
            /// </summary>
            sliderSplit2Width.Value = model.Split3Width;
            /// <summary>
            /// 第三条分割线X坐标
            /// </summary>
            txtSplit3X.Text = model.Split3X + "";
            /// <summary>
            /// 第三条分割线Y坐标
            /// </summary>
            txtSplit3Y.Text = model.Split3Y + "";

            /// <summary>
            /// 审核文字
            /// </summary>
            sliderExamineFontSize.Value = model.ExamineFontSize;
            /// <summary>
            /// 审核文字X坐标
            /// </summary>
            txtExamineFontX.Text = model.ExamineFontX + "";
            /// <summary>
            /// 审核文字Y坐标
            /// </summary>
            txtExamineFontY.Text = model.ExamineFontY + "";

            /// <summary>
            /// 复核文字
            /// </summary>
            sliderReviewFontSize.Value = model.ReviewFontSize;
            /// <summary>
            /// 复核文字X坐标
            /// </summary>
            txtReviewFontX.Text = model.ReviewFontX + "";
            /// <summary>
            /// 复核文字Y坐标
            /// </summary>
            txtReviewFontY.Text = model.ReviewFontY + "";

            /// <summary>
            /// 排药文字
            /// </summary>
            sliderSortFontSize.Value = model.SortFontSize;
            /// <summary>
            /// 排药文字X坐标
            /// </summary>
            txtSortFontX.Text = model.SortFontX + "";
            /// <summary>
            /// 排药文字Y坐标
            /// </summary>
            txtSortFontY.Text = model.SortFontY + "";

            /// <summary>
            /// 配液文字
            /// </summary>
            sliderDispensingFontSize.Value = model.DispensingFontSize;
            /// <summary>
            /// 配液文字X坐标
            /// </summary>
            txtDispensingFontX.Text = model.DispensingFontX + "";
            /// <summary>
            /// 配液文字Y坐标
            /// </summary>
            txtDispensingFontY.Text = model.DispensingFontY + "";

            /// <summary>
            /// 配液时间文字
            /// </summary>
            sliderDispensingDateFontSize.Value = model.DispensingDateFontSize;

            /// <summary>
            /// 配液时间文字X坐标
            /// </summary>
            txtDispensingDateFontX.Text = model.DispensingDateFontX + "";

            /// <summary>
            /// 配液时间文字Y坐标
            /// </summary>
            txtDispensingDateFontY.Text = model.DispensingDateFontY + "";

            /// <summary>
            /// 二维码条码宽度
            /// </summary>
            txtBarCodeWidth.Text = model.BarCodeWidth + "";
            /// <summary>
            /// 二维码条码高度
            /// </summary>
            txtBarCodeHeight.Text = model.BarCodeHeight + "";
            /// <summary>
            /// 二维码条码X坐标
            /// </summary>
            txtBarCodeX.Text = model.BarCodeX + "";
            /// <summary>
            /// 二维码条码Y坐标
            /// </summary>
            txtBarCodeY.Text = model.BarCodeY + "";
        }

        #endregion

        #region 保存数据

        private void SaveConfig()
        {
            PrintTemplateModel model = new PrintTemplateModel();

            try
            {
                /// <summary>
                /// 页面宽度
                /// </summary>
                model.PageWidth = Convert.ToInt32(txtPageWidth.Text);
                /// <summary>
                /// 页面高度
                /// </summary>
                model.PageHeight = Convert.ToInt32(txtPageHeight.Text);

                /// <summary>
                /// 病区文字
                /// </summary>
                model.AreaFontSize = Convert.ToInt32(sliderAreaFontSize.Value);
                /// <summary>
                /// 病区文字X坐标
                /// </summary>
                model.AreaFontX = Convert.ToInt32(txtAreaFontX.Text);
                /// <summary>
                /// 病区文字Y坐标
                /// </summary>
                model.AreaFontY = Convert.ToInt32(txtAreaFontY.Text);

                /// <summary>
                /// 日期文字
                /// </summary>
                model.DateFontSize = Convert.ToInt32(sliderDateFontSize.Value);
                /// <summary>
                /// 日期文字X坐标
                /// </summary>
                model.DateFontX = Convert.ToInt32(txtDateFontX.Text);
                /// <summary>
                /// 日期文字Y坐标
                /// </summary>
                model.DateFontY = Convert.ToInt32(txtDateFontY.Text);

                /// <summary>
                /// 页码文字
                /// </summary>
                model.PageNumFontSize = Convert.ToInt32(sliderPageNumFontSize.Value);
                /// <summary>
                /// 页码文字X坐标
                /// </summary>
                model.PageNumFontX = Convert.ToInt32(txtPageNumFontX.Text);
                /// <summary>
                /// 页码文字Y坐标
                /// </summary>
                model.PageNumFontY = Convert.ToInt32(txtPageNumFontY.Text);

                /// <summary>
                /// 第一条分割线宽度
                /// </summary>
                model.SplitWidth = Convert.ToInt32(sliderSplitWidth.Value);
                /// <summary>
                /// 第一条分割线X坐标
                /// </summary>
                model.SplitX = Convert.ToInt32(txtSplitX.Text);
                /// <summary>
                /// 第一条分割线Y坐标
                /// </summary>
                model.SplitY = Convert.ToInt32(txtSplitY.Text);

                /// <summary>
                /// 医嘱号文字
                /// </summary>
                model.DoctorAdviceFontSize = Convert.ToInt32(sliderDoctorAdviceFontSize.Value);
                /// <summary>
                /// 医嘱号文字X坐标
                /// </summary>
                model.DoctorAdviceFontX = Convert.ToInt32(txtDoctorAdviceFontX.Text);
                /// <summary>
                /// 医嘱号文字Y坐标
                /// </summary>
                model.DoctorAdviceFontY = Convert.ToInt32(txtDoctorAdviceFontY.Text);

                /// <summary>
                /// 床位文字
                /// </summary>
                model.BedFontSize = Convert.ToInt32(sliderBedFontSize.Value);
                /// <summary>
                /// 床位文字X坐标
                /// </summary>
                model.BedFontX = Convert.ToInt32(txtBedFontX.Text);
                /// <summary>
                /// 床位文字Y坐标
                /// </summary>
                model.BedFontY = Convert.ToInt32(txtBedFontY.Text);

                /// <summary>
                /// 患者名称文字
                /// </summary>
                model.PatientFontSize = Convert.ToInt32(sliderPageNumFontSize.Value);
                /// <summary>
                /// 患者名称文字X坐标
                /// </summary>
                model.PatientFontX = Convert.ToInt32(txtPatientFontX.Text);
                /// <summary>
                /// 患者名称文字Y坐标
                /// </summary>
                model.PatientFontY = Convert.ToInt32(txtPatientFontY.Text);

                /// <summary>
                /// 性别文字
                /// </summary>
                model.GenderFontSize = Convert.ToInt32(sliderGenderFontSize.Value);
                /// <summary>
                /// 性别文字X坐标
                /// </summary>
                model.GenderFontX = Convert.ToInt32(txtGenderFontX.Text);
                /// <summary>
                /// 性别文字Y坐标
                /// </summary>
                model.GenderFontY = Convert.ToInt32(txtGenderFontY.Text);

                /// <summary>
                /// 批号文字
                /// </summary>
                model.BatchNumberFontSize = Convert.ToInt32(sliderBatchNumberFontSize.Value);
                /// <summary>
                /// 批号文字X坐标
                /// </summary>
                model.BatchNumberFontX = Convert.ToInt32(txtBatchNumberFontX.Text);
                /// <summary>
                /// 批号文字Y坐标
                /// </summary>
                model.BatchNumberFontY = Convert.ToInt32(txtBatchNumberFontY.Text);

                /// <summary>
                /// 编号文字
                /// </summary>
                model.SerialNumberFontSize = Convert.ToInt32(sliderSerialNumberFontSize.Value);
                /// <summary>
                /// 编号文字X坐标
                /// </summary>
                model.SerialNumberFontX = Convert.ToInt32(txtSerialNumberFontX.Text);
                /// <summary>
                /// 编号文字Y坐标
                /// </summary>
                model.SerialNumberFontY = Convert.ToInt32(txtSerialNumberFontY.Text);

                /// <summary>
                /// 第二条分割线宽度
                /// </summary>
                model.Split2Width = Convert.ToInt32(sliderSplit2Width.Value);
                /// <summary>
                /// 第二条分割线X坐标
                /// </summary>
                model.Split2X = Convert.ToInt32(txtSplit2X.Text);
                /// <summary>
                /// 第二条分割线Y坐标
                /// </summary>
                model.Split2Y = Convert.ToInt32(txtSplit2Y.Text);

                /// <summary>
                /// 药名标题文字
                /// </summary>
                model.DrugsTitleFontSize = Convert.ToInt32(sliderDrugsTitleFontSize.Value);
                /// <summary>
                /// 药名标题文字X坐标
                /// </summary>
                model.DrugsTitleFontX = Convert.ToInt32(txtDrugsTitleFontX.Text);
                /// <summary>
                /// 药名标题文字Y坐标
                /// </summary>
                model.DrugsTitleFontY = Convert.ToInt32(txtDrugsTitleFontY.Text);

                /// <summary>
                /// 药品名称文字
                /// </summary>
                model.DrugsContentFontSize = Convert.ToInt32(sliderDrugsContentFontSize.Value);
                /// <summary>
                /// 药品名称文字X坐标
                /// </summary>
                model.DrugsContentFontX = Convert.ToInt32(txtDrugsContentFontX.Text);
                /// <summary>
                /// 药品名称文字Y坐标
                /// </summary>
                model.DrugsContentFontY = Convert.ToInt32(txtDrugsContentFontY.Text);

                /// <summary>
                /// 用量标题文字
                /// </summary>
                model.UseTitleFontSize = Convert.ToInt32(sliderUseTitleFontSize.Value);
                /// <summary>
                /// 用量标题文字X坐标
                /// </summary>
                model.UseTitleFontX = Convert.ToInt32(txtUseTitleFontX.Text);
                /// <summary>
                /// 用量标题文字Y坐标
                /// </summary>
                model.UseTitleFontY = Convert.ToInt32(txtUseTitleFontY.Text);

                /// <summary>
                /// 用量值文字
                /// </summary>
                model.UseValueFontSize = Convert.ToInt32(sliderUseValueFontSize.Value);
                /// <summary>
                /// 用量值文字X坐标
                /// </summary>
                model.UseValueFontX = Convert.ToInt32(txtUseValueFontX.Text);
                /// <summary>
                /// 用量值文字Y坐标
                /// </summary>
                model.UseValueFontY = Convert.ToInt32(txtUseValueFontY.Text);

                /// <summary>
                /// 处方医生文字
                /// </summary>
                model.DoctorFontSize = Convert.ToInt32(sliderDoctorAdviceFontSize.Value);
                /// <summary>
                /// 处方医生文字
                /// </summary>X坐标
                model.DoctorFontX = Convert.ToInt32(txtDoctorFontX.Text);
                /// <summary>
                /// 处方医生文字Y坐标
                /// </summary>
                model.DoctorFontY = Convert.ToInt32(txtDoctorFontY.Text);

                /// <summary>
                /// 备注文字
                /// </summary>
                model.RemarkFontSize = Convert.ToInt32(sliderRemarkFontSize.Value);
                /// <summary>
                /// 备注文字X坐标
                /// </summary>
                model.RemarkFontX = Convert.ToInt32(txtRemarkFontX.Text);
                /// <summary>
                /// 备注文字Y坐标
                /// </summary>
                model.RemarkFontY = Convert.ToInt32(txtRemarkFontY.Text);

                /// <summary>
                /// 滴速文字
                /// </summary>
                model.SpeedFontSize = Convert.ToInt32(sliderSortFontSize.Value);
                /// <summary>
                /// 滴速文字X坐标
                /// </summary>
                model.SpeedFontX = Convert.ToInt32(txtSpeedFontX.Text);
                /// <summary>
                /// 滴速文字Y坐标
                /// </summary>
                model.SpeedFontY = Convert.ToInt32(txtSpeedFontY.Text);

                /// <summary>
                /// 第三条分割线宽度
                /// </summary>
                model.Split3Width = Convert.ToInt32(sliderSplit2Width.Value);
                /// <summary>
                /// 第三条分割线X坐标
                /// </summary>
                model.Split3X = Convert.ToInt32(txtSplit3X.Text);
                /// <summary>
                /// 第三条分割线Y坐标
                /// </summary>
                model.Split3Y = Convert.ToInt32(txtSplit3Y.Text);

                /// <summary>
                /// 审核文字
                /// </summary>
                model.ExamineFontSize = Convert.ToInt32(sliderExamineFontSize.Value);
                /// <summary>
                /// 审核文字X坐标
                /// </summary>
                model.ExamineFontX = Convert.ToInt32(txtExamineFontX.Text);
                /// <summary>
                /// 审核文字Y坐标
                /// </summary>
                model.ExamineFontY = Convert.ToInt32(txtExamineFontY.Text);

                /// <summary>
                /// 复核文字
                /// </summary>
                model.ReviewFontSize = Convert.ToInt32(sliderReviewFontSize.Value);
                /// <summary>
                /// 复核文字X坐标
                /// </summary>
                model.ReviewFontX = Convert.ToInt32(txtReviewFontX.Text);
                /// <summary>
                /// 复核文字Y坐标
                /// </summary>
                model.ReviewFontY = Convert.ToInt32(txtReviewFontY.Text);

                /// <summary>
                /// 排药文字
                /// </summary>
                model.SortFontSize = Convert.ToInt32(sliderSortFontSize.Value);
                /// <summary>
                /// 排药文字X坐标
                /// </summary>
                model.SortFontX = Convert.ToInt32(txtSortFontX.Text);
                /// <summary>
                /// 排药文字Y坐标
                /// </summary>
                model.SortFontY = Convert.ToInt32(txtSortFontY.Text);

                /// <summary>
                /// 配液文字
                /// </summary>
                model.DispensingFontSize = Convert.ToInt32(sliderDispensingFontSize.Value);
                /// <summary>
                /// 配液文字X坐标
                /// </summary>
                model.DispensingFontX = Convert.ToInt32(txtDispensingFontX.Text);
                /// <summary>
                /// 配液文字Y坐标
                /// </summary>
                model.DispensingFontY = Convert.ToInt32(txtDispensingFontY.Text);

                /// <summary>
                /// 配液时间文字
                /// </summary>
                model.DispensingDateFontSize = Convert.ToInt32(sliderDispensingDateFontSize.Value);

                /// <summary>
                /// 配液时间文字X坐标
                /// </summary>
                model.DispensingDateFontX = Convert.ToInt32(txtDispensingDateFontX.Text);

                /// <summary>
                /// 配液时间文字Y坐标
                /// </summary>
                model.DispensingDateFontY = Convert.ToInt32(txtDispensingDateFontY.Text);

                /// <summary>
                /// 二维码条码宽度
                /// </summary>
                model.BarCodeWidth = Convert.ToInt32(txtBarCodeWidth.Text);
                /// <summary>
                /// 二维码条码高度
                /// </summary>
                model.BarCodeHeight = Convert.ToInt32(txtBarCodeHeight.Text);
                /// <summary>
                /// 二维码条码X坐标
                /// </summary>
                model.BarCodeX = Convert.ToInt32(txtBarCodeX.Text);
                /// <summary>
                /// 二维码条码Y坐标
                /// </summary>
                model.BarCodeY = Convert.ToInt32(txtBarCodeY.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("请输入正确的参数！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if(new PrintTemplateHelper().SaveConfig(model))
            {
                MessageBox.Show("参数保存成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("参数保存失败！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        #endregion

        #region 重置/保存

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("确认重置参数内容？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                LoadConfig();
            }
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("确认保存参数内容？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                SaveConfig();
            }
        }

        #endregion

    }
}
