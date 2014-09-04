using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Data.Cache;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Mappers;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserStorage _userStorage;
        private readonly ICacheContainer _cacheContainer;
        private readonly ICacheBuster _cacheBuster;

        public UserRepository(
            IUserStorage userStorage,
            ICacheContainer cacheContainer,
            ICacheBuster cacheBuster)
        {
            _userStorage = userStorage;
            _cacheContainer = cacheContainer;
            _cacheBuster = cacheBuster;
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
            var rawUser = RawUserFactory.Create(user);
            var updated = _userStorage.UpdateUser(rawUser);
            _cacheBuster.UserUpdated(user);
            return updated;
        }

        public int Add(User user)
        {
            var rawUser = RawUserFactory.Create(user);
            var id = _userStorage.AddUser(rawUser);
            _cacheBuster.UserAdded();
            return id;
        }

        private User GetByIdUncached(int id)
        {
            var rawUser = _userStorage.GetUserById(id);
            return rawUser != null ? UserDataMapper.Map(rawUser) : null;
        }

        private IList<User> GetListUncached(IList<int> ids)
        {
            var rawUsers = _userStorage.GetUserList(ids);
            return rawUsers.Select(UserDataMapper.Map).ToList();
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
