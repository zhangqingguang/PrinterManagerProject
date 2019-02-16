using PrinterManagerProject.Models;
using PrinterManagerProject.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using ZXing;
using ZXing.PDF417;

namespace PrinterManagerProject
{
    /// <summary>
    /// TestWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();

            // ViewBarCode();
            ViewCard();
        }

        private void ViewBarCode()
        {
            PrintTemplateModel model = new PrintTemplateHelper().GetConfig();
            if (model == null)
            {
                MessageBox.Show("打印模板参数获取失败！");
                return;
            }

            var paperWidth = model.PageWidth * 3;
            var paperHeight = model.PageHeight * 3;
            Bitmap image = new Bitmap(paperWidth, paperHeight);
            Graphics g = Graphics.FromImage(image);

            System.Drawing.Brush bush = new SolidBrush(System.Drawing.Color.Black);//填充的颜色

            try
            {
                //消除锯齿
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.PageUnit = GraphicsUnit.Pixel;
                //清空图片背景颜色
                g.Clear(System.Drawing.Color.White);

                var code = "1231125-102-23";

                #region PDF417
                PDF417EncodingOptions pdf_options = new PDF417EncodingOptions
                {
                    Margin = 0,
                    DisableECI = true,
                    CharacterSet = "UTF-8",
                    Width = ConvertInt(model.BarCodeWidth),
                    Height = ConvertInt(model.BarCodeHeight),
                    PureBarcode = false
                };
                var pdf417Writer = new ZXing.BarcodeWriter();
                pdf417Writer.Format = BarcodeFormat.PDF_417;
                pdf417Writer.Options = pdf_options;
                #endregion

                Bitmap bmp = pdf417Writer.Write(code);

                // Bitmap bmp = CreateImg.GenerateBitmap(code, 5, 5);
                g.DrawImage(bmp, ConvertInt(model.BarCodeX), ConvertInt(model.BarCodeY), ConvertInt(model.BarCodeWidth), ConvertInt(model.BarCodeHeight));

                IntPtr myImagePtr = image.GetHbitmap();     //创建GDI对象，返回指针
                BitmapSource imgsource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(myImagePtr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());  //创建imgSource

                DeleteObject(myImagePtr);

                imgTest.Source = imgsource;
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }

        }

        private void ViewCard()
        {
            PrintTemplateModel model = new PrintTemplateHelper().GetConfig();
            if (model == null)
            {
                MessageBox.Show("打印模板参数获取失败！");
                return;
            }

            //double multiple = 1.56;
            //string fontName = "SimSun";

            //Bitmap image = new Bitmap(model.PageWidth, model.PageHeight);
            //Graphics g = Graphics.FromImage(image);
            //System.Drawing.Brush bush = new SolidBrush(System.Drawing.Color.Black);//填充的颜色

            //try
            //{
            //    //消除锯齿
            //    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //    //清空图片背景颜色
            //    g.Clear(System.Drawing.Color.White);

            //    var code = "00012311251";
            //    var barcodeData = new Pdf417lib().GetPDF417Auto(code, 2, 2);
            //    using (System.IO.MemoryStream ms = new System.IO.MemoryStream(barcodeData))
            //    {
            //        using (System.Drawing.Imaging.Metafile mf = new System.Drawing.Imaging.Metafile(ms))
            //        {
            //            g.DrawImage(mf, model.BarCodeX, model.BarCodeY, model.BarCodeWidth, model.BarCodeHeight);
            //        }
            //    }

            //    g.DrawString("普通外科二病区", new Font(fontName, Convert.ToInt32(Math.Ceiling(model.AreaFontSize / multiple)), System.Drawing.FontStyle.Bold), bush, model.AreaFontX, model.AreaFontY);
            //    g.DrawString("2018-11-12补", new Font(fontName, Convert.ToInt32(Math.Ceiling(model.DateFontSize / multiple)), System.Drawing.FontStyle.Bold), bush, model.DateFontX, model.DateFontY);
            //    g.DrawString("1/1", new Font(fontName, Convert.ToInt32(Math.Ceiling(model.PageNumFontSize / multiple)), System.Drawing.FontStyle.Bold), bush, model.PageNumFontX, model.PageNumFontY);
            //    g.DrawLine(new System.Drawing.Pen(bush), new System.Drawing.Point(model.SplitX, model.SplitY), new System.Drawing.Point(model.SplitX + model.SplitWidth, model.SplitY));
            //    g.DrawString(code, new Font(fontName, Convert.ToInt32(Math.Ceiling(model.DoctorAdviceFontSize / multiple)), System.Drawing.FontStyle.Bold), bush, model.DoctorAdviceFontX, model.DoctorAdviceFontY);
            //    g.DrawString("31床", new Font(fontName, Convert.ToInt32(Math.Ceiling(model.BedFontSize / multiple)), System.Drawing.FontStyle.Bold), bush, model.BedFontX, model.BedFontY);
            //    g.DrawString("史新平", new Font(fontName, Convert.ToInt32(Math.Ceiling(model.PatientFontSize / multiple)), System.Drawing.FontStyle.Bold), bush, model.PatientFontX, model.PatientFontY);
            //    g.DrawString("男", new Font(fontName, Convert.ToInt32(Math.Ceiling(model.GenderFontSize / multiple)), System.Drawing.FontStyle.Bold), bush, model.GenderFontX, model.GenderFontY);
            //    g.DrawString("1批", new Font(fontName, Convert.ToInt32(Math.Ceiling(model.BatchNumberFontSize / multiple)), System.Drawing.FontStyle.Bold), bush, model.BatchNumberFontX, model.BatchNumberFontY);
            //    g.DrawString("[C-4]-1", new Font(fontName, Convert.ToInt32(Math.Ceiling(model.SerialNumberFontSize / multiple)), System.Drawing.FontStyle.Bold), bush, model.SerialNumberFontX, model.SerialNumberFontY);
            //    g.DrawLine(new System.Drawing.Pen(bush), new System.Drawing.Point(model.Split2X, model.Split2Y), new System.Drawing.Point(model.Split2X + model.Split2Width, model.Split2Y));
            //    g.DrawString("药品名称", new Font(fontName, Convert.ToInt32(Math.Ceiling(model.DrugsTitleFontSize / multiple)), System.Drawing.FontStyle.Bold), bush, model.DrugsTitleFontX, model.DrugsTitleFontY);
            //    g.DrawString("5%葡萄糖注射液[100ml](非PVC双阀)", new Font(fontName, Convert.ToInt32(Math.Ceiling(model.DrugsContentFontSize / multiple))), bush, model.DrugsContentFontX, model.DrugsContentFontY);
            //    g.DrawString("用量", new Font(fontName, Convert.ToInt32(Math.Ceiling(model.UseTitleFontSize / multiple)), System.Drawing.FontStyle.Bold), bush, model.UseTitleFontX, model.UseTitleFontY);
            //    g.DrawString("100ml", new Font(fontName, Convert.ToInt32(Math.Ceiling(model.UseValueFontSize / multiple)), System.Drawing.FontStyle.Bold), bush, model.UseValueFontX, model.UseValueFontY);
            //    g.DrawString("处方医生：张慧", new Font(fontName, Convert.ToInt32(Math.Ceiling(model.DoctorFontSize / multiple))), bush, model.DoctorFontX, model.DoctorFontY);
            //    g.DrawString("备注：", new Font(fontName, Convert.ToInt32(Math.Ceiling(model.RemarkFontSize / multiple))), bush, model.RemarkFontX, model.RemarkFontY);
            //    g.DrawString("滴速：40滴/分   静脉续滴   qd(8点)", new Font(fontName, Convert.ToInt32(Math.Ceiling(model.SpeedFontSize / multiple))), bush, model.SpeedFontX, model.SpeedFontY);
            //    g.DrawLine(new System.Drawing.Pen(bush), new System.Drawing.Point(model.Split3X, model.Split3Y), new System.Drawing.Point(model.Split3X + model.Split3Width, model.Split3Y));
            //    g.DrawString("审核：焦媛媛", new Font(fontName, Convert.ToInt32(Math.Ceiling(model.ExamineFontSize / multiple)), System.Drawing.FontStyle.Bold), bush, model.ExamineFontX, model.ExamineFontY);
            //    g.DrawString("复审：", new Font(fontName, Convert.ToInt32(Math.Ceiling(model.ReviewFontSize / multiple)), System.Drawing.FontStyle.Bold), bush, model.ReviewFontX, model.ReviewFontY);
            //    g.DrawString("排药：牛捷", new Font(fontName, Convert.ToInt32(Math.Ceiling(model.SortFontSize / multiple)), System.Drawing.FontStyle.Bold), bush, model.SortFontX, model.SortFontY);
            //    g.DrawString("配液：", new Font(fontName, Convert.ToInt32(Math.Ceiling(model.DispensingFontSize / multiple)), System.Drawing.FontStyle.Bold), bush, model.DispensingFontX, model.DispensingFontY);
            //    g.DrawString("配液：___时___分", new Font(fontName, Convert.ToInt32(Math.Ceiling(model.DispensingDateFontSize / multiple)), System.Drawing.FontStyle.Bold), bush, model.DispensingDateFontX, model.DispensingDateFontY);

            string fontName = "SimSun";

            var paperWidth = model.PageWidth * 3;
            var paperHeight = model.PageHeight * 3;
            Bitmap image = new Bitmap(paperWidth, paperHeight);
            Graphics g = Graphics.FromImage(image);

            System.Drawing.Brush bush = new SolidBrush(System.Drawing.Color.Black);//填充的颜色

            try
            {
                //消除锯齿
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.PageUnit = GraphicsUnit.Pixel;
                //清空图片背景颜色
                g.Clear(System.Drawing.Color.White);

                var code = "1231125-102-23";
                #region PDF417
                PDF417EncodingOptions pdf_options = new PDF417EncodingOptions
                {
                    Margin = 0,
                    DisableECI = true,
                    CharacterSet = "UTF-8",
                    Width = ConvertInt(model.BarCodeWidth),
                    Height = ConvertInt(model.BarCodeHeight),
                    PureBarcode = false
                };
                var pdf417Writer = new ZXing.BarcodeWriter();
                pdf417Writer.Format = BarcodeFormat.PDF_417;
                pdf417Writer.Options = pdf_options;
                #endregion

                Bitmap bmp = pdf417Writer.Write(code);
                g.DrawImage(bmp, ConvertInt(model.BarCodeX), ConvertInt(model.BarCodeY), ConvertInt(model.BarCodeWidth), ConvertInt(model.BarCodeHeight));

                g.DrawString("普通外科二病区", new Font(fontName, ConvertFontInt(model.AreaFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.AreaFontX), ConvertInt(model.AreaFontY));
                g.DrawString("2018-11-12补", new Font(fontName, ConvertFontInt(model.DateFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.DateFontX), ConvertInt(model.DateFontY));
                g.DrawString("1/1", new Font(fontName, ConvertFontInt(model.PageNumFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.PageNumFontX), ConvertInt(model.PageNumFontY));

                g.DrawLine(new System.Drawing.Pen(bush), new System.Drawing.Point(ConvertInt(model.SplitX), ConvertInt(model.SplitY)), new System.Drawing.Point(ConvertInt(model.SplitX + model.SplitWidth), ConvertInt(model.SplitY)));

                g.DrawString(code, new Font(fontName, ConvertFontInt(model.DoctorAdviceFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.DoctorAdviceFontX), ConvertInt(model.DoctorAdviceFontY));
                g.DrawString("31床", new Font(fontName, ConvertFontInt(model.BedFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.BedFontX), ConvertInt(model.BedFontY));
                g.DrawString("史新平", new Font(fontName, ConvertFontInt(model.PatientFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.PatientFontX), ConvertInt(model.PatientFontY));
                g.DrawString("男", new Font(fontName, ConvertFontInt(model.GenderFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.GenderFontX), ConvertInt(model.GenderFontY));
                g.DrawString("1批", new Font(fontName, ConvertFontInt(model.BatchNumberFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.BatchNumberFontX), ConvertInt(model.BatchNumberFontY));
                g.DrawString("[C-4]-1", new Font(fontName, ConvertFontInt(model.SerialNumberFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.SerialNumberFontX), ConvertInt(model.SerialNumberFontY));

                g.DrawLine(new System.Drawing.Pen(bush), new System.Drawing.Point(ConvertInt(model.Split2X), ConvertInt(model.Split2Y)), new System.Drawing.Point(ConvertInt(model.Split2X + model.Split2Width), ConvertInt(model.Split2Y)));

                g.DrawString("药品名称", new Font(fontName, ConvertFontInt(model.DrugsTitleFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.DrugsTitleFontX), ConvertInt(model.DrugsTitleFontY));
                g.DrawString("5%葡萄糖注射液[100ml](非PVC双阀)", new Font(fontName, ConvertFontInt(model.DrugsContentFontSize)), bush, ConvertInt(model.DrugsContentFontX), ConvertInt(model.DrugsContentFontY));
                g.DrawString("用量", new Font(fontName, ConvertFontInt(model.UseTitleFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.UseTitleFontX), ConvertInt(model.UseTitleFontY));
                g.DrawString("100ml", new Font(fontName, ConvertFontInt(model.UseValueFontSize)), bush, ConvertInt(model.UseValueFontX), ConvertInt(model.UseValueFontY));
                g.DrawString("处方医生：张慧", new Font(fontName, ConvertFontInt(model.DoctorFontSize)), bush, ConvertInt(model.DoctorFontX), ConvertInt(model.DoctorFontY));
                g.DrawString("备注：", new Font(fontName, ConvertFontInt(model.RemarkFontSize)), bush, ConvertInt(model.RemarkFontX), ConvertInt(model.RemarkFontY));
                g.DrawString("滴速：40滴/分   静脉续滴   qd(8点)", new Font(fontName, ConvertFontInt(model.SpeedFontSize)), bush, ConvertInt(model.SpeedFontX), ConvertInt(model.SpeedFontY));

                g.DrawLine(new System.Drawing.Pen(bush), new System.Drawing.Point(ConvertInt(model.Split3X), ConvertInt(model.Split3Y)), new System.Drawing.Point(ConvertInt(model.Split3X + model.Split3Width), ConvertInt(model.Split3Y)));

                g.DrawString("审核：焦媛媛", new Font(fontName, ConvertFontInt(model.ExamineFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.ExamineFontX), ConvertInt(model.ExamineFontY));
                g.DrawString("复审：", new Font(fontName, ConvertFontInt(model.ReviewFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.ReviewFontX), ConvertInt(model.ReviewFontY));
                g.DrawString("排药：牛捷", new Font(fontName, ConvertFontInt(model.SortFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.SortFontX), ConvertInt(model.SortFontY));
                g.DrawString("配液：", new Font(fontName, ConvertFontInt(model.DispensingFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.DispensingFontX), ConvertInt(model.DispensingFontY));
                g.DrawString("配液：___时___分", new Font(fontName, ConvertFontInt(model.DispensingDateFontSize), System.Drawing.FontStyle.Bold), bush, ConvertInt(model.DispensingDateFontX), ConvertInt(model.DispensingDateFontY));


                image.Save(AppDomain.CurrentDomain.BaseDirectory + @"Config\test" + DateTime.Now.Millisecond + ".jpg");

                IntPtr myImagePtr = image.GetHbitmap();     //创建GDI对象，返回指针
                BitmapSource imgsource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(myImagePtr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());  //创建imgSource

                DeleteObject(myImagePtr);

                imgTest.Source = imgsource;
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        private int ConvertFontInt(double num)
        {
            double multiple = 2;
            return Convert.ToInt32(num * multiple);
        }
        private int ConvertInt(double num)
        {
            double multiple = 3;
            return Convert.ToInt32(num * multiple);
        }


        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Owner.Show();
        }
    }
}
