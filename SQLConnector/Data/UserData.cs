using SQLConnector.DbAccess;
using SQLConnector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLConnector.Data
{
    public class UserData : IUserData
    {
        private readonly ISqlDataAccess _db;

        public UserData(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<IEnumerable<UserModel>> GetUsers() => _db.LoadData<UserModel, dynamic>("dbo.spUser_GetAll", new { });

        public async Task<UserModel> GetUser(int id)
        {
            var results = await _db.LoadData<UserModel, dynamic>("dbo.spUser_Get", new { Id = id });
            return results.FirstOrDefault();
        }

        public Task InsertUser(UserModel user) => _db.SaveData("dbo.spUser_Insert", new
        {
            user.AccountName,
            user.HashedPassword,
            user.Salt
        });

        public Task UpdateUser(UserModel user) => _db.SaveData("dbo.spUSer_Update", user);

        public Task DeleteUser(int id) => _db.SaveData("dbo.spUser_Delete", new { Id = id });

        public async Task<UserModel> GetUserByLogin(string accountName, string hashedPassword)
        {
            var results = await _db.LoadData<UserModel, dynamic>("dbo.spUser_GetByLogin", new { AccountName = accountName, HashedPassword = hashedPassword });
            return results.FirstOrDefault();
        }

        public async Task<UserModel> GetUserByName(string accountName)
        {
            var results = await _db.LoadData<UserModel, dynamic>("dbo.spUser_GetUserName", new { AccountName = accountName });
            return results.FirstOrDefault();
        }
    }
}
