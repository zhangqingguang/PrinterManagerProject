using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterManagerProject.Models
{
    public class PrintTemplateModel
    {
        internal int is_print_snvFontX;
        internal int is_print_snvFontSize;
        internal int is_print_snvFontY;

        /// <summary>
        /// 页面宽度
        /// </summary>
        public int PageWidth { get; set; }
        /// <summary>
        /// 页面高度
        /// </summary>
        public int PageHeight { get; set; }

        /// <summary>
        /// 病区文字
        /// </summary>
        public int AreaFontSize { get; set; }
        /// <summary>
        /// 病区文字X坐标
        /// </summary>
        public int AreaFontX { get; set; }
        /// <summary>
        /// 病区文字Y坐标
        /// </summary>
        public int AreaFontY { get; set; }

        /// <summary>
        /// 日期文字
        /// </summary>
        public int DateFontSize { get; set; }
        /// <summary>
        /// 日期文字X坐标
        /// </summary>
        public int DateFontX { get; set; }
        /// <summary>
        /// 日期文字Y坐标
        /// </summary>
        public int DateFontY { get; set; }

        /// <summary>
        /// 页码文字
        /// </summary>
        public int PageNumFontSize { get; set; }
        /// <summary>
        /// 页码文字X坐标
        /// </summary>
        public int PageNumFontX { get; set; }
        /// <summary>
        /// 页码文字Y坐标
        /// </summary>
        public int PageNumFontY { get; set; }

        /// <summary>
        /// 第一条分割线宽度
        /// </summary>
        public int SplitWidth { get; set; }
        /// <summary>
        /// 第一条分割线X坐标
        /// </summary>
        public int SplitX { get; set; }
        /// <summary>
        /// 第一条分割线Y坐标
        /// </summary>
        public int SplitY { get; set; }

        /// <summary>
        /// 医嘱号文字
        /// </summary>
        public int DoctorAdviceFontSize { get; set; }
        /// <summary>
        /// 医嘱号文字X坐标
        /// </summary>
        public int DoctorAdviceFontX { get; set; }
        /// <summary>
        /// 医嘱号文字Y坐标
        /// </summary>
        public int DoctorAdviceFontY { get; set; }

        /// <summary>
        /// 床位文字
        /// </summary>
        public int BedFontSize { get; set; }
        /// <summary>
        /// 床位文字X坐标
        /// </summary>
        public int BedFontX { get; set; }
        /// <summary>
        /// 床位文字Y坐标
        /// </summary>
        public int BedFontY { get; set; }

        /// <summary>
        /// 患者名称文字
        /// </summary>
        public int PatientFontSize { get; set; }
        /// <summary>
        /// 患者名称文字X坐标
        /// </summary>
        public int PatientFontX { get; set; }
        /// <summary>
        /// 患者名称文字Y坐标
        /// </summary>
        public int PatientFontY { get; set; }

        /// <summary>
        /// 性别文字
        /// </summary>
        public int GenderFontSize { get; set; }
        /// <summary>
        /// 性别文字X坐标
        /// </summary>
        public int GenderFontX { get; set; }
        /// <summary>
        /// 性别文字Y坐标
        /// </summary>
        public int GenderFontY { get; set; }

        /// <summary>
        /// 批号文字
        /// </summary>
        public int BatchNumberFontSize { get; set; }
        /// <summary>
        /// 批号文字X坐标
        /// </summary>
        public int BatchNumberFontX { get; set; }
        /// <summary>
        /// 批号文字Y坐标
        /// </summary>
        public int BatchNumberFontY { get; set; }

        /// <summary>
        /// 编号文字
        /// </summary>
        public int SerialNumberFontSize { get; set; }
        /// <summary>
        /// 编号文字X坐标
        /// </summary>
        public int SerialNumberFontX { get; set; }
        /// <summary>
        /// 编号文字Y坐标
        /// </summary>
        public int SerialNumberFontY { get; set; }

        /// <summary>
        /// 第二条分割线宽度
        /// </summary>
        public int Split2Width { get; set; }
        /// <summary>
        /// 第二条分割线X坐标
        /// </summary>
        public int Split2X { get; set; }
        /// <summary>
        /// 第二条分割线Y坐标
        /// </summary>
        public int Split2Y { get; set; }

        /// <summary>
        /// 药名标题文字
        /// </summary>
        public int DrugsTitleFontSize { get; set; }
        /// <summary>
        /// 药名标题文字X坐标
        /// </summary>
        public int DrugsTitleFontX { get; set; }
        /// <summary>
        /// 药名标题文字Y坐标
        /// </summary>
        public int DrugsTitleFontY { get; set; }

        /// <summary>
        /// 药品名称文字
        /// </summary>
        public int DrugsContentFontSize { get; set; }
        /// <summary>
        /// 药品名称文字X坐标
        /// </summary>
        public int DrugsContentFontX { get; set; }
        /// <summary>
        /// 药品名称文字Y坐标
        /// </summary>
        public int DrugsContentFontY { get; set; }
        #region 规格
        /// <summary>
        /// 规格标题文字
        /// </summary>
        public int SpecTitleFontSize { get; set; }
        /// <summary>
        /// 规格标题文字X坐标
        /// </summary>
        public int SpecTitleFontX { get; set; }
        /// <summary>
        /// 规格标题文字Y坐标
        /// </summary>
        public int SpecTitleFontY { get; set; }

        /// <summary>
        /// 规格值文字
        /// </summary>
        public int SpecValueFontSize { get; set; }
        /// <summary>
        /// 规格值文字X坐标
        /// </summary>
        public int SpecValueFontX { get; set; }
        /// <summary>
        /// 规格值文字Y坐标
        /// </summary>
        public int SpecValueFontY { get; set; } 
        #endregion
        #region 用量

        /// <summary>
        /// 用量标题文字
        /// </summary>
        public int UseSpTitleFontSize { get; set; }
        /// <summary>
        /// 用量标题文字X坐标
        /// </summary>
        public int UseSpTitleFontX { get; set; }
        /// <summary>
        /// 用量标题文字Y坐标
        /// </summary>
        public int UseSpTitleFontY { get; set; }

        /// <summary>
        /// 用量值文字
        /// </summary>
        public int UseSpValueFontSize { get; set; }
        /// <summary>
        /// 用量值文字X坐标
        /// </summary>
        public int UseSpValueFontX { get; set; }
        /// <summary>
        /// 用量值文字Y坐标
        /// </summary>
        public int UseSpValueFontY { get; set; } 
        #endregion

        #region 数量
        /// <summary>
        /// 数量标题文字
        /// </summary>
        public int UseTitleFontSize { get; set; }
        /// <summary>
        /// 数量标题文字X坐标
        /// </summary>
        public int UseTitleFontX { get; set; }
        /// <summary>
        /// 数量标题文字Y坐标
        /// </summary>
        public int UseTitleFontY { get; set; }

        /// <summary>
        /// 数量值文字
        /// </summary>
        public int UseValueFontSize { get; set; }
        /// <summary>
        /// 数量值文字X坐标
        /// </summary>
        public int UseValueFontX { get; set; }
        /// <summary>
        /// 数量值文字Y坐标
        /// </summary>
        public int UseValueFontY { get; set; }
        #endregion

        #region 医院名称

        /// <summary>
        /// 医院名称文字
        /// </summary>
        public int HospitalNameFontSize { get; set; }
        /// <summary>
        /// 医院名称文字X坐标
        /// </summary>
        public int HospitalNameFontX { get; set; }
        /// <summary>
        /// 医院名称文字Y坐标
        /// </summary>
        public int HospitalNameFontY { get; set; }
        #endregion

        #region is_print_snv

        ///// <summary>
        ///// is_print_snv文字
        ///// </summary>
        //public int is_print_snvFontSize { get; set; }
        ///// <summary>
        ///// is_print_snv文字X坐标
        ///// </summary>
        //public int is_print_snvFontX { get; set; }
        ///// <summary>
        ///// is_print_snv文字Y坐标
        ///// </summary>
        //public int is_print_snvFontY { get; set; }
        #endregion

        #region 医嘱类型

        /// <summary>
        /// 医嘱类型文字
        /// </summary>
        public int OrderTypeFontSize { get; set; }
        /// <summary>
        /// 医嘱类型文字X坐标
        /// </summary>
        public int OrderTypeFontX { get; set; }
        /// <summary>
        /// 医嘱类型文字Y坐标
        /// </summary>
        public int OrderTypeFontY { get; set; }
        #endregion

        #region 特殊用药

        /// <summary>
        /// 特殊用药文字
        /// </summary>
        public int special_medicationtipFontSize { get; set; }
        /// <summary>
        /// 特殊用药文字X坐标
        /// </summary>
        public int special_medicationtipFontX { get; set; }
        /// <summary>
        /// 特殊用药文字Y坐标
        /// </summary>
        public int special_medicationtipFontY { get; set; }
        #endregion

        #region 批次名称

        /// <summary>
        /// 批次名称
        /// </summary>
        public int BatchNameFontSize { get; set; }
        /// <summary>
        /// 批次名称X坐标
        /// </summary>
        public int BatchNameFontX { get; set; }
        /// <summary>
        /// 批次名称Y坐标
        /// </summary>
        public int BatchNameFontY { get; set; }
        #endregion

        #region 年龄

        /// <summary>
        /// 年龄
        /// </summary>
        public int AgeFontSize { get; set; }
        /// <summary>
        /// 年龄X坐标
        /// </summary>
        public int AgeFontX { get; set; }
        /// <summary>
        /// 年龄Y坐标
        /// </summary>
        public int AgeFontY { get; set; }
        #endregion

        /// <summary>
        /// 处方医生文字
        /// </summary>
        public int DoctorFontSize { get; set; }
        /// <summary>
        /// 处方医生文字X坐标
        /// </summary>
        public int DoctorFontX { get; set; }
        /// <summary>
        /// 处方医生文字Y坐标
        /// </summary>
        public int DoctorFontY { get; set; }

        /// <summary>
        /// 备注文字
        /// </summary>
        public int RemarkFontSize { get; set; }
        /// <summary>
        /// 备注文字X坐标
        /// </summary>
        public int RemarkFontX { get; set; }
        /// <summary>
        /// 备注文字Y坐标
        /// </summary>
        public int RemarkFontY { get; set; }

        /// <summary>
        /// 滴速文字
        /// </summary>
        public int SpeedFontSize { get; set; }
        /// <summary>
        /// 滴速文字X坐标
        /// </summary>
        public int SpeedFontX { get; set; }
        /// <summary>
        /// 滴速文字Y坐标
        /// </summary>
        public int SpeedFontY { get; set; }

        /// <summary>
        /// 第三条分割线宽度
        /// </summary>
        public int Split3Width { get; set; }
        /// <summary>
        /// 第三条分割线X坐标
        /// </summary>
        public int Split3X { get; set; }
        /// <summary>
        /// 第三条分割线Y坐标
        /// </summary>
        public int Split3Y { get; set; }

        /// <summary>
        /// 审核文字
        /// </summary>
        public int ExamineFontSize { get; set; }
        /// <summary>
        /// 审核文字X坐标
        /// </summary>
        public int ExamineFontX { get; set; }
        /// <summary>
        /// 审核文字Y坐标
        /// </summary>
        public int ExamineFontY { get; set; }

        /// <summary>
        /// 复核文字
        /// </summary>
        public int ReviewFontSize { get; set; }
        /// <summary>
        /// 复核文字X坐标
        /// </summary>
        public int ReviewFontX { get; set; }
        /// <summary>
        /// 复核文字Y坐标
        /// </summary>
        public int ReviewFontY { get; set; }

        /// <summary>
        /// 排药文字
        /// </summary>
        public int SortFontSize { get; set; }
        /// <summary>
        /// 排药文字X坐标
        /// </summary>
        public int SortFontX { get; set; }
        /// <summary>
        /// 排药文字Y坐标
        /// </summary>
        public int SortFontY { get; set; }

        /// <summary>
        /// 配液文字
        /// </summary>
        public int DispensingFontSize { get; set; }
        /// <summary>
        /// 配液文字X坐标
        /// </summary>
        public int DispensingFontX { get; set; }
        /// <summary>
        /// 配液文字Y坐标
        /// </summary>
        public int DispensingFontY { get; set; }

        /// <summary>
        /// 配液时间文字
        /// </summary>
        public int DispensingDateFontSize { get; set; }

        /// <summary>
        /// 配液时间文字X坐标
        /// </summary>
        public int DispensingDateFontX { get; set; }

        /// <summary>
        /// 配液时间文字Y坐标
        /// </summary>
        public int DispensingDateFontY { get; set; }

        /// <summary>
        /// 二维码条码宽度
        /// </summary>
        public int BarCodeWidth { get; set; }
        /// <summary>
        /// 二维码条码高度
        /// </summary>
        public int BarCodeHeight { get; set; }
        /// <summary>
        /// 二维码条码X坐标
        /// </summary>
        public int BarCodeX { get; set; }
        /// <summary>
        /// 二维码条码Y坐标
        /// </summary>
        public int BarCodeY { get; set; }
        public int GroupNumFontSize { get; internal set; }
        public int GroupNumFontX { get; internal set; }
        public int GroupNumUnFontY { get; internal set; }
        /// <summary>
        /// 用法名称文字字号
        /// </summary>
        public int UsageNameFontSize { get; internal set; }
        /// <summary>
        /// 用法文字X坐标
        /// </summary>
        public int UsageNameFontX { get; internal set; }
        /// <summary>
        /// 用法文字Y坐标
        /// </summary>
        public int UsageNameFontY { get; internal set; }
        public int UserFrequentFontSize { get; internal set; }
        public int UserFrequentFontX { get; internal set; }
        public int UserFrequentFontY { get; internal set; }
        /// <summary>
        /// 核对文字字号
        /// </summary>
        public int CheckFontSize { get; internal set; }
        /// <summary>
        /// 核对文字X坐标
        /// </summary>
        public int CheckFontX { get; internal set; }
        /// <summary>
        /// 核对文字Y坐标
        /// </summary>
        public int CheckFontY { get; internal set; }
        public int DoseFontSize { get; internal set; }
        public int DoseFontX { get; internal set; }
        public int DoseFontY { get; internal set; }
        public int QRCodeHeight { get; internal set; }
        public int QRCodeX { get; internal set; }
        public int QRCodeWidth { get; internal set; }
        public int QRCodeY { get; internal set; }
    }
}
