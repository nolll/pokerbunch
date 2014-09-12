using System.Collections.Generic;
using Core.Services;

namespace Core.Entities
{
    public class CashgameSuite
    {
	    public IList<Cashgame> Cashgames { get; private set; }
        public IList<CashgameTotalResult> TotalResults { get; private set; }
        public IList<Player> Players { get; private set; }

        public CashgameSuite(
            IList<Cashgame> cashgames, 
            IList<CashgameTotalResult> totalResults,
            IList<Player> players)
        {
            Cashgames = cashgames;
            TotalResults = totalResults;
            Players = players;
        }

        public bool SpansMultipleYears
        {
            get { return CashgameService.SpansMultipleYears(Cashgames); }
        }
	}
}