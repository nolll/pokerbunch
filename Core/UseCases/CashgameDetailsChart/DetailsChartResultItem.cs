using System;

namespace Core.UseCases.CashgameDetailsChart
{
    public class DetailsChartResultItem
    {
        public DateTime Timestamp { get; private set; }
        public int Winnings { get; private set; }

        public DetailsChartResultItem(DateTime timestamp, int winnings)
        {
            Timestamp = timestamp;
            Winnings = winnings;
        }
    }
}