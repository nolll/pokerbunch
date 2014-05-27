using System.Collections.Generic;

namespace Application.UseCases.CashgameContext
{
    public class BunchContextResult
    {
        public string Slug { get; set; }
        public string BunchName { get; set; }
    }

    public class CashgameContextResult : BunchContextResult
    {
        public bool GameIsRunning { get; set; }
        public IList<int> Years { get; set; }
        public int? SelectedYear { get; set; }
        public int? LatestYear { get; set; }
    }
}