using System.Web.Mvc;
using Application.Services.Interfaces;
using Web.Commands.PlayerCommands;
using Web.ModelServices;
using Web.Models.PlayerModels.Add;
using Web.Models.PlayerModels.Invite;

namespace Web.Controllers{
    public class PlayerController : ControllerBase
    {
	    private readonly IAuthentication _authentication;
        private readonly IAuthorization _authorization;
	    private readonly IPlayerModelService _playerModelService;
	    private readonly IUrlProvider _urlProvider;
	    private readonly IPlayerCommandProvider _playerCommandProvider;

	    public PlayerController(
            IAuthentication authentication,
            IAuthorization authorization,
            IPlayerModelService playerModelService,
            IUrlProvider urlProvider,
            IPlayerCommandProvider playerCommandProvider)
	    {
	        _authentication = authentication;
	        _authorization = authorization;
	        _playerModelService = playerModelService;
	        _urlProvider = urlProvider;
	        _playerCommandProvider = playerCommandProvider;
	    }

	    public ActionResult Index(string slug){
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            var model = _playerModelService.GetListModel(slug);
			return View("List", model);
		}

        public ActionResult Details(string slug, string playerName){
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            var model = _playerModelService.GetDetailsModel(slug, playerName);
			return View("Details", model);
		}

        public ActionResult Add(string slug){
			_authentication.RequireUser();
            _authorization.RequireManager(slug);
            var model = _playerModelService.GetAddModel(slug);
            return View("Add", model);
		}

        [HttpPost]
        public ActionResult Add(string slug, AddPlayerPostModel postModel){
			_authentication.RequireUser();
            _authorization.RequireManager(slug);
            var command = _playerCommandProvider.GetAddCommand(slug, postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetPlayerAddConfirmationUrl(slug));
            }
            AddModelErrors(command.Errors);
			var model = _playerModelService.GetAddModel(slug, postModel);
			return View("Add", model);
		}

        public ActionResult Created(string slug){
            var model = _playerModelService.GetAddConfirmationModel(slug);
			return View("AddConfirmation", model);
		}

		public ActionResult Delete(string slug, string playerName){
			_authentication.RequireUser();
            _authorization.RequireManager(slug);
            var command = _playerCommandProvider.GetDeleteCommand(slug, playerName);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetPlayerIndexUrl(slug));
            }
		    return Redirect(_urlProvider.GetPlayerDetailsUrl(slug, playerName));
		}

        public ActionResult Invite(string slug, string playerName){
			_authentication.RequireUser();
            _authorization.RequireManager(slug);
            var model = _playerModelService.GetInviteModel(slug);
            return View("Invite", model);
		}

        [HttpPost]
		public ActionResult Invite(string slug, string playerName, InvitePlayerPostModel postModel){
			_authentication.RequireUser();
            _authorization.RequireManager(slug);
            var command = _playerCommandProvider.GetInviteCommand(slug, playerName, postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetPlayerInviteConfirmationUrl(slug, playerName));
            }
            AddModelErrors(command.Errors);
            var model = _playerModelService.GetInviteModel(slug, postModel);
            return View("Invite", model);
		}

	    public ActionResult Invited(string slug, string playerName){
		    var model = _playerModelService.GetInviteConfirmationModel(slug);
			return View("InviteConfirmation", model);
		}
    }

}