using System;
using System.Collections.Generic;
using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Storage.Interfaces {

	public interface ICashgameStorage{

		int AddGame(int homegameId, Cashgame cashgame);
		bool DeleteGame(int cashgameId);
		RawCashgame GetGame(Homegame homegame, DateTime date);
		List<RawCashgame> GetGames(Homegame homegame, GameStatus? status = null, int? year = null);
		List<int> GetYears(Homegame homegame);
		bool UpdateGame(RawCashgame cashgame);
		bool HasPlayed(Player player);
        List<string> GetLocations(Homegame homegame);

	}

}