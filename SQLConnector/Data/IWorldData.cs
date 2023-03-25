using SQLConnector.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SQLConnector.Data
{
    public interface IWorldData
    {
        Task<IEnumerable<WorldModel>> GetWorlds();
    }
}