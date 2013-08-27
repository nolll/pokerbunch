using System.Web.Mvc;
using Core.Classes;
using Infrastructure.Repositories;
using Web.Models.PlayerModels.Listing;

namespace Web.Controllers{

	public class PlayerController : Controller
    {
	    private readonly UserContext _userContext;
	    private readonly HomegameRepository _homegameRepository;
	    private readonly PlayerRepository _playerRepository;
	    private readonly CashgameRepository _cashgameRepository;

	    public PlayerController(
            UserContext userContext,
			HomegameRepository homegameRepository,
			PlayerRepository playerRepository,
			CashgameRepository cashgameRepository)
	    {
	        _userContext = userContext;
	        _homegameRepository = homegameRepository;
	        _playerRepository = playerRepository;
	        _cashgameRepository = cashgameRepository;
	    }

	    public ActionResult Index(string gameName){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var isInManagerMode = _userContext.IsInRole(homegame, Role.Manager);
			var players = _playerRepository.GetAll(homegame);
			var runningGame = _cashgameRepository.GetRunning(homegame);
			var model = new PlayerListingPageModel(_userContext.GetUser(), homegame, players, isInManagerMode, runningGame);
			return View("Listing", model);
		}

    }

}