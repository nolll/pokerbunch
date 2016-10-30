using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Infrastructure.Storage.SqlDb;

namespace Infrastructure.Storage.Repositories
{
    public class AppRepository : IAppRepository
    {
        private readonly SqlAppDb _appDb;
        private readonly ICacheContainer _cacheContainer;

        public AppRepository(SqlServerStorageProvider db, ICacheContainer cacheContainer)
        {
            _appDb = new SqlAppDb(db);
            _cacheContainer = cacheContainer;
        }
        
        public App GetById(string id)
        {
            return _cacheContainer.GetAndStore(_appDb.Get, id, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<App> GetList(IList<string> ids)
        {
            return _cacheContainer.GetAndStore(_appDb.GetList, ids, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<App> List()
        {
            var ids = _appDb.Find();
            return GetList(ids);
        }

        public IList<App> ListByUser(string userId)
        {
            var ids = _appDb.FindByUser(userId);
            return GetList(ids);
        }

        public App GetByAppKey(string appKey)
        {
            var ids = _appDb.FindByAppKey(appKey);
            if (ids.Count == 0)
                throw new AppNotFoundException();
            return GetById(ids.First());
        }

        public string Add(App app)
        {
            return _appDb.Add(app);
        }

        public void Update(App app)
        {
            _appDb.Update(app);
            _cacheContainer.Remove<App>(app.Id);
        }
    }
}