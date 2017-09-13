using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.UserModels.ChangePassword;
using Web.Routes;
using Web.Urls.SiteUrls;

namespace Web.Controllers
{
    public class ChangePasswordController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.User.ChangePassword)]
        public ActionResult ChangePassword()
        {
            return ShowForm();
        }

        [HttpPost]
        [Authorize]
        [Route(WebRoutes.User.ChangePassword)]
        public ActionResult Post(ChangePasswordPostModel postModel)
        {
            try
            {
                var request = new ChangePassword.Request(Identity.UserName, postModel.Password, postModel.Repeat);
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
            return View(model);
        }

        [Route(WebRoutes.User.ChangePasswordConfirmation)]
        public ActionResult Done()
        {
            var contextResult = GetAppContext();
            var model = new ChangePasswordConfirmationPageModel(contextResult);
            return View(model);
        }
    }
}