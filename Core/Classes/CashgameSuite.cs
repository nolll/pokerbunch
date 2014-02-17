using System.Collections.Generic;

namespace Core.Classes
{
    public class CashgameSuite
    {
	    public IList<Cashgame> Cashgames { get; private set; }
        public IList<CashgameTotalResult> TotalResults { get; private set; }

        public CashgameSuite(
            IList<Cashgame> cashgames, 
            IList<CashgameTotalResult> totalResults)
        {
            Cashgames = cashgames;
            TotalResults = totalResults;
        }

        public bool SpansMultipleYears()
        {
            var years = new List<int>();
            foreach (var cashgame in Cashgames)
            {
                if (cashgame.StartTime.HasValue)
                {
                    var year = cashgame.StartTime.Value.Year;
                    if (!years.Contains(year))
                    {
                        years.Add(year);
                    }
                }
            }
            return years.Count > 1;
        }
	}
}