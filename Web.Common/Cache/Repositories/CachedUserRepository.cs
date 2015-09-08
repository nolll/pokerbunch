using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Web.Common.Cache.Repositories
{
    public class CachedUserRepository : IUserRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly ICacheContainer _cacheContainer;

        public CachedUserRepository(IUserRepository userRepository, ICacheContainer cacheContainer)
        {
            _userRepository = userRepository;
            _cacheContainer = cacheContainer;
        }

        public User GetById(int id)
        {
            var cacheKey = CacheKeyProvider.UserKey(id);
            return _cacheContainer.GetAndStore(() => _userRepository.GetById(id), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        public IList<User> Get(IList<int> ids)
        {
            return _cacheContainer.GetEachAndStore(_userRepository.Get, TimeSpan.FromMinutes(CacheTime.Long), ids);
        }
        
        public IList<int> Search()
        {
            return _userRepository.Search();
        }

        public IList<int> Search(string nameOrEmail)
        {
            return _userRepository.Search(nameOrEmail);
        }

        public bool Save(User user)
        {
            return _userRepository.Save(user);
        }

        public int Add(User user)
        {
            return _userRepository.Add(user);
        }
    }
}