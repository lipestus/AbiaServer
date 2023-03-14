using System.Collections.Generic;
using System.Threading.Tasks;

namespace SQLConnector.DbAccess
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string conenctionId = "Default");
        Task SaveData<T>(string storedProcedure, T parameters, string conenctionId = "Default");
    }
}