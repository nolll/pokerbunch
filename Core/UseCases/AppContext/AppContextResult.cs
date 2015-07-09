using Core.Urls;
using Core.UseCases.BaseContext;

namespace Core.UseCases.AppContext
{
    public class AppContextResult
    {
        public bool IsLoggedIn { get; private set; }
        public bool IsAdmin { get; private set; }
        public string UserDisplayName { get; private set; }
        public BaseContextResult BaseContext { get; private set; }
        public Url LoginUrl { get; private set; }
        public Url AddUserUrl { get; private set; }
        public Url ForgotPasswordUrl { get; private set; }
        public Url LogoutUrl { get; private set; }
        public Url UserDetailsUrl { get; private set; }

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
            UserDisplayName = userDisplayName;
            LoginUrl = new LoginUrl();
            AddUserUrl = new AddUserUrl();
            ForgotPasswordUrl = new ForgotPasswordUrl();
            LogoutUrl = new LogoutUrl();
            UserDetailsUrl = new UserDetailsUrl(userName);
        }
    }
}