using System.Web.Mvc;
using Application.Urls;
using Web.Commands.AuthCommands;
using Web.ModelFactories.AuthModelFactories;
using Web.Models.AuthModels;

namespace Web.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly ILoginPageBuilder _loginPageBuilder;
        private readonly IAuthCommandProvider _authCommandProvider;

        public AuthController(
            ILoginPageBuilder loginPageBuilder,
            IAuthCommandProvider authCommandProvider)
        {
            _loginPageBuilder = loginPageBuilder;
            _authCommandProvider = authCommandProvider;
        }

        public ActionResult Login()
        {
            var model = _loginPageBuilder.Build();
            return View("Login", model);
        }

        [HttpPost]
        public ActionResult Login(LoginPostModel postModel)
        {
            var command = _authCommandProvider.GetLoginCommand(postModel);
            if (command.Execute())
            {
                var returnUrl = string.IsNullOrEmpty(postModel.ReturnUrl) ? new HomeUrl().Relative : postModel.ReturnUrl;
                return Redirect(returnUrl);
            }
            AddModelErrors(command.Errors);
            var model = _loginPageBuilder.Build(postModel);
            return View("Login", model);
        }

        public ActionResult Logout()
        {
            var command = _authCommandProvider.GetLogoutCommand();
            command.Execute();
            return Redirect(new HomeUrl().Relative);
        }
    }
}