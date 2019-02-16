using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterManagerProject.Tools
{
    public class HistoryModel
    {
        public int AllCount { get; set; }
        public int AutoCount { get; set; }
        public int HandlerCount { get; set; }
    }

    public class HistoryHelper
    {
        public static string configPath = AppDomain.CurrentDomain.BaseDirectory + @"Config\";
        public static string fileName = "count.config";

        public HistoryModel GetHistory()
        {
            HistoryModel model;
            string path = configPath + fileName;
            try
            {
                if (File.Exists(path))
                {
                    string config = File.ReadAllText(path);
                    model = JsonConvert.DeserializeObject<HistoryModel>(config);
                }
                else
                {
                    model = new HistoryModel();
                }
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool SaveConfig(HistoryModel model)
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
