using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Core.Classes;
using Core.Repositories;
using Infrastructure.Caching;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.Factories;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private const string UserIdCacheKey = "UserId";

        private readonly IUserStorage _userStorage;
        private readonly IUserFactory _userFactory;
        private readonly IRawUserFactory _rawUserFactory;
        private readonly ICacheContainer _cacheContainer;
        private readonly ICacheKeyProvider _cacheKeyProvider;
        private readonly ICacheBuster _cacheBuster;

        public UserRepository(
            IUserStorage userStorage,
            IUserFactory userFactory,
            IRawUserFactory rawUserFactory,
            ICacheContainer cacheContainer,
            ICacheKeyProvider cacheKeyProvider,
            ICacheBuster cacheBuster)
        {
            _userStorage = userStorage;
            _userFactory = userFactory;
            _rawUserFactory = rawUserFactory;
            _cacheContainer = cacheContainer;
            _cacheKeyProvider = cacheKeyProvider;
            _cacheBuster = cacheBuster;
        }

        public User GetUserById(int id)
        {
            var cacheKey = _cacheKeyProvider.SingleUserKey(id);
            var cached = _cacheContainer.Get<User>(cacheKey);
            if (cached != null)
            {
                return cached;
            }
            var rawUser = _userStorage.GetUserById(id);
            var uncached = rawUser != null ? _userFactory.Create(rawUser) : null;
            if (uncached != null)
            {
                _cacheContainer.FakeInsert(cacheKey, uncached, TimeSpan.FromMinutes(CacheTime.Long));
            }
            return uncached;
        }

        public User GetUserByEmail(string email)
        {
            var userId = GetUserIdByEmail(email);
            return userId.HasValue ? GetUserById(userId.Value) : null;
        }

        private int? GetUserIdByEmail(string email)
        {
            var cacheKey = _cacheContainer.ConstructCacheKey(UserIdCacheKey, "email", email);
            var cached = _cacheContainer.Get<string>(cacheKey);
            if (cached != null)
            {
                return int.Parse(cached);
            }
            var uncached = _userStorage.GetUserIdByEmail(email);
            if (uncached.HasValue)
            {
                _cacheContainer.FakeInsert(cacheKey, uncached.Value.ToString(CultureInfo.InvariantCulture), TimeSpan.FromMinutes(CacheTime.Long));
            }
            return uncached;
        }

        private int? GetUserIdByToken(string token)
        {
            var cacheKey = _cacheContainer.ConstructCacheKey(UserIdCacheKey, "token", token);
            var cached = _cacheContainer.Get<string>(cacheKey);
            if (cached != null)
            {
                return int.Parse(cached);
            }
            var uncached = _userStorage.GetUserIdByToken(token);
            if (uncached.HasValue)
            {
                _cacheContainer.FakeInsert(cacheKey, uncached.Value.ToString(CultureInfo.InvariantCulture), TimeSpan.FromMinutes(CacheTime.Long));
            }
            return uncached;
        }

        public User GetUserByToken(string token)
        {
            var userId = GetUserIdByToken(token);
            return userId.HasValue ? GetUserById(userId.Value) : null;
        }

        private int? GetUserIdByName(string userName)
        {
            var cacheKey = _cacheContainer.ConstructCacheKey(UserIdCacheKey, "username", userName);
            var cached = _cacheContainer.Get<string>(cacheKey);
            if (cached != null)
            {
                return int.Parse(cached);
            }
            var uncached = _userStorage.GetUserIdByName(userName);
            if (uncached.HasValue)
            {
                _cacheContainer.FakeInsert(cacheKey, uncached.Value.ToString(CultureInfo.InvariantCulture), TimeSpan.FromMinutes(CacheTime.Long));
            }
            return uncached;
        }

        public User GetUserByName(string userName)
        {
            var userId = GetUserIdByName(userName);
            return userId.HasValue ? GetUserById(userId.Value) : null;
        }

        public User GetUserByCredentials(string userNameOrEmail, string password)
        {
            var userId = _userStorage.GetUserIdByCredentials(userNameOrEmail, password);
            return userId.HasValue ? GetUserById(userId.Value) : null;
        }

        public IList<User> GetAll()
        {
            var users = new List<User>();
            var userIds = GetUserIds();
            var uncachedIds = new List<int>();
            foreach (var id in userIds)
            {
                var cacheKey = _cacheKeyProvider.SingleUserKey(id);
                var cached = _cacheContainer.Get<User>(cacheKey);
                if (cached != null)
                {
                    users.Add(cached);
                }
                else
                {
                    uncachedIds.Add(id);
                }
            }

            if (uncachedIds.Count > 0)
            {
                var rawUsers = _userStorage.GetUsers(uncachedIds);
                var newUsers = rawUsers.Select(_userFactory.Create).ToList();
                foreach (var user in newUsers)
                {
                    var cacheKey = _cacheKeyProvider.SingleUserKey(user.Id);
                    _cacheContainer.FakeInsert(cacheKey, user, TimeSpan.FromMinutes(CacheTime.Long));
                }
                users.AddRange(newUsers);
            }

            return users.OrderBy(o => o.DisplayName).ToList();
        }

        private IEnumerable<int> GetUserIds()
        {
            var cacheKey = _cacheKeyProvider.UserIdsKey();
            var cached = _cacheContainer.Get<List<int>>(cacheKey);
            if (cached != null)
            {
                return cached;
            }
            var uncached = _userStorage.GetUserIds();
            if (uncached != null)
            {
                _cacheContainer.FakeInsert(cacheKey, uncached, TimeSpan.FromMinutes(CacheTime.Long));
            }
            return uncached;
        }

        public bool UpdateUser(User user)
        {
            var rawUser = _rawUserFactory.Create(user);
            var updated = _userStorage.UpdateUser(rawUser);
            _cacheBuster.UserUpdated(user.Id);
            return updated;
        }

        public int AddUser(User user)
        {
            var rawUser = _rawUserFactory.Create(user);
            var id = _userStorage.AddUser(rawUser);
            _cacheBuster.UserAdded();
            return id;
        }

        public bool DeleteUser(User user)
        {
            throw new global::System.NotImplementedException();
        }

        public string GetSalt(string userNameOrEmail)
        {
            return _userStorage.GetSalt(userNameOrEmail);
        }

        public bool SetSalt(User user, string salt)
        {
            return _userStorage.SetSalt(user.UserName, salt);
        }

        public bool SetEncryptedPassword(User user, string encryptedPassword)
        {
            return _userStorage.SetEncryptedPassword(user.UserName, encryptedPassword);
        }

        public string GetToken(User user)
        {
            return _userStorage.GetToken(user.UserName);
        }

        public bool SetToken(User user, string token)
        {
            return _userStorage.SetToken(user.UserName, token);
        }
    }
}
