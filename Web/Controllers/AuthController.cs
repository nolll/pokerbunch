using System.Web.Mvc;
using Core.Classes;
using Core.Services;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.System;
using Web.ModelFactories.AuthModelFactories;
using Web.Models.AuthModels;
using Web.Models.UrlModels;

namespace Web.Controllers{

	public class AuthController : Controller {
	    private readonly IUserStorage _userStorage;
	    private readonly IEncryptionService _encryptionService;
	    private readonly IWebContext _webContext;
	    private readonly IAuthLoginPageModelFactory _authLoginPageModelFactory;

	    public AuthController(
            IUserStorage userStorage, 
            IEncryptionService encryptionService, 
            IWebContext webContext, 
            IAuthLoginPageModelFactory authLoginPageModelFactory)
	    {
	        _userStorage = userStorage;
	        _encryptionService = encryptionService;
	        _webContext = webContext;
	        _authLoginPageModelFactory = authLoginPageModelFactory;
	    }

		public ActionResult Login(){
            var model = _authLoginPageModelFactory.Create();
            return View("Login", model);
		}

        [HttpPost]
		public ActionResult Login(AuthLoginPostModel postModel){
            var user = GetLoggedInUser(postModel.LoginName, postModel.Password);
            if (user != null)
            {
            	SetCookies(user, postModel.RememberMe);
                return Redirect(GetReturnUrl(postModel.ReturnUrl).Url);
			}
            ModelState.AddModelError("login_error", "There was something wrong with your username or password. Please try again.");
            var model = _authLoginPageModelFactory.Create(postModel);
            return View("Login", model);
		}

		private User GetLoggedInUser(string loginName, string password){
			var salt = _userStorage.GetSalt(loginName);
			var encryptedPassword = _encryptionService.Encrypt(password, salt);
			return _userStorage.GetUserByCredentials(loginName, encryptedPassword);
		}

		public ActionResult Logout(){
			ClearCookies();
		    return RedirectToAction("Index", "Home");
		}

		private void SetCookies(User user, bool remember){
			if(remember){
				SetPersistentCookies(user);
			} else {
				SetSessionCookies(user);
			}
		}

		private void SetSessionCookies(User user){
			var token = _userStorage.GetToken(user);
			_webContext.SetSessionCookie("token", token);
		}

		private void SetPersistentCookies(User user){
			var token = _userStorage.GetToken(user);
			_webContext.SetPersistentCookie("token", token);
		}

		private UrlModel GetReturnUrl(string returnUrl){
			if(string.IsNullOrEmpty(returnUrl)){
				return new HomeUrlModel();
			}
			return new UrlModel(returnUrl);
		}

		private void ClearCookies(){
			_webContext.ClearCookie("token");
		}

	}
}