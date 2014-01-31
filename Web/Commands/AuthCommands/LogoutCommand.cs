using Application.Services;

namespace Web.Commands.AuthCommands
{
    public class LogoutCommand : Command
    {
        private readonly IWebContext _webContext;

        public LogoutCommand(
            IWebContext webContext)
        {
            _webContext = webContext;
        }

        public override bool Execute()
        {
            _webContext.ClearCookie("token");
            return true;
        }
    }
}