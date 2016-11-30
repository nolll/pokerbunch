using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Extensions;
using Web.Models.UserModels.ForgotPassword;
using Web.Routes;
using Web.Urls.SiteUrls;

namespace Web.Controllers
{
    public class ForgotPasswordController : BaseController
    {
        [Route(WebRoutes.User.ForgotPassword)]
        public ActionResult ForgotPassword()
        {
            return ShowForm();
        }

        [HttpPost]
        [Route(WebRoutes.User.ForgotPassword)]
        public ActionResult Post(ForgotPasswordPostModel postModel)
        {
            try
            {
                var request = new ForgotPassword.Request(postModel.Email, new LoginUrl().GetAbsolute());
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

        [Route(WebRoutes.User.ForgotPasswordConfirmation)]
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