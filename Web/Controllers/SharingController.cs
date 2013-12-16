using System.Web.Mvc;
using Core.Services;
using Infrastructure.System;
using Web.Commands.SharingCommands;
using Web.ModelServices;

namespace Web.Controllers
{
	public class SharingController : Controller
    {
	    private readonly IAuthentication _authentication;
	    private readonly IWebContext _webContext;
	    private readonly ITwitterIntegration _twitterIntegration;
	    private readonly IUrlProvider _urlProvider;
	    private readonly ISharingModelService _sharingModelService;
	    private readonly ISharingCommandProvider _sharingCommandProvider;

	    public SharingController(
            IAuthentication authentication,
            IWebContext webContext,
            ITwitterIntegration twitterIntegration,
            IUrlProvider urlProvider,
            ISharingModelService sharingModelService,
            ISharingCommandProvider sharingCommandProvider)
	    {
	        _authentication = authentication;
	        _webContext = webContext;
	        _twitterIntegration = twitterIntegration;
	        _urlProvider = urlProvider;
	        _sharingModelService = sharingModelService;
	        _sharingCommandProvider = sharingCommandProvider;
	    }

	    public ActionResult Index()
        {
			_authentication.RequireUser();
	        var model = _sharingModelService.GetIndexModel();
			return View("Index", model);
		}

        public ActionResult Twitter()
        {
			_authentication.RequireUser();
			var model = _sharingModelService.GetTwitterModel();
			return View("Twitter", model);
		}

		public ActionResult TwitterStart()
        {
			_authentication.RequireUser();
			return Redirect(_twitterIntegration.GetAuthUrl());
		}

		public ActionResult TwitterStop()
        {
			_authentication.RequireUser();
		    var command = _sharingCommandProvider.GetTwitterStopCommand();
		    command.Execute();
			return Redirect(_urlProvider.GetTwitterSettingsUrl());
		}

        public ActionResult TwitterCallback()
        {
			_authentication.RequireUser();
			var token = _webContext.GetQueryParam("oauth_token");
            var verifier = _webContext.GetQueryParam("oauth_verifier");
            var command = _sharingCommandProvider.GetStartCommand(token, verifier);
            command.Execute();
            return Redirect(_urlProvider.GetTwitterSettingsUrl());
		}

	}
}