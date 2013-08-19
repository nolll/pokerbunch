using System.Collections.Generic;
using System.Web.Mvc;
using Core.Classes;
using Core.Services;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.System;
using Web.Models;
using Web.Models.Url;
using Web.Validators;

namespace Web.Controllers{

	public class AuthController : Controller {
	    private readonly IUserStorage _userStorage;
	    private readonly IEncryptionService _encryptionService;
	    private readonly IUserValidatorFactory _userValidatorFactory;
	    private readonly IWebContext _webContext;

	    public AuthController(IUserStorage userStorage, IEncryptionService encryptionService, IUserValidatorFactory userValidatorFactory, IWebContext webContext)
	    {
	        _userStorage = userStorage;
	        _encryptionService = encryptionService;
	        _userValidatorFactory = userValidatorFactory;
	        _webContext = webContext;
	    }

		public ActionResult Login(){
			return ShowForm();
		}

		public ActionResult LoginPost(){
            var loginName = _webContext.GetPostParam("ln");
            var password = _webContext.GetPostParam("pw");

			var user = GetLoggedInUser(loginName, password);

			var validator = _userValidatorFactory.GetLoginValidator(user);
			if(validator.IsValid()){
				var remember = _webContext.GetPostParam("remember") != null;
				var returnUrl = _webContext.GetPostParam("return");
				SetCookies(user, remember);
				return new RedirectResult(GetReturnUrl(returnUrl).Url);
			}
            return ShowForm(loginName, validator.GetErrors());
		}

		private User GetLoggedInUser(string loginName, string password){
			var salt = _userStorage.GetSalt(loginName);
			var encryptedPassword = _encryptionService.Encrypt(password, salt);
			return _userStorage.GetUserByCredentials(loginName, encryptedPassword);
		}

		public ActionResult Logout(){
			ClearCookies();
			return new RedirectResult(new HomeUrlModel().Url);
		}

		public ActionResult ShowForm(string loginName = null, List<string> validationErrors = null){
			var returnUrl = _webContext.GetQueryParam("return");
			var model = new AuthLoginModel(returnUrl, loginName);
			if(validationErrors != null){
				model.SetValidationErrors(validationErrors);
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