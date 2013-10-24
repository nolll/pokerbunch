using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Infrastructure.System;
using Web.Models.ChartModels;

namespace Web.ModelFactories.CashgameModelFactories.Chart
{
    public class CashgameSuiteChartModelFactory : ICashgameSuiteChartModelFactory
    {
        private readonly IGlobalization _globalization;

        public CashgameSuiteChartModelFactory(IGlobalization globalization)
        {
            _globalization = globalization;
        }

        public ChartModel Create(CashgameSuite suite)
        {
            return new ChartModel
                {
                    cols = GetColumnModels(suite.TotalResults),
                    rows = GetRowModels(suite.Cashgames, suite.TotalResults)
                };
        }

        private IList<ChartRowModel> GetRowModels(IList<Cashgame> cashgames, IList<CashgameTotalResult> results)
        {
            var playerSum = GetEmptyPlayerSumArray(results);
            var rowModels = new List<ChartRowModel>();
            rowModels.Add(GetFirstRow(results));
            for (var i = 0; i < cashgames.Count; i++)
            {
                var cashgame = cashgames[cashgames.Count - i - 1];
                var currentSum = new Dictionary<int, int?>();
                for (var j = 0; j < results.Count; j++)
                {
                    var totalResult = results[j];
                    var singleResult = cashgame.GetResult(totalResult.Player);
                    var playerId = totalResult.Player.Id;
                    if (singleResult != null || i == cashgames.Count - 1)
                    {
                        var res = singleResult != null ? singleResult.Stack - singleResult.Buyin : 0;
                        var sum = playerSum[playerId] + res;
                        playerSum[playerId] = sum;
                        currentSum[playerId] = sum;
                    }
                    else
                    {
                        currentSum[playerId] = null;
                    }
                }
                rowModels.Add(GetRowModel(cashgame, results, currentSum));
            }
            return rowModels;
        }

        private IDictionary<int, int?> GetEmptyPlayerSumArray(IEnumerable<CashgameTotalResult> results)
        {
            var playerSum = new Dictionary<int, int?>();
            foreach (var result in results)
            {
                playerSum[result.Player.Id] = 0;
            }
            return playerSum;
        }

        private IList<ChartColumnModel> GetColumnModels(IEnumerable<CashgameTotalResult> results)
        {
            var columnModels = new List<ChartColumnModel> {new ChartColumnModel("string", "Date")};
            columnModels.AddRange(results.Select(playerResult => new ChartColumnModel("number", playerResult.Player.DisplayName)));
            return columnModels;
        }

        private ChartRowModel GetFirstRow(IEnumerable<CashgameTotalResult> results)
        {
            var values = new List<ChartValueModel> {new ChartValueModel()};
            values.AddRange(results.Select(result => new ChartValueModel(0)));
            return new ChartRowModel
                {
                    c = values
                };
        }

        private ChartRowModel GetRowModel(Cashgame cashgame, IEnumerable<CashgameTotalResult> results, IDictionary<int, int?> currentSum)
        {
            var values = new List<ChartValueModel>();
            var dateStr = cashgame.StartTime.HasValue ? _globalization.FormatShortDate(cashgame.StartTime.Value) : string.Empty;
            values.Add(new ChartValueModel(dateStr));
            foreach (var result in results)
            {
                var sum = currentSum[result.Player.Id];
                values.Add(new ChartValueModel(sum));
            }
            return new ChartRowModel
                {
                    c = values
                };
        }
    }
}