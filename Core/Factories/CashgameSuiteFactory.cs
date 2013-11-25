using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Core.Factories.Interfaces;

namespace Core.Factories{
    public class CashgameSuiteFactory : ICashgameSuiteFactory{

	    private readonly ICashgameTotalResultFactory _cashgameTotalResultFactory;

	    public CashgameSuiteFactory(ICashgameTotalResultFactory cashgameTotalResultFactory)
		{
		    _cashgameTotalResultFactory = cashgameTotalResultFactory;
		}

        public CashgameSuite Create(IList<Cashgame> cashgames, IList<Player> players)
        {
			var resultIndex = GetPlayerIndex(players);
			var sortedCashgames = cashgames.OrderByDescending(o => o.StartTime).ToList();
			foreach(var cashgame in sortedCashgames){
				var results = cashgame.Results;
				foreach(var result in results){
					resultIndex[result.Player.Id].Add(result);
				}
			}

			var totalResults = _cashgameTotalResultFactory.CreateList(players, resultIndex);

            return new CashgameSuite
                (
                    sortedCashgames,
                    totalResults
                );
		}

        private Dictionary<int, IList<CashgameResult>> GetPlayerIndex(IEnumerable<Player> players)
        {
            var dictionary = new Dictionary<int, IList<CashgameResult>>();
			foreach(var player in players){
				dictionary[player.Id] = new List<CashgameResult>();
			}
			return dictionary;
		}

	}

}