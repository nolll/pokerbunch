using System.Collections.Generic;
using Application.UseCases.BunchContext;

namespace Application.UseCases.CashgameContext
{
    public class CashgameContextResult : BunchContextResult
    {
        public bool GameIsRunning { get; private set; }
        public IList<int> Years { get; private set; }
        public int? SelectedYear { get; private set; }
        public int? LatestYear { get; private set; }

        public CashgameContextResult(
            BunchContextResult bunchContextResult,
            bool gameIsRunning,
            IList<int> years,
            int? selectedYear,
            int? latestYear)
            : base(
            bunchContextResult,
            bunchContextResult.Slug,
            bunchContextResult.BunchId,
            bunchContextResult.BunchName)
        {
            GameIsRunning = gameIsRunning;
            Years = years;
            SelectedYear = selectedYear;
            LatestYear = latestYear;
        }
    }
}