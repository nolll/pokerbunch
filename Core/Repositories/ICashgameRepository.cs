using System;
using System.Collections.Generic;
using Core.Classes;
using Core.Classes.Checkpoints;

namespace Core.Repositories{
	
	public interface ICashgameRepository{

        List<Cashgame> GetPublished(Homegame homegame, int? year = null);
		Cashgame GetRunning(Homegame homegame);
		List<Cashgame> GetAll(Homegame homegame, int? year = null);
		Cashgame GetByDate(Homegame homegame, DateTime date);
        Cashgame GetByDateString(Homegame homegame, string dateString);
		CashgameSuite GetSuite(Homegame homegame, int? year = null);
        List<int> GetYears(Homegame homegame);
		List<string> GetLocations(Homegame homegame);
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