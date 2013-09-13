using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Classes;
using Core.Classes.Checkpoints;
using Core.Exceptions;
using Core.Repositories;
using Infrastructure.Factories;
using Infrastructure.System;
using Web.ModelFactories.CashgameModelFactories.Buyin;
using Web.ModelFactories.CashgameModelFactories.Matrix;
using Web.Models.CashgameModels.Action;
using Web.Models.CashgameModels.Add;
using Web.Models.CashgameModels.Buyin;
using Web.Models.CashgameModels.Chart;
using Web.Models.CashgameModels.Details;
using Web.Models.CashgameModels.Facts;
using Web.Models.CashgameModels.Leaderboard;
using Web.Models.CashgameModels.Listing;
using Web.Models.CashgameModels.Running;
using Web.Models.UrlModels;
using Web.Validators;

namespace Web.Controllers{

	public class CashgameController : Controller {
	    private readonly IHomegameRepository _homegameRepository;
	    private readonly IUserContext _userContext;
	    private readonly ICashgameRepository _cashgameRepository;
	    private readonly IPlayerRepository _playerRepository;
	    private readonly IMatrixPageModelFactory _matrixPageModelFactory;
	    private readonly IWebContext _webContext;
	    private readonly ICashgameValidatorFactory _cashgameValidatorFactory;
	    private readonly ICashgameFactory _cashgameFactory;
	    private readonly ITimeProvider _timeProvider;
	    private readonly IBuyinPageModelFactory _buyinPageModelFactory;

	    public CashgameController(
            IHomegameRepository homegameRepository,
            IUserContext userContext, 
            ICashgameRepository cashgameRepository,
            IPlayerRepository playerRepository, 
            IMatrixPageModelFactory matrixPageModelFactory,
            IWebContext webContext,
            ICashgameValidatorFactory cashgameValidatorFactory,
            ICashgameFactory cashgameFactory,
            ITimeProvider timeProvider,
            IBuyinPageModelFactory buyinPageModelFactory)
	    {
	        _homegameRepository = homegameRepository;
	        _userContext = userContext;
	        _cashgameRepository = cashgameRepository;
	        _playerRepository = playerRepository;
	        _matrixPageModelFactory = matrixPageModelFactory;
	        _webContext = webContext;
	        _cashgameValidatorFactory = cashgameValidatorFactory;
	        _cashgameFactory = cashgameFactory;
	        _timeProvider = timeProvider;
	        _buyinPageModelFactory = buyinPageModelFactory;
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
			var model = new CashgameLeaderboardPageModel(_userContext.GetUser(), homegame, suite, years, year, runningGame);
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
			var model = new CashgameDetailsChartModel(homegame, cashgame);
            return Json(model, JsonRequestBehavior.AllowGet);
		}

        public ActionResult Facts(string gameName, int? year = null){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var suite = _cashgameRepository.GetSuite(homegame, year);
            var runningGame = _cashgameRepository.GetRunning(homegame);
			var years = _cashgameRepository.GetYears(homegame);
			var model = new CashgameFactsPageModel(_userContext.GetUser(), homegame, suite, years, year, runningGame);
			return View("Facts/FactsPage", model);
		}

        public ActionResult Add(string gameName){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var runningGame = _cashgameRepository.GetRunning(homegame);
			if(runningGame != null){
				throw new AccessDeniedException("Game already running");
			}
            var locations = _cashgameRepository.GetLocations(homegame);
            var model = new AddCashgamePageModel(_userContext.GetUser(), homegame, locations);
			return ShowAddForm(model);
		}

        [HttpPost]
        public ActionResult Add(string gameName, AddCashgamePostModel postModel){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
            //todo: validation for the composite location select field
            if (ModelState.IsValid)
            {
                var cashgame = GetCashgame(postModel);
                _cashgameRepository.AddGame(homegame, cashgame);
                return new RedirectResult(new RunningCashgameUrlModel(homegame).Url);
            }
            var locations = _cashgameRepository.GetLocations(homegame);
            var model = new AddCashgamePageModel(_userContext.GetUser(), homegame, locations, postModel);
            return ShowAddForm(model);
		}

        public ActionResult Running(string gameName){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var cashgame = _cashgameRepository.GetRunning(homegame);
			if(cashgame == null){
                return new RedirectResult(new CashgameIndexUrlModel(homegame).Url);
			}
			var user = _userContext.GetUser();
			var player = _playerRepository.GetByUserName(homegame, user.UserName);
			var model = GetRunningPageModel(homegame, cashgame, player);
			return View("Running/RunningPage", model);
		}

        public ActionResult Listing(string gameName, int? year = null){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var games = _cashgameRepository.GetAll(homegame, year);
			var runningGame = _cashgameRepository.GetRunning(homegame);
			var years = _cashgameRepository.GetYears(homegame);
			var model = new CashgameListingPageModel(_userContext.GetUser(), homegame, games, years, year, runningGame);
            return View("Listing/Listing", model);
		}

        public ActionResult Chart(string gameName, int? year = null){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var runningGame = _cashgameRepository.GetRunning(homegame);
			var years = _cashgameRepository.GetYears(homegame);
			var model = new CashgameChartPageModel(_userContext.GetUser(), homegame, year, years, runningGame);
            return View("Chart/Chart", model);
		}

        public JsonResult ChartJson(string gameName, int? year = null){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var suite = _cashgameRepository.GetSuite(homegame, year);
			var model = new CashgameSuiteChartModel(suite);
            return Json(model, JsonRequestBehavior.AllowGet);
		}

        public ActionResult Action(string gameName, string dateStr, string playerName){
			var homegame = _homegameRepository.GetByName(gameName);
			var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
			var player = _playerRepository.GetByName(homegame, playerName);
			_userContext.RequirePlayer(homegame);
			var role = _userContext.GetRole(homegame);
			var runningGame = _cashgameRepository.GetRunning(homegame);
			var years = _cashgameRepository.GetYears(homegame);
			var result = cashgame.GetResult(player);
			var model = new ActionModel(_userContext.GetUser(), homegame, cashgame, player, result, role, years, runningGame);
			return View("Action/Action", model);
		}

		public JsonResult ActionChartJson(string gameName, string dateStr, string playerName){
			var homegame = _homegameRepository.GetByName(gameName);
			var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
			var player = _playerRepository.GetByName(homegame, playerName);
			var result = cashgame.GetResult(player);
			var model = new ActionChartData(homegame, cashgame, result);
            return Json(model, JsonRequestBehavior.AllowGet);
		}

        public ActionResult Buyin(string gameName, string playerName){
			var homegame = _homegameRepository.GetByName(gameName);
			var player = _playerRepository.GetByName(homegame, playerName);
			_userContext.RequirePlayer(homegame);
            var user = _userContext.GetUser();
			var runningGame = _cashgameRepository.GetRunning(homegame);
            var model = _buyinPageModelFactory.Create(user, homegame, player, runningGame);
			return ShowBuyinForm(user, player, model);
		}

        [HttpPost]
        public ActionResult Buyin(string gameName, string playerName, BuyinPostModel postedModel){
			var homegame = _homegameRepository.GetByName(gameName);
			var player = _playerRepository.GetByName(homegame, playerName);
			_userContext.RequirePlayer(homegame);
			var runningGame = _cashgameRepository.GetRunning(homegame);
			//var validator = _cashgameValidatorFactory.GetBuyinValidator(model);
			if(ModelState.IsValid){
				var checkpoint = GetBuyinCheckpoint(homegame, postedModel);
				_cashgameRepository.AddCheckpoint(runningGame, player, checkpoint);
                if(!runningGame.IsStarted){
			    	_cashgameRepository.StartGame(runningGame);
			    }
			} else {
                var user = _userContext.GetUser();
			    var model = _buyinPageModelFactory.Create(user, homegame, player, runningGame, postedModel);
				return ShowBuyinForm(user, player, model);
			}
			var runningUrl = new RunningCashgameUrlModel(homegame);
            return new RedirectResult(runningUrl.Url);
		}

		private Checkpoint GetBuyinCheckpoint(Homegame homegame, BuyinPostModel model){
			var timestamp = DateTimeFactory.Now(homegame.Timezone);
			return new BuyinCheckpoint(timestamp, model.StackAmount + model.BuyinAmount, model.BuyinAmount);
		}

        private ActionResult ShowBuyinForm(User user, Player player, BuyinPageModel model){
			if(!_userContext.IsAdmin() && player.UserName != user.UserName){
				throw new AccessDeniedException();
			}

			return View("Buyin/Buyin", model);
		}
        
		private RunningCashgamePageModel GetRunningPageModel(Homegame homegame, Cashgame cashgame, Player player){
			var isManager = _userContext.IsInRole(homegame, Role.Manager);
			var runningGame = _cashgameRepository.GetRunning(homegame);
			var years = _cashgameRepository.GetYears(homegame);
			return new RunningCashgamePageModel(_userContext.GetUser(), homegame, cashgame, player, years, isManager, _timeProvider, runningGame);
		}

        private CashgameDetailsPageModel GetDetailsModel(User user, Homegame homegame, Cashgame cashgame){
			var player = _playerRepository.GetByUserName(homegame, user.UserName);
			var isManager = _userContext.IsInRole(homegame, Role.Manager);
			var runningGame = _cashgameRepository.GetRunning(homegame);
			var years = _cashgameRepository.GetYears(homegame);
			return new CashgameDetailsPageModel(user, homegame, cashgame, player, years, isManager, runningGame);
		}

        private ActionResult ShowAddForm(AddCashgamePageModel model)
        {
			return View("Add/Add", model);
		}

		private Cashgame GetCashgame(AddCashgamePostModel addCashgamePostModel){
			return _cashgameFactory.Create(addCashgamePostModel.Location, GameStatus.Running);
		}

	}
}