using System.Web.Mvc;
using Application.Services;
using Application.Urls;
using Web.Commands.SharingCommands;
using Web.ModelFactories.SharingModelFactories;

namespace Web.Controllers
{
	public class SharingController : Controller
    {
	    private readonly IWebContext _webContext;
	    private readonly ITwitterIntegration _twitterIntegration;
	    private readonly ISharingCommandProvider _sharingCommandProvider;
	    private readonly ISharingIndexPageBuilder _sharingIndexPageBuilder;
	    private readonly ISharingTwitterPageBuilder _sharingTwitterPageBuilder;

	    public SharingController(
            IWebContext webContext,
            ITwitterIntegration twitterIntegration,
            ISharingCommandProvider sharingCommandProvider,
            ISharingIndexPageBuilder sharingIndexPageBuilder,
            ISharingTwitterPageBuilder sharingTwitterPageBuilder)
	    {
	        _webContext = webContext;
	        _twitterIntegration = twitterIntegration;
	        _sharingCommandProvider = sharingCommandProvider;
	        _sharingIndexPageBuilder = sharingIndexPageBuilder;
	        _sharingTwitterPageBuilder = sharingTwitterPageBuilder;
	    }

        [Authorize]
        public ActionResult Index()
        {
            var model = _sharingIndexPageBuilder.Build();
			return View("Index", model);
		}

        [Authorize]
        public ActionResult Twitter()
        {
            var model = _sharingTwitterPageBuilder.Build();
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