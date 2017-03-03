using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Infrastructure.Storage.SqlDb;

namespace Infrastructure.Storage.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlUserDb _userDb;
        private readonly ApiConnection _api;
        private readonly ICacheContainer _cacheContainer;

        public UserRepository(ApiConnection api, SqlServerStorageProvider db, ICacheContainer cacheContainer)
        {
            _userDb = new SqlUserDb(db);
            _api = api;
            _cacheContainer = cacheContainer;
        }

        public User GetById(string id)
        {
            return _cacheContainer.GetAndStore(_userDb.Get, id, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<User> Get(IList<string> ids)
        {
            return _cacheContainer.GetAndStore(_userDb.Get, ids, TimeSpan.FromMinutes(CacheTime.Long));
        }
        
        public IList<User> List()
        {
            var ids = _userDb.Find();
            return Get(ids);
        }

        public User GetByNameOrEmail(string nameOrEmail)
        {
            var ids = _userDb.Find(nameOrEmail);
            if (ids.Any())
                return GetById(ids.First());
            return null;
        }

        public void Update(User user)
        {
            _userDb.Update(user);
            _cacheContainer.Remove<User>(user.Id);
        }

        public string Add(User user, string password)
        {
            return _userDb.Add(user);
        }
    }
}