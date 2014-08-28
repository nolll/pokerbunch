using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Data.Cache;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Mappers;

namespace Infrastructure.Data.Repositories {

	public class BunchRepository : IBunchRepository
	{
	    private readonly IBunchStorage _bunchStorage;
	    private readonly ICacheContainer _cacheContainer;
	    private readonly ICacheKeyProvider _cacheKeyProvider;
	    private readonly ICacheBuster _cacheBuster;

	    public BunchRepository(
            IBunchStorage bunchStorage, 
            ICacheContainer cacheContainer, 
            ICacheKeyProvider cacheKeyProvider,
            ICacheBuster cacheBuster)
	    {
	        _bunchStorage = bunchStorage;
	        _cacheContainer = cacheContainer;
	        _cacheKeyProvider = cacheKeyProvider;
	        _cacheBuster = cacheBuster;
	    }

        public Bunch GetBySlug(string slug)
        {
            var userId = GetIdBySlug(slug);
            return userId.HasValue ? GetById(userId.Value) : null;
        }

        public Bunch GetById(int id)
        {
            var cacheKey = _cacheKeyProvider.HomegameKey(id);
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

        public Role GetRole(Bunch bunch, User user)
        {
            return (Role) _bunchStorage.GetBunchRole(bunch.Id, user.Id);
        }

        public Bunch Add(Bunch bunch)
        {
            var rawHomegame = RawHomegameFactory.Create(bunch);
            rawHomegame = _bunchStorage.AddBunch(rawHomegame);
            _cacheBuster.BunchAdded();
            return BunchDataMapper.Map(rawHomegame);
        }

        public bool Save(Bunch bunch)
        {
            var rawHomegame = RawHomegameFactory.Create(bunch);
            var success = _bunchStorage.UpdateBunch(rawHomegame);
            _cacheBuster.BunchUpdated(bunch);
            return success;
        }

        private Bunch GetByIdUncached(int id)
        {
            var rawHomegame = _bunchStorage.GetById(id);
            return rawHomegame != null ? BunchDataMapper.Map(rawHomegame) : null;
        }

        private int? GetIdBySlug(string slug)
        {
            var cacheKey = _cacheKeyProvider.HomegameIdBySlugKey(slug);
            return _cacheContainer.GetAndStore(() => _bunchStorage.GetIdBySlug(slug), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        private IList<Bunch> GetAllUncached(IList<int> ids)
        {
            var rawHomegames = _bunchStorage.GetBunches(ids);
            return rawHomegames.Select(BunchDataMapper.Map).ToList();
        }

        private IList<int> GetAllIds()
        {
            var cacheKey = _cacheKeyProvider.HomegameIdsKey();
            return _cacheContainer.GetAndStore(() => _bunchStorage.GetAllIds(), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

	}

}