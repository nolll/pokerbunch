using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories{
	
	public interface ICashgameRepository{

        IList<Cashgame> GetPublished(Homegame homegame, int? year = null);
        Cashgame GetRunning(Homegame homegame);
        Cashgame GetRunning(int bunchId);
        Cashgame GetByDateString(Homegame homegame, string dateString);
        IList<int> GetYears(Homegame homegame);
        IList<int> GetYears(int bunchId);
		IList<string> GetLocations(Homegame homegame);
		bool DeleteGame(Cashgame cashgame);
		int AddGame(Homegame homegame, Cashgame cashgame);
		bool UpdateGame(Cashgame cashgame);
        bool StartGame(Cashgame cashgame);
        bool EndGame(Homegame homegame, Cashgame cashgame);
		bool HasPlayed(Player player);
	}

}