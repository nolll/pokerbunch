using System.Collections.Generic;
using System.Linq;
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
            var values = new List<ChartValueModel> { new ChartValueModel() };
            values.AddRange(gameItems.Select(result => new ChartValueModel(0)));
            return new ChartRowModel
            {
                C = values
            };
        }

        private static ChartRowModel GetRowModel(ChartGameItem gameItem, IList<ChartPlayerItem> playerItems)
        {
            var values = new List<ChartValueModel>();
            values.Add(new ChartValueModel(gameItem.DateStr));
            foreach (var playerItem in playerItems)
            {
                int sum;
                if (gameItem.Winnings.TryGetValue(playerItem.Id, out sum))
                {
                    values.Add(new ChartValueModel(sum));
                }
                else
                {
                    values.Add(new ChartValueModel((int?)null));
                }
            }
            return new ChartRowModel
            {
                C = values
            };
        }
    }
}