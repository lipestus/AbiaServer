using System.Data.SqlClient;
using Dapper;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System;

namespace SQLConnector.DbAccess
{
 
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;
        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public event DatabaseErrorEventArgs OnDatabaseError;

        public async Task<IEnumerable<T>> LoadData<T, U>(
            string storedProcedure, U parameters, string conenctionId = "Default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("Default"));
            return await connection.QueryAsync<T>(storedProcedure, parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task SaveData<T>(string storedProcedure, T parameters, string conenctionId = "Default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("Default"));
            var dynamicParameters = new DynamicParameters(parameters);

            // Add the @ErrorMessage parameter as an output parameter
            dynamicParameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            // Execute the stored procedure
            await connection.ExecuteAsync(storedProcedure, dynamicParameters, commandType: CommandType.StoredProcedure);

            // Check the value of @ErrorMessage and raise an event if it is not empty
            string error = dynamicParameters.Get<string>("@ErrorMessage");
            if (!string.IsNullOrEmpty(error))
            {
                OnDatabaseError?.Invoke(error);
            }
        }
    }
}
