using Core.UseCases;

namespace Tests.Common.FakeClasses
{
    public class AppContextResultInTest : AppContext.Result
    {
        public AppContextResultInTest(
            BaseContext.Result baseContextResult = null,
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