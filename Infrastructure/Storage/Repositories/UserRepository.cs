using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Infrastructure.Storage.SqlDb;
using JetBrains.Annotations;

namespace Infrastructure.Storage.Repositories
{
    public class UserRepository : ApiRepository, IUserRepository
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

        public void ChangePassword(string oldPassword, string newPassword, string repeat)
        {
            var apiChangePassword = new ApiChangePassword(oldPassword, newPassword, repeat);
            _api.Post(Url.ChangePassword, apiChangePassword);
        }

        public class ApiChangePassword
        {
            [UsedImplicitly]
            public string OldPassword { get; set; }
            [UsedImplicitly]
            public string NewPassword { get; set; }
            [UsedImplicitly]
            public string Repeat { get; set; }

            public ApiChangePassword(string oldPassword, string newPassword, string repeat)
            {
                OldPassword = oldPassword;
                NewPassword = newPassword;
                Repeat = repeat;
            }
        }
    }
}