using Application.Services;

namespace Application.UseCases.Home
{
    public class HomeInteractor : IHomeInteractor
    {
        private readonly IAuth _auth;

        public HomeInteractor(IAuth auth)
        {
            _auth = auth;
        }

        public HomeResult Execute()
        {
            var user = _auth.CurrentUser;
            var isLoggedIn = user != null;
            var isAdmin = isLoggedIn && user.IsAdmin;

            return new HomeResult(isLoggedIn, isAdmin);
        }
    }
}