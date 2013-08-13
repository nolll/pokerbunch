using System;
using System.Collections.Generic;
using Core.Classes;

namespace Infrastructure.Factories{

	public class CashgameTotalResultFactory : ICashgameTotalResultFactory{

		public CashgameTotalResult Create(Player player, List<CashgameResult> results){
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
                {
                    Player = player,
                    Winnings = winnings,
                    GameCount = gameCount,
                    TimePlayed = timePlayed,
                    WinRate = winRate
                };
		}

	    private int GetWinRate(int timePlayed, int winnings)
	    {
	        return timePlayed > 0 ? (int) Math.Round((double) winnings/timePlayed*60) : 0;
	    }
	}

}