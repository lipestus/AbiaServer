using SQLConnector.DbAccess;
using SQLConnector.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SQLConnector.Data
{
    public class WorldData : IWorldData
    {
        private readonly ISqlDataAccess _db;

        public WorldData(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<IEnumerable<WorldModel>> GetWorlds() => _db.LoadData<WorldModel, dynamic>("dbo.spWorld_GetAll", new { });
    }
}
