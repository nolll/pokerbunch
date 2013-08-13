using System.Collections.Generic;
using System.Linq;
using Core.Classes;

namespace Infrastructure.Factories{
    public class CashgameSuiteFactory : ICashgameSuiteFactory{

	    private readonly ICashgameTotalResultFactory _cashgameTotalResultFactory;

	    public CashgameSuiteFactory(ICashgameTotalResultFactory cashgameTotalResultFactory)
		{
		    _cashgameTotalResultFactory = cashgameTotalResultFactory;
		}

	    public CashgameSuite Create(List<Cashgame> cashgames, List<Player> players){
			var totalGameTime = 0;
			var resultIndex = GetPlayerIndex(players);
			CashgameResult bestResult = null;
			CashgameResult worstResult = null;

	        var sortedCashgames = cashgames.OrderByDescending(o => o.StartTime);
			foreach(var cashgame in sortedCashgames){
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
			}

			var totalResults = GetTotalResults(players, resultIndex);
			var mostTimeResult = GetMostTimeResult(totalResults);
			var bestTotalResult = totalResults.FirstOrDefault();

            return new CashgameSuite
                {
                    TotalResults = totalResults,
                    Cashgames = cashgames,
                    GameCount = cashgames.Count,
                    TotalGameTime = totalGameTime,
                    BestResult = bestResult,
                    WorstResult = worstResult,
                    MostTimeResult = mostTimeResult,
                    BestTotalResult = bestTotalResult
                };
		}

		private Dictionary<int, List<CashgameResult>> GetPlayerIndex(List<Player> players){
			var dictionary = new Dictionary<int, List<CashgameResult>>();
			foreach(var player in players){
				dictionary[player.Id] = new List<CashgameResult>();
			}
			return dictionary;
		}

		private List<CashgameTotalResult> GetTotalResults(List<Player> players, Dictionary<int, List<CashgameResult>> resultIndex){
			var totalResults = new List<CashgameTotalResult>();
			foreach(var player in players){
				var playerResults = resultIndex[player.Id];
				if(playerResults.Count > 0){
                    var totalResult = _cashgameTotalResultFactory.Create(player, playerResults);
					totalResults.Add(totalResult);
				}
			}
            return totalResults.OrderByDescending(o => o.Winnings).ToList();
		}

		public CashgameTotalResult GetMostTimeResult(List<CashgameTotalResult> results){
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