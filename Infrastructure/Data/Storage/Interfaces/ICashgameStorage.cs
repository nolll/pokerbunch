using System;
using System.Collections.Generic;
using Core.Classes;
using Core.Classes.Checkpoints;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Storage.Interfaces {

	public interface ICashgameStorage{

		int AddGame(Homegame homegame, Cashgame cashgame);
		bool DeleteGame(Cashgame cashgame);
        int AddCheckpoint(Cashgame cashgame, Player player, Checkpoint checkpoint);
        bool UpdateCheckpoint(Checkpoint checkpoint);
        bool DeleteCheckpoint(int id);
		RawCashgame GetGame(Homegame homegame, DateTime date);
		List<RawCashgame> GetGames(Homegame homegame, GameStatus? status = null, int? year = null);
		List<int> GetYears(Homegame homegame);
		bool UpdateGame(RawCashgame cashgame);
		bool HasPlayed(Player player);
        List<string> GetLocations(Homegame homegame);

	}

}