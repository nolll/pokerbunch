using System;
using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Core.Repositories;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.Factories;

namespace Infrastructure.Repositories {

	public class HomegameRepository : IHomegameRepository{

	    private readonly IHomegameStorage _homegameStorage;
	    private readonly IHomegameFactory _homegameFactory;

	    public HomegameRepository(IHomegameStorage homegameStorage, IHomegameFactory homegameFactory)
	    {
	        _homegameStorage = homegameStorage;
	        _homegameFactory = homegameFactory;
	    }

	    public Homegame GetByName(string name){
			var rawHomegame = _homegameStorage.GetHomegameByName(name);
			if(rawHomegame == null){
				return null;
			}
			return _homegameFactory.Create(rawHomegame);
		}

        public IList<Homegame> GetAll()
        {
            var rawHomegames = _homegameStorage.GetHomegames();
            return _homegameFactory.CreateList(rawHomegames);
        }

    }

}