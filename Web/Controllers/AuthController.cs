using System.Web.Mvc;
using Core.Services;
using Web.Commands.AuthCommands;
using Web.ModelFactories.AuthModelFactories;
using Web.Models.AuthModels;

namespace Web.Controllers{

	public class AuthController : ControllerBase {
	    private readonly IAuthLoginPageModelFactory _authLoginPageModelFactory;
	    private readonly IUrlProvider _urlProvider;
	    private readonly IAuthCommandProvider _authCommandProvider;

	    public AuthController(
            IAuthLoginPageModelFactory authLoginPageModelFactory,
            IUrlProvider urlProvider,
            IAuthCommandProvider authCommandProvider)
	    {
	        _authLoginPageModelFactory = authLoginPageModelFactory;
	        _urlProvider = urlProvider;
	        _authCommandProvider = authCommandProvider;
	    }

		public ActionResult Login(){
            var model = _authLoginPageModelFactory.Create();
            return View("Login", model);
		}

        [HttpPost]
		public ActionResult Login(AuthLoginPostModel postModel)
        {
            var command = _authCommandProvider.GetLoginCommand(postModel.LoginName, postModel.Password, postModel.RememberMe);
            if (command.Execute())
            {
                var returnUrl = string.IsNullOrEmpty(postModel.ReturnUrl) ? _urlProvider.GetHomeUrl() : postModel.ReturnUrl;
                return Redirect(returnUrl);
            }
            AddModelErrors(command.Errors);
            var model = _authLoginPageModelFactory.Create(postModel);
            return View("Login", model);
		}

        public ActionResult Logout()
        {
            var command = _authCommandProvider.GetLogoutCommand();
            command.Execute();
            return RedirectToAction("Index", "Home");
        }

    }
}