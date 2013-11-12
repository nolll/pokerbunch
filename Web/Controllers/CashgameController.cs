using System.Web.Mvc;
using Core.Classes;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Infrastructure.Factories;
using Infrastructure.System;
using Web.ModelFactories.CashgameModelFactories.Action;
using Web.ModelFactories.CashgameModelFactories.Add;
using Web.ModelFactories.CashgameModelFactories.Buyin;
using Web.ModelFactories.CashgameModelFactories.Cashout;
using Web.ModelFactories.CashgameModelFactories.Chart;
using Web.ModelFactories.CashgameModelFactories.Details;
using Web.ModelFactories.CashgameModelFactories.Edit;
using Web.ModelFactories.CashgameModelFactories.End;
using Web.ModelFactories.CashgameModelFactories.Facts;
using Web.ModelFactories.CashgameModelFactories.Leaderboard;
using Web.ModelFactories.CashgameModelFactories.Listing;
using Web.ModelFactories.CashgameModelFactories.Matrix;
using Web.ModelFactories.CashgameModelFactories.Report;
using Web.ModelFactories.CashgameModelFactories.Running;
using Web.ModelMappers;
using Web.Models.CashgameModels.Add;
using Web.Models.CashgameModels.Buyin;
using Web.Models.CashgameModels.Cashout;
using Web.Models.CashgameModels.Details;
using Web.Models.CashgameModels.Edit;
using Web.Models.CashgameModels.End;
using Web.Models.CashgameModels.Report;
using Web.Models.CashgameModels.Running;

namespace Web.Controllers{

	public class CashgameController : Controller {
	    private readonly IHomegameRepository _homegameRepository;
	    private readonly IUserContext _userContext;
	    private readonly ICashgameRepository _cashgameRepository;
	    private readonly IPlayerRepository _playerRepository;
	    private readonly IMatrixPageModelFactory _matrixPageModelFactory;
	    private readonly ICashgameFactory _cashgameFactory;
	    private readonly IBuyinPageModelFactory _buyinPageModelFactory;
	    private readonly IReportPageModelFactory _reportPageModelFactory;
	    private readonly ICashoutPageModelFactory _cashoutPageModelFactory;
	    private readonly IEndPageModelFactory _endPageModelFactory;
	    private readonly IActionPageModelFactory _actionPageModelFactory;
	    private readonly IAddCashgamePageModelFactory _addCashgamePageModelFactory;
	    private readonly ICashgameChartPageModelFactory _cashgameChartPageModelFactory;
	    private readonly ICashgameDetailsPageModelFactory _cashgameDetailsPageModelFactory;
	    private readonly ICashgameEditPageModelFactory _cashgameEditPageModelFactory;
	    private readonly ICashgameFactsPageModelFactory _cashgameFactsPageModelFactory;
	    private readonly ICashgameLeaderboardPageModelFactory _cashgameLeaderboardPageModelFactory;
	    private readonly ICashgameListingPageModelFactory _cashgameListingPageModelFactory;
	    private readonly IRunningCashgamePageModelFactory _runningCashgamePageModelFactory;
	    private readonly ICashgameModelMapper _cashgameModelMapper;
	    private readonly ICheckpointModelMapper _checkpointModelMapper;
	    private readonly IUrlProvider _urlProvider;
	    private readonly ICashgameSuiteChartModelFactory _cashgameSuiteChartModelFactory;
	    private readonly IActionChartModelFactory _actionChartModelFactory;
	    private readonly ICashgameDetailsChartModelFactory _cashgameDetailsChartModelFactory;
	    private readonly ITimeProvider _timeProvider;
	    private readonly ICheckpointRepository _checkpointRepository;

	    public CashgameController(
            IHomegameRepository homegameRepository,
            IUserContext userContext, 
            ICashgameRepository cashgameRepository,
            IPlayerRepository playerRepository, 
            IMatrixPageModelFactory matrixPageModelFactory,
            ICashgameFactory cashgameFactory,
            IBuyinPageModelFactory buyinPageModelFactory,
            IReportPageModelFactory reportPageModelFactory,
            ICashoutPageModelFactory cashoutPageModelFactory,
            IEndPageModelFactory endPageModelFactory,
            IActionPageModelFactory actionPageModelFactory,
            IAddCashgamePageModelFactory addCashgamePageModelFactory,
            ICashgameChartPageModelFactory cashgameChartPageModelFactory,
            ICashgameDetailsPageModelFactory cashgameDetailsPageModelFactory,
            ICashgameEditPageModelFactory cashgameEditPageModelFactory,
            ICashgameFactsPageModelFactory cashgameFactsPageModelFactory,
            ICashgameLeaderboardPageModelFactory cashgameLeaderboardPageModelFactory,
            ICashgameListingPageModelFactory cashgameListingPageModelFactory,
            IRunningCashgamePageModelFactory runningCashgamePageModelFactory,
            ICashgameModelMapper cashgameModelMapper,
            ICheckpointModelMapper checkpointModelMapper,
            IUrlProvider urlProvider,
            ICashgameSuiteChartModelFactory cashgameSuiteChartModelFactory,
            IActionChartModelFactory actionChartModelFactory,
            ICashgameDetailsChartModelFactory cashgameDetailsChartModelFactory,
            ITimeProvider timeProvider,
            ICheckpointRepository checkpointRepository)
	    {
	        _homegameRepository = homegameRepository;
	        _userContext = userContext;
	        _cashgameRepository = cashgameRepository;
	        _playerRepository = playerRepository;
	        _matrixPageModelFactory = matrixPageModelFactory;
	        _cashgameFactory = cashgameFactory;
	        _buyinPageModelFactory = buyinPageModelFactory;
	        _reportPageModelFactory = reportPageModelFactory;
	        _cashoutPageModelFactory = cashoutPageModelFactory;
	        _endPageModelFactory = endPageModelFactory;
	        _actionPageModelFactory = actionPageModelFactory;
	        _addCashgamePageModelFactory = addCashgamePageModelFactory;
	        _cashgameChartPageModelFactory = cashgameChartPageModelFactory;
	        _cashgameDetailsPageModelFactory = cashgameDetailsPageModelFactory;
	        _cashgameEditPageModelFactory = cashgameEditPageModelFactory;
	        _cashgameFactsPageModelFactory = cashgameFactsPageModelFactory;
	        _cashgameLeaderboardPageModelFactory = cashgameLeaderboardPageModelFactory;
	        _cashgameListingPageModelFactory = cashgameListingPageModelFactory;
	        _runningCashgamePageModelFactory = runningCashgamePageModelFactory;
	        _cashgameModelMapper = cashgameModelMapper;
	        _checkpointModelMapper = checkpointModelMapper;
	        _urlProvider = urlProvider;
	        _cashgameSuiteChartModelFactory = cashgameSuiteChartModelFactory;
	        _actionChartModelFactory = actionChartModelFactory;
	        _cashgameDetailsChartModelFactory = cashgameDetailsChartModelFactory;
	        _timeProvider = timeProvider;
	        _checkpointRepository = checkpointRepository;
	    }

	    public ActionResult Index(string gameName){
            var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var years = _cashgameRepository.GetYears(homegame);
			if(years.Count > 0){
				var year = years[0];
				return Redirect(_urlProvider.GetCashgameMatrixUrl(homegame, year));
			}
			return Redirect(_urlProvider.GetCashgameAddUrl(homegame));
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
			var model = _cashgameLeaderboardPageModelFactory.Create(_userContext.GetUser(), homegame, suite, years, year, runningGame);
			return View("Leaderboard/LeaderboardPage", model);
		}

        public ActionResult Details(string gameName, string dateStr){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
			if(cashgame == null){
                return new HttpNotFoundResult();
			}
			var user = _userContext.GetUser();
			var model = GetDetailsModel(user, homegame, cashgame);
			return View("Details/DetailsPage", model);
		}

        public ActionResult DetailsChartJson(string gameName, string dateStr){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
			if(cashgame == null){
                return new HttpNotFoundResult();
			}
			var model = _cashgameDetailsChartModelFactory.Create(homegame, cashgame);
            return Json(model, JsonRequestBehavior.AllowGet);
		}

        public ActionResult Facts(string gameName, int? year = null){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var suite = _cashgameRepository.GetSuite(homegame, year);
            var runningGame = _cashgameRepository.GetRunning(homegame);
			var years = _cashgameRepository.GetYears(homegame);
			var model = _cashgameFactsPageModelFactory.Create(_userContext.GetUser(), homegame, suite, years, year, runningGame);
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
            var model = _addCashgamePageModelFactory.Create(_userContext.GetUser(), homegame, locations);
			return ShowAddForm(model);
		}

        [HttpPost]
        public ActionResult Add(string gameName, AddCashgamePostModel postModel){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
            if (ModelState.IsValid)
            {
                if (postModel.HasLocation)
                {
                    var cashgame = GetCashgame(postModel);
                    _cashgameRepository.AddGame(homegame, cashgame);
                    _cashgameRepository.ClearCashgameListFromCache(homegame, cashgame);
                    return Redirect(_urlProvider.GetRunningCashgameUrl(homegame));
                }
                ModelState.AddModelError("no_location", "Please enter a location");
            }
            var locations = _cashgameRepository.GetLocations(homegame);
            var model = _addCashgamePageModelFactory.Create(_userContext.GetUser(), homegame, locations, postModel);
            return ShowAddForm(model);
		}

        public ActionResult Edit(string gameName, string dateStr){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequireManager(homegame);
			var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
			var runningGame = _cashgameRepository.GetRunning(homegame);
			var locations = _cashgameRepository.GetLocations(homegame);
			var years = _cashgameRepository.GetYears(homegame);
			var model = _cashgameEditPageModelFactory.Create(_userContext.GetUser(), homegame, cashgame, locations, years, runningGame);
			return View("Edit/Edit", model);
		}

        [HttpPost]
		public ActionResult Edit(string gameName, string dateStr, CashgameEditPostModel postModel){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequireManager(homegame);
			var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
			if(ModelState.IsValid)
			{
			    cashgame = _cashgameModelMapper.GetCashgame(cashgame, postModel);
				_cashgameRepository.UpdateGame(cashgame);
				var detailsUrl = _urlProvider.GetCashgameDetailsUrl(homegame, cashgame);
				return Redirect(detailsUrl);
			}
			var runningGame = _cashgameRepository.GetRunning(homegame);
			var locations = _cashgameRepository.GetLocations(homegame);
			var model = _cashgameEditPageModelFactory.Create(_userContext.GetUser(), homegame, cashgame, locations, null, runningGame, postModel);
            return View("Edit/Edit", model);
		}

        public ActionResult Running(string gameName){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var cashgame = _cashgameRepository.GetRunning(homegame);
			if(cashgame == null){
                return Redirect(_urlProvider.GetCashgameIndexUrl(homegame));
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
			var model = _cashgameListingPageModelFactory.Create(_userContext.GetUser(), homegame, games, years, year, runningGame);
            return View("Listing/Listing", model);
		}

        public ActionResult Chart(string gameName, int? year = null){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var runningGame = _cashgameRepository.GetRunning(homegame);
			var years = _cashgameRepository.GetYears(homegame);
			var model = _cashgameChartPageModelFactory.Create(_userContext.GetUser(), homegame, year, years, runningGame);
            return View("Chart/Chart", model);
		}

        public JsonResult ChartJson(string gameName, int? year = null){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var suite = _cashgameRepository.GetSuite(homegame, year);
			var model = _cashgameSuiteChartModelFactory.Create(suite);
            return Json(model, JsonRequestBehavior.AllowGet);
		}

        public ActionResult Action(string gameName, string dateStr, string name){
			var homegame = _homegameRepository.GetByName(gameName);
			var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
			var player = _playerRepository.GetByName(homegame, name);
			_userContext.RequirePlayer(homegame);
			var role = _userContext.GetRole(homegame);
			var runningGame = _cashgameRepository.GetRunning(homegame);
			var years = _cashgameRepository.GetYears(homegame);
			var result = cashgame.GetResult(player);
			var model = _actionPageModelFactory.Create(_userContext.GetUser(), homegame, cashgame, player, result, role, years, runningGame);
			return View("Action/Action", model);
		}

		public JsonResult ActionChartJson(string gameName, string dateStr, string name){
			var homegame = _homegameRepository.GetByName(gameName);
			var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
			var player = _playerRepository.GetByName(homegame, name);
			var result = cashgame.GetResult(player);
			var model = _actionChartModelFactory.Create(homegame, cashgame, result);
            return Json(model, JsonRequestBehavior.AllowGet);
		}

        public ActionResult Buyin(string gameName, string name){
			var homegame = _homegameRepository.GetByName(gameName);
			var player = _playerRepository.GetByName(homegame, name);
			_userContext.RequirePlayer(homegame);
            var user = _userContext.GetUser();
			var runningGame = _cashgameRepository.GetRunning(homegame);
            var model = _buyinPageModelFactory.Create(user, homegame, player, runningGame);
			return ShowBuyinForm(user, player, model);
		}

        [HttpPost]
        public ActionResult Buyin(string gameName, string name, BuyinPostModel postModel){
			var homegame = _homegameRepository.GetByName(gameName);
			var player = _playerRepository.GetByName(homegame, name);
			_userContext.RequirePlayer(homegame);
			var runningGame = _cashgameRepository.GetRunning(homegame);
			if(ModelState.IsValid)
			{
			    var checkpoint = _checkpointModelMapper.GetCheckpoint(postModel);
				_checkpointRepository.AddCheckpoint(runningGame, player, checkpoint);
                if(!runningGame.IsStarted){
			    	_cashgameRepository.StartGame(runningGame);
			    }
                _cashgameRepository.ClearCashgameFromCache(runningGame);
			} else {
                var user = _userContext.GetUser();
			    var model = _buyinPageModelFactory.Create(user, homegame, player, runningGame, postModel);
				return ShowBuyinForm(user, player, model);
			}
            return Redirect(_urlProvider.GetRunningCashgameUrl(homegame));
		}

        public ActionResult Report(string gameName, string name){
			var homegame = _homegameRepository.GetByName(gameName);
			var cashgame = _cashgameRepository.GetRunning(homegame);
			var player = _playerRepository.GetByName(homegame, name);
			_userContext.RequirePlayer(homegame);
			var user = _userContext.GetUser();
			var model = _reportPageModelFactory.Create(user, homegame, player, cashgame);
			return ShowReportForm(player, user, model);
		}

        [HttpPost]
        public ActionResult Report(string gameName, string name, ReportPostModel postModel){
			var homegame = _homegameRepository.GetByName(gameName);
			var cashgame = _cashgameRepository.GetRunning(homegame);
			var player = _playerRepository.GetByName(homegame, name);
			_userContext.RequirePlayer(homegame);
			var user = _userContext.GetUser();
			if(ModelState.IsValid)
			{
			    var checkpoint = _checkpointModelMapper.GetCheckpoint(postModel);
                _checkpointRepository.AddCheckpoint(cashgame, player, checkpoint);
                _cashgameRepository.ClearCashgameFromCache(cashgame);
                return Redirect(_urlProvider.GetRunningCashgameUrl(homegame));
			}
            var model = _reportPageModelFactory.Create(user, homegame, player, cashgame, postModel);
            return ShowReportForm(player, user, model);
		}

        public ActionResult DeleteCheckpoint(string gameName, string dateStr, string name, int id){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequireManager(homegame);
			var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
			var player = _playerRepository.GetByName(homegame, name);
            _checkpointRepository.DeleteCheckpoint(id);
            var actionsUrl = _urlProvider.GetCashgameActionUrl(homegame, cashgame, player);
            return Redirect(actionsUrl);
		}

        public ActionResult Cashout(string gameName, string name){
			var homegame = _homegameRepository.GetByName(gameName);
			var player = _playerRepository.GetByName(homegame, name);
			_userContext.RequirePlayer(homegame);
			var user = _userContext.GetUser();
			if(!_userContext.IsAdmin() && player.UserName != user.UserName){
				throw new AccessDeniedException();
			}
			var runningGame = _cashgameRepository.GetRunning(homegame);
            var model = _cashoutPageModelFactory.Create(user, homegame, runningGame);
            return View("Cashout/Cashout", model);
		}

        [HttpPost]
        public ActionResult Cashout(string gameName, string name, CashoutPostModel postModel){
			var homegame = _homegameRepository.GetByName(gameName);
			var player = _playerRepository.GetByName(homegame, name);
			_userContext.RequirePlayer(homegame);
			var user = _userContext.GetUser();
			if(!_userContext.IsAdmin() && player.UserName != user.UserName){
				throw new AccessDeniedException();
			}
            var runningGame = _cashgameRepository.GetRunning(homegame);
            var result = runningGame.GetResult(player);
            var postedCheckpoint = _checkpointModelMapper.GetCheckpoint(postModel, result.CashoutCheckpoint);
			if(ModelState.IsValid){
				if(result.CashoutCheckpoint != null){
                    _checkpointRepository.UpdateCheckpoint(runningGame, postedCheckpoint);
				} else {
                    _checkpointRepository.AddCheckpoint(runningGame, player, postedCheckpoint);
				}
                _cashgameRepository.ClearCashgameFromCache(runningGame);
                return Redirect(_urlProvider.GetRunningCashgameUrl(homegame));
			}
            var model = _cashoutPageModelFactory.Create(user, homegame, runningGame, postModel);
            return View("Cashout/Cashout", model);
		}

        public ActionResult End(string gameName){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequirePlayer(homegame);
			var user = _userContext.GetUser();
            var runningGame = _cashgameRepository.GetRunning(homegame);
			var model = _endPageModelFactory.Create(user, homegame, runningGame);
			return View("End/End", model);
		}

        [HttpPost]
		public ActionResult End(string gameName, EndPageModel postModel){
			var homegame = _homegameRepository.GetByName(gameName);
			var cashgame = _cashgameRepository.GetRunning(homegame);
			_userContext.RequirePlayer(homegame);
			_cashgameRepository.EndGame(cashgame);
            _cashgameRepository.ClearCashgameFromCache(cashgame);
            _cashgameRepository.ClearCashgameListFromCache(homegame, cashgame);
            return Redirect(_urlProvider.GetCashgameIndexUrl(homegame));
		}

        public ActionResult Delete(string gameName, string dateStr){
			var homegame = _homegameRepository.GetByName(gameName);
			_userContext.RequireManager(homegame);
			var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
			_cashgameRepository.DeleteGame(cashgame);
            var date = _timeProvider.Parse(dateStr, homegame.Timezone);
            var listUrl = _urlProvider.GetCashgameListingUrl(homegame, date.Year);
            return Redirect(listUrl);
		}

        private ActionResult ShowReportForm(Player player, User user, ReportPageModel model){
            if (!_userContext.IsAdmin() && player.UserName != user.UserName)
            {
                throw new AccessDeniedException();
            }
            return View("Report/Report", model);
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
			return _runningCashgamePageModelFactory.Create(_userContext.GetUser(), homegame, cashgame, player, years, isManager, runningGame);
		}

        private CashgameDetailsPageModel GetDetailsModel(User user, Homegame homegame, Cashgame cashgame){
			var player = _playerRepository.GetByUserName(homegame, user.UserName);
			var isManager = _userContext.IsInRole(homegame, Role.Manager);
			var runningGame = _cashgameRepository.GetRunning(homegame);
			var years = _cashgameRepository.GetYears(homegame);
			return _cashgameDetailsPageModelFactory.Create(user, homegame, cashgame, player, years, isManager, runningGame);
		}

        private ActionResult ShowAddForm(AddCashgamePageModel model)
        {
			return View("Add/Add", model);
		}

		private Cashgame GetCashgame(AddCashgamePostModel addCashgamePostModel)
		{
			return _cashgameFactory.Create(addCashgamePostModel.Location, (int)GameStatus.Running);
		}

	}
}