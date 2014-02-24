using System;
using System.Collections.Generic;
using System.Linq;
using Application.Factories;
using Core.Classes;
using Core.Repositories;
using Infrastructure.Data.Cache;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Mappers;

namespace Infrastructure.Data.Repositories {

	public class HomegameRepository : IHomegameRepository
	{
	    private readonly IHomegameStorage _homegameStorage;
	    private readonly IHomegameFactory _homegameFactory;
	    private readonly ICacheContainer _cacheContainer;
	    private readonly ICacheKeyProvider _cacheKeyProvider;
	    private readonly ICacheBuster _cacheBuster;
	    private readonly IRawHomegameFactory _rawHomegameFactory;
	    private readonly IHomegameDataMapper _homegameDataMapper;

	    public HomegameRepository(
            IHomegameStorage homegameStorage, 
            IHomegameFactory homegameFactory, 
            ICacheContainer cacheContainer, 
            ICacheKeyProvider cacheKeyProvider,
            ICacheBuster cacheBuster,
            IRawHomegameFactory rawHomegameFactory,
            IHomegameDataMapper homegameDataMapper)
	    {
	        _homegameStorage = homegameStorage;
	        _homegameFactory = homegameFactory;
	        _cacheContainer = cacheContainer;
	        _cacheKeyProvider = cacheKeyProvider;
	        _cacheBuster = cacheBuster;
	        _rawHomegameFactory = rawHomegameFactory;
	        _homegameDataMapper = homegameDataMapper;
	    }

        public Homegame GetByName(string name)
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
            {
                return new List<Homegame>();
            }
            var rawHomegames = _homegameStorage.GetHomegamesByUserId(user.Id);
            return rawHomegames.Select(_homegameDataMapper.Map).ToList();
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
            var rawHomegame = _rawHomegameFactory.Create(homegame);
            rawHomegame = _homegameStorage.AddHomegame(rawHomegame);
            _cacheBuster.HomegameAdded();
            return _homegameDataMapper.Map(rawHomegame);
        }

        public bool Save(Homegame homegame)
        {
            var rawHomegame = _rawHomegameFactory.Create(homegame);
            var success = _homegameStorage.UpdateHomegame(rawHomegame);
            _cacheBuster.HomegameUpdated(homegame);
            return success;
        }

        private Homegame GetByIdUncached(int id)
        {
            var rawHomegame = _homegameStorage.GetById(id);
            return rawHomegame != null ? _homegameDataMapper.Map(rawHomegame) : null;
        }

        private int? GetIdBySlug(string slug)
        {
            var cacheKey = _cacheKeyProvider.HomegameIdBySlugKey(slug);
            return _cacheContainer.GetAndStore(() => _homegameStorage.GetIdBySlug(slug), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

        private IList<Homegame> GetAllUncached(IList<int> ids)
        {
            var rawHomegames = _homegameStorage.GetHomegames(ids);
            return rawHomegames.Select(_homegameDataMapper.Map).ToList();
        }

        private IList<int> GetAllIds()
        {
            var cacheKey = _cacheKeyProvider.HomegameIdsKey();
            return _cacheContainer.GetAndStore(() => _homegameStorage.GetAllIds(), TimeSpan.FromMinutes(CacheTime.Long), cacheKey);
        }

	}

}