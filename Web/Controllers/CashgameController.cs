using System;
using System.Web.Mvc;
using Core.Classes;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Infrastructure.Factories;
using Infrastructure.System;
using Web.Commands.CashgameCommands;
using Web.ModelFactories.CashgameModelFactories.Action;
using Web.ModelFactories.CashgameModelFactories.Buyin;
using Web.ModelFactories.CashgameModelFactories.Cashout;
using Web.ModelFactories.CashgameModelFactories.Chart;
using Web.ModelFactories.CashgameModelFactories.End;
using Web.ModelFactories.CashgameModelFactories.List;
using Web.ModelFactories.CashgameModelFactories.Report;
using Web.ModelFactories.CashgameModelFactories.Running;
using Web.ModelMappers;
using Web.ModelServices;
using Web.Models.CashgameModels.Add;
using Web.Models.CashgameModels.Buyin;
using Web.Models.CashgameModels.Cashout;
using Web.Models.CashgameModels.Edit;
using Web.Models.CashgameModels.End;
using Web.Models.CashgameModels.Toplist;
using Web.Models.CashgameModels.Report;
using Web.Models.CashgameModels.Running;

namespace Web.Controllers{

	public class CashgameController : Controller {
	    private readonly IHomegameRepository _homegameRepository;
	    private readonly IAuthentication _authentication;
	    private readonly IAuthorization _authorization;
	    private readonly ICashgameRepository _cashgameRepository;
	    private readonly IPlayerRepository _playerRepository;
	    private readonly ICashgameFactory _cashgameFactory;
	    private readonly IBuyinPageModelFactory _buyinPageModelFactory;
	    private readonly IReportPageModelFactory _reportPageModelFactory;
	    private readonly ICashoutPageModelFactory _cashoutPageModelFactory;
	    private readonly IEndPageModelFactory _endPageModelFactory;
	    private readonly IActionPageModelFactory _actionPageModelFactory;
	    private readonly ICashgameChartPageModelFactory _cashgameChartPageModelFactory;
	    private readonly ICashgameListPageModelFactory _cashgameListPageModelFactory;
	    private readonly IRunningCashgamePageModelFactory _runningCashgamePageModelFactory;
	    private readonly ICashgameModelMapper _cashgameModelMapper;
	    private readonly ICheckpointModelMapper _checkpointModelMapper;
	    private readonly IUrlProvider _urlProvider;
	    private readonly ICashgameSuiteChartModelFactory _cashgameSuiteChartModelFactory;
	    private readonly IActionChartModelFactory _actionChartModelFactory;
	    private readonly ITimeProvider _timeProvider;
	    private readonly ICheckpointRepository _checkpointRepository;
	    private readonly ICashgameService _cashgameService;
	    private readonly ICashgameCommandProvider _cashgameCommandProvider;
	    private readonly ICashgameModelService _cashgameModelService;
	    private readonly IWebContext _webContext;

	    public CashgameController(
            IHomegameRepository homegameRepository,
            IAuthentication authentication, 
            IAuthorization authorization,
            ICashgameRepository cashgameRepository,
            IPlayerRepository playerRepository, 
            ICashgameFactory cashgameFactory,
            IBuyinPageModelFactory buyinPageModelFactory,
            IReportPageModelFactory reportPageModelFactory,
            ICashoutPageModelFactory cashoutPageModelFactory,
            IEndPageModelFactory endPageModelFactory,
            IActionPageModelFactory actionPageModelFactory,
            ICashgameChartPageModelFactory cashgameChartPageModelFactory,
            ICashgameListPageModelFactory cashgameListPageModelFactory,
            IRunningCashgamePageModelFactory runningCashgamePageModelFactory,
            ICashgameModelMapper cashgameModelMapper,
            ICheckpointModelMapper checkpointModelMapper,
            IUrlProvider urlProvider,
            ICashgameSuiteChartModelFactory cashgameSuiteChartModelFactory,
            IActionChartModelFactory actionChartModelFactory,
            ITimeProvider timeProvider,
            ICheckpointRepository checkpointRepository,
            ICashgameService cashgameService,
            ICashgameCommandProvider cashgameCommandProvider,
            ICashgameModelService cashgameModelService,
            IWebContext webContext)
	    {
	        _homegameRepository = homegameRepository;
	        _authentication = authentication;
	        _authorization = authorization;
	        _cashgameRepository = cashgameRepository;
	        _playerRepository = playerRepository;
	        _cashgameFactory = cashgameFactory;
	        _buyinPageModelFactory = buyinPageModelFactory;
	        _reportPageModelFactory = reportPageModelFactory;
	        _cashoutPageModelFactory = cashoutPageModelFactory;
	        _endPageModelFactory = endPageModelFactory;
	        _actionPageModelFactory = actionPageModelFactory;
	        _cashgameChartPageModelFactory = cashgameChartPageModelFactory;
	        _cashgameListPageModelFactory = cashgameListPageModelFactory;
	        _runningCashgamePageModelFactory = runningCashgamePageModelFactory;
	        _cashgameModelMapper = cashgameModelMapper;
	        _checkpointModelMapper = checkpointModelMapper;
	        _urlProvider = urlProvider;
	        _cashgameSuiteChartModelFactory = cashgameSuiteChartModelFactory;
	        _actionChartModelFactory = actionChartModelFactory;
	        _timeProvider = timeProvider;
	        _checkpointRepository = checkpointRepository;
	        _cashgameService = cashgameService;
	        _cashgameCommandProvider = cashgameCommandProvider;
	        _cashgameModelService = cashgameModelService;
	        _webContext = webContext;
	    }

	    public ActionResult Index(string slug)
	    {
	        var url = _cashgameModelService.GetIndexUrl(slug);
            return Redirect(url);
		}

        public ActionResult Matrix(string slug, int? year = null)
        {
            var model = _cashgameModelService.GetMatrixModel(slug, year);
			return View("Matrix/MatrixPage", model);
		}

        public ActionResult Toplist(string slug, int? year = null)
        {
            var sortOrder = GetToplistSortOrder();
            var model = _cashgameModelService.GetToplistModel(slug, sortOrder, year);
            return View("Toplist/ToplistPage", model);
		}

	    public ActionResult Details(string slug, string dateStr)
        {
            var model = _cashgameModelService.GetDetailsModel(slug, dateStr);
			return View("Details/DetailsPage", model);
		}

        public ActionResult DetailsChartJson(string slug, string dateStr)
        {
            var model = _cashgameModelService.GetDetailsChartJsonModel(slug, dateStr);
		    return Json(model, JsonRequestBehavior.AllowGet);
		}

        public ActionResult Facts(string slug, int? year = null)
        {
            var model = _cashgameModelService.GetFactsModel(slug, year);
			return View("Facts/FactsPage", model);
		}

        public ActionResult Add(string slug)
        {
            return ShowAddForm(slug);
		}

        [HttpPost]
        public ActionResult Add(string slug, AddCashgamePostModel postModel){
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            if (ModelState.IsValid)
            {
                if (postModel.HasLocation)
                {
                    var cashgame = GetCashgame(postModel);
                    var homegame = _homegameRepository.GetByName(slug);
			        _cashgameRepository.AddGame(homegame, cashgame);
                    return Redirect(_urlProvider.GetRunningCashgameUrl(homegame));
                }
                ModelState.AddModelError("no_location", "Please enter a location");
            }
            return ShowAddForm(slug);
		}

        public ActionResult Edit(string slug, string dateStr)
        {
            var model = _cashgameModelService.GetEditModel(slug, dateStr);
			return View("Edit/Edit", model);
		}

        [HttpPost]
		public ActionResult Edit(string slug, string dateStr, CashgameEditPostModel postModel){
			_authentication.RequireUser();
            _authorization.RequireManager(slug);
            var homegame = _homegameRepository.GetByName(slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
			if(ModelState.IsValid)
			{
			    cashgame = _cashgameModelMapper.GetCashgame(cashgame, postModel);
				_cashgameRepository.UpdateGame(cashgame);
				var detailsUrl = _urlProvider.GetCashgameDetailsUrl(homegame, cashgame);
				return Redirect(detailsUrl);
			}
            var model = _cashgameModelService.GetEditModel(slug, dateStr);
            return View("Edit/Edit", model);
		}

        public ActionResult Running(string slug){
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            var homegame = _homegameRepository.GetByName(slug);
            if(!_cashgameService.CashgameIsRunning(slug))
            {
                return Redirect(_urlProvider.GetCashgameIndexUrl(homegame));
			}
			var user = _authentication.GetUser();
			var player = _playerRepository.GetByUserName(homegame, user.UserName);
            var cashgame = _cashgameRepository.GetRunning(homegame);
			var model = GetRunningPageModel(homegame, cashgame, player);
			return View("Running/RunningPage", model);
		}

        public ActionResult List(string slug, int? year = null){
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            var homegame = _homegameRepository.GetByName(slug);
            var games = _cashgameRepository.GetPublished(homegame, year);
			var years = _cashgameRepository.GetYears(homegame);
			var model = _cashgameListPageModelFactory.Create(_authentication.GetUser(), homegame, games, years, year);
            return View("List/List", model);
		}

        public ActionResult Chart(string slug, int? year = null){
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            var homegame = _homegameRepository.GetByName(slug);
            var years = _cashgameRepository.GetYears(homegame);
			var model = _cashgameChartPageModelFactory.Create(_authentication.GetUser(), homegame, year, years);
            return View("Chart/Chart", model);
		}

        public JsonResult ChartJson(string slug, int? year = null){
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            var homegame = _homegameRepository.GetByName(slug);
            var suite = _cashgameService.GetSuite(homegame, year);
			var model = _cashgameSuiteChartModelFactory.Create(suite);
            return Json(model, JsonRequestBehavior.AllowGet);
		}

        public ActionResult Action(string slug, string dateStr, string playerName){
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            var homegame = _homegameRepository.GetByName(slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
            var player = _playerRepository.GetByName(homegame, playerName);
            var role = _authorization.GetRole(homegame);
			var result = cashgame.GetResult(player.Id);
			var model = _actionPageModelFactory.Create(_authentication.GetUser(), homegame, cashgame, player, result, role);
			return View("Action/Action", model);
		}

		public JsonResult ActionChartJson(string slug, string dateStr, string playerName){
			var homegame = _homegameRepository.GetByName(slug);
			var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
			var player = _playerRepository.GetByName(homegame, playerName);
			var result = cashgame.GetResult(player.Id);
			var model = _actionChartModelFactory.Create(homegame, cashgame, result);
            return Json(model, JsonRequestBehavior.AllowGet);
		}

        public ActionResult Buyin(string slug, string playerName){
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            var user = _authentication.GetUser();
            var homegame = _homegameRepository.GetByName(slug);
            var player = _playerRepository.GetByName(homegame, playerName);
            var runningGame = _cashgameRepository.GetRunning(homegame);
            var model = _buyinPageModelFactory.Create(user, homegame, player, runningGame);
			return ShowBuyinForm(user, player, model);
		}

        [HttpPost]
        public ActionResult Buyin(string slug, string playerName, BuyinPostModel postModel){
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            var homegame = _homegameRepository.GetByName(slug);
            var player = _playerRepository.GetByName(homegame, playerName);
            var runningGame = _cashgameRepository.GetRunning(homegame);
			if(ModelState.IsValid)
			{
			    var checkpoint = _checkpointModelMapper.GetCheckpoint(postModel);
				_checkpointRepository.AddCheckpoint(runningGame, player, checkpoint);
                if(!runningGame.IsStarted){
			    	_cashgameRepository.StartGame(runningGame);
			    }
			} else {
                var user = _authentication.GetUser();
			    var model = _buyinPageModelFactory.Create(user, homegame, player, runningGame, postModel);
				return ShowBuyinForm(user, player, model);
			}
            return Redirect(_urlProvider.GetRunningCashgameUrl(homegame));
		}

        public ActionResult Report(string slug, string playerName){
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
			var user = _authentication.GetUser();
            var homegame = _homegameRepository.GetByName(slug);
            var cashgame = _cashgameRepository.GetRunning(homegame);
            var player = _playerRepository.GetByName(homegame, playerName);
            var model = _reportPageModelFactory.Create(user, homegame, player, cashgame);
			return ShowReportForm(player, user, model);
		}

        [HttpPost]
        public ActionResult Report(string slug, string playerName, ReportPostModel postModel){
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
			var user = _authentication.GetUser();
            var homegame = _homegameRepository.GetByName(slug);
            var cashgame = _cashgameRepository.GetRunning(homegame);
            var player = _playerRepository.GetByName(homegame, playerName);
            if(ModelState.IsValid)
			{
			    var checkpoint = _checkpointModelMapper.GetCheckpoint(postModel);
                _checkpointRepository.AddCheckpoint(cashgame, player, checkpoint);
                return Redirect(_urlProvider.GetRunningCashgameUrl(homegame));
			}
            var model = _reportPageModelFactory.Create(user, homegame, player, cashgame, postModel);
            return ShowReportForm(player, user, model);
		}

        public ActionResult DeleteCheckpoint(string slug, string dateStr, string playerName, int checkpointId){
			_authentication.RequireUser();
            _authorization.RequireManager(slug);
            var homegame = _homegameRepository.GetByName(slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
			var player = _playerRepository.GetByName(homegame, playerName);
            _checkpointRepository.DeleteCheckpoint(cashgame, checkpointId);
            var actionsUrl = _urlProvider.GetCashgameActionUrl(homegame, cashgame, player);
            return Redirect(actionsUrl);
		}

        public ActionResult Cashout(string slug, string playerName){
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
			var user = _authentication.GetUser();
            var homegame = _homegameRepository.GetByName(slug);
            var player = _playerRepository.GetByName(homegame, playerName);
            if (!_authentication.IsAdmin() && player.UserId != user.Id)
            {
				throw new AccessDeniedException();
			}
            var model = _cashoutPageModelFactory.Create(user, homegame);
            return View("Cashout/Cashout", model);
		}

        [HttpPost]
        public ActionResult Cashout(string slug, string playerName, CashoutPostModel postModel){
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
			var user = _authentication.GetUser();
            var homegame = _homegameRepository.GetByName(slug);
            var player = _playerRepository.GetByName(homegame, playerName);
            if (!_authentication.IsAdmin() && player.UserId != user.Id)
            {
				throw new AccessDeniedException();
			}
            var runningGame = _cashgameRepository.GetRunning(homegame);
            var result = runningGame.GetResult(player.Id);
            var postedCheckpoint = _checkpointModelMapper.GetCheckpoint(postModel, result.CashoutCheckpoint);
			if(ModelState.IsValid){
				if(result.CashoutCheckpoint != null){
                    _checkpointRepository.UpdateCheckpoint(runningGame, postedCheckpoint);
				} else {
                    _checkpointRepository.AddCheckpoint(runningGame, player, postedCheckpoint);
				}
                return Redirect(_urlProvider.GetRunningCashgameUrl(homegame));
			}
            var model = _cashoutPageModelFactory.Create(user, homegame, postModel);
            return View("Cashout/Cashout", model);
		}

        public ActionResult End(string slug){
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
			var user = _authentication.GetUser();
            var homegame = _homegameRepository.GetByName(slug);
            var model = _endPageModelFactory.Create(user, homegame);
			return View("End/End", model);
		}

        [HttpPost]
		public ActionResult End(string slug, EndPageModel postModel){
			_authentication.RequireUser();
            _authorization.RequirePlayer(slug);
            var homegame = _homegameRepository.GetByName(slug);
            var command = _cashgameCommandProvider.GetEndGameCommand(homegame);
            command.Execute();
            return Redirect(_urlProvider.GetCashgameIndexUrl(homegame));
		}

        public ActionResult Delete(string slug, string dateStr){
			_authentication.RequireUser();
            _authorization.RequireManager(slug);
            var homegame = _homegameRepository.GetByName(slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
			_cashgameRepository.DeleteGame(cashgame);
            var date = _timeProvider.Parse(dateStr, homegame.Timezone);
            var listUrl = _urlProvider.GetCashgameListUrl(homegame, date.Year);
            return Redirect(listUrl);
		}

        private ActionResult ShowReportForm(Player player, User user, ReportPageModel model){
            if (!_authentication.IsAdmin() && player.UserId != user.Id)
            {
                throw new AccessDeniedException();
            }
            return View("Report/Report", model);
		}
        
        private ActionResult ShowBuyinForm(User user, Player player, BuyinPageModel model){
			if(!_authentication.IsAdmin() && player.UserId != user.Id){
				throw new AccessDeniedException();
			}

			return View("Buyin/Buyin", model);
		}
        
		private RunningCashgamePageModel GetRunningPageModel(Homegame homegame, Cashgame cashgame, Player player){
            var isManager = _authorization.IsInRole(homegame, Role.Manager);
			return _runningCashgamePageModelFactory.Create(_authentication.GetUser(), homegame, cashgame, player, isManager);
		}

        private ActionResult ShowAddForm(string slug)
        {
            var model = _cashgameModelService.GetAddModel(slug);
			return View("Add/Add", model);
		}

		private Cashgame GetCashgame(AddCashgamePostModel addCashgamePostModel)
		{
			return _cashgameFactory.Create(addCashgamePostModel.Location, (int)GameStatus.Running);
		}

        private ToplistSortOrder GetToplistSortOrder()
        {
            var param = _webContext.GetQueryParam("orderby");
            if (param == null)
            {
                return ToplistSortOrder.winnings;
            }
            ToplistSortOrder sortOrder;
            if (Enum.TryParse(param, out sortOrder))
            {
                return sortOrder;
            }
            return ToplistSortOrder.winnings;
        }

	}
}