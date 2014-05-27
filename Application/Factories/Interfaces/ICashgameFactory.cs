using System;
using System.Collections.Generic;
using Core.Entities;

namespace Application.Factories
{
	public interface ICashgameFactory
	{
        Cashgame Create(int id, int homegameId, string location, GameStatus status, bool isStarted, DateTime? startTime, DateTime? endTime, int duration, IList<CashgameResult> results, int playerCount, int diff, int turnover, bool hasActivePlayers, int totalStacks, int averageBuyin, string dateString);
	    Cashgame Create(string location, int homegameId, int? status = null, int? id = null, IList<CashgameResult> results = null);
	}
}