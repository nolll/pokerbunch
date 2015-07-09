using Core.UseCases.AppContext;
using Core.UseCases.BaseContext;

namespace Tests.Common.FakeClasses
{
    public class AppContextResultInTest : AppContextResult
    {
        public AppContextResultInTest(
            BaseContextResult baseContextResult = null,
            bool isLoggedIn = false,
            bool isAdmin = false,
            int userId = 0,
            string userName = "",
            string userDisplayName = "")

            : base(
                baseContextResult ?? new BaseContextResultInTest(),
                isLoggedIn,
                isAdmin,
                userId,
                userName,
                userDisplayName)
        {
        }
    }
}