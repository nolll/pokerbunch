using System;
using System.Collections.Generic;
using Core.Classes;
using Core.Classes.Checkpoints;

namespace Core.Repositories{
	
	public interface ICashgameRepository{

        IList<Cashgame> GetPublished(Homegame homegame, int? year = null);
		Cashgame GetRunning(Homegame homegame);
		IList<Cashgame> GetAll(Homegame homegame, int? year = null);
        Cashgame GetByDateString(Homegame homegame, string dateString);
		CashgameSuite GetSuite(Homegame homegame, int? year = null);
        IList<int> GetYears(Homegame homegame);
		IList<string> GetLocations(Homegame homegame);
		bool DeleteGame(Cashgame cashgame);
		int AddGame(Homegame homegame, Cashgame cashgame);
	    void AddCheckpoint(Cashgame cashgame, Player player, Checkpoint checkpoint);
	    void UpdateCheckpoint(Checkpoint checkpoint);
	    void DeleteCheckpoint(int id);
		bool UpdateGame(Cashgame cashgame);
        bool StartGame(Cashgame cashgame);
        bool EndGame(Cashgame cashgame);
		bool HasPlayed(Player player);

	}

}