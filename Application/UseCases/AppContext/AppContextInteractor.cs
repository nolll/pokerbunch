using Application.Services;

namespace Application.UseCases.AppContext
{
    public class AppContextInteractor : IAppContextInteractor
    {
        private readonly IAuth _auth;

        public AppContextInteractor(IAuth auth)
        {
            _auth = auth;
        }

        public AppContextResult Execute()
        {
            var user = _auth.CurrentUser;
            var isLoggedIn = user != null;
            var userName = isLoggedIn ? user.UserName : string.Empty;
            var userDisplayName = isLoggedIn ? user.DisplayName : string.Empty;
            var isAdmin = isLoggedIn && user.IsAdmin;
            const bool isInProduction = false;
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            return new AppContextResult(
                isLoggedIn,
                isAdmin,
                userName,
                userDisplayName,
                isInProduction,
                version);
        }
    }
}