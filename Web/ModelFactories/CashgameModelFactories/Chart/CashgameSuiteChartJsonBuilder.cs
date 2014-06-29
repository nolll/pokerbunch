using System.Collections.Generic;
using System.Linq;
using Application.Services;
using Core.Entities;
using Core.Repositories;
using Core.Services.Interfaces;
using Web.ModelFactories.ChartModelFactories;
using Web.Models.ChartModels;

namespace Web.ModelFactories.CashgameModelFactories.Chart
{
    public class CashgameSuiteChartJsonBuilder : ICashgameSuiteChartJsonBuilder
    {
        private readonly IChartValueModelFactory _chartValueModelFactory;
        private readonly IPlayerRepository _playerRepository;
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameService _cashgameService;

        public CashgameSuiteChartJsonBuilder(
            IChartValueModelFactory chartValueModelFactory,
            IPlayerRepository playerRepository,
            IHomegameRepository homegameRepository,
            ICashgameService cashgameService)
        {
            _chartValueModelFactory = chartValueModelFactory;
            _playerRepository = playerRepository;
            _homegameRepository = homegameRepository;
            _cashgameService = cashgameService;
        }

        public ChartModel Build(string slug, int? year)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var suite = _cashgameService.GetSuite(homegame, year);
            
            return new ChartModel
                {
                    Columns = GetColumnModels(suite.TotalResults),
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

        private IList<ChartColumnModel> GetColumnModels(IEnumerable<CashgameTotalResult> results)
        {
            var columnModels = new List<ChartColumnModel> {new ChartColumnModel("string", "Date")};
            foreach (var result in results)
            {
                var player = _playerRepository.GetById(result.Player.Id);
                columnModels.Add(new ChartColumnModel("number", player.DisplayName));
            }
            return columnModels;
        }

        private ChartRowModel GetFirstRow(IEnumerable<CashgameTotalResult> results)
        {
            var values = new List<ChartValueModel> {_chartValueModelFactory.Create()};
            values.AddRange(results.Select(result => _chartValueModelFactory.Create(0)));
            return new ChartRowModel
                {
                    C = values
                };
        }

        private ChartRowModel GetRowModel(Cashgame cashgame, IEnumerable<CashgameTotalResult> results, IDictionary<int, int?> currentSum)
        {
            var values = new List<ChartValueModel>();
            var dateStr = cashgame.StartTime.HasValue ? Globalization.FormatShortDate(cashgame.StartTime.Value) : string.Empty;
            values.Add(_chartValueModelFactory.Create(dateStr));
            foreach (var result in results)
            {
                var sum = currentSum[result.Player.Id];
                values.Add(_chartValueModelFactory.Create(sum));
            }
            return new ChartRowModel
                {
                    C = values
                };
        }
    }
}