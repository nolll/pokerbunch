using System.Web.Mvc;
using Core.Exceptions;
using Core.Urls;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.UserModels.ForgotPassword;

namespace Web.Controllers
{
    public class ForgotPasswordController : BaseController
    {
        [Route(Routes.ForgotPassword)]
        public ActionResult ForgotPassword()
        {
            return ShowForm();
        }

        [HttpPost]
        [Route(Routes.ForgotPassword)]
        public ActionResult Post(ForgotPasswordPostModel postModel)
        {
            try
            {
                var request = new ForgotPassword.Request(postModel.Email);
                var result = UseCase.ForgotPassword.Execute(request);
                return Redirect(result.ReturnUrl.Relative);
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

        [Route(Routes.ForgotPasswordConfirmation)]
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