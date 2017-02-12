using System.Collections.Generic;
using Core.Entities;

namespace Core.Services
{
    public static class CashgameService
    {
        public static bool SpansMultipleYears(IEnumerable<ListCashgame> cashgames)
        {
            var years = new List<int>();
            foreach (var cashgame in cashgames)
            {
                var year = cashgame.StartTime.Year;
                if (!years.Contains(year))
                {
                    years.Add(year);
                }
            }
            return years.Count > 1;
        }
    }
}