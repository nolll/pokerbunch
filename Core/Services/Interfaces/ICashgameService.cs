using System.Collections.Generic;
using Core.Entities;

namespace Core.Services
{
	public interface ICashgameService
	{
	    DetailedCashgame GetDetailedById(string id);
        IList<ListCashgame> PlayerList(string playerId);
        void DeleteGame(string id);
        DetailedCashgame Update(string id, string locationId, string eventId);
    }
}