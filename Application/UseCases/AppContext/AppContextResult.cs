using Application.UseCases.BaseContext;

namespace Application.UseCases.AppContext
{
    public class AppContextResult
    {
        public bool IsLoggedIn { get; private set; }
        public bool IsAdmin { get; private set; }
        public string UserName { get; private set; }
        public string UserDisplayName { get; private set; }
        public BaseContextResult BaseContext { get; private set; }

        public AppContextResult(
            BaseContextResult baseContextResult,
            bool isLoggedIn,
            bool isAdmin,
            string userName,
            string userDisplayName)
        {
            BaseContext = baseContextResult;
            IsLoggedIn = isLoggedIn;
            IsAdmin = isAdmin;
            UserName = userName;
            UserDisplayName = userDisplayName;
        }
    }
}