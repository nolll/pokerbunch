using System.Collections.Generic;
using System.Web.Mvc;
using Core.Classes;
using Core.Services;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.System;
using Web.ModelFactories.AuthModelFactories;
using Web.Models.AuthModels;
using Web.Models.UrlModels;
using Web.Validators;

namespace Web.Controllers{

	public class AuthController : Controller {
	    private readonly IUserStorage _userStorage;
	    private readonly IEncryptionService _encryptionService;
	    private readonly IUserValidatorFactory _userValidatorFactory;
	    private readonly IWebContext _webContext;
	    private readonly IAuthLoginPageModelFactory _authLoginPageModelFactory;

	    public AuthController(IUserStorage userStorage, IEncryptionService encryptionService, IUserValidatorFactory userValidatorFactory, IWebContext webContext, IAuthLoginPageModelFactory authLoginPageModelFactory)
	    {
	        _userStorage = userStorage;
	        _encryptionService = encryptionService;
	        _userValidatorFactory = userValidatorFactory;
	        _webContext = webContext;
	        _authLoginPageModelFactory = authLoginPageModelFactory;
	    }

		public ActionResult Login(){
			return ShowForm();
		}

        [HttpPost]
		public ActionResult Login(AuthLoginPageModel loginPageModel){
			var user = GetLoggedInUser(loginPageModel.LoginName, loginPageModel.Password);

			var validator = _userValidatorFactory.GetLoginValidator(user);
			if(validator.IsValid){
				SetCookies(user, loginPageModel.RememberMe);
                return Redirect(GetReturnUrl(loginPageModel.ReturnUrl).Url);
			}
            return ShowForm(loginPageModel.LoginName, validator.GetErrors());
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

		public ActionResult ShowForm(string loginName = null, List<string> validationErrors = null){
			var returnUrl = _webContext.GetQueryParam("return");
			var model = _authLoginPageModelFactory.Create(returnUrl, loginName);
			if(validationErrors != null){
				//todo: Fix validation
                //model.SetValidationErrors(validationErrors);
			}
            return View("Login", model);
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