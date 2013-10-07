using System;
using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Core.Repositories;
using Infrastructure.Caching;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.Factories;

namespace Infrastructure.Repositories {

    internal class HomegameRepository : IHomegameRepository
	{
	    private const string BaseCacheKey = "Homegame";

	    private readonly IHomegameStorage _homegameStorage;
	    private readonly IHomegameFactory _homegameFactory;
	    private readonly ICacheContainer _cacheContainer;
	    private readonly IRawHomegameFactory _rawHomegameFactory;

	    public HomegameRepository(IHomegameStorage homegameStorage, IHomegameFactory homegameFactory, ICacheContainer cacheContainer, IRawHomegameFactory rawHomegameFactory)
	    {
	        _homegameStorage = homegameStorage;
	        _homegameFactory = homegameFactory;
	        _cacheContainer = cacheContainer;
	        _rawHomegameFactory = rawHomegameFactory;
	    }

        public Homegame GetByName(string name)
        {
            var cacheKey = _cacheContainer.ConstructCacheKey(BaseCacheKey, name);
            var cached = _cacheContainer.Get<Homegame>(cacheKey);
            if (cached != null)
            {
                return cached;
            }
            var rawHomegame = _homegameStorage.GetHomegameByName(name);
            var uncached = rawHomegame != null ? _homegameFactory.Create(rawHomegame) : null;
            if (uncached != null)
            {
                _cacheContainer.Insert(cacheKey, uncached, TimeSpan.FromMinutes(10));
            }
            return uncached;
        }

        public IList<Homegame> GetByUser(User user)
        {
            if (user == null)
            {
                return new List<Homegame>();
            }
            var rawHomegames = _homegameStorage.GetHomegamesByUserId(user.Id);
            return rawHomegames.Select(_homegameFactory.Create).ToList();
        }

        public IList<Homegame> GetAll()
        {
            var homegames = new List<Homegame>();
            var slugs = GetSlugs();
            var uncachedSlugs = new List<string>();
            foreach (var slug in slugs)
            {
                var cacheKey = _cacheContainer.ConstructCacheKey(BaseCacheKey, slug);
                var cached = _cacheContainer.Get<Homegame>(cacheKey);
                if (cached != null)
                {
                    homegames.Add(cached);
                }
                else
                {
                    uncachedSlugs.Add(slug);
                }
            }

            if (uncachedSlugs.Count > 0)
            {
                var rawHomegames = _homegameStorage.GetHomegames(uncachedSlugs);
                var newHomegames = rawHomegames.Select(_homegameFactory.Create).ToList();
                foreach (var homegame in newHomegames)
                {
                    _cacheContainer.Insert(_cacheContainer.ConstructCacheKey(BaseCacheKey, homegame.Slug), homegame, TimeSpan.FromMinutes(10));
                }
                homegames.AddRange(newHomegames);
            }

            return homegames.OrderBy(o => o.DisplayName).ToList();
        }

        private IList<string> GetSlugs()
        {
            var cacheKey = _cacheContainer.ConstructCacheKey(BaseCacheKey, "AllSlugs");
            var cached = _cacheContainer.Get<List<string>>(cacheKey);
            if (cached != null)
            {
                return cached;
            }
            var uncached = _homegameStorage.GetAllSlugs();
            if (uncached != null)
            {
                _cacheContainer.Insert(cacheKey, uncached, TimeSpan.FromMinutes(10));
            }
            return uncached;
        } 

        public Role GetHomegameRole(Homegame homegame, User user)
        {
            return (Role) _homegameStorage.GetHomegameRole(homegame.Id, user.Id);
        }

        public Homegame AddHomegame(Homegame homegame)
        {
            var rawHomegame = _rawHomegameFactory.Create(homegame);
            rawHomegame = _homegameStorage.AddHomegame(rawHomegame);
            return _homegameFactory.Create(rawHomegame);
        }

        public bool SaveHomegame(Homegame homegame)
        {
            var rawHomegame = _rawHomegameFactory.Create(homegame);
            var success = _homegameStorage.UpdateHomegame(rawHomegame);
            var cacheKey = _cacheContainer.ConstructCacheKey(BaseCacheKey, homegame.Slug);
            _cacheContainer.Remove(cacheKey);
            return success;
        }
	}

}