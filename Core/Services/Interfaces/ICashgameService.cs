using System;
using System.Collections.Generic;
using Core.Entities;

namespace Core.Services
{
	public interface ICashgameService
	{
	    DetailedCashgame GetDetailedById(string id);
	    DetailedCashgame GetCurrent(string bunchId);

        IList<ListCashgame> EventList(string eventId);
        IList<ListCashgame> PlayerList(string playerId);
        
        void DeleteGame(string id);
        string Add(string bunchId, string locationId);
        DetailedCashgame Update(string id, string locationId, string eventId);

        void UpdateAction(string cashgameId, string actionId, DateTime timestamp, int stack, int added);
        void DeleteAction(string cashgameId, string actionId);
    }
}