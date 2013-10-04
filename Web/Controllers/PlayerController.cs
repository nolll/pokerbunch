using System.Web.Mvc;
using Core.Classes;
using Core.Repositories;
using Core.Services;
using Infrastructure.Data.Storage.Interfaces;
using Web.ModelFactories.HomegameModelFactories;
using Web.ModelFactories.MiscModelFactories;
using Web.ModelFactories.PlayerModelFactories;
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
	    private readonly IUserRepository _userRepository;
	    private readonly IAvatarModelFactory _avatarModelFactory;
	    private readonly IPlayerListingPageModelFactory _playerListingPageModelFactory;
	    private readonly IPlayerDetailsPageModelFactory _playerDetailsPageModelFactory;
	    private readonly IAddPlayerPageModelFactory _addPlayerPageModelFactory;
	    private readonly IAddPlayerConfirmationPageModelFactory _addPlayerConfirmationPageModelFactory;
	    private readonly IInvitePlayerPageModelFactory _invitePlayerPageModelFactory;
	    private readonly IInvitePlayerConfirmationPageModelFactory _invitePlayerConfirmationPageModelFactory;
	    private readonly IInvitationSender _invitationSender;

	    public PlayerController(
            IUserContext userContext,
			IHomegameRepository homegameRepository,
			IPlayerRepository playerRepository,
			ICashgameRepository cashgameRepository,
            IUserRepository userRepository,
            IAvatarModelFactory avatarModelFactory,
            IPlayerListingPageModelFactory playerListingPageModelFactory,
            IPlayerDetailsPageModelFactory playerDetailsPageModelFactory,
            IAddPlayerPageModelFactory addPlayerPageModelFactory,
            IAddPlayerConfirmationPageModelFactory addPlayerConfirmationPageModelFactory,
            IInvitePlayerPageModelFactory invitePlayerPageModelFactory,
            IInvitePlayerConfirmationPageModelFactory invitePlayerConfirmationPageModelFactory,
            IInvitationSender invitationSender)
	    {
	        _userContext = userContext;
	        _homegameRepository = homegameRepository;
	        _playerRepository = playerRepository;
	        _cashgameRepository = cashgameRepository;
	        _userRepository = userRepository;
	        _avatarModelFactory = avatarModelFactory;
	        _playerListingPageModelFactory = playerListingPageModelFactory;
	        _playerDetailsPageModelFactory = playerDetailsPageModelFactory;
	        _addPlayerPageModelFactory = addPlayerPageModelFactory;
	        _addPlayerConfirmationPageModelFactory = addPlayerConfirmationPageModelFactory;
	        _invitePlayerPageModelFactory = invitePlayerPageModelFactory;
	        _invitePlayerConfirmationPageModelFactory = invitePlayerConfirmationPageModelFactory;
	        _invitationSender = invitationSender;
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
			var player = _playerRepository.GetByName(homegame, name);
			var user = _userRepository.GetUserByName(player.UserName);
			var cashgames = _cashgameRepository.GetPublished(homegame);
			var isManager = _userContext.IsInRole(homegame, Role.Manager);
			var hasPlayed = _cashgameRepository.HasPlayed(player);
			var runningGame = _cashgameRepository.GetRunning(homegame);
			var model = _playerDetailsPageModelFactory.Create(currentUser, homegame, player, user, cashgames, isManager, hasPlayed, _avatarModelFactory, runningGame);
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
                    return Redirect(new PlayerAddConfirmationUrlModel(homegame).Url);
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
				return Redirect(new PlayerDetailsUrlModel(homegame, player).ToString());
			}
			_playerRepository.DeletePlayer(player);
			return Redirect(new PlayerIndexUrlModel(homegame).ToString());
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
				return Redirect(new PlayerInviteConfirmationUrlModel(homegame, player).Url);
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