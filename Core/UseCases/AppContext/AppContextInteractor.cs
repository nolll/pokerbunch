using System;
using Core.Services;
using Core.UseCases.BaseContext;

namespace Core.UseCases.AppContext
{
    public class AppContextInteractor
    {
        public static AppContextResult Execute(Func<BaseContextResult> baseContext, IAuth auth)
        {
            var contextResult = baseContext();

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