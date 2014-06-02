using Application.Services;
using Web.Security;

namespace Web.Commands.AuthCommands
{
    public class LogoutCommand : Command
    {
        private readonly IAuth _auth;

        public LogoutCommand(
            IAuth auth)
        {
            _auth = auth;
        }

        public override bool Execute()
        {
            _auth.SignOut();
            return true;
        }
    }
}