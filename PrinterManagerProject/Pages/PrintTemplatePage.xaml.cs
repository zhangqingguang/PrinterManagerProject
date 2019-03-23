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
            labelQRCode.Foreground = color;
            labelSplit.Foreground = color;
            labelBed.Foreground = color;
            labelPatient.Foreground = color;
            labelGender.Foreground = color;
            labelCheck.Foreground = color;
            labelSerialNumber.Foreground = color;
            labelSplit2.Foreground = color;
            labelDrugsTitle.Foreground = color;
            labelDrugsContent.Foreground = color;
            labelUseTitle.Foreground = color;
            labelUseValue.Foreground = color;
            labelDose.Foreground = color;
            labelRemark.Foreground = color;
            labelSpeed.Foreground = color;
            labelSplit3.Foreground = color;
            labelExamine.Foreground = color;
            labelReview.Foreground = color;
            labelSort.Foreground = color;
            labelDispensing.Foreground = color;
            labelUsageName.Foreground = color;
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
                    case "labelQRCode":
                        border = borderQRCode;
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
                    case "labelCheck":
                        border = borderCheck;
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
                    case "labelDose":
                        border = borderDose;
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
                    case "labelUsageName":
                        border = borderUsageName;
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
            labelQRCode.Foreground = color;
            labelSplit.Foreground = color;
            labelBed.Foreground = color;
            labelPatient.Foreground = color;
            labelGender.Foreground = color;
            labelCheck.Foreground = color;
            labelSerialNumber.Foreground = color;
            labelSplit2.Foreground = color;
            labelDrugsTitle.Foreground = color;
            labelDrugsContent.Foreground = color;
            labelUseTitle.Foreground = color;
            labelUseValue.Foreground = color;
            labelDose.Foreground = color;
            labelRemark.Foreground = color;
            labelSpeed.Foreground = color;
            labelSplit3.Foreground = color;
            labelExamine.Foreground = color;
            labelReview.Foreground = color;
            labelSort.Foreground = color;
            labelDispensing.Foreground = color;
            labelUsageName.Foreground = color;
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
                    case "borderQRCode":
                        labelQRCode.Foreground = color;
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
                    case "borderCheck":
                        labelCheck.Foreground = color;
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
                    case "borderDose":
                        labelDose.Foreground = color;
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
                    case "borderUsageName":
                        labelUsageName.Foreground = color;
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
            /// 二维码文字
            /// </summary>
            sliderQRCodeFontSize.Value = model.QRCodeHeight;
            /// <summary>
            /// 二维码文字X坐标
            /// </summary>
            txtQRCodeFontX.Text = model.QRCodeX + "";
            /// <summary>
            /// 二维码文字Y坐标
            /// </summary>
            txtQRCodeFontY.Text = model.QRCodeY + "";

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

            #region 病人编号
            /// <summary>
            /// 病人编号文字
            /// </summary>
            sliderDoctorAdviceFontSize.Value = model.GroupNumFontSize;
            /// <summary>
            /// 病人编号文字X坐标
            /// </summary>
            txtDoctorAdviceFontX.Text = model.GroupNumFontX + "";
            /// <summary>
            /// 病人编号文字Y坐标
            /// </summary>
            txtDoctorAdviceFontY.Text = model.GroupNumUnFontY + ""; 
            #endregion

            #region 床位
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
            #endregion

            /// <summary>
            /// 患者名称文字
            /// </summary>
            sliderPatientFontSize.Value = model.PatientFontSize;
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
            /// 核对文字
            /// </summary>
            sliderCheckFontSize.Value = model.CheckFontSize;
            /// <summary>
            /// 核对文字X坐标
            /// </summary>
            txtCheckFontX.Text = model.CheckFontX + "";
            /// <summary>
            /// 核对文字Y坐标
            /// </summary>
            txtCheckFontY.Text = model.CheckFontY + "";

            /// <summary>
            /// 编号文字
            /// </summary>
            sliderSerialNumberFontSize.Value = model.is_print_snvFontSize;
            /// <summary>
            /// 编号文字X坐标
            /// </summary>
            txtSerialNumberFontX.Text = model.is_print_snvFontX + "";
            /// <summary>
            /// 编号文字Y坐标
            /// </summary>
            txtSerialNumberFontY.Text = model.is_print_snvFontY + "";

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

            #region 规格
            /// <summary>
            /// 规格标题文字
            /// </summary>
            sliderSpecTitleFontSize.Value = model.SpecTitleFontSize;
            /// <summary>
            /// 规格标题文字X坐标
            /// </summary>
            txtSpecTitleFontX.Text = model.SpecTitleFontX + "";
            /// <summary>
            /// 规格标题文字Y坐标
            /// </summary>
            txtSpecTitleFontY.Text = model.SpecTitleFontY + "";

            /// <summary>
            /// 规格值文字
            /// </summary>
            sliderSpecValueFontSize.Value = model.SpecValueFontSize;
            /// <summary>
            /// 规格值文字X坐标
            /// </summary>
            txtSpecValueFontX.Text = model.SpecValueFontX + "";
            /// <summary>
            /// 规格值文字Y坐标
            /// </summary>
            txtSpecValueFontY.Text = model.SpecValueFontY + "";
            #endregion

            #region 用量
            /// <summary>
            /// 用量标题文字
            /// </summary>
            sliderUseSpTitleFontSize.Value = model.UseSpTitleFontSize;
            /// <summary>
            /// 用量标题文字X坐标
            /// </summary>
            txtUseSpTitleFontX.Text = model.UseSpTitleFontX + "";
            /// <summary>
            /// 用量标题文字Y坐标
            /// </summary>
            txtUseSpTitleFontY.Text = model.UseSpTitleFontY + "";

            /// <summary>
            /// 用量值文字
            /// </summary>
            sliderUseSpValueFontSize.Value = model.UseSpValueFontSize;
            /// <summary>
            /// 用量值文字X坐标
            /// </summary>
            txtUseSpValueFontX.Text = model.UseSpValueFontX + "";
            /// <summary>
            /// 用量值文字Y坐标
            /// </summary>
            txtUseSpValueFontY.Text = model.UseSpValueFontY + ""; 
            #endregion

            #region 数量
            /// <summary>
            /// 数量标题文字
            /// </summary>
            sliderUseTitleFontSize.Value = model.UseTitleFontSize;
            /// <summary>
            /// 数量标题文字X坐标
            /// </summary>
            txtUseTitleFontX.Text = model.UseTitleFontX + "";
            /// <summary>
            /// 数量标题文字Y坐标
            /// </summary>
            txtUseTitleFontY.Text = model.UseTitleFontY + "";

            /// <summary>
            /// 数量值文字
            /// </summary>
            sliderUseValueFontSize.Value = model.UseValueFontSize;
            /// <summary>
            /// 数量值文字X坐标
            /// </summary>
            txtUseValueFontX.Text = model.UseValueFontX + "";
            /// <summary>
            /// 数量值文字Y坐标
            /// </summary>
            txtUseValueFontY.Text = model.UseValueFontY + "";
            #endregion

            #region 医院名称
            /// <summary>
            /// 医院名称文字
            /// </summary>
            sliderHospitalNameFontSize.Value = model.HospitalNameFontSize;
            /// <summary>
            /// 医院名称文字
            /// </summary>X坐标
            txtHospitalNameFontX.Text = model.HospitalNameFontX + "";
            /// <summary>
            /// 医院名称文字Y坐标
            /// </summary>
            txtHospitalNameFontY.Text = model.HospitalNameFontY + "";
            #endregion

            #region 医嘱类型
            /// <summary>
            /// 医院名称文字
            /// </summary>
            sliderOrderTypeFontSize.Value = model.OrderTypeFontSize;
            /// <summary>
            /// 医院名称文字
            /// </summary>X坐标
            txtOrderTypeFontX.Text = model.OrderTypeFontX + "";
            /// <summary>
            /// 医院名称文字Y坐标
            /// </summary>
            txtOrderTypeFontY.Text = model.OrderTypeFontY + "";
            #endregion

            #region 特殊用药
            /// <summary>
            /// 特殊用药文字
            /// </summary>
            sliderspecial_medicationtipFontSize.Value = model.special_medicationtipFontSize;
            /// <summary>
            /// 特殊用药文字X坐标
            /// </summary>
            txtspecial_medicationtipFontX.Text = model.special_medicationtipFontX + "";
            /// <summary>
            /// 特殊用药文字Y坐标
            /// </summary>
            txtspecial_medicationtipFontY.Text = model.special_medicationtipFontY + "";
            #endregion

            #region 批次名称
            /// <summary>
            /// 批次名称文字
            /// </summary>
            sliderBatchNameFontSize.Value = model.BatchNameFontSize;
            /// <summary>
            /// 批次名称文字X坐标
            /// </summary>
            txtBatchNameFontX.Text = model.BatchNameFontX + "";
            /// <summary>
            /// 批次名称文字Y坐标
            /// </summary>
            txtBatchNameFontY.Text = model.BatchNameFontY + "";
            #endregion

            #region 年龄
            /// <summary>
            /// 年龄文字
            /// </summary>
            sliderAgeFontSize.Value = model.AgeFontSize;
            /// <summary>
            /// 年龄文字X坐标
            /// </summary>
            txtAgeFontX.Text = model.AgeFontX + "";
            /// <summary>
            /// 年龄文字Y坐标
            /// </summary>
            txtAgeFontY.Text = model.AgeFontY + "";
            #endregion

            /// <summary>
            /// 给药文字
            /// </summary>
            sliderDoseFontSize.Value = model.DoseFontSize;
            /// <summary>
            /// 给药文字
            /// </summary>X坐标
            txtDoseFontX.Text = model.DoseFontX + "";
            /// <summary>
            /// 给药文字Y坐标
            /// </summary>
            txtDoseFontY.Text = model.DoseFontY + "";

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
            /// 用药频次文字
            /// </summary>
            sliderSortFontSize.Value = model.UserFrequentFontSize;
            /// <summary>
            /// 用药频次文字X坐标
            /// </summary>
            txtSpeedFontX.Text = model.UserFrequentFontX + "";
            /// <summary>
            /// 用药频次文字Y坐标
            /// </summary>
            txtSpeedFontY.Text = model.UserFrequentFontY + "";

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
            /// 用法文字
            /// </summary>
            sliderUsageNameFontSize.Value = model.UsageNameFontSize;

            /// <summary>
            /// 用法文字X坐标
            /// </summary>
            txtUsageNameFontX.Text = model.UsageNameFontX + "";

            /// <summary>
            /// 用法文字Y坐标
            /// </summary>
            txtUsageNameFontY.Text = model.UsageNameFontY + "";

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
                /// 二维码文字
                /// </summary>
                model.QRCodeHeight = Convert.ToInt32(sliderQRCodeFontSize.Value);
                model.QRCodeWidth = Convert.ToInt32(sliderQRCodeFontSize.Value);
                /// <summary>
                /// 二维码文字X坐标
                /// </summary>
                model.QRCodeX = Convert.ToInt32(txtQRCodeFontX.Text);
                /// <summary>
                /// 二维码文字Y坐标
                /// </summary>
                model.QRCodeY = Convert.ToInt32(txtQRCodeFontY.Text);

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
                model.GroupNumFontSize = Convert.ToInt32(sliderDoctorAdviceFontSize.Value);
                /// <summary>
                /// 医嘱号文字X坐标
                /// </summary>
                model.GroupNumFontX = Convert.ToInt32(txtDoctorAdviceFontX.Text);
                /// <summary>
                /// 医嘱号文字Y坐标
                /// </summary>
                model.GroupNumUnFontY = Convert.ToInt32(txtDoctorAdviceFontY.Text);

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
                model.PatientFontSize = Convert.ToInt32(sliderPatientFontSize.Value);
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
                /// 核对文字
                /// </summary>
                model.CheckFontSize = Convert.ToInt32(sliderCheckFontSize.Value);
                /// <summary>
                /// 核对文字X坐标
                /// </summary>
                model.CheckFontX = Convert.ToInt32(txtCheckFontX.Text);
                /// <summary>
                /// 核对文字Y坐标
                /// </summary>
                model.CheckFontY = Convert.ToInt32(txtCheckFontY.Text);

                /// <summary>
                /// 编号文字
                /// </summary>
                model.is_print_snvFontSize = Convert.ToInt32(sliderSerialNumberFontSize.Value);
                /// <summary>
                /// 编号文字X坐标
                /// </summary>
                model.is_print_snvFontX = Convert.ToInt32(txtSerialNumberFontX.Text);
                /// <summary>
                /// 编号文字Y坐标
                /// </summary>
                model.is_print_snvFontY = Convert.ToInt32(txtSerialNumberFontY.Text);

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
                /// 数量标题文字
                /// </summary>
                model.UseTitleFontSize = Convert.ToInt32(sliderUseTitleFontSize.Value);
                /// <summary>
                /// 数量标题文字X坐标
                /// </summary>
                model.UseTitleFontX = Convert.ToInt32(txtUseTitleFontX.Text);
                /// <summary>
                /// 数量标题文字Y坐标
                /// </summary>
                model.UseTitleFontY = Convert.ToInt32(txtUseTitleFontY.Text);

                /// <summary>
                /// 数量值文字
                /// </summary>
                model.UseValueFontSize = Convert.ToInt32(sliderUseValueFontSize.Value);
                /// <summary>
                /// 数量值文字X坐标
                /// </summary>
                model.UseValueFontX = Convert.ToInt32(txtUseValueFontX.Text);
                /// <summary>
                /// 数量值文字Y坐标
                /// </summary>
                model.UseValueFontY = Convert.ToInt32(txtUseValueFontY.Text);

                /// <summary>
                /// 给药文字
                /// </summary>
                model.DoseFontSize = Convert.ToInt32(sliderDoseFontSize.Value);
                /// <summary>
                /// 给药文字
                /// </summary>X坐标
                model.DoseFontX = Convert.ToInt32(txtDoseFontX.Text);
                /// <summary>
                /// 给药文字Y坐标
                /// </summary>
                model.DoseFontY = Convert.ToInt32(txtDoseFontY.Text);

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
                /// 用药频次文字
                /// </summary>
                model.UserFrequentFontSize = Convert.ToInt32(sliderSortFontSize.Value);
                /// <summary>
                /// 用药频次文字X坐标
                /// </summary>
                model.UserFrequentFontX = Convert.ToInt32(txtSpeedFontX.Text);
                /// <summary>
                /// 用药频次文字Y坐标
                /// </summary>
                model.UserFrequentFontY = Convert.ToInt32(txtSpeedFontY.Text);

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
                /// 用法文字
                /// </summary>
                model.UsageNameFontSize = Convert.ToInt32(sliderUsageNameFontSize.Value);

                /// <summary>
                /// 用法文字X坐标
                /// </summary>
                model.UsageNameFontX = Convert.ToInt32(txtUsageNameFontX.Text);

                /// <summary>
                /// 用法文字Y坐标
                /// </summary>
                model.UsageNameFontY = Convert.ToInt32(txtUsageNameFontY.Text);

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



                /// <summary>
                /// 规格标题文字
                /// </summary>
                model.SpecTitleFontSize = Convert.ToInt32(sliderSpecTitleFontSize.Value);

                /// <summary>
                /// 规格标题文字X坐标
                /// </summary>
                model.SpecTitleFontX = Convert.ToInt32(txtSpecTitleFontX.Text);

                /// <summary>
                /// 规格标题文字Y坐标
                /// </summary>
                model.SpecTitleFontY = Convert.ToInt32(txtSpecTitleFontY.Text);


                /// <summary>
                /// 规格值文字
                /// </summary>
                model.SpecValueFontSize = Convert.ToInt32(sliderSpecValueFontSize.Value);

                /// <summary>
                /// 规格值文字X坐标
                /// </summary>
                model.SpecValueFontX = Convert.ToInt32(txtSpecValueFontX.Text);

                /// <summary>
                /// 规格值文字Y坐标
                /// </summary>
                model.SpecValueFontY = Convert.ToInt32(txtSpecValueFontY.Text);


                /// <summary>
                /// 用量标题文字
                /// </summary>
                model.UseSpTitleFontSize = Convert.ToInt32(sliderUseSpTitleFontSize.Value);

                /// <summary>
                /// 用量标题文字X坐标
                /// </summary>
                model.UseSpTitleFontX = Convert.ToInt32(txtUseSpTitleFontX.Text);

                /// <summary>
                /// 用量标题文字Y坐标
                /// </summary>
                model.UseSpTitleFontY = Convert.ToInt32(txtUseSpTitleFontY.Text);


                /// <summary>
                /// 用量值文字
                /// </summary>
                model.UseSpValueFontSize = Convert.ToInt32(sliderUseSpValueFontSize.Value);

                /// <summary>
                /// 用量值文字X坐标
                /// </summary>
                model.UseSpValueFontX = Convert.ToInt32(txtUseSpValueFontX.Text);

                /// <summary>
                /// 用量值文字Y坐标
                /// </summary>
                model.UseSpValueFontY = Convert.ToInt32(txtUseSpValueFontY.Text);


                /// <summary>
                /// 数量标题文字
                /// </summary>
                model.UseTitleFontSize = Convert.ToInt32(sliderUseTitleFontSize.Value);

                /// <summary>
                /// 数量标题文字X坐标
                /// </summary>
                model.UseTitleFontX = Convert.ToInt32(txtUseTitleFontX.Text);

                /// <summary>
                /// 数量标题文字Y坐标
                /// </summary>
                model.UseTitleFontY = Convert.ToInt32(txtUseTitleFontY.Text);


                /// <summary>
                /// 数量值文字
                /// </summary>
                model.UseValueFontSize = Convert.ToInt32(sliderUseValueFontSize.Value);

                /// <summary>
                /// 数量值文字X坐标
                /// </summary>
                model.UseValueFontX = Convert.ToInt32(txtUseValueFontX.Text);

                /// <summary>
                /// 数量值文字Y坐标
                /// </summary>
                model.UseValueFontY = Convert.ToInt32(txtUseValueFontY.Text);


                /// <summary>
                /// 医院名称文字
                /// </summary>
                model.HospitalNameFontSize = Convert.ToInt32(sliderHospitalNameFontSize.Value);

                /// <summary>
                /// 医院名称文字X坐标
                /// </summary>
                model.HospitalNameFontX = Convert.ToInt32(txtHospitalNameFontX.Text);

                /// <summary>
                /// 医院名称文字Y坐标
                /// </summary>
                model.HospitalNameFontY = Convert.ToInt32(txtHospitalNameFontY.Text);

                /// <summary>
                /// 医院名称文字
                /// </summary>
                model.OrderTypeFontSize = Convert.ToInt32(sliderOrderTypeFontSize.Value);

                /// <summary>
                /// 医院名称文字X坐标
                /// </summary>
                model.OrderTypeFontX = Convert.ToInt32(txtOrderTypeFontX.Text);

                /// <summary>
                /// 医院名称文字Y坐标
                /// </summary>
                model.OrderTypeFontY = Convert.ToInt32(txtOrderTypeFontY.Text);

                /// <summary>
                /// 特殊用药文字
                /// </summary>
                model.special_medicationtipFontSize = Convert.ToInt32(sliderspecial_medicationtipFontSize.Value);

                /// <summary>
                /// 特殊用药文字X坐标
                /// </summary>
                model.special_medicationtipFontX = Convert.ToInt32(txtspecial_medicationtipFontX.Text);

                /// <summary>
                /// 特殊用药文字Y坐标
                /// </summary>
                model.special_medicationtipFontY = Convert.ToInt32(txtspecial_medicationtipFontY.Text);

                /// <summary>
                /// 批次名称文字
                /// </summary>
                model.BatchNameFontSize = Convert.ToInt32(sliderBatchNameFontSize.Value);

                /// <summary>
                /// 批次名称文字X坐标
                /// </summary>
                model.BatchNameFontX = Convert.ToInt32(txtBatchNameFontX.Text);

                /// <summary>
                /// 批次名称文字Y坐标
                /// </summary>
                model.BatchNameFontY = Convert.ToInt32(txtBatchNameFontY.Text);


                /// <summary>
                /// 用法文字
                /// </summary>
                model.AgeFontSize = Convert.ToInt32(sliderAgeFontSize.Value);

                /// <summary>
                /// 用法文字X坐标
                /// </summary>
                model.AgeFontX = Convert.ToInt32(txtAgeFontX.Text);

                /// <summary>
                /// 用法文字Y坐标
                /// </summary>
                model.AgeFontY = Convert.ToInt32(txtAgeFontY.Text);


                ///// <summary>
                ///// 用法文字
                ///// </summary>
                //model.UsageNameFontSize = Convert.ToInt32(sliderUsageNameFontSize.Value);

                ///// <summary>
                ///// 用法文字X坐标
                ///// </summary>
                //model.UsageNameFontX = Convert.ToInt32(txtUsageNameFontX.Text);

                ///// <summary>
                ///// 用法文字Y坐标
                ///// </summary>
                //model.UsageNameFontY = Convert.ToInt32(txtUsageNameFontY.Text);



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
