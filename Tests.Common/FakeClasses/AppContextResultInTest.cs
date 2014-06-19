using Application.UseCases.AppContext;

namespace Tests.Common.FakeClasses
{
    public class AppContextResultInTest : AppContextResult
    {
        public AppContextResultInTest(
            bool isLoggedIn = false, 
            bool isAdmin = false, 
            string userName = "", 
            string userDisplayName = "", 
            bool isInProduction = false, 
            string version = "")
            
            : base(
                isLoggedIn, 
                isAdmin, 
                userName, 
                userDisplayName, 
                isInProduction, 
                version)
        {
        }
    }
}