using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.Services.Interfaces;
using Web.Models.ChartModels;

namespace Web.ModelFactories.CashgameModelFactories.Chart
{
    public class ChartPlayerItem
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public ChartPlayerItem(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class ChartGameItem
    {
        public string DateStr { get; set; }
        public IList<ResultItem> Results { get; set; }
    }

    public class ResultItem
    {
        public int PlayerId { get; set; }
        public int Stack { get; set; }
    }

    public class CashgameSuiteChartJsonBuilder : ICashgameSuiteChartJsonBuilder
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameService _cashgameService;

        public CashgameSuiteChartJsonBuilder(
            IBunchRepository bunchRepository,
            ICashgameService cashgameService)
        {
            _bunchRepository = bunchRepository;
            _cashgameService = cashgameService;
        }

        public ChartModel Build(string slug, int? year)
        {
            var bunch = _bunchRepository.GetBySlug(slug);
            var suite = _cashgameService.GetSuite(bunch, year);

            var playerItems = GetPlayerItems(suite.TotalResults);

            return new ChartModel
                {
                    Columns = GetColumnModels(playerItems),
                    Rows = GetRowModels(suite.Cashgames, suite.TotalResults)
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
                foreach (var totalResult in results)
                {
                    var singleResult = cashgame.GetResult(totalResult.Player.Id);
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

        private IList<ChartPlayerItem> GetPlayerItems(IEnumerable<CashgameTotalResult> results)
        {
            return results.Select(result => new ChartPlayerItem(result.Player.Id, result.Player.DisplayName)).ToList();
        }

        private IList<ChartColumnModel> GetColumnModels(IEnumerable<ChartPlayerItem> playerItems)
        {
            var columnModels = new List<ChartColumnModel> { new ChartColumnModel("string", "Date") };
            columnModels.AddRange(playerItems.Select(item => new ChartColumnModel("number", item.Name)));
            return columnModels;
        }

        private ChartRowModel GetFirstRow(IEnumerable<CashgameTotalResult> results)
        {
            var values = new List<ChartValueModel> { new ChartValueModel() };
            values.AddRange(results.Select(result => new ChartValueModel(0)));
            return new ChartRowModel
                {
                    C = values
                };
        }

        private ChartRowModel GetRowModel(Cashgame cashgame, IEnumerable<CashgameTotalResult> results, IDictionary<int, int?> currentSum)
        {
            var values = new List<ChartValueModel>();
            var dateStr = cashgame.StartTime.HasValue ? Globalization.FormatShortDate(cashgame.StartTime.Value) : string.Empty;
            values.Add(new ChartValueModel(dateStr));
            foreach (var result in results)
            {
                var sum = currentSum[result.Player.Id];
                values.Add(new ChartValueModel(sum));
            }
            return new ChartRowModel
            {
                C = values
            };
        }

        private ChartRowModel GetGameItem(Cashgame cashgame, IEnumerable<CashgameTotalResult> results, IDictionary<int, int?> currentSum)
        {
            var gameItem = new ChartGameItem();
            var values = new List<ChartValueModel>();
            var dateStr = cashgame.StartTime.HasValue ? Globalization.FormatShortDate(cashgame.StartTime.Value) : string.Empty;
            values.Add(new ChartValueModel(dateStr));
            foreach (var result in results)
            {
                var sum = currentSum[result.Player.Id];
                values.Add(new ChartValueModel(sum));
            }
            return new ChartRowModel
            {
                C = values
            };
        }
    }
}