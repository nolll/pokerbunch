using System.Collections.Generic;

namespace Application.UseCases.CashgameContext
{
    public class ApplicationContextResult
    {
        public bool IsLoggedIn { get; set; }
        public bool IsAdmin { get; set; }
        public string UserName { get; set; }
        public string UserDisplayName { get; set; }
        public bool IsInProduction { get; set; }
        public string Version { get; set; }
    }

    public class BunchContextResult : ApplicationContextResult
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