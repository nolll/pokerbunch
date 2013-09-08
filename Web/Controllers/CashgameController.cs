using System.Collections.Generic;
using System.Web.Mvc;
using Core.Classes;
using Core.Exceptions;
using Core.Repositories;
using Infrastructure.Factories;
using Infrastructure.System;
using Web.ModelFactories.CashgameModelFactories.Matrix;
using Web.Models.CashgameModels.Add;
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

	    public CashgameController(
            IHomegameRepository homegameRepository,
            IUserContext userContext, 
            ICashgameRepository cashgameRepository,
            IPlayerRepository playerRepository, 
            IMatrixPageModelFactory matrixPageModelFactory,
            IWebContext webContext,
            ICashgameValidatorFactory cashgameValidatorFactory,
            ICashgameFactory cashgameFactory,
            ITimeProvider timeProvider)
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
			return ShowAddForm(homegame);
		}

        [HttpPost]
        public ActionResult Add(string gameName, AddCashgamePageModel addCashgamePageModel){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var postModel = new AddCashgamePostModel(_webContext.GetPostParam("location"), _webContext.GetPostParam("location-dropdown"));
			return HandleAddPost(homegame, postModel);
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

        private ActionResult ShowAddForm(Homegame homegame, Cashgame cashgame = null, List<string> validationErrors = null){
			var runningGame = _cashgameRepository.GetRunning(homegame);
			var locations = _cashgameRepository.GetLocations(homegame);
			var years = _cashgameRepository.GetYears(homegame);
			var model = new AddCashgamePageModel(_userContext.GetUser(), homegame, cashgame, locations, years, runningGame);
			if(validationErrors != null){
				model.SetValidationErrors(validationErrors);
			}
			return View("Add/Add", model);
		}

        private ActionResult HandleAddPost(Homegame homegame, AddCashgamePostModel addCashgamePostModel){
            var cashgame = GetCashgame(addCashgamePostModel);
            var validator = _cashgameValidatorFactory.GetAddCashgameValidator(homegame, cashgame);
            if(validator.IsValid){
                _cashgameRepository.AddGame(homegame, cashgame);
                return new RedirectResult(new RunningCashgameUrlModel(homegame).Url);
            }
            return ShowAddForm(homegame, cashgame, validator.GetErrors());
        }

		private Cashgame GetCashgame(AddCashgamePostModel addCashgamePostModel){
			return _cashgameFactory.Create(addCashgamePostModel.Location, GameStatus.Running);
		}

	}

}