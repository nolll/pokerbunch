using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Common.Routes;
using Web.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.UserModels.ChangePassword;

namespace Web.Controllers
{
    public class ChangePasswordController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.ChangePassword)]
        public ActionResult ChangePassword()
        {
            return ShowForm();
        }

        [HttpPost]
        [Authorize]
        [Route(WebRoutes.ChangePassword)]
        public ActionResult Post(ChangePasswordPostModel postModel)
        {
            try
            {
                var request = new ChangePassword.Request(CurrentUserName, postModel.Password, postModel.Repeat);
                UseCase.ChangePassword.Execute(request);
                return Redirect(new ChangePasswordConfirmationUrl().Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }

            return ShowForm();
        }

        private ActionResult ShowForm()
        {
            var contextResult = GetAppContext();
            var model = new ChangePasswordPageModel(contextResult);
            return View("~/Views/Pages/ChangePassword/ChangePassword.cshtml", model);
        }

        [Route(WebRoutes.ChangePasswordConfirmation)]
        public ActionResult Done()
        {
            var contextResult = GetAppContext();
            var model = new ChangePasswordConfirmationPageModel(contextResult);
            return View("~/Views/Pages/ChangePassword/ChangePasswordDone.cshtml", model);
        }
    }
}