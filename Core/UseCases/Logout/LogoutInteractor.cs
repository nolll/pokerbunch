using Core.Services;

namespace Core.UseCases.Logout
{
    public static class LogoutInteractor
    {
        public static LogoutResult Execute(IAuth auth)
        {
            auth.SignOut();

            return new LogoutResult();
        }
    }
}