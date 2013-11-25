using System.Collections.Generic;

namespace Core.Classes{
    public class CashgameSuite {

	    public IList<Cashgame> Cashgames { get; private set; }
        public IList<CashgameTotalResult> TotalResults { get; private set; }

        public CashgameSuite(
            IList<Cashgame> cashgames, 
            IList<CashgameTotalResult> totalResults)
        {
            Cashgames = cashgames;
            TotalResults = totalResults;
        }

	}
}