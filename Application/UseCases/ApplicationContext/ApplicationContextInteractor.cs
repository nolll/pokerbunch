using Application.Services;
using Application.UseCases.CashgameContext;

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

            return new ApplicationContextResult
                {
                    UserName = userName,
                    UserDisplayName = userDisplayName,
                    IsLoggedIn = isLoggedIn,
                    IsAdmin = isAdmin,
                    IsInProduction = false,
                    Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()
                };
        }
    }
}