using Application.Services;

namespace Application.UseCases.ApplicationContext
{
    public class ApplicationContextInteractor : IApplicationContextInteractor
    {
        private readonly IAuth _auth;

        public ApplicationContextInteractor(IAuth auth)
        {
            _auth = auth;
        }

        public ApplicationContextResult Execute()
        {
            var user = _auth.CurrentUser;
            var isLoggedIn = user != null;
            var userName = isLoggedIn ? user.UserName : string.Empty;
            var userDisplayName = isLoggedIn ? user.DisplayName : string.Empty;
            var isAdmin = isLoggedIn && user.IsAdmin;
            const bool isInProduction = false;
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            return new ApplicationContextResult(
                isLoggedIn,
                isAdmin,
                userName,
                userDisplayName,
                isInProduction,
                version);
        }
    }
}