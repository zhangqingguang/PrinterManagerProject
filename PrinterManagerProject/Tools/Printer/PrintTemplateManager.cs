using PrinterManagerProject.EF;
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
        /// <summary>
        /// 药品行间距
        /// </summary>
        int drugLineMargin = 25;
        public string GetPrintCommand(tOrder order)
        {
            if (tempConfig == null)
            {
                GetPrintConfig();
            }
            StringBuilder sb = new StringBuilder();
            var orderManager = new OrderManager();
            List<PrintDrugModel> drugs = orderManager.GetPrintDrugs(order.Id);

            var startTime = DateTime.Now;
            
            //            var command = $@"^XA
            //^CI17
            //^A@N,60,60,E:000.FNT^F8^FD1一二三四五六七八九十This is a test.^FS
            //^XZ";
            sb.Append("^XA");
            sb.Append("^POI");
            // E:001.FNT 华文仿宋
            sb.Append("^CWJ,E:000.FNT^FS"); // E:000.FNT 微软雅黑
            //sb.Append("^A@N,60,60,E:000.FNT^F8^FD1一二三四五六七八九十This is a test.^FS");
            sb.Append(GetBarCodeCommand(order.barcode, tempConfig.BarCodeX, tempConfig.BarCodeY));
            sb.Append(GetQRCodeCommand(order.barcode, tempConfig.QRCodeX, tempConfig.QRCodeY));
            //g.DrawImage(bmp, ConvertInt(tempConfig.BarCodeX), ConvertInt(tempConfig.BarCodeY), ConvertInt(tempConfig.BarCodeWidth), ConvertInt(tempConfig.BarCodeHeight));
            sb.Append(GetLabelCommand("河南科技大学第一附属医院", tempConfig.HospitalNameFontSize, tempConfig.HospitalNameFontX, tempConfig.HospitalNameFontY));
            sb.Append(GetLabelCommand(order.order_type, tempConfig.OrderTypeFontSize, tempConfig.OrderTypeFontX, tempConfig.OrderTypeFontY));
            sb.Append(GetLabelCommand("【" + order.special_medicationtip + "】", tempConfig.special_medicationtipFontSize, tempConfig.special_medicationtipFontX, tempConfig.special_medicationtipFontY));
            sb.Append(GetLabelCommand(order.batch_name, tempConfig.BatchNameFontSize, tempConfig.BatchNameFontX, tempConfig.BatchNameFontY));
            sb.Append(GetLabelCommand(order.departmengt_name, tempConfig.AreaFontSize, tempConfig.AreaFontX, tempConfig.AreaFontY));
            sb.Append(GetLabelCommand(order.patient_name, tempConfig.PatientFontSize, tempConfig.PatientFontX, tempConfig.PatientFontY));
            sb.Append(GetLabelCommand(order.bed_number + "床", tempConfig.BedFontSize, tempConfig.BedFontX, tempConfig.BedFontY));
            sb.Append(GetLabelCommand($"{order.age}", tempConfig.AgeFontSize, tempConfig.AgeFontX, tempConfig.AgeFontY));
            sb.Append(GetLabelCommand(order.sex, tempConfig.GenderFontSize, tempConfig.GenderFontX, tempConfig.GenderFontY));
            sb.Append(GetLabelCommand(order.patient_id, tempConfig.GroupNumFontSize, tempConfig.GroupNumFontX, tempConfig.GroupNumUnFontY));
            sb.Append(GetLabelCommand($"{order.use_frequency}({order.freq_counter_sub})", tempConfig.UserFrequentFontSize, tempConfig.UserFrequentFontX, tempConfig.UserFrequentFontY));
            sb.Append(GetLabelCommand($"no{order.is_print_snv}", tempConfig.is_print_snvFontSize, tempConfig.is_print_snvFontX-Convert.ToInt32((order.is_print_snv.Length - 1)* tempConfig.is_print_snvFontSize * 1.25), tempConfig.is_print_snvFontY));
            sb.Append(GetLabelCommand(order.use_date, tempConfig.DateFontSize, tempConfig.DateFontX, tempConfig.DateFontY));

            sb.Append(GetLabelCommand("备注:"+order.pass_remark, tempConfig.RemarkFontSize, tempConfig.RemarkFontX, tempConfig.RemarkFontY));

            var showSpec = LimitTextWidth(order.usage_name, 125, tempConfig.UsageNameFontSize);
            if(showSpec== order.usage_name)
            {
                sb.Append(GetLabelCommand(order.usage_name, tempConfig.UsageNameFontSize, tempConfig.UsageNameFontX, tempConfig.UsageNameFontY));
            }
            else
            {
                sb.Append(GetLabelCommand(showSpec, tempConfig.UsageNameFontSize, tempConfig.UsageNameFontX, tempConfig.UsageNameFontY-(tempConfig.UsageNameFontSize+drugLineMargin)/2));
                sb.Append(GetLabelCommand(order.usage_name.Replace(showSpec,""), tempConfig.UsageNameFontSize, tempConfig.UsageNameFontX, tempConfig.UsageNameFontY+(tempConfig.UsageNameFontSize + drugLineMargin) / 2));
            }
            
            sb.Append(GetLabelCommand($"审方:{order.checker}", tempConfig.ExamineFontSize, tempConfig.ExamineFontX, tempConfig.ExamineFontY));
            sb.Append(GetLabelCommand($"摆药:{order.deliveryer}", tempConfig.SortFontSize, tempConfig.SortFontX, tempConfig.SortFontY));
            sb.Append(GetLabelCommand($"配药:{order.config_person}", tempConfig.DispensingFontSize, tempConfig.DispensingFontX, tempConfig.DispensingFontY));
            sb.Append(GetLabelCommand($"核对:{order.is_cpfhr}", tempConfig.CheckFontSize, tempConfig.CheckFontX, tempConfig.CheckFontY));
            sb.Append(GetLabelCommand($"复核:{order.pyhfr}", tempConfig.ReviewFontSize, tempConfig.ReviewFontX, tempConfig.ReviewFontY));
            sb.Append(GetLabelCommand($"给药:", tempConfig.DoseFontSize, tempConfig.DoseFontX, tempConfig.DoseFontY));

            sb.Append(GetLabelCommand("——————————————————————————", tempConfig.DrugsTitleFontSize, 0, tempConfig.SplitY - 5));
            sb.Append(GetLabelCommand("——————————————————————————", tempConfig.DrugsTitleFontSize, 0, tempConfig.Split2Y - 5));
            sb.Append(GetLabelCommand("——————————————————————————", tempConfig.DrugsTitleFontSize, 0, tempConfig.Split3Y - 5));

            sb.Append(GetLabelCommand("药品名称", tempConfig.DrugsTitleFontSize, tempConfig.DrugsTitleFontX, tempConfig.DrugsTitleFontY));
            sb.Append(GetLabelCommand("规格", tempConfig.SpecTitleFontSize, tempConfig.SpecTitleFontX, tempConfig.SpecTitleFontY));
            sb.Append(GetLabelCommand("用量", tempConfig.UseSpTitleFontSize, tempConfig.UseSpTitleFontX, tempConfig.UseSpTitleFontY));
            sb.Append(GetLabelCommand("数量", tempConfig.UseTitleFontSize, tempConfig.UseTitleFontX, tempConfig.UseTitleFontY));

            var height = tempConfig.DrugsContentFontY;
            var paperWidth = tempConfig.PageWidth;
            var paperHeight = tempConfig.PageHeight;
            #region 药品列表
            // 药品信息
            for (int i = 0; i < drugs.Count; i++)
            {
                var rowCount = 1;

                int fontHeight = tempConfig.DrugsContentFontSize;
                GetDrugNameCommand(sb, drugs, height, i, ref rowCount);
                //规格
                //sb.Append(GetLabelCommand(LimitTextWidth(drugs[i].spec, 130, tempConfig.SpecValueFontSize), tempConfig.SpecValueFontSize, tempConfig.SpecValueFontX, height));
                GetDrugSpecCommand(sb, drugs, height, i, ref rowCount);
                //用量
                if (drugs[i].durg_use_sp[0] == 9608)
                {
                    // 以实心方块开头的，用量增加下划线
                    sb.Append(GetLabelCommand("_____", tempConfig.UseSpValueFontSize, tempConfig.UseSpValueFontX, height));
                    sb.Append(GetLabelCommand("_____", tempConfig.UseSpValueFontSize, tempConfig.UseSpValueFontX, height + 2));
                    sb.Append(GetLabelCommand("_____", tempConfig.UseSpValueFontSize, tempConfig.UseSpValueFontX, height + 4));
                    drugs[i].durg_use_sp = drugs[i].durg_use_sp.Trim((char)9608);
                }
                sb.Append(GetLabelCommand(LimitTextWidth(drugs[i].durg_use_sp, 140, tempConfig.UseSpValueFontSize), tempConfig.UseSpValueFontSize, tempConfig.UseSpValueFontX, height));
                //数量
                sb.Append(GetLabelCommand(drugs[i].use_count.Replace(".0000", ""), tempConfig.UseValueFontSize, tempConfig.UseValueFontX, height));

                // 只修改Y轴，向下平铺
                height += (tempConfig.DrugsContentFontSize + drugLineMargin) * rowCount;
            }
            #endregion

            //sb.Append(GetLabelCommand($"处方医生：{order.doctor_name}", tempConfig.DoctorFontSize, tempConfig.DoctorFontX, tempConfig.DoctorFontY));
            //sb.Append(GetLabelCommand($"备注：{order.pass_remark}", tempConfig.RemarkFontSize, tempConfig.RemarkFontX, tempConfig.RemarkFontY));
            //sb.Append(GetLabelCommand($"滴速：{order.ml_speed}   {order.usage_name}   {order.use_frequency}({order.use_time})", tempConfig.SpeedFontSize, tempConfig.SpeedFontX, tempConfig.SpeedFontY));

            //g.DrawLine(new System.Drawing.Pen(bush), new System.Drawing.Point(ConvertInt(tempConfig.Split3X), ConvertInt(tempConfig.Split3Y)), new System.Drawing.Point(ConvertInt(tempConfig.Split3X + tempConfig.Split3Width), ConvertInt(tempConfig.Split3Y)));

            //sb.Append(GetLabelCommand($"审核：{order.checker}", tempConfig.ExamineFontSize, tempConfig.ExamineFontX, tempConfig.ExamineFontY));
            //sb.Append(GetLabelCommand($"复审：", tempConfig.ReviewFontSize, tempConfig.ReviewFontX, tempConfig.ReviewFontY));
            //sb.Append(GetLabelCommand($"排药：{order.deliveryer}", tempConfig.SortFontSize, tempConfig.SortFontX, tempConfig.SortFontY));
            //sb.Append(GetLabelCommand($"配液：{order.config_person}", tempConfig.DispensingFontSize, tempConfig.DispensingFontX, tempConfig.DispensingFontY));
            //sb.Append(GetLabelCommand($"配液：___时___分", tempConfig.DispensingDateFontSize, tempConfig.DispensingDateFontX, tempConfig.DispensingDateFontY));

            sb.Append("^XZ");
            myEventLog.LogInfo($"生成打印模板命令时间:{(DateTime.Now - startTime).TotalMilliseconds}");

            return sb.ToString();
        }
        /// <summary>
        /// 显示药品规格
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="drugs"></param>
        /// <param name="height"></param>
        /// <param name="i"></param>
        /// <param name="rowCount"></param>
        private void GetDrugSpecCommand(StringBuilder sb, List<PrintDrugModel> drugs, int height, int i, ref int rowCount)
        {
            //sb.Append(GetLabelCommand(LimitTextWidth(drugs[i].durg_use_sp, 140, tempConfig.UseSpValueFontSize), tempConfig.UseSpValueFontSize, tempConfig.UseSpValueFontX, height));
            //sb.Append(GetLabelCommand(LimitTextWidth(drugs[i].spec, 130, tempConfig.SpecValueFontSize), tempConfig.SpecValueFontSize, tempConfig.SpecValueFontX, height));

            var row = 0;
            var drugSpec = drugs[i].spec;
            var remainDrugSpec = drugSpec;
            while (string.IsNullOrEmpty(remainDrugSpec) == false)
            {
                var showSpec = LimitTextWidth(remainDrugSpec, 190, tempConfig.SpecValueFontSize);
                // 药名
                sb.Append(GetLabelCommand(showSpec, tempConfig.SpecValueFontSize, tempConfig.SpecValueFontX, height));
                remainDrugSpec = remainDrugSpec.Replace(showSpec, "");
                row++;
                height += (tempConfig.SpecValueFontSize + drugLineMargin);
            }
            if (row >= rowCount)
            {
                rowCount = row;
            }
        }

        /// <summary>
        /// 显示药品名称
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="drugs"></param>
        /// <param name="height"></param>
        /// <param name="i"></param>
        /// <param name="rowCount"></param>
        private void GetDrugNameCommand(StringBuilder sb, List<PrintDrugModel> drugs, int height, int i,ref int rowCount)
        {
            var row = 0;
            var drugName = drugs[i].drug_name;
            var remainDrugName = drugName;
            while (string.IsNullOrEmpty(remainDrugName)==false)
            {
                var showName = LimitTextWidth(remainDrugName, 250, tempConfig.DrugsContentFontSize);
                // 药名
                sb.Append(GetLabelCommand(showName, tempConfig.DrugsContentFontSize, tempConfig.DrugsContentFontX, height));
                remainDrugName = remainDrugName.Replace(showName, "");
                row++;
                height += (tempConfig.DrugsContentFontSize + drugLineMargin);
            }
            if (row >= rowCount)
            {
                rowCount = row;
            }
        }

        private string LimitTextWidth(string str,int width,int fontSize)
        {
            double strWidth = 0;
            str = str ?? "";
            var result = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] > 127)
                {
                    strWidth += fontSize*2;
                }
                else
                {
                    strWidth += fontSize*1.25;
                }
                if (strWidth > width)
                {
                    break;
                }

                    result += str[i];
            }

            return result;
        }
        int paddingLeft = 0 ;
        int paddingTop = 0 ;

        private string GetLabelCommand(string content, int fontSize, int x, int y)
        {
            x = Convert.ToInt32((x+ paddingLeft) *printMultiple);
            y = Convert.ToInt32((y+ paddingTop) *printMultiple);

            int width = Convert.ToInt32(fontSize * 2.5);
            int height = Convert.ToInt32(fontSize * 2.5);

            return $"^FO{x},{y}^AJN,{width},{height}^CI17^F8^FD{content}^FS";
        }

        private string GetBarCodeCommand(string content, int x, int y)
        {
            x = Convert.ToInt32((x + paddingLeft) * printMultiple);
            y = Convert.ToInt32((y + paddingTop) * printMultiple);

            return $@"^By3,3^FO{x},{y},^B7N,7,3,2,13,N^FD{content}^FS";
            //return $@"^By3,3^FO{x},{y},^B7N,7,4,4,13,N^FD{content}^FS";
        }

        private string GetQRCodeCommand(string content, int x, int y)
        {
            x = Convert.ToInt32((x + paddingLeft) * printMultiple);
            y = Convert.ToInt32((y + paddingTop) * printMultiple);

            return $@"^^FO{x},{y},^BQN,2,6^FDHM,N{content}^FS";
            //return $@"^^FO{x},{y},^BQN,3,6^FDMM,{content}^FS";
        }


        double printMultiple = 1.1;
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

                var paperWidth = Convert.ToInt32(tempConfig.PageWidth * printMultiple);
                var paperHeight = Convert.ToInt32(tempConfig.PageHeight * printMultiple);

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

                    g.DrawString("河南科技大学第一附属医院", new Font(fontName, ConvertFontInt(tempConfig.HospitalNameFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.HospitalNameFontX), ConvertInt(tempConfig.HospitalNameFontY));
                    g.DrawString(order.order_type, new Font(fontName, ConvertFontInt(tempConfig.OrderTypeFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.OrderTypeFontX), ConvertInt(tempConfig.OrderTypeFontY));
                    g.DrawString("【"+ order.special_medicationtip + "】", new Font(fontName, ConvertFontInt(tempConfig.special_medicationtipFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.special_medicationtipFontX), ConvertInt(tempConfig.special_medicationtipFontY));
                    g.DrawString(order.batch_name, new Font(fontName, ConvertFontInt(tempConfig.BatchNumberFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.BatchNumberFontX), ConvertInt(tempConfig.BatchNumberFontY));
                    g.DrawString(order.departmengt_name, new Font(fontName, ConvertFontInt(tempConfig.AreaFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.AreaFontX), ConvertInt(tempConfig.AreaFontY));
                    g.DrawString(order.patient_name, new Font(fontName, ConvertFontInt(tempConfig.PatientFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.PatientFontX), ConvertInt(tempConfig.PatientFontY));
                    g.DrawString(order.bed_number+"床", new Font(fontName, ConvertFontInt(tempConfig.BedFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.BedFontX), ConvertInt(tempConfig.BedFontY));
                    g.DrawString(order.age, new Font(fontName, ConvertFontInt(tempConfig.AgeFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.AgeFontX), ConvertInt(tempConfig.AgeFontY));
                    g.DrawString(order.sex, new Font(fontName, ConvertFontInt(tempConfig.GenderFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.GenderFontX), ConvertInt(tempConfig.GenderFontY));
                    g.DrawString(order.group_num, new Font(fontName, ConvertFontInt(tempConfig.GroupNumFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.GroupNumFontX), ConvertInt(tempConfig.GroupNumUnFontY));
                    g.DrawString( $"{order.use_frequency}({order.freq_counter_sub})", new Font(fontName, ConvertFontInt(tempConfig.UserFrequentFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.UserFrequentFontX), ConvertInt(tempConfig.UserFrequentFontY));
                    g.DrawString(order.is_print_snv, new Font(fontName, ConvertFontInt(tempConfig.is_print_snvFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.is_print_snvFontX), ConvertInt(tempConfig.is_print_snvFontY));
                    g.DrawString(order.use_date, new Font(fontName, ConvertFontInt(tempConfig.DateFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.DateFontX), ConvertInt(tempConfig.DateFontY));

                    g.DrawString(order.pass_remark, new Font(fontName, ConvertFontInt(tempConfig.RemarkFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.RemarkFontX), ConvertInt(tempConfig.RemarkFontY));
                    g.DrawString(order.usage_name, new Font(fontName, ConvertFontInt(tempConfig.UsageNameFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.UsageNameFontX), ConvertInt(tempConfig.UsageNameFontY));

                    g.DrawString("审方:", new Font(fontName, ConvertFontInt(tempConfig.ExamineFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.ExamineFontX), ConvertInt(tempConfig.ExamineFontY));
                    g.DrawString("摆药:", new Font(fontName, ConvertFontInt(tempConfig.SortFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.SortFontX), ConvertInt(tempConfig.SortFontY));
                    g.DrawString("配药:", new Font(fontName, ConvertFontInt(tempConfig.DispensingFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.DispensingFontX), ConvertInt(tempConfig.DispensingFontY));
                    g.DrawString("核对:", new Font(fontName, ConvertFontInt(tempConfig.CheckFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.CheckFontX), ConvertInt(tempConfig.CheckFontY));
                    g.DrawString("复核:", new Font(fontName, ConvertFontInt(tempConfig.ReviewFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.ReviewFontX), ConvertInt(tempConfig.ReviewFontY));
                    g.DrawString("给药:", new Font(fontName, ConvertFontInt(tempConfig.DoseFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.DoseFontX), ConvertInt(tempConfig.DoseFontY));



                    g.DrawString("1/1", new Font(fontName, ConvertFontInt(tempConfig.PageNumFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.PageNumFontX), ConvertInt(tempConfig.PageNumFontY));

                    g.DrawLine(new System.Drawing.Pen(bush), new System.Drawing.Point(ConvertInt(tempConfig.SplitX), ConvertInt(tempConfig.SplitY)), new System.Drawing.Point(ConvertInt(tempConfig.SplitX + tempConfig.SplitWidth), ConvertInt(tempConfig.SplitY)));

                    g.DrawString(order.patient_name, new Font(fontName, ConvertFontInt(tempConfig.PatientFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.PatientFontX), ConvertInt(tempConfig.PatientFontY));
                    g.DrawString("男", new Font(fontName, ConvertFontInt(tempConfig.GenderFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.GenderFontX), ConvertInt(tempConfig.GenderFontY));
                    g.DrawString(string.Format("[{0}]", order.zone), new Font(fontName, ConvertFontInt(tempConfig.SerialNumberFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.SerialNumberFontX), ConvertInt(tempConfig.SerialNumberFontY));

                    g.DrawLine(new System.Drawing.Pen(bush), new System.Drawing.Point(ConvertInt(tempConfig.Split2X), ConvertInt(tempConfig.Split2Y)), new System.Drawing.Point(ConvertInt(tempConfig.Split2X + tempConfig.Split2Width), ConvertInt(tempConfig.Split2Y)));

                    g.DrawString("药品名称", new Font(fontName, ConvertFontInt(tempConfig.DrugsTitleFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.DrugsTitleFontX), ConvertInt(tempConfig.DrugsTitleFontY));
                    g.DrawString("数量", new Font(fontName, ConvertFontInt(tempConfig.UseTitleFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.UseTitleFontX), ConvertInt(tempConfig.UseTitleFontY));

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
                        //数量
                        g.DrawString(drugs[i].use_count, new Font(fontName, ConvertFontInt(tempConfig.UseValueFontSize)), bush, u_x, u_y);

                        // 只修改Y轴，向下平铺
                        y += (int)height + margin;
                        u_y += (int)height + margin;
                    }
                    #endregion

                    //g.DrawString(string.Format("处方医生：{0}", order.doctor_name), new Font(fontName, ConvertFontInt(tempConfig.DoctorFontSize)), bush, ConvertInt(tempConfig.DoctorFontX), ConvertInt(tempConfig.DoctorFontY));
                    //g.DrawString(string.Format("备注：{0}", order.pass_remark), new Font(fontName, ConvertFontInt(tempConfig.RemarkFontSize)), bush, ConvertInt(tempConfig.RemarkFontX), ConvertInt(tempConfig.RemarkFontY));
                    //g.DrawString(string.Format("滴速：{0}   {1}   qd(8点)", order.ml_speed, order.usage_name), new Font(fontName, ConvertFontInt(tempConfig.SpeedFontSize)), bush, ConvertInt(tempConfig.SpeedFontX), ConvertInt(tempConfig.SpeedFontY));

                    //g.DrawLine(new System.Drawing.Pen(bush), new System.Drawing.Point(ConvertInt(tempConfig.Split3X), ConvertInt(tempConfig.Split3Y)), new System.Drawing.Point(ConvertInt(tempConfig.Split3X + tempConfig.Split3Width), ConvertInt(tempConfig.Split3Y)));

                    //g.DrawString(string.Format("审核：{0}", order.checker), new Font(fontName, ConvertFontInt(tempConfig.ExamineFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.ExamineFontX), ConvertInt(tempConfig.ExamineFontY));
                    //g.DrawString(string.Format("复审：{0}", ""), new Font(fontName, ConvertFontInt(tempConfig.ReviewFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.ReviewFontX), ConvertInt(tempConfig.ReviewFontY));
                    //g.DrawString(string.Format("排药：{0}", order.deliveryer), new Font(fontName, ConvertFontInt(tempConfig.SortFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.SortFontX), ConvertInt(tempConfig.SortFontY));
                    //g.DrawString(string.Format("配液：{0}", order.config_person), new Font(fontName, ConvertFontInt(tempConfig.DispensingFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.DispensingFontX), ConvertInt(tempConfig.DispensingFontY));
                    //g.DrawString("配液：___时___分", new Font(fontName, ConvertFontInt(tempConfig.DispensingDateFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(tempConfig.DispensingDateFontX), ConvertInt(tempConfig.DispensingDateFontY));

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
