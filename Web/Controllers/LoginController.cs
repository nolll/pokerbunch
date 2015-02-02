using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases.Login;
using Core.UseCases.LoginForm;
using Web.Controllers.Base;
using Web.Models.AuthModels;

namespace Web.Controllers
{
    public class LoginController : PokerBunchController
    {
        [Route("-/auth/login")]
        public ActionResult Login(string returnUrl = null)
        {
            return ShowForm(returnUrl);
        }

        [HttpPost]
        [Route("-/auth/login")]
        public ActionResult Post(LoginPostModel postModel)
        {
            var request = new LoginRequest(postModel.LoginName, postModel.Password, postModel.RememberMe, postModel.ReturnUrl);

            try
            {
                var result = UseCase.Login.Execute(request);
                return Redirect(result.ReturnUrl.Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }

            return ShowForm(postModel);
        }

        private ActionResult ShowForm(string returnUrl, LoginPostModel postModel = null)
        {
            var contextResult = UseCase.AppContext.Execute();
            var loginFormResult = UseCase.LoginForm.Execute(new LoginFormRequest(returnUrl));
            var model = new LoginPageModel(contextResult, loginFormResult, postModel);
            return View("~/Views/Login/Login.cshtml", model);
        }

        private ActionResult ShowForm(LoginPostModel postModel)
        {
            return ShowForm(postModel.ReturnUrl, postModel);
        }
    }
}