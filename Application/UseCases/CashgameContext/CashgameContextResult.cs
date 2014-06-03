using System.Collections.Generic;

namespace Application.UseCases.CashgameContext
{
    public class BunchContextResult : ApplicationContextResult
    {
        public string Slug { get; private set; }
        public string BunchName { get; private set; }
        public int BunchId { get; private set; }

        public BunchContextResult(
            ApplicationContextResult applicationContextResult,
            string slug,
            int bunchId,
            string bunchName)

            : base(
            applicationContextResult.IsLoggedIn,
            applicationContextResult.IsAdmin,
            applicationContextResult.UserName,
            applicationContextResult.UserDisplayName,
            applicationContextResult.IsInProduction,
            applicationContextResult.Version)
        {
            Slug = slug;
            BunchId = bunchId;
            BunchName = bunchName;
        }

        protected BunchContextResult(
            bool isLoggedIn,
            bool isAdmin,
            string userName,
            string userDisplayName,
            bool isInProduction,
            string version,
            string slug,
            int bunchId,
            string bunchName)

            : base(
            isLoggedIn,
            isAdmin,
            userName,
            userDisplayName,
            isInProduction,
            version)
        {
            Slug = slug;
            BunchId = bunchId;
            BunchName = bunchName;
        }
    }

    public class BunchContextResultInTest : BunchContextResult
    {
        public BunchContextResultInTest(
            bool isLoggedIn = false, 
            bool isAdmin = false, 
            string userName = null, 
            string userDisplayName = null, 
            bool isInProduction = false, 
            string version = null,
            string slug = null, 
            int bunchId = default(int), 
            string bunchName = null)
            
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
        }
    }

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

    public class CashgameContextResultInTest : CashgameContextResult
    {
        public CashgameContextResultInTest(
            bool isLoggedIn = false, 
            bool isAdmin = false, 
            string userName = null, 
            string userDisplayName = null, 
            bool isInProduction = false, 
            string version = null,
            string slug = null, 
            int bunchId = default(int), 
            string bunchName = null,
            bool gameIsRunning = false,
            IList<int> years = null,
            int? selectedYear = null,
            int? latestYear = null)
            
            : base(
            isLoggedIn,
            isAdmin,
            userName,
            userDisplayName,
            isInProduction,
            version,
            slug,
            bunchId,
            bunchName,
            gameIsRunning,
            years,
            selectedYear,
            latestYear)
        {
        }
    }
}