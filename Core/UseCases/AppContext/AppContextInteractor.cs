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

            var user = auth.CurrentUser;
            var isLoggedIn = user != null;
            var userName = isLoggedIn ? user.UserName : string.Empty;
            var userDisplayName = isLoggedIn ? user.DisplayName : string.Empty;

            return new AppContextResult(
                contextResult,
                isLoggedIn,
                userName,
                userDisplayName);
        }
    }
}