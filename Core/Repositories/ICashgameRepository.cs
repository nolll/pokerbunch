using System.Collections.Generic;
using Core.Entities;

namespace Core.Repositories{
	
	public interface ICashgameRepository{

        IList<Cashgame> GetFinished(int bunchId, int? year = null);
        Cashgame GetRunning(Bunch bunch);
        Cashgame GetRunning(int bunchId);
        Cashgame GetByDateString(Bunch bunch, string dateString);
        IList<int> GetYears(Bunch bunch);
        IList<int> GetYears(int bunchId);
		IList<string> GetLocations(Bunch bunch);
		bool DeleteGame(Cashgame cashgame);
		int AddGame(Bunch bunch, Cashgame cashgame);
		bool UpdateGame(Cashgame cashgame);
        bool StartGame(Cashgame cashgame);
        bool EndGame(Bunch bunch, Cashgame cashgame);
		bool HasPlayed(int playerId);
	}

}