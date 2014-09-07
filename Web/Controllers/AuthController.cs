using System.Web.Mvc;
using Application.Exceptions;
using Application.UseCases.Login;
using Application.UseCases.LoginForm;
using Web.Models.AuthModels;

namespace Web.Controllers
{
    public class AuthController : ControllerBase
    {
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

            try
            {
                var result = UseCase.Login(request);
                return Redirect(result.ReturnUrl.Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }

            var model = BuildLoginModel(postModel);
            return View("Login", model);
        }

        [Route("-/auth/logout")]
        public ActionResult Logout()
        {
            var result = UseCase.Logout();
            return Redirect(result.ReturnUrl.Relative);
        }

        private LoginPageModel BuildLoginModel(string returnUrl, LoginPostModel postModel = null)
        {
            var contextResult = UseCase.AppContext();
            var loginFormResult = UseCase.LoginForm(new LoginFormRequest(returnUrl));
            return new LoginPageModel(contextResult, loginFormResult, postModel);
        }

        private LoginPageModel BuildLoginModel(LoginPostModel postModel)
        {
            return BuildLoginModel(postModel.ReturnUrl, postModel);
        }
    }
}