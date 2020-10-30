using System.Collections.Generic;
using Core.Entities;

namespace Core.Services
{
	public interface ICashgameService
	{
	    DetailedCashgame GetDetailedById(string id);
	    DetailedCashgame GetCurrent(string bunchId);

        IList<ListCashgame> PlayerList(string playerId);
        
        void DeleteGame(string id);
        string Add(string bunchId, string locationId);
        DetailedCashgame Update(string id, string locationId, string eventId);
    }
}