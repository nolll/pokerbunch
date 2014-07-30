using Application.UseCases.AppContext;
using Application.UseCases.BaseContext;

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
}