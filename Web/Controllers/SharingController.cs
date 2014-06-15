using System.Web.Mvc;
using Application.Services;
using Application.Urls;
using Web.Commands.SharingCommands;
using Web.ModelServices;
using Web.Models.UrlModels;

namespace Web.Controllers
{
	public class SharingController : Controller
    {
	    private readonly IWebContext _webContext;
	    private readonly ITwitterIntegration _twitterIntegration;
	    private readonly ISharingModelService _sharingModelService;
	    private readonly ISharingCommandProvider _sharingCommandProvider;

	    public SharingController(
            IWebContext webContext,
            ITwitterIntegration twitterIntegration,
            ISharingModelService sharingModelService,
            ISharingCommandProvider sharingCommandProvider)
	    {
	        _webContext = webContext;
	        _twitterIntegration = twitterIntegration;
	        _sharingModelService = sharingModelService;
	        _sharingCommandProvider = sharingCommandProvider;
	    }

        [Authorize]
        public ActionResult Index()
        {
	        var model = _sharingModelService.GetIndexModel();
			return View("Index", model);
		}

        [Authorize]
        public ActionResult Twitter()
        {
			var model = _sharingModelService.GetTwitterModel();
			return View("Twitter", model);
		}

        [Authorize]
        public ActionResult TwitterStart()
        {
			return Redirect(_twitterIntegration.GetAuthUrl());
		}

        [Authorize]
        public ActionResult TwitterStop()
        {
		    var command = _sharingCommandProvider.GetTwitterStopCommand();
		    command.Execute();
			return Redirect(new TwitterSettingsUrl().Relative);
		}

        [Authorize]
        public ActionResult TwitterCallback()
        {
			var token = _webContext.GetQueryParam("oauth_token");
            var verifier = _webContext.GetQueryParam("oauth_verifier");
            var command = _sharingCommandProvider.GetStartCommand(token, verifier);
            command.Execute();
            return Redirect(new TwitterSettingsUrl().Relative);
		}

	}
}