using System.Web.Mvc;
using Core.Classes;
using Core.Repositories;
using Core.Services;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.System;
using Web.ModelFactories.SharingModelFactories;
using Web.Models.UrlModels;

namespace Web.Controllers{

	public class SharingController : Controller {
	    private readonly IUserContext _userContext;
	    private readonly ISharingStorage _sharingStorage;
	    private readonly ISharingIndexPageModelFactory _sharingIndexPageModelFactory;
	    private readonly IWebContext _webContext;
	    private readonly ITwitterStorage _twitterStorage;
	    private readonly ITwitterIntegration _twitterIntegration;
	    private readonly ISharingTwitterPageModelFactory _sharingTwitterPageModelFactory;

	    public SharingController(
            IUserContext userContext,
			ISharingStorage sharingStorage,
            ISharingIndexPageModelFactory sharingIndexPageModelFactory,
            IWebContext webContext,
            ITwitterStorage twitterStorage,
            ITwitterIntegration twitterIntegration,
            ISharingTwitterPageModelFactory sharingTwitterPageModelFactory)
	    {
	        _userContext = userContext;
	        _sharingStorage = sharingStorage;
	        _sharingIndexPageModelFactory = sharingIndexPageModelFactory;
	        _webContext = webContext;
	        _twitterStorage = twitterStorage;
	        _twitterIntegration = twitterIntegration;
	        _sharingTwitterPageModelFactory = sharingTwitterPageModelFactory;
	    }

	    public ActionResult Index(){
			_userContext.RequireUser();
			var user = _userContext.GetUser();
			var isSharing = _sharingStorage.IsSharing(user, SocialServiceIdentifier.Twitter);
			var model = _sharingIndexPageModelFactory.Create(user, isSharing);
			return View("Index", model);
		}

        public ActionResult Twitter(){
			_userContext.RequireUser();
			var user = _userContext.GetUser();
			var isSharing = _sharingStorage.IsSharing(user, SocialServiceIdentifier.Twitter);
			var credentials = _twitterStorage.GetCredentials(user);
			var model = _sharingTwitterPageModelFactory.Create(user, isSharing, credentials);
			return View("Twitter", model);
		}

		public ActionResult TwitterStart(){
			_userContext.RequireUser();
			var url = _twitterIntegration.GetAuthUrl();
			return Redirect(url);
		}

		public ActionResult TwitterStop(){
			_userContext.RequireUser();
			var user = _userContext.GetUser();
			_sharingStorage.RemoveSharing(user, SocialServiceIdentifier.Twitter);
			return Redirect(new TwitterSettingsUrlModel().Url);
		}

        public ActionResult TwitterCallback()
        {
			_userContext.RequireUser();
			var user = _userContext.GetUser();
            var token = _webContext.GetQueryParam("oauth_token");
            var verifier = _webContext.GetQueryParam("oauth_verifier");
            var twitterCredentials = _twitterIntegration.GetCredentials(token, verifier);
            _twitterStorage.AddCredentials(user, twitterCredentials);
            _sharingStorage.AddSharing(user, SocialServiceIdentifier.Twitter);
			return Redirect(new TwitterSettingsUrlModel().Url);
		}

	}

}