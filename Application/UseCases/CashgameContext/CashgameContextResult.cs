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
            bunchContextResult.IsLoggedIn,
            bunchContextResult.IsAdmin,
            bunchContextResult.UserName,
            bunchContextResult.UserDisplayName,
            bunchContextResult.IsInProduction,
            bunchContextResult.Version,
            bunchContextResult.Slug,
            bunchContextResult.BunchId,
            bunchContextResult.BunchName)
        {
            GameIsRunning = gameIsRunning;
            Years = years;
            SelectedYear = selectedYear;
            LatestYear = latestYear;
        }

        protected CashgameContextResult(
            bool isLoggedIn,
            bool isAdmin,
            string userName,
            string userDisplayName,
            bool isInProduction,
            string version,
            string slug,
            int bunchId,
            string bunchName,
            bool gameIsRunning,
            IList<int> years,
            int? selectedYear,
            int? latestYear)
            : base(
            isLoggedIn,
            isAdmin,
            userName,
            userDisplayName,
            isInProduction,
            version,
            slug,
            bunchId,
            bunchName)
        {
            GameIsRunning = gameIsRunning;
            Years = years;
            SelectedYear = selectedYear;
            LatestYear = latestYear;
        }
    }
}