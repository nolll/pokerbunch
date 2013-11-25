using System.Collections.Generic;
using Core.Classes;

namespace Core.Repositories{
	
	public interface ICashgameRepository{

        IList<Cashgame> GetPublished(Homegame homegame, int? year = null);
		Cashgame GetRunning(Homegame homegame);
		IList<Cashgame> GetAll(Homegame homegame, int? year = null);
        Cashgame GetByDateString(Homegame homegame, string dateString);
		IList<int> GetYears(Homegame homegame);
		IList<string> GetLocations(Homegame homegame);
		bool DeleteGame(Cashgame cashgame);
		int AddGame(Homegame homegame, Cashgame cashgame);
		bool UpdateGame(Cashgame cashgame);
        bool StartGame(Cashgame cashgame);
        bool EndGame(Cashgame cashgame);
		bool HasPlayed(Player player);

	    void ClearCashgameFromCache(Cashgame cashgame);
	    void ClearCashgameListFromCache(Homegame homegame, Cashgame cashgame);
	}

}