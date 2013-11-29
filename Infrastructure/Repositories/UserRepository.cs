using System;
using System.Collections.Generic;
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

        public User GetById(int id)
        {
            var cacheKey = _cacheKeyProvider.UserKey(id);
            return _cacheContainer.GetAndStore(() => GetByIdUncached(id), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        public User GetByToken(string token)
        {
            var userId = GetIdByToken(token);
            return userId.HasValue ? GetById(userId.Value) : null;
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
            var rawUser = _rawUserFactory.Create(user);
            var updated = _userStorage.UpdateUser(rawUser);
            _cacheBuster.UserUpdated(user);
            return updated;
        }

        public int Add(User user)
        {
            var rawUser = _rawUserFactory.Create(user);
            var id = _userStorage.AddUser(rawUser);
            _cacheBuster.UserAdded();
            return id;
        }

        private User GetByIdUncached(int id)
        {
            var rawUser = _userStorage.GetUserById(id);
            return rawUser != null ? _userFactory.Create(rawUser) : null;
        }

        private IList<User> GetListUncached(IEnumerable<int> ids)
        {
            var rawUsers = _userStorage.GetUsers(ids);
            return rawUsers.Select(_userFactory.Create).ToList();
        }

        private int? GetIdByToken(string token)
        {
            var cacheKey = _cacheKeyProvider.UserIdByTokenKey(token);
            return _cacheContainer.GetAndStore(() => _userStorage.GetUserIdByToken(token), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        private int? GetIdByNameOrEmail(string nameOrEmail)
        {
            var cacheKey = _cacheKeyProvider.UserIdByNameOrEmailKey(nameOrEmail);
            return _cacheContainer.GetAndStore(() => _userStorage.GetUserIdByNameOrEmail(nameOrEmail), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        private IList<int> GetIds()
        {
            var cacheKey = _cacheKeyProvider.UserIdsKey();
            return _cacheContainer.GetAndStore(() => _userStorage.GetUserIds(), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

    }
}
