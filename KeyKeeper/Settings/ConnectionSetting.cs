using KeyKeeper.Model.DataAccess;
using System.Configuration;

namespace KeyKeeper.Settings
{
    /// <summary>
    /// Simple setting class to "play" with connection string.
    /// </summary>
    public static class ConnectionSetting
    {
        /// <summary>
        /// Restores default conection string. 
        /// </summary>
        public static void RestoreDefaultConnectionString()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            string defaultConnectionString = "Server=DESKTOP;Database=KeyKeeperDataBase;User ID=KeyKeeperClient;password=KeyKeeperPassword";

            config.ConnectionStrings.ConnectionStrings["ContextOnLocalServer"].ConnectionString = defaultConnectionString;
            config.ConnectionStrings.ConnectionStrings["ContextOnLocalServer"].ProviderName = "System.Data.SqlClient";

            config.Save(ConfigurationSaveMode.Modified);
        }
        /// <summary>
        /// Replaces current connection string by one with given parameters.
        /// </summary>
        /// <param name="server">Database server name.</param>
        /// <param name="database">Database name itself.</param>
        /// <param name="userId">Client login/id.</param>
        /// <param name="password">Client password.</param>
        public static void SetNewConnectionString(string server, string database, string userId, string password)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            string newConnectionString = $"Server={server};Database={database};User ID={userId};password={password}";

            config.ConnectionStrings.ConnectionStrings["ContextOnLocalServer"].ConnectionString = newConnectionString;
            config.ConnectionStrings.ConnectionStrings["ContextOnLocalServer"].ProviderName = "System.Data.SqlClient"; ;

            config.Save(ConfigurationSaveMode.Modified);
        }
        /// <summary>
        /// Get current connection string.
        /// </summary>
        /// <returns>Connection string.</returns>
        public static string GetCurrentConnectionString()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            return config.ConnectionStrings.ConnectionStrings["ContextOnLocalServer"].ConnectionString;
        }
        /// <summary>
        /// Just checking if database exists.
        /// </summary>
        /// <returns>True if so.</returns>
        public static bool TestDatabaseExistance()
        {
            using (CompanyDbContext companyDb = new CompanyDbContext())
            {
                return companyDb.Database.Exists();
            }
        }
    }
}