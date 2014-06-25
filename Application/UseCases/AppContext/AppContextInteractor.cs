using Application.Services;
using Application.UseCases.BaseContext;

namespace Application.UseCases.AppContext
{
    public class AppContextInteractor : IAppContextInteractor
    {
        private readonly IBaseContextInteractor _baseContextInteractor;
        private readonly IAuth _auth;

        public AppContextInteractor(
            IBaseContextInteractor baseContextInteractor,
            IAuth auth)
        {
            _baseContextInteractor = baseContextInteractor;
            _auth = auth;
        }

        public AppContextResult Execute()
        {
            var contextResult = _baseContextInteractor.Execute();

            var user = _auth.CurrentUser;
            var isLoggedIn = user != null;
            var userName = isLoggedIn ? user.UserName : string.Empty;
            var userDisplayName = isLoggedIn ? user.DisplayName : string.Empty;
            var isAdmin = isLoggedIn && user.IsAdmin;

            return new AppContextResult(
                contextResult,
                isLoggedIn,
                isAdmin,
                userName,
                userDisplayName);
        }
    }
}