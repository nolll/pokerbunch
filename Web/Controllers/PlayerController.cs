using System.Web.Mvc;
using Core.Services;
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

	    public ActionResult Index(string gameName){
			_authentication.RequireUser();
            _authorization.RequirePlayer(gameName);
            var model = _playerModelService.GetListModel(gameName);
			return View("List", model);
		}

        public ActionResult Details(string gameName, string name){
			_authentication.RequireUser();
            _authorization.RequirePlayer(gameName);
            var model = _playerModelService.GetDetailsModel(gameName, name);
			return View("Details", model);
		}

        public ActionResult Add(string gameName){
			_authentication.RequireUser();
            _authorization.RequireManager(gameName);
            var model = _playerModelService.GetAddModel(gameName);
            return View("Add", model);
		}

        [HttpPost]
        public ActionResult Add(string gameName, AddPlayerPostModel postModel){
			_authentication.RequireUser();
            _authorization.RequireManager(gameName);
            var command = _playerCommandProvider.GetAddCommand(gameName, postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetPlayerAddConfirmationUrl(gameName));
            }
            AddModelErrors(command.Errors);
			var model = _playerModelService.GetAddModel(gameName, postModel);
			return View("Add", model);
		}

        public ActionResult Created(string gameName){
            var model = _playerModelService.GetAddConfirmationModel(gameName);
			return View("AddConfirmation", model);
		}

		public ActionResult Delete(string gameName, string name){
			_authentication.RequireUser();
            _authorization.RequireManager(gameName);
            var command = _playerCommandProvider.GetDeleteCommand(gameName, name);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetPlayerIndexUrl(gameName));
            }
		    return Redirect(_urlProvider.GetPlayerDetailsUrl(gameName, name));
		}

        public ActionResult Invite(string gameName, string name){
			_authentication.RequireUser();
            _authorization.RequireManager(gameName);
            var model = _playerModelService.GetInviteModel(gameName);
            return View("Invite", model);
		}

        [HttpPost]
		public ActionResult Invite(string gameName, string name, InvitePlayerPostModel postModel){
			_authentication.RequireUser();
            _authorization.RequireManager(gameName);
            var command = _playerCommandProvider.GetInviteCommand(gameName, name, postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetPlayerInviteConfirmationUrl(gameName, name));
            }
            AddModelErrors(command.Errors);
            var model = _playerModelService.GetInviteModel(gameName);
            return View("Invite", model);
		}

	    public ActionResult Invited(string gameName, string name){
		    var model = _playerModelService.GetInviteConfirmationModel(gameName);
			return View("InviteConfirmation", model);
		}
    }

}