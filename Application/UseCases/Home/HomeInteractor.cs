using Application.Services;

namespace Application.UseCases.Home
{
    public static class HomeInteractor
    {
        public static HomeResult Execute(IAuth auth)
        {
            var user = auth.CurrentUser;
            var isLoggedIn = user != null;
            var isAdmin = isLoggedIn && user.IsAdmin;

            return new HomeResult(isLoggedIn, isAdmin);
        }
    }
}