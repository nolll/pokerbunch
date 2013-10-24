using System;
using System.Collections.Generic;
using Core.Classes;
using Core.Classes.Checkpoints;
using Infrastructure.System;
using Web.Models.ChartModels;

namespace Web.ModelFactories.CashgameModelFactories.Action
{
    public class ActionChartModelFactory : IActionChartModelFactory
    {
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
                        rowModels.Add(GetActionRow(checkpoint.Timestamp, stackBefore, totalBuyin));
                    }
                    totalBuyin += checkpoint.Amount;
                }
                rowModels.Add(GetActionRow(checkpoint.Timestamp, checkpoint.Stack, totalBuyin));
            }
            if (cashgame.Status == GameStatus.Running)
            {
                var timestamp = DateTimeFactory.Now(homegame.Timezone);
                rowModels.Add(GetActionRow(timestamp, result.Stack, result.Buyin));
            }
            return rowModels;
        }

        private IEnumerable<Checkpoint> GetCheckpoints(CashgameResult result)
        {
            if (PlayerIsInGame(result))
            {
                return result.Checkpoints;
            }
            else
            {
                return new List<Checkpoint>();
            }
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
            var row = new ChartRowModel();
            row.AddValue(new ChartDateTimeValueModel(dateTime));
            row.AddValue(new ChartValueModel(stack));
            row.AddValue(new ChartValueModel(buyin));
            return row;
        }
    }
}