using System.Collections.Generic;
using Application.UseCases.BunchContext;
using Application.UseCases.CashgameContext;

namespace Tests.Common.FakeClasses
{
    public class CashgameContextResultInTest : CashgameContextResult
    {
        public CashgameContextResultInTest(
            BunchContextResult bunchContextResult = null,
            bool gameIsRunning = false,
            IList<int> years = null,
            int? selectedYear = null,
            int? latestYear = null)

            : base(
                bunchContextResult ?? new BunchContextResultInTest(), 
                gameIsRunning,
                years,
                selectedYear,
                latestYear)
        {
        }
    }
}