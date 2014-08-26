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

	public class HomegameRepository : IHomegameRepository
	{
	    private readonly IHomegameStorage _homegameStorage;
	    private readonly ICacheContainer _cacheContainer;
	    private readonly ICacheKeyProvider _cacheKeyProvider;
	    private readonly ICacheBuster _cacheBuster;

	    public HomegameRepository(
            IHomegameStorage homegameStorage, 
            ICacheContainer cacheContainer, 
            ICacheKeyProvider cacheKeyProvider,
            ICacheBuster cacheBuster)
	    {
	        _homegameStorage = homegameStorage;
	        _cacheContainer = cacheContainer;
	        _cacheKeyProvider = cacheKeyProvider;
	        _cacheBuster = cacheBuster;
	    }

        public Homegame GetBySlug(string name)
        {
            var userId = GetIdBySlug(name);
            return userId.HasValue ? GetById(userId.Value) : null;
        }

        public Homegame GetById(int id)
        {
            var cacheKey = _cacheKeyProvider.HomegameKey(id);
            return _cacheContainer.GetAndStore(() => GetByIdUncached(id), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        public IList<Homegame> GetByUser(User user)
        {
            if (user == null)
                return new List<Homegame>();
            return GetByUserId(user.Id);
        }

	    private IList<Homegame> GetByUserId(int userId)
        {
            var rawHomegames = _homegameStorage.GetHomegamesByUserId(userId);
            return rawHomegames.Select(HomegameDataMapper.Map).ToList();
        }

        public IList<Homegame> GetList()
        {
            var ids = GetAllIds();
            var homegames = _cacheContainer.GetEachAndStore(GetAllUncached, TimeSpan.FromMinutes(CacheTime.Long), ids);
            return homegames.OrderBy(o => o.DisplayName).ToList();
        }

        public Role GetHomegameRole(Homegame homegame, User user)
        {
            return (Role) _homegameStorage.GetHomegameRole(homegame.Id, user.Id);
        }

        public Homegame Add(Homegame homegame)
        {
            var rawHomegame = RawHomegameFactory.Create(homegame);
            rawHomegame = _homegameStorage.AddHomegame(rawHomegame);
            _cacheBuster.HomegameAdded();
            return HomegameDataMapper.Map(rawHomegame);
        }

        public bool Save(Homegame homegame)
        {
            var rawHomegame = RawHomegameFactory.Create(homegame);
            var success = _homegameStorage.UpdateHomegame(rawHomegame);
            _cacheBuster.HomegameUpdated(homegame);
            return success;
        }

        private Homegame GetByIdUncached(int id)
        {
            var rawHomegame = _homegameStorage.GetById(id);
            return rawHomegame != null ? HomegameDataMapper.Map(rawHomegame) : null;
        }

        private int? GetIdBySlug(string slug)
        {
            var cacheKey = _cacheKeyProvider.HomegameIdBySlugKey(slug);
            return _cacheContainer.GetAndStore(() => _homegameStorage.GetIdBySlug(slug), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        private IList<Homegame> GetAllUncached(IList<int> ids)
        {
            var rawHomegames = _homegameStorage.GetHomegames(ids);
            return rawHomegames.Select(HomegameDataMapper.Map).ToList();
        }

        private IList<int> GetAllIds()
        {
            var cacheKey = _cacheKeyProvider.HomegameIdsKey();
            return _cacheContainer.GetAndStore(() => _homegameStorage.GetAllIds(), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

	}

}