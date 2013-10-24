using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Core.Classes;
using Core.Classes.Checkpoints;
using Infrastructure.System;
using Web.Models.ChartModels;

namespace Web.ModelFactories.CashgameModelFactories.Details
{
    public class CashgameDetailsChartModelFactory : ICashgameDetailsChartModelFactory
    {
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
            var timestamp = DateTimeFactory.Now(homegame.Timezone);
            var row = new ChartRowModel();
            row.AddValue(new ChartDateTimeValueModel(timestamp));
            foreach (var result in results)
            {
                var winnings = result.Stack - result.Buyin;
                row.AddValue(new ChartValueModel(winnings));
            }
            return row;
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
            var row1 = new ChartRowModel();
            row1.AddValue(new ChartDateTimeValueModel(dateTime));
            var playerNames = cashgame.GetPlayerNames();
            foreach (var playerName in playerNames)
            {
                string val = null;
                if (playerName == currentPlayerName)
                {
                    val = winnings.ToString(CultureInfo.InvariantCulture);
                }
                row1.AddValue(new ChartValueModel(val));
            }
            return row1;
        }
    }
}