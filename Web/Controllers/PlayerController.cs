using System.Web.Mvc;
using Core.Classes;
using Core.Repositories;
using Core.Services;
using Web.ModelFactories.HomegameModelFactories;
using Web.ModelFactories.PlayerModelFactories;
using Web.ModelServices;
using Web.Models.PlayerModels.Add;
using Web.Models.PlayerModels.Invite;
using Web.Models.UrlModels;

namespace Web.Controllers{

	public class PlayerController : Controller
    {
	    private readonly IUserContext _userContext;
	    private readonly IHomegameRepository _homegameRepository;
	    private readonly IPlayerRepository _playerRepository;
	    private readonly ICashgameRepository _cashgameRepository;
	    private readonly IPlayerListingPageModelFactory _playerListingPageModelFactory;
	    private readonly IAddPlayerPageModelFactory _addPlayerPageModelFactory;
	    private readonly IAddPlayerConfirmationPageModelFactory _addPlayerConfirmationPageModelFactory;
	    private readonly IInvitePlayerPageModelFactory _invitePlayerPageModelFactory;
	    private readonly IInvitePlayerConfirmationPageModelFactory _invitePlayerConfirmationPageModelFactory;
	    private readonly IInvitationSender _invitationSender;
	    private readonly IPlayerModelService _playerModelService;
	    private readonly IUrlProvider _urlProvider;

	    public PlayerController(
            IUserContext userContext,
			IHomegameRepository homegameRepository,
			IPlayerRepository playerRepository,
			ICashgameRepository cashgameRepository,
            IPlayerListingPageModelFactory playerListingPageModelFactory,
            IAddPlayerPageModelFactory addPlayerPageModelFactory,
            IAddPlayerConfirmationPageModelFactory addPlayerConfirmationPageModelFactory,
            IInvitePlayerPageModelFactory invitePlayerPageModelFactory,
            IInvitePlayerConfirmationPageModelFactory invitePlayerConfirmationPageModelFactory,
            IInvitationSender invitationSender,
            IPlayerModelService playerModelService,
            IUrlProvider urlProvider)
	    {
	        _userContext = userContext;
	        _homegameRepository = homegameRepository;
	        _playerRepository = playerRepository;
	        _cashgameRepository = cashgameRepository;
	        _playerListingPageModelFactory = playerListingPageModelFactory;
	        _addPlayerPageModelFactory = addPlayerPageModelFactory;
	        _addPlayerConfirmationPageModelFactory = addPlayerConfirmationPageModelFactory;
	        _invitePlayerPageModelFactory = invitePlayerPageModelFactory;
	        _invitePlayerConfirmationPageModelFactory = invitePlayerConfirmationPageModelFactory;
	        _invitationSender = invitationSender;
	        _playerModelService = playerModelService;
	        _urlProvider = urlProvider;
	    }

	    public ActionResult Index(string gameName){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var isInManagerMode = _userContext.IsInRole(homegame, Role.Manager);
			var players = _playerRepository.GetAll(homegame);
			var runningGame = _cashgameRepository.GetRunning(homegame);
			var model = _playerListingPageModelFactory.Create(_userContext.GetUser(), homegame, players, isInManagerMode, runningGame);
			return View("Listing", model);
		}

        public ActionResult Details(string gameName, string name){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var currentUser = _userContext.GetUser();
			var model = _playerModelService.GetDetailsModel(currentUser, homegame, name);
			return View("Details", model);
		}

        public ActionResult Add(string gameName){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequireManager(homegame);
            var runningGame = _cashgameRepository.GetRunning(homegame);
            var model = _addPlayerPageModelFactory.Create(_userContext.GetUser(), homegame, runningGame);
            return View("Add", model);
		}

        [HttpPost]
        public ActionResult Add(string gameName, AddPlayerPostModel postModel){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequireManager(homegame);
			if(ModelState.IsValid)
			{
			    var existingPlayer = _playerRepository.GetByName(homegame, postModel.Name);
                if (existingPlayer == null)
                {
                    _playerRepository.AddPlayer(homegame, postModel.Name);
                    return Redirect(_urlProvider.GetPlayerAddConfirmationUrl(homegame));
                }
                ModelState.AddModelError("player_exists", "The Display Name is in use by someone else");
			}
            var runningGame = _cashgameRepository.GetRunning(homegame);
            var model = _addPlayerPageModelFactory.Create(_userContext.GetUser(), homegame, runningGame, postModel);
			return View("Add", model);
		}

        public ActionResult Created(string gameName){
			var homegame = _homegameRepository.GetByName(gameName);
			var runningGame = _cashgameRepository.GetRunning(homegame);
			var model = _addPlayerConfirmationPageModelFactory.Create(_userContext.GetUser(), homegame, runningGame);
			return View("AddConfirmation", model);
		}

		public ActionResult Delete(string gameName, string name){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequireManager(homegame);
			var player = _playerRepository.GetByName(homegame, name);
			var hasPlayed = _cashgameRepository.HasPlayed(player);
			if(hasPlayed){
				return Redirect(_urlProvider.GetPlayerDetailsUrl(homegame, player));
			}
			_playerRepository.DeletePlayer(player);
			return Redirect(_urlProvider.GetPlayerIndexUrl(homegame));
		}

        public ActionResult Invite(string gameName, string name){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequireManager(homegame);
			return ShowInviteForm(homegame);
		}

        [HttpPost]
		public ActionResult Invite(string gameName, string name, InvitePlayerPostModel postModel){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequireManager(homegame);
			var player = _playerRepository.GetByName(homegame, name);
			if(ModelState.IsValid){
				_invitationSender.Send(homegame, player, postModel.Email);
				return Redirect(_urlProvider.GetPlayerInviteConfirmationUrl(homegame, player));
			} else {
				return ShowInviteForm(homegame);
			}
		}

		public ActionResult Invited(string gameName, string name){
			var homegame = _homegameRepository.GetByName(gameName);
			var runningGame = _cashgameRepository.GetRunning(homegame);
            var model = _invitePlayerConfirmationPageModelFactory.Create(_userContext.GetUser(), homegame, runningGame);
			return View("InviteConfirmation", model);
		}

		private ActionResult ShowInviteForm(Homegame homegame){
			var runningGame = _cashgameRepository.GetRunning(homegame);
            var model = _invitePlayerPageModelFactory.Create(_userContext.GetUser(), homegame, runningGame);
			return View("Invite", model);
		}

    }
}