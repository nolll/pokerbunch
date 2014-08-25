using Application.Services;

namespace Application.UseCases.Logout
{
    public class LogoutInteractor : ILogoutInteractor
    {
        private readonly IAuth _auth;

        public LogoutInteractor(IAuth auth)
        {
            _auth = auth;
        }

        public LogoutResult Execute()
        {
            _auth.SignOut();

            return new LogoutResult();
        }
    }
}