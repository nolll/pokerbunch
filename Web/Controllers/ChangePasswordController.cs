using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases.ChangePassword;
using Web.Controllers.Base;
using Web.Models.UserModels.ChangePassword;

namespace Web.Controllers
{
    public class ChangePasswordController : PokerBunchController
    {
        [Authorize]
        [Route("-/user/changepassword")]
        public ActionResult ChangePassword()
        {
            return ShowForm();
        }

        [HttpPost]
        [Authorize]
        [Route("-/user/changepassword")]
        public ActionResult Post(ChangePasswordPostModel postModel)
        {
            try
            {
                var request = new ChangePasswordRequest(Identity.UserId, postModel.Password, postModel.Repeat);
                var result = UseCase.ChangePassword.Execute(request);
                return Redirect(result.ReturnUrl.Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }

            return ShowForm();
        }

        private ActionResult ShowForm()
        {
            var contextResult = UseCase.AppContext.Execute();
            var model = new ChangePasswordPageModel(contextResult);
            return View("~/Views/Pages/ChangePassword/ChangePassword.cshtml", model);
        }

        [Route("-/user/changedpassword")]
        public ActionResult Done()
        {
            var contextResult = UseCase.AppContext.Execute();
            var model = new ChangePasswordConfirmationPageModel(contextResult);
            return View("~/Views/Pages/ChangePassword/ChangePasswordDone.cshtml", model);
        }
    }
}