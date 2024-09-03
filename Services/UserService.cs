using ApiDevBP.Database;
using ApiDevBP.Entities;
using Microsoft.Extensions.Options;
using SQLite;
using System.Reflection;

namespace ApiDevBP.Services
{
    public class UserService
    {
        private readonly SQLiteConnection _db;

        public UserService(IOptions<DatabaseSettings> dbSettings)
        {
            string localDb = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "localDb");
            _db = new SQLiteConnection(localDb);
            _db.CreateTable<UserEntity>();
        }

        public int SaveUser(UserEntity user)
        {
            return _db.Insert(user);
        }

        public IEnumerable<UserEntity> GetUsers()
        {
            return _db.Query<UserEntity>("Select * from Users");
        }

        public int DeleteUser(int userId)
        {
            return _db.Delete<UserEntity>(userId);
        }

        public int UpdateUser(UserEntity user)
        {
            return _db.Update(user);
        }
    }

}
