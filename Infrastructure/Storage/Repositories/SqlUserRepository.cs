using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Infrastructure.Storage.Cache;
using Infrastructure.Storage.Classes;

namespace Infrastructure.Storage.Repositories
{
    public class SqlUserRepository : IUserRepository
    {
        private readonly SqlServerUserStorage _userStorage;
        private readonly ICacheContainer _cacheContainer;
        private readonly SqlServerStorageProvider _db;

        public SqlUserRepository(
            SqlServerUserStorage userStorage,
            ICacheContainer cacheContainer)
        {
            _userStorage = userStorage;
            _cacheContainer = cacheContainer;
            _db = new SqlServerStorageProvider();
        }

        public User GetById(int id)
        {
            var cacheKey = CacheKeyProvider.UserKey(id);
            return _cacheContainer.GetAndStore(() => GetByIdUncached(id), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        public User GetByNameOrEmail(string nameOrEmail)
        {
            var userId = GetIdByNameOrEmail(nameOrEmail);
            return userId.HasValue ? GetById(userId.Value) : null;
        }

        public IList<User> GetList()
        {
            var ids = GetIds();
            var users = _cacheContainer.GetEachAndStore(GetListUncached, TimeSpan.FromMinutes(CacheTime.Long), ids);
            return users.OrderBy(o => o.DisplayName).ToList();
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

        private User GetByIdUncached(int id)
        {
            var rawUser = _userStorage.GetUserById(id);
            return rawUser != null ? RawUser.CreateReal(rawUser) : null;
        }

        private IList<User> GetListUncached(IList<int> ids)
        {
            var rawUsers = _userStorage.GetUserList(ids);
            return rawUsers.Select(RawUser.CreateReal).ToList();
        }

        private int? GetIdByNameOrEmail(string nameOrEmail)
        {
            var cacheKey = CacheKeyProvider.UserIdByNameOrEmailKey(nameOrEmail);
            return _cacheContainer.GetAndStore(() => _userStorage.GetUserIdByNameOrEmail(nameOrEmail), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        private IList<int> GetIds()
        {
            var cacheKey = CacheKeyProvider.UserIdsKey();
            return _cacheContainer.GetAndStore(() => _userStorage.GetUserIdList(), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }
    }
}
