using Newtonsoft.Json;
using PrinterManagerProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterManagerProject.Tools
{
    public class PrintTemplateHelper
    {
        public string configPath = AppDomain.CurrentDomain.BaseDirectory + @"Config\";
        public string fileName = "template.config";

        public PrintTemplateModel GetConfig()
        {
            PrintTemplateModel model;
            model = DefaultConfig();
            return model;
            string path = configPath + fileName;
            try
            {
                if (File.Exists(path))
                {
                    myEventLog.LogInfo("自定义模板");

                    string config = File.ReadAllText(path);
                    model = JsonConvert.DeserializeObject<PrintTemplateModel>(config);
                }
                else
                {
                    myEventLog.LogInfo("默认模板");
                    model = DefaultConfig();
                }
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool SaveConfig(PrintTemplateModel model)
        {
            try
            {
                if (model == null)
                {
                    return false;
                }

                if (!Directory.Exists(configPath))
                {
                    Directory.CreateDirectory(configPath);
                }

                string path = configPath + fileName;
                //把配置信息写入文件
                string json = JsonConvert.SerializeObject(model);
                File.WriteAllText(path, json);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region 默认配置

        private PrintTemplateModel DefaultConfig()
        {
            PrintTemplateModel model = new PrintTemplateModel();

            #region 页面大小
            /// <summary>
            /// 页面宽度
            /// </summary>
            model.PageWidth = 700;
            /// <summary>
            /// 页面高度
            /// </summary>
            model.PageHeight = 800; 
            #endregion

            #region 医院名称

            /// <summary>
            /// 医院名称文字
            /// </summary>
            model.HospitalNameFontSize = 12;
            /// <summary>
            /// 医院名称文字X坐标
            /// </summary>
            model.HospitalNameFontX = 20;
            /// <summary>
            /// 医院名称文字Y坐标
            /// </summary>
            model.HospitalNameFontY = 20;

            #endregion

            #region 医嘱类型
            /// <summary>
            /// 医嘱类型文字
            /// </summary>
            model.OrderTypeFontSize = 14;
            /// <summary>
            /// 医嘱类型文字X坐标
            /// </summary>
            model.OrderTypeFontX = 410;
            /// <summary>
            /// 医嘱类型文字Y坐标
            /// </summary>
            model.OrderTypeFontY = 20;
            #endregion

            #region 特殊用药文字
            /// <summary>
            /// 特殊用药文字
            /// </summary>
            model.special_medicationtipFontSize = 14;
            /// <summary>
            /// 特殊用药文字X坐标
            /// </summary>
            model.special_medicationtipFontX = 480;
            /// <summary>
            /// 特殊用药文字Y坐标
            /// </summary>
            model.special_medicationtipFontY = 20;
            #endregion

            #region 批次名称
            /// <summary>
            /// 批次名称文字
            /// </summary>
            model.BatchNameFontSize = 14;
            /// <summary>
            /// 批次名称文字X坐标
            /// </summary>
            model.BatchNameFontX = 590;
            /// <summary>
            /// 批次名称文字Y坐标
            /// </summary>
            model.BatchNameFontY = 20;
            #endregion

            #region 病区
            /// <summary>
            /// 病区文字
            /// </summary>
            model.AreaFontSize = 14;
            /// <summary>
            /// 病区文字X坐标
            /// </summary>
            model.AreaFontX = 20;
            /// <summary>
            /// 病区文字Y坐标
            /// </summary>
            model.AreaFontY = 65;
            #endregion

            #region 床位
            /// <summary>
            /// 床位文字
            /// </summary>
            model.BedFontSize = 16;
            /// <summary>
            /// 床位文字X坐标
            /// </summary>
            model.BedFontX = 20;
            /// <summary>
            /// 床位文字Y坐标
            /// </summary>
            model.BedFontY = 110;
            #endregion

            #region 患者名称
            /// <summary>
            /// 患者名称文字
            /// </summary>
            model.PatientFontSize = 16;
            /// <summary>
            /// 患者名称文字X坐标
            /// </summary>
            model.PatientFontX = 140;
            /// <summary>
            /// 患者名称文字Y坐标
            /// </summary>
            model.PatientFontY = 110;
            #endregion

            #region 年龄
            /// <summary>
            /// 年龄文字
            /// </summary>
            model.AgeFontSize = 12;
            /// <summary>
            /// 年龄文字X坐标
            /// </summary>
            model.AgeFontX = 20;
            /// <summary>
            /// 年龄文字Y坐标
            /// </summary>
            model.AgeFontY = 190;
            #endregion

            #region 性别
            /// <summary>
            /// 性别文字
            /// </summary>
            model.GenderFontSize = 12;
            /// <summary>
            /// 性别文字X坐标
            /// </summary>
            model.GenderFontX = 85;
            /// <summary>
            /// 性别文字Y坐标
            /// </summary>
            model.GenderFontY = 190; 
            #endregion

            #region 医嘱编号
            /// <summary>
            /// 医嘱编号文字
            /// </summary>
            model.GroupNumFontSize = 12;
            /// <summary>
            /// 医嘱编号文字X坐标
            /// </summary>
            model.GroupNumFontX = 120;
            /// <summary>
            /// 医嘱编号文字Y坐标
            /// </summary>
            model.GroupNumUnFontY = 190; 
            #endregion

            #region 日期
            /// <summary>
            /// 日期文字
            /// </summary>
            model.DateFontSize = 12;
            /// <summary>
            /// 日期文字X坐标
            /// </summary>
            model.DateFontX = 520;
            /// <summary>
            /// 日期文字Y坐标
            /// </summary>
            model.DateFontY = 70;
            #endregion

            #region 使用频率（use_time）
            /// <summary>
            /// 使用频率文字
            /// </summary>
            model.UserFrequentFontSize = 12;
            /// <summary>
            /// 使用频率文字X坐标
            /// </summary>
            model.UserFrequentFontX = 520;
            /// <summary>
            /// 使用频率文字Y坐标
            /// </summary>
            model.UserFrequentFontY = 190;
            #endregion

            #region is_print_sn

            /// <summary>
            /// is_print_snv文字
            /// </summary>
            model.is_print_snvFontSize = 12;
            /// <summary>
            /// is_print_snv文字X坐标
            /// </summary>
            model.is_print_snvFontX = 450;
            /// <summary>
            /// is_print_snv文字Y坐标
            /// </summary>
            model.is_print_snvFontY = 70;
            #endregion

            #region 第一条分割线
            /// <summary>
            /// 第一条分割线宽度
            /// </summary>
            model.SplitWidth = 300;
            /// <summary>
            /// 第一条分割线X坐标
            /// </summary>
            model.SplitX = 5;
            /// <summary>
            /// 第一条分割线Y坐标
            /// </summary>
            model.SplitY = 205;
            #endregion

            #region 第二条分割线

            /// <summary>
            /// 第二条分割线宽度
            /// </summary>
            model.Split2Width = 700;
            /// <summary>
            /// 第二条分割线X坐标
            /// </summary>
            model.Split2X = 0;
            /// <summary>
            /// 第二条分割线Y坐标
            /// </summary>
            model.Split2Y = 260;
            #endregion

            #region 第三条分割线
            /// <summary>
            /// 第三条分割线宽度
            /// </summary>
            model.Split3Width = 730;
            /// <summary>
            /// 第三条分割线X坐标
            /// </summary>
            model.Split3X = 0;
            /// <summary>
            /// 第三条分割线Y坐标
            /// </summary>
            model.Split3Y = 685; 
            #endregion


            /// <summary>
            /// 页码文字
            /// </summary>
            model.PageNumFontSize = 14;
            /// <summary>
            /// 页码文字X坐标
            /// </summary>
            model.PageNumFontX = 230;
            /// <summary>
            /// 页码文字Y坐标
            /// </summary>
            model.PageNumFontY = 5;


            /// <summary>
            /// 医嘱号文字
            /// </summary>
            model.DoctorAdviceFontSize = 14;
            /// <summary>
            /// 医嘱号文字X坐标
            /// </summary>
            model.DoctorAdviceFontX = 10;
            /// <summary>
            /// 医嘱号文字Y坐标
            /// </summary>
            model.DoctorAdviceFontY = 30;



            /// <summary>
            /// 批号文字
            /// </summary>
            model.BatchNumberFontSize = 14;
            /// <summary>
            /// 批号文字X坐标
            /// </summary>
            model.BatchNumberFontX = 10;
            /// <summary>
            /// 批号文字Y坐标
            /// </summary>
            model.BatchNumberFontY = 70;

            /// <summary>
            /// 编号文字
            /// </summary>
            model.SerialNumberFontSize = 18;
            /// <summary>
            /// 编号文字X坐标
            /// </summary>
            model.SerialNumberFontX = 70;
            /// <summary>
            /// 编号文字Y坐标
            /// </summary>
            model.SerialNumberFontY = 70;


            #region 药品名称标题

            /// <summary>
            /// 药名标题文字
            /// </summary>
            model.DrugsTitleFontSize = 14;
            /// <summary>
            /// 药名标题文字X坐标
            /// </summary>
            model.DrugsTitleFontX = 10;
            /// <summary>
            /// 药名标题文字Y坐标
            /// </summary>
            model.DrugsTitleFontY = 225;
            #endregion

            #region 药品名称内容
            /// <summary>
            /// 药品名称文字
            /// </summary>
            model.DrugsContentFontSize = 14;
            /// <summary>
            /// 药品名称文字X坐标
            /// </summary>
            model.DrugsContentFontX = 10;
            /// <summary>
            /// 药品名称文字Y坐标
            /// </summary>
            model.DrugsContentFontY = 280;
            #endregion

            #region 规格

            #region 规格标题
            /// <summary>
            /// 规格标题文字
            /// </summary>
            model.SpecTitleFontSize = 14;
            /// <summary>
            /// 规格标题文字X坐标
            /// </summary>
            model.SpecTitleFontX = 310;
            /// <summary>
            /// 规格标题文字Y坐标
            /// </summary>
            model.SpecTitleFontY = 225; 
            #endregion

            /// <summary>
            /// 规格值文字
            /// </summary>
            model.SpecValueFontSize = 14;
            /// <summary>
            /// 规格值文字X坐标
            /// </summary>
            model.SpecValueFontX = 310;
            /// <summary>
            /// 规格值文字Y坐标
            /// </summary>
            model.SpecValueFontY = 280;
            #endregion

            #region 用量
            /// <summary>
            /// 用量标题文字
            /// </summary>
            model.UseSpTitleFontSize = 14;
            /// <summary>
            /// 用量标题文字X坐标
            /// </summary>
            model.UseSpTitleFontX = 510;
            /// <summary>
            /// 用量标题文字Y坐标
            /// </summary>
            model.UseSpTitleFontY = 225;

            /// <summary>
            /// 用量值文字
            /// </summary>
            model.UseSpValueFontSize = 14;
            /// <summary>
            /// 用量值文字X坐标
            /// </summary>
            model.UseSpValueFontX = 510;
            /// <summary>
            /// 用量值文字Y坐标
            /// </summary>
            model.UseSpValueFontY = 280;
            #endregion

            #region 数量
            /// <summary>
            /// 数量标题文字
            /// </summary>
            model.UseTitleFontSize = 14;
            /// <summary>
            /// 数量标题文字X坐标
            /// </summary>
            model.UseTitleFontX = 630;
            /// <summary>
            /// 数量标题文字Y坐标
            /// </summary>
            model.UseTitleFontY = 225;

            /// <summary>
            /// 数量值文字
            /// </summary>
            model.UseValueFontSize = 14;
            /// <summary>
            /// 数量值文字X坐标
            /// </summary>
            model.UseValueFontX = 660;
            /// <summary>
            /// 数量值文字Y坐标
            /// </summary>
            model.UseValueFontY = 280;
            #endregion



            /// <summary>
            /// 处方医生文字
            /// </summary>
            model.DoctorFontSize = 14;
            /// <summary>
            /// 处方医生文字X坐标
            /// </summary>
            model.DoctorFontX = 10;
            /// <summary>
            /// 处方医生文字Y坐标
            /// </summary>
            model.DoctorFontY = 238;

            #region 医嘱备注
            /// <summary>
            /// 备注文字
            /// </summary>
            model.RemarkFontSize = 14;
            /// <summary>
            /// 备注文字X坐标
            /// </summary>
            model.RemarkFontX = 10;
            /// <summary>
            /// 备注文字Y坐标
            /// </summary>
            model.RemarkFontY = 650;
            #endregion

            #region 用法
            /// <summary>
            /// 用法文字
            /// </summary>
            model.UsageNameFontSize = 14;
            /// <summary>
            /// 用法文字X坐标
            /// </summary>
            model.UsageNameFontX = 570;
            /// <summary>
            /// 用法文字Y坐标
            /// </summary>
            model.UsageNameFontY = 120; 
            #endregion

            /// <summary>
            /// 滴速文字
            /// </summary>
            model.SpeedFontSize = 14;
            /// <summary>
            /// 滴速文字X坐标
            /// </summary>
            model.SpeedFontX = 10;
            /// <summary>
            /// 滴速文字Y坐标
            /// </summary>
            model.SpeedFontY = 253;



            #region 审方
            /// <summary>
            /// 审方文字
            /// </summary>
            model.ExamineFontSize = 14;
            /// <summary>
            /// 审方文字X坐标
            /// </summary>
            model.ExamineFontX = 10;
            /// <summary>
            /// 审方文字Y坐标
            /// </summary>
            model.ExamineFontY = 710;
            #endregion

            #region MyRegion
            /// <summary>
            /// 摆药文字
            /// </summary>
            model.SortFontSize = 14;
            /// <summary>
            /// 摆文字X坐标
            /// </summary>
            model.SortFontX = 430;
            /// <summary>
            /// 摆文字Y坐标
            /// </summary>
            model.SortFontY = 710;
            #endregion

            #region 配药
            /// <summary>
            /// 配药文字
            /// </summary>
            model.DispensingFontSize = 14;
            /// <summary>
            /// 配药文字X坐标
            /// </summary>
            model.DispensingFontX = 10;
            /// <summary>
            /// 配药文字Y坐标
            /// </summary>
            model.DispensingFontY = 750;
            #endregion


            #region 核对
            /// <summary>
            /// 核对文字
            /// </summary>
            model.CheckFontSize = 14;
            /// <summary>
            /// 核对文字X坐标
            /// </summary>
            model.CheckFontX = 430;
            /// <summary>
            /// 核对文字Y坐标
            /// </summary>
            model.CheckFontY = 750;
            #endregion

            #region 复核 
            /// <summary>
            /// 复核文字
            /// </summary>
            model.ReviewFontSize = 14;
            /// <summary>
            /// 复核文字X坐标
            /// </summary>
            model.ReviewFontX = 10;
            /// <summary>
            /// 复核文字Y坐标
            /// </summary>
            model.ReviewFontY = 790;
            #endregion

            #region 给药 
            /// <summary>
            /// 给药文字
            /// </summary>
            model.DoseFontSize = 14;
            /// <summary>
            /// 给药文字X坐标
            /// </summary>
            model.DoseFontX = 430;
            /// <summary>
            /// 给药文字Y坐标
            /// </summary>
            model.DoseFontY = 790;
            #endregion


            /// <summary>
            /// 配液时间文字
            /// </summary>
            model.DispensingDateFontSize = 14;

            /// <summary>
            /// 配液时间文字X坐标
            /// </summary>
            model.DispensingDateFontX = 200;

            /// <summary>
            /// 配液时间文字Y坐标
            /// </summary>
            model.DispensingDateFontY = 275;

            #region PDF417码
            /// <summary>
            /// 二维码条码宽度
            /// </summary>
            model.BarCodeWidth = 150;
            /// <summary>
            /// 二维码条码高度
            /// </summary>
            model.BarCodeHeight = 34;
            /// <summary>
            /// 二维码条码X坐标
            /// </summary>
            model.BarCodeX = 290;
            /// <summary>
            /// 二维码条码Y坐标
            /// </summary>
            model.BarCodeY = 100;
            #endregion

            #region 二维码
            /// <summary>
            /// 二维码条码宽度
            /// </summary>
            model.QRCodeWidth = 90;
            /// <summary>
            /// 二维码条码高度
            /// </summary>
            model.QRCodeHeight = 90;
            /// <summary>
            /// 二维码条码X坐标
            /// </summary>
            model.QRCodeX = 250;
            /// <summary>
            /// 二维码条码Y坐标
            /// </summary>
            model.QRCodeY =700;
            #endregion


            return model;
        }

        #endregion
    }
}
