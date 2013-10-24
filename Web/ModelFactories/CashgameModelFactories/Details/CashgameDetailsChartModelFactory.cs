using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Core.Classes;
using Core.Classes.Checkpoints;
using Infrastructure.System;
using Web.ModelFactories.ChartModelFactories;
using Web.Models.ChartModels;

namespace Web.ModelFactories.CashgameModelFactories.Details
{
    public class CashgameDetailsChartModelFactory : ICashgameDetailsChartModelFactory
    {
        private readonly ITimeProvider _timeProvider;
        private readonly IChartValueModelFactory _chartValueModelFactory;

        public CashgameDetailsChartModelFactory(
            ITimeProvider timeProvider,
            IChartValueModelFactory chartValueModelFactory)
        {
            _timeProvider = timeProvider;
            _chartValueModelFactory = chartValueModelFactory;
        }

        public ChartModel Create(Homegame homegame, Cashgame cashgame)
        {
            return new ChartModel
                {
                    cols = GetActionColumns(cashgame),
                    rows = GetActionRows(homegame, cashgame)
                };
        }

        private IList<ChartRowModel> GetActionRows(Homegame homegame, Cashgame cashgame)
        {
            var rowModels = new List<ChartRowModel>();
            var results = cashgame.Results;
            foreach (var result in results)
            {
                var totalBuyin = 0;
                var checkpoints = result.Checkpoints;
                var playerName = result.Player.DisplayName;
                foreach (var checkpoint in checkpoints)
                {
                    if (checkpoint.Type == CheckpointType.Buyin)
                    {
                        totalBuyin += checkpoint.Amount;
                    }
                    rowModels.Add(GetActionRow(cashgame, checkpoint.Timestamp, checkpoint.Stack - totalBuyin, playerName));
                }
            }
            if (cashgame.Status == GameStatus.Running)
            {
                rowModels.Add(GetCurrentStacks(homegame, results));
            }
            return rowModels;
        }

        private ChartRowModel GetCurrentStacks(Homegame homegame, IEnumerable<CashgameResult> results)
        {
            var timestamp = _timeProvider.GetTime(homegame.Timezone);
            var values = new List<ChartValueModel> {_chartValueModelFactory.Create(timestamp)};
            values.AddRange(results.Select(result => _chartValueModelFactory.Create(result.Winnings)));
            return new ChartRowModel
                {
                    c = values
                };
        }

        private IList<ChartColumnModel> GetActionColumns(Cashgame cashgame)
        {
            var columnModels = new List<ChartColumnModel> { new ChartDateTimeColumnModel("Time", "HH:mm") };
            var playerNames = cashgame.GetPlayerNames();
            columnModels.AddRange(playerNames.Select(playerName => new ChartNumberColumnModel(playerName)));
            return columnModels;
        }

        private ChartRowModel GetActionRow(Cashgame cashgame, DateTime dateTime, int winnings, string currentPlayerName)
        {
            var values = new List<ChartValueModel>();
            values.Add(_chartValueModelFactory.Create(dateTime));
            var playerNames = cashgame.GetPlayerNames();
            foreach (var playerName in playerNames)
            {
                string val = null;
                if (playerName == currentPlayerName)
                {
                    val = winnings.ToString(CultureInfo.InvariantCulture);
                }
                values.Add(_chartValueModelFactory.Create(val));
            }
            return new ChartRowModel
                {
                    c = values
                };
        }
    }
}