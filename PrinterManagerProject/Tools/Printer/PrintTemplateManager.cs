﻿using PrinterManagerProject.EF;
using PrinterManagerProject.EF.Models;
using PrinterManagerProject.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;
using ZXing;
using ZXing.PDF417;

namespace PrinterManagerProject.Tools
{
    public class PrintTemplateManager
    {
        private PrintTemplateModel tempConfig;
        public PrintTemplateModel GetPrintConfig()
        {
            if(tempConfig == null)
            {
                tempConfig = new PrintTemplateHelper().GetConfig();
            }
            return tempConfig;
        }
        public string GetPrintCommand(tOrder order)
        {
            StringBuilder sb = new StringBuilder();
            var orderManager = new OrderManager();
            List<PrintDrugModel> drugs = orderManager.GetPrintDrugs(order.Id);

            var startTime = DateTime.Now;
            
            //            var command = $@"^XA
            //^CI17
            //^A@N,60,60,E:000.FNT^F8^FD1一二三四五六七八九十This is a test.^FS
            //^XZ";
            sb.Append("^XA");
            sb.Append("^CWJ,E:001.FNT^FS");
            //sb.Append("^A@N,60,60,E:000.FNT^F8^FD1一二三四五六七八九十This is a test.^FS");
            sb.Append(GetBarCodeCommand(order.barcode, tempConfig.BarCodeX, tempConfig.BarCodeY));
            //g.DrawImage(bmp, ConvertInt(tempConfig.BarCodeX), ConvertInt(tempConfig.BarCodeY), ConvertInt(tempConfig.BarCodeWidth), ConvertInt(tempConfig.BarCodeHeight));
            sb.Append(GetLabelCommand(order.patient_name, tempConfig.AreaFontSize, tempConfig.AreaFontX, tempConfig.AreaFontY));
            sb.Append(GetLabelCommand(order.use_date, tempConfig.DateFontSize, tempConfig.DateFontX, tempConfig.DateFontY));
            sb.Append(GetLabelCommand(order.is_print_snv, tempConfig.PageNumFontSize, tempConfig.PageNumFontX, tempConfig.PageNumFontY));
            sb.Append(GetLabelCommand("——————————————————————————", tempConfig.DrugsTitleFontSize, 0, tempConfig.SplitY - 5));
            sb.Append(GetLabelCommand(order.group_num, tempConfig.DoctorAdviceFontSize, tempConfig.DoctorAdviceFontX, tempConfig.DoctorAdviceFontY));
            sb.Append(GetLabelCommand(order.bed_number, tempConfig.BedFontSize, tempConfig.BedFontX, tempConfig.BedFontY));
            sb.Append(GetLabelCommand(order.patient_name, tempConfig.PatientFontSize, tempConfig.PatientFontX, tempConfig.PatientFontY));
            sb.Append(GetLabelCommand("男", tempConfig.GenderFontSize, tempConfig.GenderFontX, tempConfig.GenderFontY));
            sb.Append(GetLabelCommand(order.batch, tempConfig.BatchNumberFontSize, tempConfig.BatchNumberFontX, tempConfig.BatchNumberFontY));
            sb.Append(GetLabelCommand(order.zone?.ToString(), tempConfig.SerialNumberFontSize, tempConfig.SerialNumberFontX, tempConfig.SerialNumberFontY));
            //g.DrawLine(new System.Drawing.Pen(bush), new System.Drawing.Point(ConvertInt(tempConfig.Split2X), ConvertInt(tempConfig.Split2Y)), new System.Drawing.Point(ConvertInt(tempConfig.Split2X + tempConfig.Split2Width), ConvertInt(tempConfig.Split2Y)));
            sb.Append(GetLabelCommand("——————————————————————————", tempConfig.DrugsTitleFontSize, 0, tempConfig.Split2Y - 5));
            sb.Append(GetLabelCommand("药品名称", tempConfig.DrugsTitleFontSize, tempConfig.DrugsTitleFontX, tempConfig.DrugsTitleFontY));
            sb.Append(GetLabelCommand("用量", tempConfig.UseTitleFontSize, tempConfig.UseTitleFontX, tempConfig.UseTitleFontY));

            //g.DrawLine(new System.Drawing.Pen(bush), new System.Drawing.Point(ConvertInt(tempConfig.SplitX), ConvertInt(tempConfig.SplitY)), new System.Drawing.Point(ConvertInt(tempConfig.SplitX + tempConfig.SplitWidth), ConvertInt(tempConfig.SplitY)));
            var margin = 5;
            var height = tempConfig.DrugsContentFontY + margin;
            var paperWidth = tempConfig.PageWidth;
            var paperHeight = tempConfig.PageHeight;

            #region 药品列表
            // 药品信息
            for (int i = 0; i < drugs.Count; i++)
            {
                int fontHeight = tempConfig.DrugsContentFontSize;
                // 药名
                sb.Append(GetLabelCommand(drugs[i].drug_name, tempConfig.DrugsContentFontSize, tempConfig.DrugsContentFontX, height));
                //用量
                sb.Append(GetLabelCommand(drugs[i].use_count.TrimEnd('0').TrimEnd('.'), tempConfig.DrugsContentFontSize, tempConfig.PageWidth - 80, height));

                // 只修改Y轴，向下平铺
                height += tempConfig.DrugsContentFontSize + margin;
            }
            #endregion

            sb.Append(GetLabelCommand($"处方医生：{order.doctor_name}", tempConfig.DoctorFontSize, tempConfig.DoctorFontX, tempConfig.DoctorFontY));
            sb.Append(GetLabelCommand($"备注：{order.pass_remark}", tempConfig.RemarkFontSize, tempConfig.RemarkFontX, tempConfig.RemarkFontY));
            sb.Append(GetLabelCommand($"滴速：{order.ml_speed}   {order.usage_name}   {order.use_frequency}({order.use_time})", tempConfig.SpeedFontSize, tempConfig.SpeedFontX, tempConfig.SpeedFontY));

            //g.DrawLine(new System.Drawing.Pen(bush), new System.Drawing.Point(ConvertInt(tempConfig.Split3X), ConvertInt(tempConfig.Split3Y)), new System.Drawing.Point(ConvertInt(tempConfig.Split3X + tempConfig.Split3Width), ConvertInt(tempConfig.Split3Y)));

            sb.Append(GetLabelCommand("——————————————————————————", tempConfig.DrugsTitleFontSize, 0, tempConfig.Split3Y - 5));
            sb.Append(GetLabelCommand($"审核：{order.checker}", tempConfig.ExamineFontSize, tempConfig.ExamineFontX, tempConfig.ExamineFontY));
            sb.Append(GetLabelCommand($"复审：", tempConfig.ReviewFontSize, tempConfig.ReviewFontX, tempConfig.ReviewFontY));
            sb.Append(GetLabelCommand($"排药：{order.deliveryer}", tempConfig.SortFontSize, tempConfig.SortFontX, tempConfig.SortFontY));
            sb.Append(GetLabelCommand($"配液：{order.config_person}", tempConfig.DispensingFontSize, tempConfig.DispensingFontX, tempConfig.DispensingFontY));
            sb.Append(GetLabelCommand($"配液：___时___分", tempConfig.DispensingDateFontSize, tempConfig.DispensingDateFontX, tempConfig.DispensingDateFontY));

            sb.Append("^XZ");
            myEventLog.LogInfo($"生成打印模板命令时间:{(DateTime.Now - startTime).TotalMilliseconds}");

            return sb.ToString();
        }

        private string GetLabelCommand(string content, int fontSize, int x, int y)
        {
            x = x * printMultiple;
            y = y * printMultiple;

            int width = Convert.ToInt32(fontSize * 2.5);
            int height = Convert.ToInt32(fontSize * 2.5);

            return $"^FO{x},{y}^AJN,{width},{height}^CI17^F8^FD{content}^FS";
        }

        private string GetBarCodeCommand(string content, int x, int y)
        {
            x = x * printMultiple;
            y = y * printMultiple;

            return $@"^By3,3^FO{x},{y},^B7N,7,4,4,13,N^FD{content}^FS";
        }


        int printMultiple = 3;
        /// <summary>
        /// 生成打印内容并推送到打印机
        /// </summary>
        private Bitmap GetPrintImage(tOrder order)
        {
            #region 打印

            try
            {
                var orderManager = new OrderManager();
                List<PrintDrugModel> drugs = orderManager.GetPrintDrugs(order.Id);

                string fontName = "SimSun";

                var paperWidth = tempConfig.PageWidth * printMultiple;
                var paperHeight = tempConfig.PageHeight * printMultiple;

                var startTime = DateTime.Now;
                Bitmap image = new Bitmap(paperWidth, paperHeight);
                Graphics g = Graphics.FromImage(image);

                System.Drawing.Brush bush = new SolidBrush(System.Drawing.Color.Black);//填充的颜色

                try
                {
                    //消除锯齿
                    g.SmoothingMode = SmoothingMode.Default;  //使绘图质量最高，即消除锯齿
                    //g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    //g.CompositingQuality = CompositingQuality.HighSpeed;
                    //g.TextRenderingHint = TextRenderingHint.AntiAlias; //消除文字锯齿
                    g.PageUnit = GraphicsUnit.Pixel;
                    //清空图片背景颜色
                    g.Clear(System.Drawing.Color.White);

                    // 生成条形码数据
                    if (string.IsNullOrEmpty(order.barcode))
                    {
                        order.barcode = Guid.NewGuid().ToString();
                        order.barcode = order.barcode.Substring(order.barcode.Length - 21, 20);
                    }
                    myEventLog.LogInfo($"生成条形码：{order.barcode}");

                    #region PDF417
                    PDF417EncodingOptions pdf_options = new PDF417EncodingOptions
                    {
                        Margin = 0,
                        DisableECI = true,
                        CharacterSet = "UTF-8",
                        Width = ConvertInt(tempConfig.BarCodeWidth),
                        Height = ConvertInt(tempConfig.BarCodeHeight),
                        PureBarcode = false
                    };
                    var pdf417Writer = new ZXing.BarcodeWriter();
                    pdf417Writer.Format = BarcodeFormat.PDF_417;
                    pdf417Writer.Options = pdf_options;
                    #endregion

                    myEventLog.LogInfo($"打印标签二维码内容：{order.barcode}");
                    Bitmap bmp = pdf417Writer.Write(order.barcode);

                    myEventLog.LogInfo($"标签尺寸：配置 {tempConfig.BarCodeWidth}/{tempConfig.BarCodeHeight}");
                    myEventLog.LogInfo($"标签尺寸：转换 {ConvertInt(tempConfig.BarCodeWidth)}/{ConvertInt(tempConfig.BarCodeHeight)}");

                    g.DrawImage(bmp, ConvertInt(tempConfig.BarCodeX), ConvertInt(tempConfig.BarCodeY), ConvertInt(tempConfig.BarCodeWidth), ConvertInt(tempConfig.BarCodeHeight));

                    g.DrawString(order.patient_name, new Font(fontName, ConvertFontInt(tempConfig.AreaFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.AreaFontX), ConvertInt(tempConfig.AreaFontY));
                    g.DrawString(order.use_date, new Font(fontName, ConvertFontInt(tempConfig.DateFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.DateFontX), ConvertInt(tempConfig.DateFontY));
                    g.DrawString("1/1", new Font(fontName, ConvertFontInt(tempConfig.PageNumFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.PageNumFontX), ConvertInt(tempConfig.PageNumFontY));

                    g.DrawLine(new System.Drawing.Pen(bush), new System.Drawing.Point(ConvertInt(tempConfig.SplitX), ConvertInt(tempConfig.SplitY)), new System.Drawing.Point(ConvertInt(tempConfig.SplitX + tempConfig.SplitWidth), ConvertInt(tempConfig.SplitY)));

                    g.DrawString(order.group_num, new Font(fontName, ConvertFontInt(tempConfig.DoctorAdviceFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.DoctorAdviceFontX), ConvertInt(tempConfig.DoctorAdviceFontY));
                    g.DrawString(order.bed_number, new Font(fontName, ConvertFontInt(tempConfig.BedFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.BedFontX), ConvertInt(tempConfig.BedFontY));
                    g.DrawString(order.patient_name, new Font(fontName, ConvertFontInt(tempConfig.PatientFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.PatientFontX), ConvertInt(tempConfig.PatientFontY));
                    g.DrawString("男", new Font(fontName, ConvertFontInt(tempConfig.GenderFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.GenderFontX), ConvertInt(tempConfig.GenderFontY));
                    g.DrawString(string.Format("{0}批", order.batch), new Font(fontName, ConvertFontInt(tempConfig.BatchNumberFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.BatchNumberFontX), ConvertInt(tempConfig.BatchNumberFontY));
                    g.DrawString(string.Format("[{0}]", order.zone), new Font(fontName, ConvertFontInt(tempConfig.SerialNumberFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.SerialNumberFontX), ConvertInt(tempConfig.SerialNumberFontY));

                    g.DrawLine(new System.Drawing.Pen(bush), new System.Drawing.Point(ConvertInt(tempConfig.Split2X), ConvertInt(tempConfig.Split2Y)), new System.Drawing.Point(ConvertInt(tempConfig.Split2X + tempConfig.Split2Width), ConvertInt(tempConfig.Split2Y)));

                    g.DrawString("药品名称", new Font(fontName, ConvertFontInt(tempConfig.DrugsTitleFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.DrugsTitleFontX), ConvertInt(tempConfig.DrugsTitleFontY));
                    g.DrawString("用量", new Font(fontName, ConvertFontInt(tempConfig.UseTitleFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.UseTitleFontX), ConvertInt(tempConfig.UseTitleFontY));

                    int x = ConvertInt(tempConfig.DrugsContentFontX);
                    int y = ConvertInt(tempConfig.DrugsContentFontY);
                    int u_x = ConvertInt(tempConfig.UseValueFontX);
                    int u_y = ConvertInt(tempConfig.UseValueFontY);

                    #region 药品列表
                    // 药品信息
                    for (int i = 0; i < drugs.Count; i++)
                    {
                        int fontHeight = ConvertFontInt(tempConfig.DrugsContentFontSize);
                        int margin = 10;
                        float width = paperWidth - ConvertInt(80);
                        float height = fontHeight;
                        int row = (int)Math.Ceiling(height * order.drug_name.Length / width);
                        for (int j = 0; j < row; j++)
                        {
                            height += j * fontHeight + margin;
                        }

                        // 药名
                        RectangleF rectangle = new RectangleF(x, y, width, height);
                        Font font = new Font(fontName, ConvertFontInt(tempConfig.DrugsContentFontSize));
                        StringFormat sf = new StringFormat();
                        //sf.Alignment = StringAlignment.Near;
                        //sf.LineAlignment = StringAlignment.Near;
                        g.DrawString(drugs[i].drug_name, font, bush, rectangle, sf);

                        //g.DrawString(drug.drug_name, new Font(fontName, ConvertFontInt(tempConfig.DrugsContentFontSize)), bush, x, y);
                        //用量
                        g.DrawString(drugs[i].use_count, new Font(fontName, ConvertFontInt(tempConfig.UseValueFontSize)), bush, u_x, u_y);

                        // 只修改Y轴，向下平铺
                        y += (int)height + margin;
                        u_y += (int)height + margin;
                    }
                    #endregion

                    g.DrawString(string.Format("处方医生：{0}", order.doctor_name), new Font(fontName, ConvertFontInt(tempConfig.DoctorFontSize)), bush, ConvertInt(tempConfig.DoctorFontX), ConvertInt(tempConfig.DoctorFontY));
                    g.DrawString(string.Format("备注：{0}", order.pass_remark), new Font(fontName, ConvertFontInt(tempConfig.RemarkFontSize)), bush, ConvertInt(tempConfig.RemarkFontX), ConvertInt(tempConfig.RemarkFontY));
                    g.DrawString(string.Format("滴速：{0}   {1}   qd(8点)", order.ml_speed, order.usage_name), new Font(fontName, ConvertFontInt(tempConfig.SpeedFontSize)), bush, ConvertInt(tempConfig.SpeedFontX), ConvertInt(tempConfig.SpeedFontY));

                    g.DrawLine(new System.Drawing.Pen(bush), new System.Drawing.Point(ConvertInt(tempConfig.Split3X), ConvertInt(tempConfig.Split3Y)), new System.Drawing.Point(ConvertInt(tempConfig.Split3X + tempConfig.Split3Width), ConvertInt(tempConfig.Split3Y)));

                    g.DrawString(string.Format("审核：{0}", order.checker), new Font(fontName, ConvertFontInt(tempConfig.ExamineFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.ExamineFontX), ConvertInt(tempConfig.ExamineFontY));
                    g.DrawString(string.Format("复审：{0}", ""), new Font(fontName, ConvertFontInt(tempConfig.ReviewFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.ReviewFontX), ConvertInt(tempConfig.ReviewFontY));
                    g.DrawString(string.Format("排药：{0}", order.deliveryer), new Font(fontName, ConvertFontInt(tempConfig.SortFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.SortFontX), ConvertInt(tempConfig.SortFontY));
                    g.DrawString(string.Format("配液：{0}", order.config_person), new Font(fontName, ConvertFontInt(tempConfig.DispensingFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.DispensingFontX), ConvertInt(tempConfig.DispensingFontY));
                    g.DrawString("配液：___时___分", new Font(fontName, ConvertFontInt(tempConfig.DispensingDateFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.DispensingDateFontX), ConvertInt(tempConfig.DispensingDateFontY));

                    //ZebraImageI imageI = ZebraImageFactory.GetImage(image);

                    //b.AppendLine(string.Format("生成标签完成：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                    //myEventLog.LogInfo($"图片尺寸:{imageI.Width}×{imageI.Height}");

                    //myEventLog.LogInfo($"画图花费时间:{(DateTime.Now - startTime).TotalMilliseconds}");
                    //// 控制打印宽度
                    //startTime = DateTime.Now;
                    //printer.PrintImage(imageI, 0, 0, paperWidth, paperHeight, false);
                    //myEventLog.LogInfo($"发送打印内容花费时间:{(DateTime.Now - startTime).TotalMilliseconds}");

                    return image;

                }
                catch (Exception e)
                {
                    myEventLog.LogError(e.Message, e);
                    new LogHelper().PrinterLog(e.Message);
                }
                finally
                {
                    g.Dispose();
                    image.Dispose();
                }
            }
            catch (ConnectionException e)
            {
                myEventLog.LogError(e.Message, e);
                new LogHelper().PrinterLog(e.Message);
                Console.WriteLine(e.ToString());
            }
            catch (ZebraPrinterLanguageUnknownException e)
            {
                myEventLog.LogError(e.Message, e);
                new LogHelper().PrinterLog(e.Message);
                Console.WriteLine(e.ToString());
            }
            catch (IOException e)
            {
                myEventLog.LogError(e.Message, e);
                new LogHelper().PrinterLog(e.Message);
                Console.WriteLine(e.ToString());
            }
            finally
            {
            }
            return null;

            #endregion
        }
        private int ConvertFontInt(double num)
        {
            double multiple = 2;
            return Convert.ToInt32(num * multiple);
        }

        private int ConvertInt(double num)
        {
            double multiple = printMultiple;
            return Convert.ToInt32(num * multiple);
        }
    }
}
