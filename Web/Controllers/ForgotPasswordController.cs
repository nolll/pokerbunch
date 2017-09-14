using System.Collections.Generic;
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
            var errors = new List<string>();

            try
            {
                var request = new ForgotPassword.Request(postModel.Email);
                UseCase.ForgotPassword.Execute(request);
                return Redirect(new ForgotPasswordConfirmationUrl().Relative);
            }
            catch (ValidationException ex)
            {
                errors.AddRange(ex.Messages);
            }
            catch (UserNotFoundException ex)
            {
                errors.Add(ex.Message);
            }

            return ShowForm(postModel, errors);
        }

        [Route(WebRoutes.User.ForgotPasswordConfirmation)]
        public ActionResult Done()
        {
            var contextResult = GetAppContext();
            var model = new ForgotPasswordConfirmationPageModel(contextResult);
            return View(model);
        }

        private ActionResult ShowForm(ForgotPasswordPostModel postModel = null, IEnumerable<string> errors = null)
        {
            var contextResult = GetAppContext();
            var model = new ForgotPasswordPageModel(contextResult, postModel, errors);
            return View(model);
        }
    }
}