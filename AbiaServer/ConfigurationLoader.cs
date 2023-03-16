using Microsoft.Extensions.Configuration;
using SQLConnector.Data;
using SQLConnector.DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiaServer
{
    public class ConfigurationLoader
    {
        public IConfigurationRoot Config { get; private set; }
        public string ConnectionString { get; private set; }

        public ConfigurationLoader()
        {
            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            Config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            ConnectionString = Config.GetConnectionString("Default");
        }

    }
}
