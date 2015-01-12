using Core.Services;

namespace Core.UseCases.Home
{
    public static class HomeInteractor
    {
        public static HomeResult Execute(IAuth auth)
        {
            var identity = auth.CurrentIdentity;
            var isAdmin = identity.IsAdmin;

            return new HomeResult(identity.IsAuthenticated, isAdmin);
        }
    }
}