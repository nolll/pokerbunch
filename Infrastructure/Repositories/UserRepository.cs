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
            var cacheKey = _cacheKeyProvider.UserKey(id);
            return _cacheContainer.GetAndStore(() => GetUserByIdUncached(id), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        private User GetUserByIdUncached(int id)
        {
            var rawUser = _userStorage.GetUserById(id);
            return rawUser != null ? _userFactory.Create(rawUser) : null;
        }

        public User GetUserByToken(string token)
        {
            var userId = GetUserIdByToken(token);
            return userId.HasValue ? GetUserById(userId.Value) : null;
        }

        public User GetUserByNameOrEmail(string userNameOrEmail)
        {
            var userId = GetUserIdByNameOrEmail(userNameOrEmail);
            return userId.HasValue ? GetUserById(userId.Value) : null;
        }

        public IList<User> GetAll()
        {
            var users = new List<User>();
            var userIds = GetUserIds();
            var uncachedIds = new List<int>();
            foreach (var id in userIds)
            {
                var cacheKey = _cacheKeyProvider.UserKey(id);
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
                    var cacheKey = _cacheKeyProvider.UserKey(user.Id);
                    _cacheContainer.Insert(cacheKey, user, TimeSpan.FromMinutes(CacheTime.Long));
                }
                users.AddRange(newUsers);
            }

            return users.OrderBy(o => o.DisplayName).ToList();
        }

        public bool UpdateUser(User user)
        {
            var rawUser = _rawUserFactory.Create(user);
            var updated = _userStorage.UpdateUser(rawUser);
            _cacheBuster.UserUpdated(user);
            return updated;
        }

        public int AddUser(User user)
        {
            var rawUser = _rawUserFactory.Create(user);
            var id = _userStorage.AddUser(rawUser);
            _cacheBuster.UserAdded();
            return id;
        }

        private int? GetUserIdByToken(string token)
        {
            var cacheKey = _cacheKeyProvider.UserIdByTokenKey(token);
            var cached = _cacheContainer.Get<string>(cacheKey);
            if (cached != null)
            {
                return int.Parse(cached);
            }
            var uncached = _userStorage.GetUserIdByToken(token);
            if (uncached.HasValue)
            {
                _cacheContainer.Insert(cacheKey, uncached.Value.ToString(CultureInfo.InvariantCulture), TimeSpan.FromMinutes(CacheTime.Long));
            }
            return uncached;
        }

        private int? GetUserIdByNameOrEmail(string nameOrEmail)
        {
            var cacheKey = _cacheKeyProvider.UserIdByNameOrEmailKey(nameOrEmail);
            var cached = _cacheContainer.Get<string>(cacheKey);
            if (cached != null)
            {
                return int.Parse(cached);
            }
            var uncached = _userStorage.GetUserIdByNameOrEmail(nameOrEmail);
            if (uncached.HasValue)
            {
                _cacheContainer.Insert(cacheKey, uncached.Value.ToString(CultureInfo.InvariantCulture), TimeSpan.FromMinutes(CacheTime.Long));
            }
            return uncached;
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
                _cacheContainer.Insert(cacheKey, uncached, TimeSpan.FromMinutes(CacheTime.Long));
            }
            return uncached;
        }

    }
}
