using System.Collections.Generic;
using Application.UseCases.CashgameContext;

namespace Tests.Common.FakeClasses
{
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