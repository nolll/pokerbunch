using System.Collections.Generic;

namespace Core.UseCases.CashgameDetailsChart
{
    public class CashgameDetailsChartResult
    {
        public IList<DetailsChartPlayerItem> PlayerItems { get; private set; }

        public CashgameDetailsChartResult(IList<DetailsChartPlayerItem> playerItems)
        {
            PlayerItems = playerItems;
        }
    }
}