using System.Web.Mvc;
using Application.Exceptions;
using Application.UseCases.ForgotPassword;
using Web.Models.UserModels.ForgotPassword;

namespace Web.Controllers
{
    public class ForgotPasswordController : ControllerBase
    {
        [Route("-/user/forgotpassword")]
        public ActionResult ForgotPassword()
        {
            return GetForm();
        }

        [HttpPost]
        [Route("-/user/forgotpassword")]
        public ActionResult Post(ForgotPasswordPostModel postModel)
        {
            try
            {
                var request = new ForgotPasswordRequest(postModel.Email);
                var result = UseCase.ForgotPassword(request);
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

            return GetForm(postModel);
        }

        [Route("-/user/passwordsent")]
        public ActionResult Done()
        {
            var contextResult = UseCase.AppContext();
            var model = new ForgotPasswordConfirmationPageModel(contextResult);
            return View("~/Views/Pages/ForgotPassword/ForgotPasswordDone.cshtml", model);
        }

        private ActionResult GetForm(ForgotPasswordPostModel postModel = null)
        {
            var contextResult = UseCase.AppContext();
            var model = new ForgotPasswordPageModel(contextResult, postModel);
            return View("~/Views/Pages/ForgotPassword/ForgotPassword.cshtml", model);
        }
    }
}