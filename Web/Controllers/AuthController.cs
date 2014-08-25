using System.Web.Mvc;
using Application.Urls;
using Application.UseCases.AppContext;
using Application.UseCases.LoginForm;
using Web.Commands.AuthCommands;
using Web.Models.AuthModels;

namespace Web.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IAppContextInteractor _appContextInteractor;
        private readonly ILoginFormInteractor _loginFormInteractor;
        private readonly IAuthCommandProvider _authCommandProvider;

        public AuthController(
            IAppContextInteractor appContextInteractor,
            ILoginFormInteractor loginFormInteractor,
            IAuthCommandProvider authCommandProvider)
        {
            _appContextInteractor = appContextInteractor;
            _loginFormInteractor = loginFormInteractor;
            _authCommandProvider = authCommandProvider;
        }

        public ActionResult Login(string returnUrl = null)
        {
            var model = BuildLoginModel(returnUrl);
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
            var model = BuildLoginModel(postModel);
            return View("Login", model);
        }

        private LoginPageModel BuildLoginModel(string returnUrl, LoginPostModel postModel = null)
        {
            var contextResult = _appContextInteractor.Execute();
            var loginFormResult = _loginFormInteractor.Execute(new LoginFormRequest(returnUrl));
            return new LoginPageModel(contextResult, loginFormResult, postModel);
        }

        private LoginPageModel BuildLoginModel(LoginPostModel postModel)
        {
            return BuildLoginModel(postModel.ReturnUrl, postModel);
        }

        public ActionResult Logout()
        {
            var command = _authCommandProvider.GetLogoutCommand();
            command.Execute();
            return Redirect(new HomeUrl().Relative);
        }
    }
}