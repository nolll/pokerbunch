using Application.UseCases.AppContext;

namespace Tests.Common.FakeClasses
{
    public class AppContextResultInTest : AppContextResult
    {
        public AppContextResultInTest(
            BaseContextResult baseContextResult = null,
            bool isLoggedIn = false, 
            bool isAdmin = false, 
            string userName = "", 
            string userDisplayName = "")
            
            : base(
                baseContextResult ?? new BaseContextResultInTest(),
                isLoggedIn, 
                isAdmin, 
                userName, 
                userDisplayName)
        {
        }
    }

    public class BaseContextResultInTest : BaseContextResult
    {
        public BaseContextResultInTest(
            bool isInProduction = false,
            string version = null)
            
            : base(
            isInProduction,
            version)
        {
        }
    }
}