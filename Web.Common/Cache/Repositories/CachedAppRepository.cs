using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Web.Common.Cache.Repositories
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

        public IList<App> ListApps()
        {
            return _appRepository.ListApps();
        }

        public IList<App> ListApps(int userId)
        {
            return _appRepository.ListApps(userId);
        }

        public App Get(int id)
        {
            return _appRepository.Get(id);
        }

        public App Get(string appKey)
        {
            return _appRepository.Get(appKey);
        }

        public IList<int> Find()
        {
            return _appRepository.Find();
        }

        public IList<int> Find(int userId)
        {
            return _appRepository.Find(userId);
        }

        public IList<int> Find(string appKey)
        {
            return _appRepository.Find(appKey);
        }

        public int Add(App app)
        {
            return _appRepository.Add(app);
        }

        public void Update(App app)
        {
            _appRepository.Update(app);
        }
    }
}