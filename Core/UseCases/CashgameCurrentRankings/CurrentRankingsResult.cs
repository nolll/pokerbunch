using System.Collections.Generic;
using System.Linq;

namespace Core.UseCases.CashgameCurrentRankings
{
    public class CurrentRankingsResult
    {
        public IList<CurrentRankingsItem> Items { get; private set; }

        public CurrentRankingsResult(IEnumerable<CurrentRankingsItem> items)
        {
            Items = items.ToList();
        }
    }
}