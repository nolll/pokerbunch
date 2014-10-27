using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Infrastructure.Cache;
using Infrastructure.SqlServer.Interfaces;
using Infrastructure.SqlServer.Mappers;
using Infrastructure.Storage;

namespace Infrastructure.SqlServer.Repositories
{
	public class SqlBunchRepository : IBunchRepository
	{
	    private readonly IBunchStorage _bunchStorage;
	    private readonly ICacheContainer _cacheContainer;
	    private readonly ICacheBuster _cacheBuster;

	    public SqlBunchRepository(
            IBunchStorage bunchStorage, 
            ICacheContainer cacheContainer, 
            ICacheBuster cacheBuster)
	    {
	        _bunchStorage = bunchStorage;
	        _cacheContainer = cacheContainer;
	        _cacheBuster = cacheBuster;
	    }

        public Bunch GetBySlug(string slug)
        {
            var userId = GetIdBySlug(slug);
            if (!userId.HasValue)
                throw new BunchNotFoundException(slug);
            var bunch = GetById(userId.Value);
            if (bunch == null)
                throw new BunchNotFoundException(slug);
            return GetById(userId.Value);
        }

        public Bunch GetById(int id)
        {
            var cacheKey = CacheKeyProvider.BunchKey(id);
            return _cacheContainer.GetAndStore(() => GetByIdUncached(id), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        public IList<Bunch> GetByUser(User user)
        {
            if (user == null)
                return new List<Bunch>();
            return GetByUserId(user.Id);
        }

	    private IList<Bunch> GetByUserId(int userId)
        {
            var rawHomegames = _bunchStorage.GetBunchesByUserId(userId);
            return rawHomegames.Select(BunchDataMapper.Map).ToList();
        }

        public IList<Bunch> GetList()
        {
            var ids = GetAllIds();
            var homegames = _cacheContainer.GetEachAndStore(GetAllUncached, TimeSpan.FromMinutes(CacheTime.Long), ids);
            return homegames.OrderBy(o => o.DisplayName).ToList();
        }

        public Role GetRole(int bunchId, int userId)
        {
            return (Role) _bunchStorage.GetBunchRole(bunchId, userId);
        }

        public int Add(Bunch bunch)
        {
            var rawHomegame = RawBunch.Create(bunch);
            var id = _bunchStorage.AddBunch(rawHomegame);
            _cacheBuster.BunchAdded();
            return id;
        }

        public bool Save(Bunch bunch)
        {
            var rawHomegame = RawBunch.Create(bunch);
            var success = _bunchStorage.UpdateBunch(rawHomegame);
            _cacheBuster.BunchUpdated(bunch.Id);
            return success;
        }

        private Bunch GetByIdUncached(int id)
        {
            var rawHomegame = _bunchStorage.GetById(id);
            return rawHomegame != null ? BunchDataMapper.Map(rawHomegame) : null;
        }

        private int? GetIdBySlug(string slug)
        {
            var cacheKey = CacheKeyProvider.BunchIdBySlugKey(slug);
            return _cacheContainer.GetAndStore(() => _bunchStorage.GetIdBySlug(slug), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        private IList<Bunch> GetAllUncached(IList<int> ids)
        {
            var rawHomegames = _bunchStorage.GetBunches(ids);
            return rawHomegames.Select(BunchDataMapper.Map).ToList();
        }

        private IList<int> GetAllIds()
        {
            var cacheKey = CacheKeyProvider.BunchIdsKey();
            return _cacheContainer.GetAndStore(() => _bunchStorage.GetAllIds(), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }
	}
}