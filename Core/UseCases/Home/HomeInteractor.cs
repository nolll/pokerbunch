using Core.Services;

namespace Core.UseCases.Home
{
    public class HomeInteractor
    {
        private readonly IAuth _auth;

        public HomeInteractor(IAuth auth)
        {
            _auth = auth;
        }

        public HomeResult Execute()
        {
            var identity = _auth.CurrentIdentity;
            var isAdmin = identity.IsAdmin;

            return new HomeResult(identity.IsAuthenticated, isAdmin);
        }
    }
}