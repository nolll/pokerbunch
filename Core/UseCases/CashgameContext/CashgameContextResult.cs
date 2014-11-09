using System.Collections.Generic;
using Core.UseCases.BunchContext;

namespace Core.UseCases.CashgameContext
{
    public class CashgameContextResult
    {
        public bool GameIsRunning { get; private set; }
        public CashgamePage SelectedPage { get; private set; }
        public IList<int> Years { get; private set; }
        public int? SelectedYear { get; private set; }
        public BunchContextResult BunchContext { get; private set; }

        public CashgameContextResult(
            BunchContextResult bunchContextResult,
            bool gameIsRunning,
            CashgamePage selectedPage,
            IList<int> years,
            int? selectedYear)
        {
            BunchContext = bunchContextResult;
            GameIsRunning = gameIsRunning;
            SelectedPage = selectedPage;
            Years = years;
            SelectedYear = selectedYear;
        }
    }
}