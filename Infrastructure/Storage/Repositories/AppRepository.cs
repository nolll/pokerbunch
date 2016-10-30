using System;
using System.Collections.Generic;
using Core.Entities;
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
        
        public App Get(string id)
        {
            return _cacheContainer.GetAndStore(_appDb.Get, id, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<App> GetList(IList<string> ids)
        {
            return _cacheContainer.GetAndStore(_appDb.GetList, ids, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<string> Find()
        {
            return _appDb.Find();
        }

        public IList<string> FindByUser(string userId)
        {
            return _appDb.FindByUser(userId);
        }

        public IList<string> FindByAppKey(string appKey)
        {
            return _appDb.FindByAppKey(appKey);
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