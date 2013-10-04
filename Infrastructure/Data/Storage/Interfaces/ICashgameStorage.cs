using System;
using System.Collections.Generic;
using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Storage.Interfaces {

	public interface ICashgameStorage{

		int AddGame(int homegameId, RawCashgame cashgame);
		bool DeleteGame(int cashgameId);
		RawCashgame GetGame(Homegame homegame, DateTime date);
		IList<RawCashgame> GetGames(Homegame homegame, GameStatus? status = null, int? year = null);
		IList<int> GetYears(string slug);
		bool UpdateGame(RawCashgame cashgame);
		bool HasPlayed(int playerId);
        IList<string> GetLocations(string slug);

	}

}