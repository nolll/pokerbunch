using System.Collections.Generic;
using Application.UseCases.BunchContext;

namespace Application.UseCases.CashgameContext
{
    public class CashgameContextResult
    {
        public bool GameIsRunning { get; private set; }
        public CashgamePage SelectedPage { get; private set; }
        public IList<int> Years { get; private set; }
        public int? SelectedYear { get; private set; }
        public int? LatestYear { get; private set; }
        public BunchContextResult BunchContext { get; private set; }

        public CashgameContextResult(
            BunchContextResult bunchContextResult,
            bool gameIsRunning,
            CashgamePage selectedPage,
            IList<int> years,
            int? selectedYear,
            int? latestYear)
        {
            BunchContext = bunchContextResult;
            GameIsRunning = gameIsRunning;
            SelectedPage = selectedPage;
            Years = years;
            SelectedYear = selectedYear;
            LatestYear = latestYear;
        }
    }
}