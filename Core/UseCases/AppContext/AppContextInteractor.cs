using Core.Services;
using Core.UseCases.BaseContext;

namespace Core.UseCases.AppContext
{
    public class AppContextInteractor
    {
        private readonly IAuth _auth;

        public AppContextInteractor(IAuth auth)
        {
            _auth = auth;
        }

        public AppContextResult Execute()
        {
            var identity = _auth.CurrentIdentity;
            var userName = identity.IsAuthenticated ? identity.Name : string.Empty;
            var userDisplayName = identity.IsAuthenticated ? identity.DisplayName : string.Empty;
            var baseContextResult = new BaseContextInteractor().Execute();

            return new AppContextResult(
                baseContextResult,
                identity.IsAuthenticated,
                identity.IsAdmin,
                userName,
                userDisplayName);
        }
    }
}