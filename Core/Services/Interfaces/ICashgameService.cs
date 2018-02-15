using System;
using System.Collections.Generic;
using Core.Entities;

namespace Core.Services
{
	public interface ICashgameService
	{
	    DetailedCashgame GetDetailedById(string id);
	    DetailedCashgame GetCurrent(string bunchId);

        IList<ListCashgame> List(string bunchId, int? year = null);
        IList<ListCashgame> EventList(string eventId);
        IList<ListCashgame> PlayerList(string playerId);
        
        void DeleteGame(string id);
        string Add(string bunchId, string locationId);
        DetailedCashgame Update(string id, string locationId, string eventId);

        IList<int> GetYears(string bunchId);

        void Report(string cashgameId, string playerId, int stack);
        void Buyin(string cashgameId, string playerId, int added, int stack);
        void Cashout(string cashgameId, string playerId, int stack);
        void UpdateAction(string cashgameId, string actionId, DateTime timestamp, int stack, int added);
        void DeleteAction(string cashgameId, string actionId);
    }
}