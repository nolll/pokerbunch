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
	    private readonly ICacheHandler _cacheHandler;

	    public HomegameRepository(IHomegameStorage homegameStorage, IHomegameFactory homegameFactory, ICacheHandler cacheHandler)
	    {
	        _homegameStorage = homegameStorage;
	        _homegameFactory = homegameFactory;
	        _cacheHandler = cacheHandler;
	    }

        public Homegame GetByName(string name)
        {
            var cacheKey = "Homegame:" + name;
            var cached = _cacheHandler.Get(cacheKey);
            if (cached != null)
            {
                return (Homegame)cached;
            }
            var homegame = GetByNameUncached(name);
            _cacheHandler.Put(cacheKey, homegame, TimeSpan.FromMinutes(10));
            return homegame;
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
            return _homegameFactory.CreateList(rawHomegames);
        }

        public IList<Homegame> GetAll()
        {
            var rawHomegames = _homegameStorage.GetHomegames();
            return _homegameFactory.CreateList(rawHomegames);
        }

        public Role GetHomegameRole(Homegame homegame, User user)
        {
            return (Role) _homegameStorage.GetHomegameRole(homegame.Id, user.Id);
        }

    }

}