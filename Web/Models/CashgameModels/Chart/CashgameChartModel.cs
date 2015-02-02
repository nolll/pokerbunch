using System.Collections.Generic;
using System.Linq;
using Core.Services;
using Core.UseCases.CashgameChart;
using Web.Models.ChartModels;

namespace Web.Models.CashgameModels.Chart
{
    public class CashgameChartModel : ChartModel
    {
        public CashgameChartModel(CashgameChartResult cashgameChartResult)
            : base(
                GetColumnModels(cashgameChartResult.PlayerItems),
                GetRowModels(cashgameChartResult.GameItems, cashgameChartResult.PlayerItems))
        {
        }

        private static IList<ChartRowModel> GetRowModels(IList<ChartGameItem> gameItems, IList<ChartPlayerItem> playerItems)
        {
            var rowModels = new List<ChartRowModel> {GetFirstRow(gameItems)};

            foreach (var gameItem in gameItems)
            {
                rowModels.Add(GetRowModel(gameItem, playerItems));
            }
            return rowModels;
        }

        private static IList<ChartColumnModel> GetColumnModels(IEnumerable<ChartPlayerItem> playerItems)
        {
            var columnModels = new List<ChartColumnModel> { new ChartColumnModel("string", "Date") };
            columnModels.AddRange(playerItems.Select(item => new ChartColumnModel("number", item.Name)));
            return columnModels;
        }

        private static ChartRowModel GetFirstRow(IEnumerable<ChartGameItem> gameItems)
        {
            var values = new List<ChartValueModel> { new ChartValueModel("") };
            values.AddRange(gameItems.Select(result => new ChartIntValueModel(0)));
            return new ChartRowModel(values);
        }

        private static ChartRowModel GetRowModel(ChartGameItem gameItem, IEnumerable<ChartPlayerItem> playerItems)
        {
            var values = new List<ChartValueModel> { new ChartValueModel(Globalization.FormatShortDate(gameItem.Date)) };
            foreach (var playerItem in playerItems)
            {
                values.Add(GetValueModel(gameItem, playerItem));
            }
            return new ChartRowModel(values);
        }

        private static ChartValueModel GetValueModel(ChartGameItem gameItem, ChartPlayerItem playerItem)
        {
            int sum;
            if (gameItem.Winnings.TryGetValue(playerItem.Id, out sum))
                return new ChartIntValueModel(sum);
            return new ChartIntValueModel(null);
        }
    }
}