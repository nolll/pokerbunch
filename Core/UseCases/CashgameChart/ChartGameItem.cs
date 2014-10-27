using System.Collections.Generic;
using Core.Entities;

namespace Core.UseCases.CashgameChart
{
    public class ChartGameItem
    {
        public Date Date { get; private set; }
        public IDictionary<int, int> Winnings { get; private set; }

        public ChartGameItem(Date date, IDictionary<int, int> winnings)
        {
            Date = date;
            Winnings = winnings;
        }
    }
}