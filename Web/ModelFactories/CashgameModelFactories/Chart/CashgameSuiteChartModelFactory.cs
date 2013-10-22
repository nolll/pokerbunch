using System.Collections.Generic;
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
            var columnModels = new List<ChartColumnModel>();
            columnModels.Add(new ChartColumnModel("string", "Date"));
            foreach (var playerResult in results)
            {
                columnModels.Add(new ChartColumnModel("number", playerResult.Player.DisplayName));
            }
            return columnModels;
        }

        private ChartRowModel GetFirstRow(IEnumerable<CashgameTotalResult> results)
        {
            var row1 = new ChartRowModel();
            row1.AddValue(new ChartValueModel());
            foreach (var result in results)
            {
                row1.AddValue(new ChartValueModel(0));
            }
            return row1;
        }

        private ChartRowModel GetRowModel(Cashgame cashgame, IEnumerable<CashgameTotalResult> results, IDictionary<int, int?> currentSum)
        {
            var row1 = new ChartRowModel();
            var dateStr = cashgame.StartTime.HasValue ? _globalization.FormatShortDate(cashgame.StartTime.Value) : string.Empty;
            row1.AddValue(new ChartValueModel(dateStr));
            foreach (var result in results)
            {
                var sum = currentSum[result.Player.Id];
                row1.AddValue(new ChartValueModel(sum));
            }
            return row1;
        }
    }
}