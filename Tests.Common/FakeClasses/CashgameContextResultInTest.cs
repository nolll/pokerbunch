using System.Collections.Generic;
using Core.UseCases.BunchContext;
using Core.UseCases.CashgameContext;

namespace Tests.Common.FakeClasses
{
    public class CashgameContextResultInTest : CashgameContextResult
    {
        public CashgameContextResultInTest(
            BunchContextResult bunchContextResult = null,
            CashgamePage selectedPage = CashgamePage.Matrix,
            bool gameIsRunning = false,
            IList<int> years = null,
            int? selectedYear = null,
            int? latestYear = null)

            : base(
            bunchContextResult ?? new BunchContextResultInTest(), 
            gameIsRunning,
            selectedPage, 
            years,
            selectedYear,
            latestYear)
        {
        }
    }
}