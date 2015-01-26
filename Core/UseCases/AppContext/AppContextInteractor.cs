using Core.Services;
using Core.UseCases.BaseContext;

namespace Core.UseCases.AppContext
{
    public static class AppContextInteractor
    {
        public static AppContextResult Execute(BaseContextResult contextResult, IAuth auth)
        {
            var identity = auth.CurrentIdentity;
            var userName = identity.IsAuthenticated ? identity.UserName : string.Empty;
            var userDisplayName = identity.IsAuthenticated ? identity.DisplayName : string.Empty;

            return new AppContextResult(
                contextResult,
                identity.IsAuthenticated,
                userName,
                userDisplayName);
        }
    }
}