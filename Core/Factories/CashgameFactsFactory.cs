using System.Collections.Generic;
using System.Linq;
using Core.Classes;

namespace Core.Factories
{
    public class CashgameFactsFactory : ICashgameFactsFactory{

	    private readonly ICashgameTotalResultFactory _cashgameTotalResultFactory;

	    public CashgameFactsFactory(ICashgameTotalResultFactory cashgameTotalResultFactory)
		{
		    _cashgameTotalResultFactory = cashgameTotalResultFactory;
		}

        public CashgameFacts Create(IList<Cashgame> cashgames, IList<Player> players)
        {
			var totalGameTime = 0;
            var totalTurnover = 0;
			var resultIndex = GetPlayerIndex(players);
			CashgameResult bestResult = null;
			CashgameResult worstResult = null;

			foreach(var cashgame in cashgames){
				var results = cashgame.Results;
				foreach(var result in results){
					resultIndex[result.Player.Id].Add(result);
					if(bestResult == null || result.Winnings > bestResult.Winnings){
						bestResult = result;
					}
					if(worstResult == null || result.Winnings < worstResult.Winnings){
						worstResult = result;
					}
				}
				totalGameTime += cashgame.Duration;
			    totalTurnover += cashgame.Turnover;
			}

            var totalResults = _cashgameTotalResultFactory.CreateList(players, resultIndex);
			var mostTimeResult = GetMostTimeResult(totalResults);
			var bestTotalResult = totalResults.FirstOrDefault();

            return new CashgameFacts
                (
                    cashgames.Count(),
                    bestTotalResult,
                    bestResult,
                    worstResult,
                    mostTimeResult,
                    totalGameTime,
                    totalTurnover
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

        private CashgameTotalResult GetMostTimeResult(IEnumerable<CashgameTotalResult> results){
			CashgameTotalResult mostTimeResult = null;
			foreach(var result in results){
				if(mostTimeResult == null || result.TimePlayed > mostTimeResult.TimePlayed){
					mostTimeResult = result;
				}
			}
			return mostTimeResult;
		}

	}

}