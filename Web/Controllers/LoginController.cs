using System.Web.Mvc;
using Application.Exceptions;
using Application.UseCases.Login;
using Application.UseCases.LoginForm;
using Web.Models.AuthModels;

namespace Web.Controllers
{
    public class LoginController : ControllerBase
    {
        [Route("-/auth/login")]
        public ActionResult Login(string returnUrl = null)
        {
            return GetForm(returnUrl);
        }

        [HttpPost]
        [Route("-/auth/login")]
        public ActionResult Post(LoginPostModel postModel)
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

            return GetForm(postModel);
        }

        private ActionResult GetForm(string returnUrl, LoginPostModel postModel = null)
        {
            var contextResult = UseCase.AppContext();
            var loginFormResult = UseCase.LoginForm(new LoginFormRequest(returnUrl));
            var model = new LoginPageModel(contextResult, loginFormResult, postModel);
            return View("~/Views/Login/Login.cshtml", model);
        }

        private ActionResult GetForm(LoginPostModel postModel)
        {
            return GetForm(postModel.ReturnUrl, postModel);
        }
    }
}