using System.Collections.Generic;
using System.Linq;
using Core.Entities;

namespace Core.Factories
{
    public static class CashgameSuiteFactory
    {
        public static CashgameSuite Create(IList<Cashgame> cashgames, IList<Player> players)
        {
			var sortedCashgames = cashgames.OrderByDescending(o => o.StartTime).ToList();
            var totalResults = CreateTotalResults(players, cashgames);

            return new CashgameSuite
                (
                    sortedCashgames,
                    totalResults,
                    players
                );
		}

        private static IList<CashgameTotalResult> CreateTotalResults(IList<Player> players, IList<Cashgame> cashgames)
        {
            return players.Select(player => new CashgameTotalResult(player, cashgames)).Where(o => o.GameCount > 0).OrderByDescending(o => o.Winnings).ToList();
        }
	}
}