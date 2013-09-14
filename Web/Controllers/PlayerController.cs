using System.Web.Mvc;
using Core.Classes;
using Core.Repositories;
using Infrastructure.Data.Storage.Interfaces;
using Web.ModelFactories.MiscModelFactories;
using Web.ModelFactories.PlayerModelFactories;
using Web.Models.UrlModels;

namespace Web.Controllers{

	public class PlayerController : Controller
    {
	    private readonly IUserContext _userContext;
	    private readonly IHomegameRepository _homegameRepository;
	    private readonly IPlayerRepository _playerRepository;
	    private readonly ICashgameRepository _cashgameRepository;
	    private readonly IUserStorage _userStorage;
	    private readonly IAvatarModelFactory _avatarModelFactory;
	    private readonly IPlayerListingPageModelFactory _playerListingPageModelFactory;
	    private readonly IPlayerDetailsPageModelFactory _playerDetailsPageModelFactory;

	    public PlayerController(
            IUserContext userContext,
			IHomegameRepository homegameRepository,
			IPlayerRepository playerRepository,
			ICashgameRepository cashgameRepository,
            IUserStorage userStorage,
            IAvatarModelFactory avatarModelFactory,
            IPlayerListingPageModelFactory playerListingPageModelFactory,
            IPlayerDetailsPageModelFactory playerDetailsPageModelFactory)
	    {
	        _userContext = userContext;
	        _homegameRepository = homegameRepository;
	        _playerRepository = playerRepository;
	        _cashgameRepository = cashgameRepository;
	        _userStorage = userStorage;
	        _avatarModelFactory = avatarModelFactory;
	        _playerListingPageModelFactory = playerListingPageModelFactory;
	        _playerDetailsPageModelFactory = playerDetailsPageModelFactory;
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
			var user = _userStorage.GetUserByName(player.UserName);
			var cashgames = _cashgameRepository.GetPublished(homegame);
			var isManager = _userContext.IsInRole(homegame, Role.Manager);
			var hasPlayed = _cashgameRepository.HasPlayed(player);
			var runningGame = _cashgameRepository.GetRunning(homegame);
			var model = _playerDetailsPageModelFactory.Create(currentUser, homegame, player, user, cashgames, isManager, hasPlayed, _avatarModelFactory, runningGame);
			return View("Details", model);
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

    }

}