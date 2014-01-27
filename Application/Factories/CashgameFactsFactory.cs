using System.Collections.Generic;
using System.Linq;
using Application.Factories.Interfaces;
using Core.Classes;

namespace Application.Factories
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
					resultIndex[result.PlayerId].Add(result);
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

            var totalResults = _cashgameTotalResultFactory.CreateList(players, cashgames);
			var mostTimeResult = GetMostTimeResult(totalResults);
            var biggestTotalBuyinResult = GetBiggestTotalBuyinResult(totalResults);
            var biggestTotalCashoutResult = GetBiggestTotalCashoutResult(totalResults);
			var bestTotalResult = totalResults.FirstOrDefault();
            var worstTotalResult = totalResults.LastOrDefault();

            return new CashgameFacts
                (
                    cashgames.Count(),
                    bestResult,
                    worstResult,
                    bestTotalResult,
                    worstTotalResult,
                    mostTimeResult,
                    biggestTotalBuyinResult,
                    biggestTotalCashoutResult,
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

        private CashgameTotalResult GetMostTimeResult(IEnumerable<CashgameTotalResult> results)
        {
            CashgameTotalResult mostTimeResult = null;
            foreach (var result in results)
            {
                if (mostTimeResult == null || result.TimePlayed > mostTimeResult.TimePlayed)
                {
                    mostTimeResult = result;
                }
            }
            return mostTimeResult;
        }

        private CashgameTotalResult GetBiggestTotalBuyinResult(IEnumerable<CashgameTotalResult> results)
        {
            CashgameTotalResult biggestTotalBuyinResult = null;
            foreach (var result in results)
            {
                if (biggestTotalBuyinResult == null || result.Buyin > biggestTotalBuyinResult.Buyin)
                {
                    biggestTotalBuyinResult = result;
                }
            }
            return biggestTotalBuyinResult;
        }

        private CashgameTotalResult GetBiggestTotalCashoutResult(IEnumerable<CashgameTotalResult> results)
        {
            CashgameTotalResult biggestTotalCashoutResult = null;
            foreach (var result in results)
            {
                if (biggestTotalCashoutResult == null || result.Cashout > biggestTotalCashoutResult.Cashout)
                {
                    biggestTotalCashoutResult = result;
                }
            }
            return biggestTotalCashoutResult;
        }

	}

}