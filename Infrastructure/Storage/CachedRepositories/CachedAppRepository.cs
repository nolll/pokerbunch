using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Infrastructure.Storage.CachedRepositories
{
    public class CachedAppRepository : IAppRepository
    {
        private readonly IAppRepository _appRepository;
        private readonly ICacheContainer _cacheContainer;

        public CachedAppRepository(IAppRepository appRepository, ICacheContainer cacheContainer)
        {
            _appRepository = appRepository;
            _cacheContainer = cacheContainer;
        }
        
        public App Get(string id)
        {
            return _cacheContainer.GetAndStore(_appRepository.Get, id, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<App> GetList(IList<string> ids)
        {
            return _cacheContainer.GetAndStore(_appRepository.GetList, ids, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<string> Find()
        {
            return _appRepository.Find();
        }

        public IList<string> FindByUser(string userId)
        {
            return _appRepository.FindByUser(userId);
        }

        public IList<string> FindByAppKey(string appKey)
        {
            return _appRepository.FindByAppKey(appKey);
        }

        public string Add(App app)
        {
            return _appRepository.Add(app);
        }

        public void Update(App app)
        {
            _appRepository.Update(app);
            _cacheContainer.Remove<App>(app.Id);
        }
    }
}