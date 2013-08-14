using System.Web.Mvc;
using Core.Classes;
using Core.Repositories;
using Infrastructure.Data.Storage.Interfaces;
using Web.Models;

namespace Web.Controllers{

	public class HomeController : Controller {
	    private readonly IUserContext _userContext;
	    private readonly IHomegameStorage _homegameStorage;
	    private readonly ICashgameRepository _cashgameRepository;

	    public HomeController(IUserContext userContext, IHomegameStorage homegameStorage, ICashgameRepository cashgameRepository)
	    {
	        _userContext = userContext;
	        _homegameStorage = homegameStorage;
	        _cashgameRepository = cashgameRepository;
	    }

		public ActionResult Index(){
			var homegame = GetHomegame();
			var runningGame = GetRunningGame(homegame);
			var model = new HomeModel(_userContext.GetUser(), homegame, runningGame);
			return View(model);
		}

		private Homegame GetHomegame(){
			var games = _homegameStorage.GetHomegamesByRole(_userContext.GetToken(), (int)Role.Player);
			if(games.Count == 1){
				return games[0];
			}
			return null;
		}

		private Cashgame GetRunningGame(Homegame homegame){
			if(homegame == null){
				return null;
			}
			return _cashgameRepository.GetRunning(homegame);
		}

	}

}