using System.Web.Mvc;
using Web.Commands.AuthCommands;
using Web.ModelServices;
using Web.Models.AuthModels;
using Web.Models.UrlModels;

namespace Web.Controllers
{

    public class AuthController : ControllerBase
    {
        private readonly IAuthCommandProvider _authCommandProvider;
        private readonly IAuthModelService _authModelService;

        public AuthController(
            IAuthCommandProvider authCommandProvider,
            IAuthModelService authModelService)
        {
            _authCommandProvider = authCommandProvider;
            _authModelService = authModelService;
        }

        public ActionResult Login()
        {
            var model = _authModelService.GetLoginModel();
            return View("Login", model);
        }

        [HttpPost]
        public ActionResult Login(AuthLoginPostModel postModel)
        {
            var command = _authCommandProvider.GetLoginCommand(postModel);
            if (command.Execute())
            {
                var returnUrl = string.IsNullOrEmpty(postModel.ReturnUrl) ? new HomeUrl().Relative : postModel.ReturnUrl;
                return Redirect(returnUrl);
            }
            AddModelErrors(command.Errors);
            var model = _authModelService.GetLoginModel(postModel);
            return View("Login", model);
        }

        public ActionResult Logout()
        {
            var command = _authCommandProvider.GetLogoutCommand();
            command.Execute();
            return Redirect(new HomeUrl().Relative);
        }

    }
}