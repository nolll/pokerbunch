using System;
using System.Collections.Generic;
using System.Linq;
using Core.Classes;

namespace Core.Factories{

	public class CashgameTotalResultFactory : ICashgameTotalResultFactory{

        public CashgameTotalResult Create(int playerId, IList<CashgameResult> results)
        {
			var winnings = 0;
			var gameCount = 0;
			var timePlayed = 0;

			foreach(var result in results){
				winnings += result.Winnings;
				gameCount++;
				timePlayed += result.PlayedTime;
			}

			var winRate = GetWinRate(timePlayed, winnings);

            return new CashgameTotalResult
                (
                    winnings,
                    gameCount,
                    timePlayed,
                    winRate,
                    playerId
                );
		}

        public IList<CashgameTotalResult> CreateList(IEnumerable<Player> players, IDictionary<int, IList<CashgameResult>> resultIndex)
	    {
            var totalResults = new List<CashgameTotalResult>();
            foreach (var player in players)
            {
                var playerResults = resultIndex[player.Id];
                if (playerResults.Count > 0)
                {
                    var totalResult = Create(player.Id, playerResults);
                    totalResults.Add(totalResult);
                }
            }
            return totalResults.OrderByDescending(o => o.Winnings).ToList();
	    }

	    private int GetWinRate(int timePlayed, int winnings)
	    {
	        return timePlayed > 0 ? (int) Math.Round((double) winnings/timePlayed*60) : 0;
	    }
	}

}