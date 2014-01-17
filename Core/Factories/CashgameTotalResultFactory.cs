using System;
using System.Collections.Generic;
using System.Linq;
using Core.Classes;

namespace Core.Factories{

	public class CashgameTotalResultFactory : ICashgameTotalResultFactory
    {
	    public CashgameTotalResult Create(Player player, IList<Cashgame> cashgames)
        {
            var playerCashgames = cashgames.Where(o => o.IsInGame(player.Id)).ToList();

            var winnings = 0;
            var gameCount = 0;
            var timePlayed = 0;
            var buyin = 0;
            var cashout = 0;
	        var winRate = 0;

            if(playerCashgames.Count > 0)
            {
                foreach (var cashgame in playerCashgames)
                {
                    var result = cashgame.GetResult(player.Id);
                    winnings += result.Winnings;
                    gameCount++;
                    timePlayed += result.PlayedTime;
                    buyin += result.Buyin;
                    cashout += result.Stack;
                }
                winRate = GetWinRate(timePlayed, winnings);
            }

            return new CashgameTotalResult
                (
                    winnings,
                    gameCount,
                    timePlayed,
                    winRate,
                    player.Id,
                    buyin,
                    cashout
                );
        }

        public IList<CashgameTotalResult> CreateList(IList<Player> players, IList<Cashgame> cashgames)
        {
            var list = players.Select(player => Create(player, cashgames)).ToList();
            return list.Where(o => o.GameCount > 0).OrderByDescending(o => o.Winnings).ToList();
        }

	    private int GetWinRate(int timePlayed, int winnings)
	    {
	        return timePlayed > 0 ? (int) Math.Round((double) winnings/timePlayed*60) : 0;
	    }
	}

}