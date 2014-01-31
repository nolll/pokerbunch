using System.Collections.Generic;
using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Storage
{
	public interface ICashgameStorage
    {
        int AddGame(Homegame homegame, RawCashgameWithResults cashgame);
		bool DeleteGame(int cashgameId);
        RawCashgame GetGame(int cashgameId);
        int? GetRunningCashgameId(int homegameId);
        int? GetCashgameId(int homegameId, string dateStr);
        IList<RawCashgameWithResults> GetGames(IEnumerable<int> ids);
        IList<int> GetGameIds(int homegameId, int? status = null, int? year = null);
		IList<int> GetYears(int homegameId);
        bool UpdateGame(RawCashgameWithResults cashgame);
		bool HasPlayed(int playerId);
        IList<string> GetLocations(string slug);
	}
}