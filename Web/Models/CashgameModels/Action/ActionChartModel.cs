using System;
using System.Collections.Generic;
using Core.UseCases.ActionsChart;
using Web.Models.ChartModels;

namespace Web.Models.CashgameModels.Action
{
    public class ActionChartModel : ChartModel
    {
        public ActionChartModel(ActionsChartResult actionsChartResult)
            : base(
            GetActionColumns(),
            GetRowsNew(actionsChartResult.CheckpointItems))
        {
        }

        private static IList<ChartRowModel> GetRowsNew(IList<ActionsChartCheckpointItem> checkpointItems)
        {
            var rowModels = new List<ChartRowModel>();
            foreach (var item in checkpointItems)
            {
                if (item.AddedMoney > 0)
                {
                    var stackBefore = item.Stack - item.AddedMoney;
                    var buyinBefore = item.TotalBuyin - item.AddedMoney;
                    rowModels.Add(GetActionRow(item.Timestamp, stackBefore, buyinBefore));
                }
                rowModels.Add(GetActionRow(item.Timestamp, item.Stack, item.TotalBuyin));
            }
            return rowModels;
        }

        private static IList<ChartColumnModel> GetActionColumns()
        {
            return new List<ChartColumnModel>
		        {
		            new ChartDateTimeColumnModel("Time", "HH:mm"),
		            new ChartNumberColumnModel("Stack"),
		            new ChartNumberColumnModel("Buyin")
		        };
        }

        private static ChartRowModel GetActionRow(DateTime dateTime, int stack, int buyin)
        {
            var values = new List<ChartValueModel>
                {
                    new ChartDateTimeValueModel(dateTime),
                    new ChartIntValueModel(stack),
                    new ChartIntValueModel(buyin)
                };
            return new ChartRowModel(values);
        }
    }
}