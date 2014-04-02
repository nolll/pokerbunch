using Application.Services;
using Web.Security;
using Web.Services;

namespace Web.Commands.AuthCommands
{
    public class LogoutCommand : Command
    {
        private readonly IWebContext _webContext;
        private readonly IAuthenticationService _authenticationService;

        public LogoutCommand(
            IWebContext webContext,
            IAuthenticationService authenticationService)
        {
            _webContext = webContext;
            _authenticationService = authenticationService;
        }

        public override bool Execute()
        {
            _authenticationService.SignOut();
            _webContext.ClearCookie("token");
            return true;
        }
    }
}