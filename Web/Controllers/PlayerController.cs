using System.Web.Mvc;
using Core.Repositories;
using Core.Services;
using Web.Commands.PlayerCommands;
using Web.ModelServices;
using Web.Models.PlayerModels.Add;
using Web.Models.PlayerModels.Invite;

namespace Web.Controllers{
    public class PlayerController : ControllerBase
    {
	    private readonly IUserContext _userContext;
	    private readonly IHomegameRepository _homegameRepository;
	    private readonly IPlayerRepository _playerRepository;
	    private readonly IPlayerModelService _playerModelService;
	    private readonly IUrlProvider _urlProvider;
	    private readonly IPlayerCommandProvider _playerCommandProvider;

	    public PlayerController(
            IUserContext userContext,
			IHomegameRepository homegameRepository,
			IPlayerRepository playerRepository,
            IPlayerModelService playerModelService,
            IUrlProvider urlProvider,
            IPlayerCommandProvider playerCommandProvider)
	    {
	        _userContext = userContext;
	        _homegameRepository = homegameRepository;
	        _playerRepository = playerRepository;
	        _playerModelService = playerModelService;
	        _urlProvider = urlProvider;
	        _playerCommandProvider = playerCommandProvider;
	    }

	    public ActionResult Index(string gameName){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
	        var model = _playerModelService.GetListModel(homegame);
			return View("List", model);
		}

        public ActionResult Details(string gameName, string name){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var model = _playerModelService.GetDetailsModel(homegame, name);
			return View("Details", model);
		}

        public ActionResult Add(string gameName){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequireManager(homegame);
            var model = _playerModelService.GetAddModel(homegame);
            return View("Add", model);
		}

        [HttpPost]
        public ActionResult Add(string gameName, AddPlayerPostModel postModel){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequireManager(homegame);
            var command = _playerCommandProvider.GetAddCommand(homegame, postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetPlayerAddConfirmationUrl(homegame));
            }
            AddModelErrors(command.Errors);
			var model = _playerModelService.GetAddModel(homegame, postModel);
			return View("Add", model);
		}

        public ActionResult Created(string gameName){
			var homegame = _homegameRepository.GetByName(gameName);
            var model = _playerModelService.GetAddConfirmationModel(homegame);
			return View("AddConfirmation", model);
		}

		public ActionResult Delete(string gameName, string name){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequireManager(homegame);
			var player = _playerRepository.GetByName(homegame, name);
		    var command = _playerCommandProvider.GetDeleteCommand(homegame, player);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetPlayerIndexUrl(homegame));
            }
    		return Redirect(_urlProvider.GetPlayerDetailsUrl(homegame, player));
		}

        public ActionResult Invite(string gameName, string name){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequireManager(homegame);
            var model = _playerModelService.GetInviteModel(homegame);
            return View("Invite", model);
		}

        [HttpPost]
		public ActionResult Invite(string gameName, string name, InvitePlayerPostModel postModel){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequireManager(homegame);
			var player = _playerRepository.GetByName(homegame, name);
            var command = _playerCommandProvider.GetInviteCommand(homegame, player, postModel);
            if (command.Execute())
            {
                return Redirect(_urlProvider.GetPlayerInviteConfirmationUrl(homegame, player));
            }
            AddModelErrors(command.Errors);
            var model = _playerModelService.GetInviteModel(homegame);
            return View("Invite", model);
		}

	    public ActionResult Invited(string gameName, string name){
			var homegame = _homegameRepository.GetByName(gameName);
		    var model = _playerModelService.GetInviteConfirmationModel(homegame);
			return View("InviteConfirmation", model);
		}
    }

}