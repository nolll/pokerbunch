using System.Collections.Generic;
using System.Linq;
using Core.Services;

namespace Core.Entities
{
    public class CashgameSuite
    {
	    public IList<Cashgame> Cashgames { get; private set; }
        public IList<CashgameTotalResult> TotalResults { get; private set; }
        public IList<Player> Players { get; private set; }

        public CashgameSuite(IList<Cashgame> cashgames, IList<CashgameTotalResult> totalResults, IList<Player> players)
        {
            Cashgames = cashgames;
            TotalResults = totalResults;
            Players = players;
        }

        public CashgameSuite(IList<Cashgame> cashgames, IList<Player> players)
        {
            var sortedCashgames = cashgames.OrderByDescending(o => o.StartTime).ToList();
            var totalResults = CreateTotalResults(players, cashgames);

            Cashgames = sortedCashgames;
            TotalResults = totalResults;
            Players = players;
        }

        private static IList<CashgameTotalResult> CreateTotalResults(IList<Player> players, IList<Cashgame> cashgames)
        {
            return players.Select(player => new CashgameTotalResult(player, cashgames)).Where(o => o.GameCount > 0).OrderByDescending(o => o.Winnings).ToList();
        }

        public bool SpansMultipleYears
        {
            get { return CashgameService.SpansMultipleYears(Cashgames); }
        }
	}
}