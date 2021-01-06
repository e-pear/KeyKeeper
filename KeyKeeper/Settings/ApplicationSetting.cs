using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Settings
{
    public static class ApplicationSetting
    {
        public static string GetBuildInfoSetting()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            return config.AppSettings.Settings["Build"].Value;
        }
    }
}
