using System.Web.Mvc;
using Application.UseCases.AppContext;
using Application.UseCases.Login;
using Application.UseCases.LoginForm;
using Application.UseCases.Logout;
using Web.Models.AuthModels;

namespace Web.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IAppContextInteractor _appContextInteractor;
        private readonly ILoginFormInteractor _loginFormInteractor;
        private readonly ILoginInteractor _loginInteractor;
        private readonly ILogoutInteractor _logoutInteractor;

        public AuthController(
            IAppContextInteractor appContextInteractor,
            ILoginFormInteractor loginFormInteractor,
            ILoginInteractor loginInteractor,
            ILogoutInteractor logoutInteractor)
        {
            _appContextInteractor = appContextInteractor;
            _loginFormInteractor = loginFormInteractor;
            _loginInteractor = loginInteractor;
            _logoutInteractor = logoutInteractor;
        }

        [Route("-/auth/login")]
        public ActionResult Login(string returnUrl = null)
        {
            var model = BuildLoginModel(returnUrl);
            return View("Login", model);
        }

        [HttpPost]
        [Route("-/auth/login")]
        public ActionResult Login_Post(LoginPostModel postModel)
        {
            var request = new LoginRequest(postModel.LoginName, postModel.Password, postModel.RememberMe, postModel.ReturnUrl);
            var result = _loginInteractor.Execute(request);

            if(result.Success)
                return Redirect(result.ReturnUrl.Relative);
            
            AddModelErrors(result.Errors);
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

        [Route("-/auth/logout")]
        public ActionResult Logout()
        {
            var result = _logoutInteractor.Execute();
            return Redirect(result.ReturnUrl.Relative);
        }
    }
}