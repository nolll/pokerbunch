using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Core.UseCases.CashgameDetailsChart;
using Web.Models.ChartModels;

namespace Web.Models.CashgameModels.Details
{
    public class DetailsChartModel : ChartModel
    {
        public DetailsChartModel(CashgameDetailsChartResult cashgameDetailsChartResult)
            : base(
            GetColumns(cashgameDetailsChartResult.PlayerItems),
            GetRows(cashgameDetailsChartResult.PlayerItems))
        {
        }

        private static IList<ChartRowModel> GetRows(IList<DetailsChartPlayerItem> playerItems)
        {
            var rowModels = new List<ChartRowModel>();
            foreach (var playerItem in playerItems)
            {
                foreach (var resultItem in playerItem.Results)
                {
                    rowModels.Add(GetRow(playerItems, resultItem, playerItem.Id));
                }
            }
            return rowModels;
        }

        private static IList<ChartColumnModel> GetColumns(IList<DetailsChartPlayerItem> playerItems)
        {
            var columnModels = new List<ChartColumnModel> { new ChartDateTimeColumnModel("Time", "HH:mm") };
            columnModels.AddRange(playerItems.Select(item => new ChartNumberColumnModel(item.Name)));
            return columnModels;
        }

        private static ChartRowModel GetRow(IList<DetailsChartPlayerItem> playerItems, DetailsChartResultItem resultItem, int playerId)
        {
            var values = new List<ChartValueModel> { new ChartDateTimeValueModel(resultItem.Timestamp) };
            foreach (var item in playerItems)
            {
                string val = null;
                if (item.Id == playerId)
                {
                    val = resultItem.Winnings.ToString(CultureInfo.InvariantCulture);
                }
                values.Add(new ChartValueModel(val));
            }
            return new ChartRowModel(values);
        }
    }
}