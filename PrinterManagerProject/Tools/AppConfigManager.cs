using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterManagerProject.Tools
{
    public class AppConfigManager
    {
        private string getConfig(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }


        public int GetInt(string key,int value = 0)
        {
            var config = getConfig(key);
            if (string.IsNullOrEmpty(config))
            {
                return value;
            }
            if(int.TryParse(config,out value))
            {

            }
            return value;
        }
        public bool GetBool(string key, bool value = false)
        {
            var config = getConfig(key);
            if (string.IsNullOrEmpty(config))
            {
                return value;
            }
            if (bool.TryParse(config, out value))
            {

            }
            return value;
        }
        public string GetString(string key, string value = "")
        {
            var config = getConfig(key);
            if (string.IsNullOrEmpty(config))
            {
                return value;
            }
            return config.ToString();
        }
    }
}
