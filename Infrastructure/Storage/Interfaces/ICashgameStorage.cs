using System.Collections.Generic;
using Core.Entities;
using Infrastructure.Storage.Classes;

namespace Infrastructure.Storage.Interfaces
{
	public interface ICashgameStorage
    {
        int AddGame(Bunch bunch, RawCashgame cashgame);
		bool DeleteGame(int cashgameId);
        RawCashgame GetGame(int cashgameId);
        int? GetRunningCashgameId(int homegameId);
        IList<RawCashgame> GetGames(IList<int> idList);
        IList<int> GetGameIds(int homegameId, int? status = null, int? year = null);
        IList<int> GetGameIdsByEvent(int eventId);
		IList<int> GetYears(int homegameId);
        bool UpdateGame(RawCashgame cashgame);
		bool HasPlayed(int playerId);
        IList<string> GetLocations(int bunchId);
	}
}