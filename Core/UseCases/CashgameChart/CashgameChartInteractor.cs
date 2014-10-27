using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases.CashgameChart
{
    public class CashgameChartInteractor
    {
        public static CashgameChartResult Execute(IBunchRepository bunchRepository, ICashgameService cashgameService, CashgameChartRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            var suite = cashgameService.GetSuite(bunch.Id, request.Year);

            var playerItems = GetPlayerItems(suite.TotalResults);
            var gameItems = GetGameItems(suite.Cashgames, suite.TotalResults);

            return new CashgameChartResult(gameItems, playerItems);
        }

        private static IList<ChartPlayerItem> GetPlayerItems(IEnumerable<CashgameTotalResult> results)
        {
            return results.Select(result => new ChartPlayerItem(result.Player.Id, result.Player.DisplayName)).ToList();
        }

        private static IList<ChartGameItem> GetGameItems(IList<Cashgame> cashgames, IList<CashgameTotalResult> results)
        {
            var playerSum = GetEmptyPlayerSumArray(results);
            var gameItems = new List<ChartGameItem>();
            for (var i = 0; i < cashgames.Count; i++)
            {
                var cashgame = cashgames[cashgames.Count - i - 1];
                var currentSums = new Dictionary<int, int>();
                foreach (var totalResult in results)
                {
                    var singleResult = cashgame.GetResult(totalResult.Player.Id);
                    var playerId = totalResult.Player.Id;
                    if (singleResult != null || i == cashgames.Count - 1)
                    {
                        var res = singleResult != null ? singleResult.Stack - singleResult.Buyin : 0;
                        var sum = playerSum[playerId] + res;

                        playerSum[playerId] = sum;
                        currentSums.Add(playerId, sum.Value);
                    }
                }
                var gameItem = new ChartGameItem(new Date(cashgame.StartTime.Value), currentSums);
                gameItems.Add(gameItem);
            }
            return gameItems;
        }

        private static IDictionary<int, int?> GetEmptyPlayerSumArray(IEnumerable<CashgameTotalResult> results)
        {
            var playerSum = new Dictionary<int, int?>();
            foreach (var result in results)
            {
                playerSum.Add(result.Player.Id, 0);
            }
            return playerSum;
        }
    }
}
