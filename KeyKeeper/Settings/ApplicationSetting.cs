using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Settings
{
    /// <summary>
    /// Simple setting class to "manage"... well application settings.
    /// </summary>
    public static class ApplicationSetting
    {
        /// <summary>
        /// Provide an build number information stored in appconfig.
        /// </summary>
        /// <returns></returns>
        public static string GetBuildInfoSetting()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            return config.AppSettings.Settings["Build"].Value;
        }
    }
}
