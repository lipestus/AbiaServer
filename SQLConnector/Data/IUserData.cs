using SQLConnector.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SQLConnector.Data
{
    public interface IUserData
    {
        Task<UserModel> GetUserByLogin(string accountName, string hashedPassword);
        Task DeleteUser(int id);
        Task<UserModel> GetUser(int id);
        Task<IEnumerable<UserModel>> GetUsers();
        Task InsertUser(UserModel user);
        Task UpdateUser(UserModel user);
        Task<UserModel> GetUserByName(string accountName);
    }
}