using System;
using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Core.Repositories;
using Infrastructure.Caching;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.Factories;

namespace Infrastructure.Repositories {

	public class HomegameRepository : IHomegameRepository{

	    private readonly IHomegameStorage _homegameStorage;
	    private readonly IHomegameFactory _homegameFactory;
	    private readonly ICacheContainer _cacheContainer;

	    public HomegameRepository(IHomegameStorage homegameStorage, IHomegameFactory homegameFactory, ICacheContainer cacheContainer)
	    {
	        _homegameStorage = homegameStorage;
	        _homegameFactory = homegameFactory;
	        _cacheContainer = cacheContainer;
	    }

        public Homegame GetByName(string name)
        {
            var cacheKey = _cacheContainer.ConstructCacheKey("Homegame", "AllSlugs");
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

        private Homegame GetByNameUncached(string name)
        {
            var rawHomegame = _homegameStorage.GetHomegameByName(name);
            return rawHomegame != null ? _homegameFactory.Create(rawHomegame) : null;
        }

        public IList<Homegame> GetByUser(User user)
        {
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
                var cacheKey = _cacheContainer.ConstructCacheKey("Homegame", slug);
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
                    _cacheContainer.Insert(_cacheContainer.ConstructCacheKey("Homegame", homegame.Slug), homegame, TimeSpan.FromMinutes(10));
                }
                homegames.AddRange(newHomegames);
            }

            return homegames.OrderBy(o => o.DisplayName).ToList();
        }

        private IList<string> GetSlugs()
        {
            const string cacheKey = "Homegame:AllSlugs";
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

    }

}