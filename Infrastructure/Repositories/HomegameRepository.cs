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
	    private readonly ICacheRepository _cacheRepository;

	    public HomegameRepository(IHomegameStorage homegameStorage, IHomegameFactory homegameFactory, ICacheRepository cacheRepository)
	    {
	        _homegameStorage = homegameStorage;
	        _homegameFactory = homegameFactory;
	        _cacheRepository = cacheRepository;
	    }

        public Homegame GetByName(string name)
        {
            return _cacheRepository.GetCachedIfAvailable(() => GetByNameUncached(name), TimeSpan.FromMinutes(10), "Homegame:", name);
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