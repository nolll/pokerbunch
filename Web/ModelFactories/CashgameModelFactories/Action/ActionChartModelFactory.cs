using System;
using System.Collections.Generic;
using Application.Services.Interfaces;
using Core.Classes;
using Core.Classes.Checkpoints;
using Web.ModelFactories.ChartModelFactories;
using Web.Models.ChartModels;

namespace Web.ModelFactories.CashgameModelFactories.Action
{
    public class ActionChartModelFactory : IActionChartModelFactory
    {
        private readonly ITimeProvider _timeProvider;
        private readonly IChartValueModelFactory _chartValueModelFactory;

        public ActionChartModelFactory(
            ITimeProvider timeProvider,
            IChartValueModelFactory chartValueModelFactory)
        {
            _timeProvider = timeProvider;
            _chartValueModelFactory = chartValueModelFactory;
        }

        public ChartModel Create(Homegame homegame, Cashgame cashgame, CashgameResult result)
        {
            return new ChartModel
                {
                    cols = GetActionColumns(),
			        rows = GetActionRows(homegame, cashgame, result)
                };
        }

        private IList<ChartRowModel> GetActionRows(Homegame homegame, Cashgame cashgame, CashgameResult result)
        {
            var rowModels = new List<ChartRowModel>();
            var checkpoints = GetCheckpoints(result);
            var totalBuyin = 0;
            foreach (var checkpoint in checkpoints)
            {
                if (checkpoint.Type == CheckpointType.Buyin)
                {
                    if (totalBuyin > 0)
                    {
                        var stackBefore = checkpoint.Stack - checkpoint.Amount;
                        rowModels.Add(GetActionRow(TimeZoneInfo.ConvertTime(checkpoint.Timestamp, homegame.Timezone), stackBefore, totalBuyin));
                    }
                    totalBuyin += checkpoint.Amount;
                }
                rowModels.Add(GetActionRow(TimeZoneInfo.ConvertTime(checkpoint.Timestamp, homegame.Timezone), checkpoint.Stack, totalBuyin));
            }
            if (cashgame.Status == GameStatus.Running)
            {
                rowModels.Add(GetActionRow(TimeZoneInfo.ConvertTime(_timeProvider.GetTime(), homegame.Timezone), result.Stack, result.Buyin));
            }
            return rowModels;
        }

        private IEnumerable<Checkpoint> GetCheckpoints(CashgameResult result)
        {
            return PlayerIsInGame(result) ? result.Checkpoints : new List<Checkpoint>();
        }

        private bool PlayerIsInGame(CashgameResult result)
        {
            return result != null;
        }

        private IList<ChartColumnModel> GetActionColumns()
        {
            return new List<ChartColumnModel>
		        {
		            new ChartDateTimeColumnModel("Time", "HH:mm"),
		            new ChartNumberColumnModel("Stack"),
		            new ChartNumberColumnModel("Buyin")
		        };
        }

        private ChartRowModel GetActionRow(DateTime dateTime, int stack, int buyin)
        {
            var values = new List<ChartValueModel>
                {
                    _chartValueModelFactory.Create(dateTime),
                    _chartValueModelFactory.Create(stack),
                    _chartValueModelFactory.Create(buyin)
                };
            return new ChartRowModel
                {
                    c = values
                };
        }
    }
}