using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Entities
{
	public class CashgameTotalResult
    {
        public int Winnings { get; }
        public int GameCount { get; }
        public int TimePlayed { get; }
        public int WinRate { get; }
        public Player Player { get; }
	    public int Buyin { get; }
	    public int Cashout { get; }

        public CashgameTotalResult(Player player, IEnumerable<ListCashgame> cashgames)
        {
            Player = player;

            var playerCashgames = cashgames.Where(o => o.Players.FirstOrDefault(p => p.Id == player.Id) != null).ToList();

            if (playerCashgames.Count > 0)
            {
                foreach (var cashgame in playerCashgames)
                {
                    var result = cashgame.Players.First(o => o.Id == player.Id);
                    Winnings += result.Winnings;
                    GameCount++;
                    var timePlayed = result.UpdatedTime - result.StartTime;
                    TimePlayed += (int)Math.Round(timePlayed.TotalMinutes);
                    Buyin += result.Buyin;
                    Cashout += result.Stack;
                }
                WinRate = GetWinRate(TimePlayed, Winnings);
            }
        }

        private int GetWinRate(int timePlayed, int winnings)
        {
            return timePlayed > 0 ? (int)Math.Round((double)winnings / timePlayed * 60) : 0;
        }
	}
}