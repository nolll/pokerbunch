using System;
using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Core.Repositories;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Storage.Interfaces;

namespace Infrastructure.Repositories {

	public class HomegameRepository : IHomegameRepository{

	    private readonly IHomegameStorage _homegameStorage;

	    public HomegameRepository(IHomegameStorage homegameStorage)
	    {
	        _homegameStorage = homegameStorage;
	    }

		public Homegame GetByName(string name){
			var rawHomegame = _homegameStorage.GetHomegameByName(name);
			if(rawHomegame == null){
				return null;
			}
			return GetHomegameFromRawHomegame(rawHomegame);
		}

        public IList<Homegame> GetAll()
        {
            var rawHomegames = _homegameStorage.GetHomegames();
            return GetHomegamesFromRawHomegames(rawHomegames);
        }

        private IList<Homegame> GetHomegamesFromRawHomegames(IEnumerable<RawHomegame> rawHomegames)
        {
            return rawHomegames.Select(GetHomegameFromRawHomegame).ToList();
        }

	    private Homegame GetHomegameFromRawHomegame(RawHomegame rawHomegame){
			return new Homegame
			    {
			        Id = rawHomegame.Id,
			        Slug = rawHomegame.Slug,
			        DisplayName = rawHomegame.DisplayName,
			        Description = rawHomegame.Description,
			        HouseRules = rawHomegame.HouseRules,
			        Currency = new CurrencySettings(rawHomegame.CurrencySymbol, rawHomegame.CurrencyLayout),
                    Timezone = TimeZoneInfo.FindSystemTimeZoneById(rawHomegame.TimezoneName),
			        DefaultBuyin = rawHomegame.DefaultBuyin,
			        CashgamesEnabled = rawHomegame.CashgamesEnabled,
			        TournamentsEnabled = rawHomegame.TournamentsEnabled,
			        VideosEnabled = rawHomegame.VideosEnabled
			    };
		}

	}

}