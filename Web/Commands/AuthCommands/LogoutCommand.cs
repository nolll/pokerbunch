using Application.Services;
using Web.Services;

namespace Web.Commands.AuthCommands
{
    public class LogoutCommand : Command
    {
        private readonly IWebContext _webContext;
        private readonly IFormsAuthenticationService _formsAuthenticationService;

        public LogoutCommand(
            IWebContext webContext,
            IFormsAuthenticationService formsAuthenticationService)
        {
            _webContext = webContext;
            _formsAuthenticationService = formsAuthenticationService;
        }

        public override bool Execute()
        {
            _formsAuthenticationService.SignOut();
            _webContext.ClearCookie("token");
            return true;
        }
    }
}