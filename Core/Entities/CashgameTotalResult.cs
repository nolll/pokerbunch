using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Entities
{
	public class CashgameTotalResult
    {
        public int Winnings { get; private set; }
        public int GameCount { get; private set; }
        public int TimePlayed { get; private set; }
        public int WinRate { get; private set; }
        public int PlayerId { get; private set; }
        public Player Player { get; private set; }
	    public int Buyin { get; private set; }
	    public int Cashout { get; private set; }

        public CashgameTotalResult(Player player, IEnumerable<Cashgame> cashgames)
        {
            Player = player;

            var playerCashgames = cashgames.Where(o => o.IsInGame(player.Id)).ToList();

            if (playerCashgames.Count > 0)
            {
                foreach (var cashgame in playerCashgames)
                {
                    var result = cashgame.GetResult(player.Id);
                    Winnings += result.Winnings;
                    GameCount++;
                    TimePlayed += result.PlayedTime;
                    Buyin += result.Buyin;
                    Cashout += result.Stack;
                }
                WinRate = GetWinRate(TimePlayed, Winnings);
            }
        }

	    protected CashgameTotalResult(
            int winnings,
            int gameCount,
            int timePlayed,
            int winRate,
            Player player,
            int buyin,
            int cashout)
        {
            Winnings = winnings;
            GameCount = gameCount;
            TimePlayed = timePlayed;
            WinRate = winRate;
	        Player = player;
            Buyin = buyin;
            Cashout = cashout;
        }

        private int GetWinRate(int timePlayed, int winnings)
        {
            return timePlayed > 0 ? (int)Math.Round((double)winnings / timePlayed * 60) : 0;
        }
	}
}