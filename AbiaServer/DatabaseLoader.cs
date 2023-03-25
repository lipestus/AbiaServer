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
    public class DatabaseLoader
    {
        public ISqlDataAccess Db { get; private set; }
        public UserData UserData { get; private set; }
        public WorldData WorldData { get; private set; }
        private IConfigurationRoot _config;

        public DatabaseLoader(IConfigurationRoot config)
        {
            _config = config;
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            Db = new SqlDataAccess(_config);
            Db.OnDatabaseError += OnDatabaseError;
            UserData = new UserData(Db);
            WorldData = new WorldData(Db);
        }

        private void OnDatabaseError(string errorMessage)
        {
            // Handle the database error here or log it
        }
    }
}
