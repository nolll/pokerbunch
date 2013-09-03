using System.Web.Mvc;
using Core.Classes;
using Core.Repositories;
using Infrastructure.System;
using Web.ModelFactories.CashgameModelFactories.Matrix;
using Web.Models.CashgameModels.Details;
using Web.Models.CashgameModels.Leaderboard;
using Web.Models.UrlModels;
using Web.Views.Cashgame.Chart;

namespace Web.Controllers{

	public class CashgameController : Controller {
	    private readonly IHomegameRepository _homegameRepository;
	    private readonly IUserContext _userContext;
	    private readonly ICashgameRepository _cashgameRepository;
	    private readonly IPlayerRepository _playerRepository;
	    private readonly IMatrixPageModelFactory _matrixPageModelFactory;

	    public CashgameController(
            IHomegameRepository homegameRepository,
            IUserContext userContext, 
            ICashgameRepository cashgameRepository,
            IPlayerRepository playerRepository, 
            IMatrixPageModelFactory matrixPageModelFactory)
	    {
	        _homegameRepository = homegameRepository;
	        _userContext = userContext;
	        _cashgameRepository = cashgameRepository;
	        _playerRepository = playerRepository;
	        _matrixPageModelFactory = matrixPageModelFactory;
	    }

	    public ActionResult Index(string gameName){
            var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var years = _cashgameRepository.GetYears(homegame);
			if(years.Count > 0){
				var year = years[0];
				return new RedirectResult(new CashgameMatrixUrlModel(homegame, year).Url);
			}
			return new RedirectResult(new CashgameAddUrlModel(homegame).Url);
		}

        public ActionResult Matrix(string gameName, int? year = null){
            var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var model = _matrixPageModelFactory.Create(homegame, _userContext.GetUser(), year);
			return View("Matrix/MatrixPage", model);
		}

        public ActionResult Leaderboard(string gameName, int? year = null){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var suite = _cashgameRepository.GetSuite(homegame, year);
			var runningGame = _cashgameRepository.GetRunning(homegame);
			var years = _cashgameRepository.GetYears(homegame);
			var model = new LeaderboardPageModel(_userContext.GetUser(), homegame, suite, years, year, runningGame);
			return View("Leaderboard/LeaderboardPage", model);
		}

        public ActionResult Details(string gameName, string dateStr){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var date = DateTimeFactory.Create(dateStr, homegame.Timezone);
			var cashgame = _cashgameRepository.GetByDate(homegame, date);
			if(cashgame == null){
                return new HttpNotFoundResult();
			}
			var user = _userContext.GetUser();
			var model = GetDetailsModel(user, homegame, cashgame);
			return View("Details/DetailsPage", model);
		}

        public JsonResult DetailsChartJson(string gameName, string dateStr){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var date = DateTimeFactory.Create(dateStr, homegame.Timezone);
			var cashgame = _cashgameRepository.GetByDate(homegame, date);
			if(cashgame == null){
                //todo: find out how to handle 404 for Json requests
                //return new HttpNotFoundResult();
			    return null;
			}
			var model = new GameChartData(homegame, cashgame);
			return Json((object)model);
		}

        public ActionResult Facts(string gameName, int? year = null){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var suite = _cashgameRepository.GetSuite(homegame, year);
            var runningGame = _cashgameRepository.GetRunning(homegame);
			var years = _cashgameRepository.GetYears(homegame);
			var model = new CashgameFactsModel(_userContext.GetUser(), homegame, suite, years, year, runningGame);
			return View("Facts/FactsPage", model);
		}

        private DetailsPageModel GetDetailsModel(User user, Homegame homegame, Cashgame cashgame){
			var player = _playerRepository.GetByUserName(homegame, user.UserName);
			var isManager = _userContext.IsInRole(homegame, Role.Manager);
			var runningGame = _cashgameRepository.GetRunning(homegame);
			var years = _cashgameRepository.GetYears(homegame);
			return new DetailsPageModel(user, homegame, cashgame, player, years, isManager, runningGame);
		}

	}

}