using System.Web.Mvc;
using Core.Classes;
using Core.Repositories;
using Core.Services;
using Infrastructure.System;
using Web.ModelFactories.SharingModelFactories;

namespace Web.Controllers{

	public class SharingController : Controller {
	    private readonly IAuthentication _authentication;
	    private readonly ISharingRepository _sharingRepository;
	    private readonly ISharingIndexPageModelFactory _sharingIndexPageModelFactory;
	    private readonly IWebContext _webContext;
	    private readonly ITwitterRepository _twitterRepository;
	    private readonly ITwitterIntegration _twitterIntegration;
	    private readonly ISharingTwitterPageModelFactory _sharingTwitterPageModelFactory;
	    private readonly IUrlProvider _urlProvider;

	    public SharingController(
            IAuthentication authentication,
            ISharingRepository sharingRepository,
            ISharingIndexPageModelFactory sharingIndexPageModelFactory,
            IWebContext webContext,
            ITwitterRepository twitterRepository,
            ITwitterIntegration twitterIntegration,
            ISharingTwitterPageModelFactory sharingTwitterPageModelFactory,
            IUrlProvider urlProvider)
	    {
	        _authentication = authentication;
	        _sharingRepository = sharingRepository;
	        _sharingIndexPageModelFactory = sharingIndexPageModelFactory;
	        _webContext = webContext;
	        _twitterRepository = twitterRepository;
	        _twitterIntegration = twitterIntegration;
	        _sharingTwitterPageModelFactory = sharingTwitterPageModelFactory;
	        _urlProvider = urlProvider;
	    }

	    public ActionResult Index(){
			_authentication.RequireUser();
			var user = _authentication.GetUser();
			var isSharing = _sharingRepository.IsSharing(user, SocialServiceIdentifier.Twitter);
			var model = _sharingIndexPageModelFactory.Create(user, isSharing);
			return View("Index", model);
		}

        public ActionResult Twitter(){
			_authentication.RequireUser();
			var user = _authentication.GetUser();
			var isSharing = _sharingRepository.IsSharing(user, SocialServiceIdentifier.Twitter);
			var credentials = _twitterRepository.GetCredentials(user);
			var model = _sharingTwitterPageModelFactory.Create(user, isSharing, credentials);
			return View("Twitter", model);
		}

		public ActionResult TwitterStart(){
			_authentication.RequireUser();
			var url = _twitterIntegration.GetAuthUrl();
			return Redirect(url);
		}

		public ActionResult TwitterStop(){
			_authentication.RequireUser();
			var user = _authentication.GetUser();
			_sharingRepository.RemoveSharing(user, SocialServiceIdentifier.Twitter);
			return Redirect(_urlProvider.GetTwitterSettingsUrl());
		}

        public ActionResult TwitterCallback()
        {
			_authentication.RequireUser();
			var user = _authentication.GetUser();
            var token = _webContext.GetQueryParam("oauth_token");
            var verifier = _webContext.GetQueryParam("oauth_verifier");
            var twitterCredentials = _twitterIntegration.GetCredentials(token, verifier);
            _twitterRepository.AddCredentials(user, twitterCredentials);
            _sharingRepository.AddSharing(user, SocialServiceIdentifier.Twitter);
            return Redirect(_urlProvider.GetTwitterSettingsUrl());
		}

	}

}