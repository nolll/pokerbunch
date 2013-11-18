using System.Web.Mvc;
using Core.Services;
using Infrastructure.System;
using Web.Commands.AuthCommands;
using Web.ModelFactories.AuthModelFactories;
using Web.Models.AuthModels;

namespace Web.Controllers{

	public class AuthController : ControllerBase {
	    private readonly IWebContext _webContext;
	    private readonly IAuthLoginPageModelFactory _authLoginPageModelFactory;
	    private readonly IUrlProvider _urlProvider;
	    private readonly IAuthCommandProvider _authCommandProvider;

	    public AuthController(
            IWebContext webContext, 
            IAuthLoginPageModelFactory authLoginPageModelFactory,
            IUrlProvider urlProvider,
            IAuthCommandProvider authCommandProvider)
	    {
	        _webContext = webContext;
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
                return Redirect(GetReturnUrl(postModel.ReturnUrl));
            }
            AddModelErrors(command.Errors);
            var model = _authLoginPageModelFactory.Create(postModel);
            return View("Login", model);
		}

        public ActionResult Logout()
        {
            ClearCookies();
            return RedirectToAction("Index", "Home");
        }

        private string GetReturnUrl(string returnUrl){
			if(string.IsNullOrEmpty(returnUrl)){
				return _urlProvider.GetHomeUrl();
			}
			return returnUrl;
		}

		private void ClearCookies(){
			_webContext.ClearCookie("token");
		}

	}
}