using Web.Security;

namespace Web.Commands.AuthCommands
{
    public class LogoutCommand : Command
    {
        private readonly IAuthentication _authentication;

        public LogoutCommand(
            IAuthentication authentication)
        {
            _authentication = authentication;
        }

        public override bool Execute()
        {
            _authentication.SignOut();
            return true;
        }
    }
}