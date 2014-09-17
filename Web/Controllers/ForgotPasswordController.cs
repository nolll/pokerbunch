using System.Web.Mvc;
using Application.Urls;
using Web.Commands.UserCommands;
using Web.Models.UserModels.ForgotPassword;

namespace Web.Controllers
{
    public class ForgotPasswordController : ControllerBase
    {
        private readonly IUserCommandProvider _userCommandProvider;

        public ForgotPasswordController(
            IUserCommandProvider userCommandProvider)
        {
            _userCommandProvider = userCommandProvider;
        }

        [Route("-/user/forgotpassword")]
        public ActionResult ForgotPassword()
        {
            return GetForm();
        }

        [HttpPost]
        [Route("-/user/forgotpassword")]
        public ActionResult Post(ForgotPasswordPostModel postModel)
        {
            var command = _userCommandProvider.GetForgotPasswordCommand(postModel);
            if (command.Execute())
                return Redirect(new ForgotPasswordConfirmationUrl().Relative);
            AddModelErrors(command.Errors);
            return GetForm(postModel);
        }

        private ActionResult GetForm(ForgotPasswordPostModel postModel = null)
        {
            var contextResult = UseCase.AppContext();
            var model = new ForgotPasswordPageModel(contextResult, postModel);
            return View("~/Views/Pages/ForgotPassword/ForgotPassword.cshtml", model);
        }

        [Route("-/user/passwordsent")]
        public ActionResult Done()
        {
            var contextResult = UseCase.AppContext();
            var model = new ForgotPasswordConfirmationPageModel(contextResult);
            return View("~/Views/Pages/ForgotPassword/ForgotPasswordDone.cshtml", model);
        }
    }
}