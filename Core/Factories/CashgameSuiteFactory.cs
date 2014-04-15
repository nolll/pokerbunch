using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Core.Factories.Interfaces;

namespace Core.Factories
{
    public class CashgameSuiteFactory : ICashgameSuiteFactory
    {
	    private readonly ICashgameTotalResultFactory _cashgameTotalResultFactory;

	    public CashgameSuiteFactory(ICashgameTotalResultFactory cashgameTotalResultFactory)
		{
		    _cashgameTotalResultFactory = cashgameTotalResultFactory;
		}

        public CashgameSuite Create(IList<Cashgame> cashgames, IList<Player> players)
        {
			var sortedCashgames = cashgames.OrderByDescending(o => o.StartTime).ToList();

			var totalResults = _cashgameTotalResultFactory.CreateList(players, cashgames);

            return new CashgameSuite
                (
                    sortedCashgames,
                    totalResults
                );
		}
	}
}