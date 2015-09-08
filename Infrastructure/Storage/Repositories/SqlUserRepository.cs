using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Storage.Classes;

namespace Infrastructure.Storage.Repositories
{
    public class SqlUserRepository : IUserRepository
    {
        private readonly SqlServerUserStorage _userStorage;
        private readonly SqlServerStorageProvider _db;

        public SqlUserRepository()
        {
            _userStorage = new SqlServerUserStorage();
            _db = new SqlServerStorageProvider();
        }

        public User GetById(int id)
        {
            var rawUser = _userStorage.GetUserById(id);
            return rawUser != null ? RawUser.CreateReal(rawUser) : null;
        }

        public IList<User> Get(IList<int> ids)
        {
            var rawUsers = _userStorage.GetUserList(ids);
            return rawUsers.Select(RawUser.CreateReal).OrderBy(o => o.DisplayName).ToList();
        }

        public IList<int> Search()
        {
            return GetIds();
        }

        public IList<int> Search(string nameOrEmail)
        {
            var userId = GetIdByNameOrEmail(nameOrEmail);
            if(userId.HasValue)
                return new List<int>{userId.Value};
            return new List<int>();
        }

        public bool Save(User user)
        {
            var rawUser = RawUser.Create(user);
            return _userStorage.UpdateUser(rawUser);
        }

        public int Add(User user)
        {
            const string sql = "INSERT INTO [user] (UserName, DisplayName, Email, RoleId, Password, Salt) VALUES (@userName, @displayName, @email, 1, @password, @salt) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            var parameters = new List<SimpleSqlParameter>
		        {
		            new SimpleSqlParameter("@userName", user.UserName),
		            new SimpleSqlParameter("@displayName", user.DisplayName),
		            new SimpleSqlParameter("@email", user.Email),
		            new SimpleSqlParameter("@password", user.EncryptedPassword),
		            new SimpleSqlParameter("@salt", user.Salt)
		        };
            return _db.ExecuteInsert(sql, parameters);
        }
        
        private int? GetIdByNameOrEmail(string nameOrEmail)
        {
            return _userStorage.GetUserIdByNameOrEmail(nameOrEmail);
        }

        private IList<int> GetIds()
        {
            return _userStorage.GetUserIdList();
        }
    }
}
