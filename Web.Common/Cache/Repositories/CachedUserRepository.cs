using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Web.Common.Cache.Repositories
{
    public class CachedBunchRepository : IBunchRepository
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICacheContainer _cacheContainer;

        public CachedBunchRepository(IBunchRepository bunchRepository, ICacheContainer cacheContainer)
        {
            _bunchRepository = bunchRepository;
            _cacheContainer = cacheContainer;
        }

        public Bunch Get(int id)
        {
            var cacheKey = CacheKeyProvider.ConstructCacheKey(typeof(Bunch), id);
            return _cacheContainer.GetAndStore(() => _bunchRepository.Get(id), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        public IList<Bunch> Get(IList<int> ids)
        {
            return _cacheContainer.GetAndStore(_bunchRepository.Get, TimeSpan.FromMinutes(CacheTime.Long), ids);
        }

        public IList<int> Search()
        {
            return _bunchRepository.Search();
        }

        public IList<int> Search(string slug)
        {
            return _bunchRepository.Search(slug);
        }

        public IList<int> Search(int userId)
        {
            return _bunchRepository.Search(userId);
        }

        public int Add(Bunch bunch)
        {
            return _bunchRepository.Add(bunch);
        }

        public bool Save(Bunch bunch)
        {
            return _bunchRepository.Save(bunch);
        }
    }

    public class CachedUserRepository : IUserRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly ICacheContainer _cacheContainer;

        public CachedUserRepository(IUserRepository userRepository, ICacheContainer cacheContainer)
        {
            _userRepository = userRepository;
            _cacheContainer = cacheContainer;
        }

        public User Get(int id)
        {
            var cacheKey = CacheKeyProvider.ConstructCacheKey(typeof(User), id);
            return _cacheContainer.GetAndStore(() => _userRepository.Get(id), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        public IList<User> Get(IList<int> ids)
        {
            return _cacheContainer.GetAndStore(_userRepository.Get, TimeSpan.FromMinutes(CacheTime.Long), ids);
        }
        
        public IList<int> Find()
        {
            return _userRepository.Find();
        }

        public IList<int> Find(string nameOrEmail)
        {
            return _userRepository.Find(nameOrEmail);
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