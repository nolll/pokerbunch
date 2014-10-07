using System.Web.Mvc;
using Core.Urls;
using Web.Commands.UserCommands;
using Web.Controllers.Base;
using Web.Models.UserModels.ChangePassword;

namespace Web.Controllers
{
    public class ChangePasswordController : PokerBunchController
    {
        private readonly IUserCommandProvider _userCommandProvider;

        public ChangePasswordController(
            IUserCommandProvider userCommandProvider)
        {
            _userCommandProvider = userCommandProvider;
        }

        [Authorize]
        [Route("-/user/changepassword")]
        public ActionResult ChangePassword()
        {
            return GetForm();
        }

        [HttpPost]
        [Authorize]
        [Route("-/user/changepassword")]
        public ActionResult Post(ChangePasswordPostModel postModel)
        {
            var command = _userCommandProvider.GetChangePasswordCommand(postModel);
            if (command.Execute())
            {
                return Redirect(new ChangePasswordConfirmationUrl().Relative);
            }
            AddModelErrors(command.Errors);
            return GetForm();
        }

        private ActionResult GetForm()
        {
            var contextResult = UseCase.AppContext();
            var model = new ChangePasswordPageModel(contextResult);
            return View("~/Views/Pages/ChangePassword/ChangePassword.cshtml", model);
        }

        [Route("-/user/changedpassword")]
        public ActionResult Done()
        {
            var contextResult = UseCase.AppContext();
            var model = new ChangePasswordConfirmationPageModel(contextResult);
            return View("~/Views/Pages/ChangePassword/ChangePasswordDone.cshtml", model);
        }
    }
}