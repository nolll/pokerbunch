using Core.UseCases.BaseContext;

namespace Core.UseCases.AppContext
{
    public class AppContextResult
    {
        public bool IsLoggedIn { get; private set; }
        public string UserName { get; private set; }
        public string UserDisplayName { get; private set; }
        public BaseContextResult BaseContext { get; private set; }

        public AppContextResult(
            BaseContextResult baseContextResult,
            bool isLoggedIn,
            string userName,
            string userDisplayName)
        {
            BaseContext = baseContextResult;
            IsLoggedIn = isLoggedIn;
            UserName = userName;
            UserDisplayName = userDisplayName;
        }
    }
}