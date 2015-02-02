using Core.Services;

namespace Core.UseCases.Logout
{
    public class LogoutInteractor
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