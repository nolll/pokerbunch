using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Common.Routes;
using Web.Controllers.Base;
using Web.Models.UserModels.ForgotPassword;
using Web.Urls;

namespace Web.Controllers
{
    public class ForgotPasswordController : BaseController
    {
        [Route(WebRoutes.ForgotPassword)]
        public ActionResult ForgotPassword()
        {
            return ShowForm();
        }

        [HttpPost]
        [Route(WebRoutes.ForgotPassword)]
        public ActionResult Post(ForgotPasswordPostModel postModel)
        {
            try
            {
                var request = new ForgotPassword.Request(postModel.Email, new LoginUrl().Absolute);
                UseCase.ForgotPassword.Execute(request);
                return Redirect(new ForgotPasswordConfirmationUrl().Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }
            catch (UserNotFoundException ex)
            {
                AddModelError(ex.Message);
            }

            return ShowForm(postModel);
        }

        [Route(WebRoutes.ForgotPasswordConfirmation)]
        public ActionResult Done()
        {
            var contextResult = GetAppContext();
            var model = new ForgotPasswordConfirmationPageModel(contextResult);
            return View("~/Views/Pages/ForgotPassword/ForgotPasswordDone.cshtml", model);
        }

        private ActionResult ShowForm(ForgotPasswordPostModel postModel = null)
        {
            var contextResult = GetAppContext();
            var model = new ForgotPasswordPageModel(contextResult, postModel);
            return View("~/Views/Pages/ForgotPassword/ForgotPassword.cshtml", model);
        }
    }
}