using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Infrastructure.Storage.CachedRepositories
{
    public class CachedBunchRepository : IBunchRepository
    {
        private readonly IBunchRepository _sqlBunchRepository;
        private readonly IBunchRepository _apiBunchRepository;
        private readonly ICacheContainer _cacheContainer;

        public CachedBunchRepository(IBunchRepository sqlBunchRepository, IBunchRepository apiBunchRepository, ICacheContainer cacheContainer)
        {
            _sqlBunchRepository = sqlBunchRepository;
            _apiBunchRepository = apiBunchRepository;
            _cacheContainer = cacheContainer;
        }

        public Bunch Get(string slug)
        {
            return _cacheContainer.GetAndStore(_sqlBunchRepository.Get, slug, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<Bunch> Get(IList<string> ids)
        {
            return _cacheContainer.GetAndStore(_sqlBunchRepository.Get, ids, TimeSpan.FromMinutes(CacheTime.Long));
        }

        public IList<string> Search() 
        {
            return _sqlBunchRepository.Search();
        }

        public IList<string> SearchBySlug(string slug)
        {
            return _sqlBunchRepository.SearchBySlug(slug);
        }

        public IList<string> SearchByUser(string userId)
        {
            return _sqlBunchRepository.SearchByUser(userId);
        }

        public string Add(Bunch bunch)
        {
            return _sqlBunchRepository.Add(bunch);
        }

        public void Update(Bunch bunch)
        {
            _sqlBunchRepository.Update(bunch);
            _cacheContainer.Remove<Bunch>(bunch.Id);
        }
    }
}