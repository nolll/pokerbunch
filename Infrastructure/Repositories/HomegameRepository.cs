using System;
using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Core.Repositories;
using Infrastructure.Caching;
using Infrastructure.Data.Classes;
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
            return _cacheContainer.GetCachedIfAvailable(() => GetByNameUncached(name), TimeSpan.FromMinutes(10), "Homegame", name);
        }

        private Homegame GetByNameUncached(string name)
        {
            var rawHomegame = _homegameStorage.GetHomegameByName(name);
            return rawHomegame != null ? _homegameFactory.Create(rawHomegame) : null;
        }

        public IList<Homegame> GetByUser(User user)
        {
            var rawHomegames = _homegameStorage.GetHomegamesByUserId(user.Id);
            if (rawHomegames == null)
            {
                return null;
            }
            return rawHomegames.Select(_homegameFactory.Create).ToList();
        }

        public IList<Homegame> GetAll()
        {
            var homegames = new List<Homegame>();
            var slugs = GetSlugs();
            var uncachedSlugs = new List<string>();
            foreach (var slug in slugs)
            {
                Homegame cached;
                var cacheKey = _cacheContainer.ConstructCacheKey("Homegame", slug);
                if (_cacheContainer.TryGet(cacheKey, out cached))
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
            return _cacheContainer.GetCachedIfAvailable(GetSlugsUncached, TimeSpan.FromMinutes(10), "Homegame:AllSlugs");
        } 

        private IList<string> GetSlugsUncached()
        {
            return _homegameStorage.GetAllSlugs();
        }

        public Role GetHomegameRole(Homegame homegame, User user)
        {
            return (Role) _homegameStorage.GetHomegameRole(homegame.Id, user.Id);
        }

    }

}