using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Infrastructure.Storage.SqlDb;

namespace Infrastructure.Storage.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlUserDb _userDb;
        private readonly ICacheContainer _cacheContainer;

        public UserRepository(SqlServerStorageProvider db, ICacheContainer cacheContainer)
        {
            _userDb = new SqlUserDb(db);
            _cacheContainer = cacheContainer;
        }

        public User Get(string id)
        {
            return _cacheContainer.GetAndStore(_userDb.Get, id, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<User> Get(IList<string> ids)
        {
            return _cacheContainer.GetAndStore(_userDb.Get, ids, TimeSpan.FromMinutes(CacheTime.Long));
        }
        
        public IList<string> Find()
        {
            return _userDb.Find();
        }

        public IList<string> Find(string nameOrEmail)
        {
            return _userDb.Find(nameOrEmail);
        }

        public void Update(User user)
        {
            _userDb.Update(user);
            _cacheContainer.Remove<User>(user.Id);
        }

        public string Add(User user)
        {
            return _userDb.Add(user);
        }
    }
}