using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TrackerLibrary.DataAccess;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        public static IDataConnection Connection { get; private set; }

        /// <summary>
        /// Initializes database connection to either an SQL database or a textfile.
        /// </summary>
        /// <param name="db">Storage type. SQL or textfile.</param>
        public static void InitializeConnections(DatabaseType db)
        {
            if (db == DatabaseType.Sql)
            {
                SqlConnector sql = new SqlConnector();
                Connection = sql;
            }
            else if (db == DatabaseType.TextFile)
            {
                 TextConnector text = new TextConnector();
                Connection = text;
            }
        }

        /// <summary>
        /// Gets connection String from the appsettings.json file. 
        /// </summary>
        /// <returns>Connection string</returns>
        public static string CnnString()
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            string strConnection = builder.Build().GetConnectionString("Tournament");

            return strConnection;
        }

        /// <summary>
        /// Gets file save location from the appsettings.json file.
        /// </summary>
        /// <returns>file save path.</returns>
        public static string GetFilePath()
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            string filePath = builder.Build().GetSection("FileParameter").GetSection("filePath").Value;

            return filePath;
        }
    }
}
