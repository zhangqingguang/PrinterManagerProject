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
            /// <summary>
            /// 页面宽度
            /// </summary>
            model.PageWidth = 310;
            /// <summary>
            /// 页面高度
            /// </summary>
            model.PageHeight = 315;

            /// <summary>
            /// 病区文字
            /// </summary>
            model.AreaFontSize = 14;
            /// <summary>
            /// 病区文字X坐标
            /// </summary>
            model.AreaFontX = 10;
            /// <summary>
            /// 病区文字Y坐标
            /// </summary>
            model.AreaFontY = 5;

            /// <summary>
            /// 日期文字
            /// </summary>
            model.DateFontSize = 14;
            /// <summary>
            /// 日期文字X坐标
            /// </summary>
            model.DateFontX = 110;
            /// <summary>
            /// 日期文字Y坐标
            /// </summary>
            model.DateFontY = 5;

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
            model.SplitY = 25;

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
            /// 床位文字
            /// </summary>
            model.BedFontSize = 14;
            /// <summary>
            /// 床位文字X坐标
            /// </summary>
            model.BedFontX = 105;
            /// <summary>
            /// 床位文字Y坐标
            /// </summary>
            model.BedFontY = 30;

            /// <summary>
            /// 患者名称文字
            /// </summary>
            model.PatientFontSize = 14;
            /// <summary>
            /// 患者名称文字X坐标
            /// </summary>
            model.PatientFontX = 10;
            /// <summary>
            /// 患者名称文字Y坐标
            /// </summary>
            model.PatientFontY = 50;

            /// <summary>
            /// 性别文字
            /// </summary>
            model.GenderFontSize = 14;
            /// <summary>
            /// 性别文字X坐标
            /// </summary>
            model.GenderFontX = 70;
            /// <summary>
            /// 性别文字Y坐标
            /// </summary>
            model.GenderFontY = 50;

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

            /// <summary>
            /// 第二条分割线宽度
            /// </summary>
            model.Split2Width = 300;
            /// <summary>
            /// 第二条分割线X坐标
            /// </summary>
            model.Split2X = 5;
            /// <summary>
            /// 第二条分割线Y坐标
            /// </summary>
            model.Split2Y = 90;

            /// <summary>
            /// 药名标题文字
            /// </summary>
            model.DrugsTitleFontSize = 14;
            /// <summary>
            /// 药名标题文字X坐标
            /// </summary>
            model.DrugsTitleFontX = 25;
            /// <summary>
            /// 药名标题文字Y坐标
            /// </summary>
            model.DrugsTitleFontY = 95;

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
            model.DrugsContentFontY = 115;
            #region 规格

            /// <summary>
            /// 规格标题文字
            /// </summary>
            model.SpecTitleFontSize = 14;
            /// <summary>
            /// 规格标题文字X坐标
            /// </summary>
            model.SpecTitleFontX = 190;
            /// <summary>
            /// 规格标题文字Y坐标
            /// </summary>
            model.SpecTitleFontY = 105;

            /// <summary>
            /// 规格值文字
            /// </summary>
            model.SpecValueFontSize = 14;
            /// <summary>
            /// 规格值文字X坐标
            /// </summary>
            model.SpecValueFontX = 190;
            /// <summary>
            /// 规格值文字Y坐标
            /// </summary>
            model.SpecValueFontY = 120;
            #endregion

            #region 用量
            /// <summary>
            /// 用量标题文字
            /// </summary>
            model.UseSpTitleFontSize = 14;
            /// <summary>
            /// 用量标题文字X坐标
            /// </summary>
            model.UseSpTitleFontX = 225;
            /// <summary>
            /// 用量标题文字Y坐标
            /// </summary>
            model.UseSpTitleFontY = 105;

            /// <summary>
            /// 用量值文字
            /// </summary>
            model.UseSpValueFontSize = 14;
            /// <summary>
            /// 用量值文字X坐标
            /// </summary>
            model.UseSpValueFontX = 225;
            /// <summary>
            /// 用量值文字Y坐标
            /// </summary>
            model.UseSpValueFontY = 125;
            #endregion


            #region 数量
            /// <summary>
            /// 数量标题文字
            /// </summary>
            model.UseTitleFontSize = 14;
            /// <summary>
            /// 数量标题文字X坐标
            /// </summary>
            model.UseTitleFontX = 265;
            /// <summary>
            /// 数量标题文字Y坐标
            /// </summary>
            model.UseTitleFontY = 105;

            /// <summary>
            /// 数量值文字
            /// </summary>
            model.UseValueFontSize = 14;
            /// <summary>
            /// 数量值文字X坐标
            /// </summary>
            model.UseValueFontX = 250;
            /// <summary>
            /// 数量值文字Y坐标
            /// </summary>
            model.UseValueFontY = 125; 
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

            /// <summary>
            /// 备注文字
            /// </summary>
            model.RemarkFontSize = 14;
            /// <summary>
            /// 备注文字X坐标
            /// </summary>
            model.RemarkFontX = 130;
            /// <summary>
            /// 备注文字Y坐标
            /// </summary>
            model.RemarkFontY = 238;

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

            /// <summary>
            /// 第三条分割线宽度
            /// </summary>
            model.Split3Width = 300;
            /// <summary>
            /// 第三条分割线X坐标
            /// </summary>
            model.Split3X = 5;
            /// <summary>
            /// 第三条分割线Y坐标
            /// </summary>
            model.Split3Y = 270;

            /// <summary>
            /// 审核文字
            /// </summary>
            model.ExamineFontSize = 14;
            /// <summary>
            /// 审核文字X坐标
            /// </summary>
            model.ExamineFontX = 10;
            /// <summary>
            /// 审核文字Y坐标
            /// </summary>
            model.ExamineFontY = 275;

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
            model.ReviewFontY = 290;

            /// <summary>
            /// 排药文字
            /// </summary>
            model.SortFontSize = 14;
            /// <summary>
            /// 排药文字X坐标
            /// </summary>
            model.SortFontX = 110;
            /// <summary>
            /// 排药文字Y坐标
            /// </summary>
            model.SortFontY = 275;

            /// <summary>
            /// 配液文字
            /// </summary>
            model.DispensingFontSize = 14;
            /// <summary>
            /// 配液文字X坐标
            /// </summary>
            model.DispensingFontX = 110;
            /// <summary>
            /// 配液文字Y坐标
            /// </summary>
            model.DispensingFontY = 290;

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
            model.BarCodeX = 100;
            /// <summary>
            /// 二维码条码Y坐标
            /// </summary>
            model.BarCodeY = 42;

            return model;
        }

        #endregion
    }
}
