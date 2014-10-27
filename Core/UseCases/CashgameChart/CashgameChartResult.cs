using System.Collections.Generic;

namespace Core.UseCases.CashgameChart
{
    public class CashgameChartResult
    {
        public IList<ChartGameItem> GameItems { get; private set; }
        public IList<ChartPlayerItem> PlayerItems { get; private set; }

        public CashgameChartResult(IList<ChartGameItem> gameItems, IList<ChartPlayerItem> playerItems)
        {
            GameItems = gameItems;
            PlayerItems = playerItems;
        }
    }
}