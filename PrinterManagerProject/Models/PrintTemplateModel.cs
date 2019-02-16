using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterManagerProject.Models
{
    public class PrintTemplateModel
    {
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

        /// <summary>
        /// 用量标题文字
        /// </summary>
        public int UseTitleFontSize { get; set; }
        /// <summary>
        /// 用量标题文字X坐标
        /// </summary>
        public int UseTitleFontX { get; set; }
        /// <summary>
        /// 用量标题文字Y坐标
        /// </summary>
        public int UseTitleFontY { get; set; }

        /// <summary>
        /// 用量值文字
        /// </summary>
        public int UseValueFontSize { get; set; }
        /// <summary>
        /// 用量值文字X坐标
        /// </summary>
        public int UseValueFontX { get; set; }
        /// <summary>
        /// 用量值文字Y坐标
        /// </summary>
        public int UseValueFontY { get; set; }

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
    }
}
