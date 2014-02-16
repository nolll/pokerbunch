using System.Collections.Generic;
using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Interfaces
{
	public interface ICashgameStorage
    {
        int AddGame(Homegame homegame, RawCashgame cashgame);
		bool DeleteGame(int cashgameId);
        RawCashgame GetGame(int cashgameId);
        int? GetRunningCashgameId(int homegameId);
        int? GetCashgameId(int homegameId, string dateStr);
        IList<RawCashgame> GetGames(IList<int> idList);
        IList<int> GetGameIds(int homegameId, int? status = null, int? year = null);
		IList<int> GetYears(int homegameId);
        bool UpdateGame(RawCashgame cashgame);
		bool HasPlayed(int playerId);
        IList<string> GetLocations(string slug);
	}
}