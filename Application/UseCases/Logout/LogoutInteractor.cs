using Application.Services;

namespace Application.UseCases.Logout
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