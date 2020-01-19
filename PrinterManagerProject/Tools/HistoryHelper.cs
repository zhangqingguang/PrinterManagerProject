using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterManagerProject.Tools
{
    /// <summary>
    /// 
    /// </summary>
    public class SummaryCountModel
    {
        /// <summary>
        /// 总液体数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 已贴签总数
        /// </summary>
        public int PrintedTotalCount { get; set; }
        /// <summary>
        /// 未贴签总数
        /// </summary>
        public int NotPrintTotalCount { get; set; }
        /// <summary>
        /// 已自动贴签总数
        /// </summary>
        public int AutoCount { get; set; }
        /// <summary>
        /// 已人工贴签总数
        /// </summary>
        public int ManualCount { get; set; }
        /// <summary>
        /// 贴签失败总数
        /// </summary>
        public int FailCount { get; set; }
    }

    public class HistoryHelper
    {
        public static string configPath = AppDomain.CurrentDomain.BaseDirectory + @"Config\";
        public static string fileName = "count.config";

        public SummaryCountModel GetHistory()
        {
            SummaryCountModel model;
            string path = configPath + fileName;
            try
            {
                if (File.Exists(path))
                {
                    string config = File.ReadAllText(path);
                    model = JsonConvert.DeserializeObject<SummaryCountModel>(config);
                }
                else
                {
                    model = new SummaryCountModel();
                }
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool SaveConfig(SummaryCountModel model)
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
    }
}
