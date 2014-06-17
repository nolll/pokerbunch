using System;
using System.Web;
using Application.Exceptions;
using Application.Services;
using Application.Urls;
using Core.Entities;
using Core.Repositories;
using Core.Services.Interfaces;
using Web.ModelFactories.CashgameModelFactories.Action;
using Web.ModelFactories.CashgameModelFactories.Add;
using Web.ModelFactories.CashgameModelFactories.Buyin;
using Web.ModelFactories.CashgameModelFactories.Cashout;
using Web.ModelFactories.CashgameModelFactories.Chart;
using Web.ModelFactories.CashgameModelFactories.Checkpoints;
using Web.ModelFactories.CashgameModelFactories.Details;
using Web.ModelFactories.CashgameModelFactories.Edit;
using Web.ModelFactories.CashgameModelFactories.End;
using Web.ModelFactories.CashgameModelFactories.List;
using Web.ModelFactories.CashgameModelFactories.Matrix;
using Web.ModelFactories.CashgameModelFactories.Report;
using Web.ModelFactories.CashgameModelFactories.Running;
using Web.Models.CashgameModels.Add;
using Web.Models.CashgameModels.Buyin;
using Web.Models.CashgameModels.Cashout;
using Web.Models.CashgameModels.Chart;
using Web.Models.CashgameModels.Checkpoints;
using Web.Models.CashgameModels.Details;
using Web.Models.CashgameModels.Edit;
using Web.Models.CashgameModels.End;
using Web.Models.CashgameModels.List;
using Web.Models.CashgameModels.Report;
using Web.Models.CashgameModels.Running;
using Web.Models.CashgameModels.Matrix;
using Web.Models.ChartModels;
using Web.Models.UrlModels;

namespace Web.ModelServices
{
    public class CashgameModelService : ICashgameModelService
    {
        public Url GetIndexUrl(string slug)
        {
            var year = _cashgameService.GetLatestYear(slug);
            if (year.HasValue)
            {
                return new CashgameMatrixUrl(slug, year);
            }
            return new AddCashgameUrl(slug);
        }

        public CashgameMatrixPageModel GetMatrixModel(string slug, int? year = null)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            return _matrixPageBuilder.Build(homegame, year);
        }

        public CashgameDetailsPageModel GetDetailsModel(string slug, string dateStr)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
            if (cashgame == null)
            {
                throw new HttpException(404, "Cashgame not found");
            }
            var user = _auth.CurrentUser;
            var player = _playerRepository.GetByUserName(homegame, user.UserName);
            var isManager = _auth.IsInRole(slug, Role.Manager);
            return _cashgameDetailsPageBuilder.Build(homegame, cashgame, player, isManager);
        }

        public ChartModel GetDetailsChartJsonModel(string slug, string dateStr)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
            if (cashgame == null)
            {
                throw new HttpException(404, "Cashgame not found");
            }
            return _cashgameDetailsChartModelFactory.Create(homegame, cashgame);
        }

        public AddCashgamePageModel GetAddModel(string slug, AddCashgamePostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var runningGame = _cashgameRepository.GetRunning(homegame);
            if (runningGame != null)
            {
                throw new CashgameRunningException();
            }
            var locations = _cashgameRepository.GetLocations(homegame);
            return _addCashgamePageBuilder.Build(homegame, locations, postModel);
        }

        public CashgameEditPageModel GetEditModel(string slug, string dateStr, CashgameEditPostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
            var locations = _cashgameRepository.GetLocations(homegame);
            return _cashgameEditPageBuilder.Build(homegame, cashgame, locations, postModel);
        }

        public RunningCashgamePageModel GetRunningModel(string slug)
        {
            var user = _auth.CurrentUser;
            var homegame = _homegameRepository.GetBySlug(slug);
            var player = _playerRepository.GetByUserName(homegame, user.UserName);
            var cashgame = _cashgameRepository.GetRunning(homegame);
            var isManager = _auth.IsInRole(slug, Role.Manager);
            return _runningCashgamePageBuilder.Build(homegame, cashgame, player, isManager);
        }

        public CashgameListPageModel GetListModel(string slug, int? year)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var games = _cashgameRepository.GetPublished(homegame, year);
            var years = _cashgameRepository.GetYears(homegame);
            var sortOrder = GetListSortOrder();
            return _cashgameListPageBuilder.Build(homegame, games, years, sortOrder, year);
        }

        public CashgameChartPageModel GetChartModel(string slug, int? year)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var years = _cashgameRepository.GetYears(homegame);
            return _cashgameChartPageBuilder.Build(homegame, year, years);
        }

        public ChartModel GetChartJsonModel(string slug, int? year)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var suite = _cashgameService.GetSuite(homegame, year);
            return _cashgameSuiteChartModelFactory.Create(suite);
        }

        public ChartModel GetActionChartJsonModel(string slug, string dateStr, int playerId)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
            var result = cashgame.GetResult(playerId);
            return _actionChartModelFactory.Create(homegame, cashgame, result);
        }

        public BuyinPageModel GetBuyinModel(string slug, int playerId, BuyinPostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var player = _playerRepository.GetById(playerId);
            var runningGame = _cashgameRepository.GetRunning(homegame);
            return _buyinPageBuilder.Build(homegame, player, runningGame, postModel);
        }

        public ReportPageModel GetReportModel(string slug, ReportPostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            return _reportPageBuilder.Build(homegame, postModel);
        }

        public CashoutPageModel GetCashoutModel(string slug, CashoutPostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            return _cashoutPageBuilder.Build(homegame, postModel);
        }

        public EndPageModel GetEndGameModel(string slug)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            return _endPageBuilder.Build(homegame);
        }

        public EditCheckpointPageModel GetEditCheckpointModel(string slug, string dateStr, int playerId, int checkpointId, EditCheckpointPostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var checkpoint = _checkpointRepository.GetCheckpoint(checkpointId);
            return _editCheckpointPageBuilder.Build(homegame, checkpoint, dateStr, playerId, postModel);
        }

        private ListSortOrder GetListSortOrder()
        {
            var param = _webContext.GetQueryParam("orderby");
            if (param == null)
            {
                return ListSortOrder.date;
            }
            ListSortOrder sortOrder;
            if (Enum.TryParse(param, out sortOrder))
            {
                return sortOrder;
            }
            return ListSortOrder.date;
        }

        private readonly IHomegameRepository _homegameRepository;
        private readonly IAuth _auth;
        private readonly IMatrixPageBuilder _matrixPageBuilder;
        private readonly ICashgameService _cashgameService;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ICashgameDetailsPageBuilder _cashgameDetailsPageBuilder;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICashgameDetailsChartModelFactory _cashgameDetailsChartModelFactory;
        private readonly IAddCashgamePageBuilder _addCashgamePageBuilder;
        private readonly ICashgameEditPageBuilder _cashgameEditPageBuilder;
        private readonly IWebContext _webContext;
        private readonly IRunningCashgamePageBuilder _runningCashgamePageBuilder;
        private readonly ICashgameListPageBuilder _cashgameListPageBuilder;
        private readonly ICashgameChartPageBuilder _cashgameChartPageBuilder;
        private readonly ICashgameSuiteChartModelFactory _cashgameSuiteChartModelFactory;
        private readonly IActionPageBuilder _actionPageBuilder;
        private readonly IActionChartModelFactory _actionChartModelFactory;
        private readonly IBuyinPageBuilder _buyinPageBuilder;
        private readonly IReportPageBuilder _reportPageBuilder;
        private readonly ICashoutPageBuilder _cashoutPageBuilder;
        private readonly IEndPageBuilder _endPageBuilder;
        private readonly IEditCheckpointPageBuilder _editCheckpointPageBuilder;
        private readonly ICheckpointRepository _checkpointRepository;

        public CashgameModelService(
            IHomegameRepository homegameRepository,
            IAuth auth,
            IMatrixPageBuilder matrixPageBuilder,
            ICashgameService cashgameService,
            ICashgameRepository cashgameRepository,
            ICashgameDetailsPageBuilder cashgameDetailsPageBuilder,
            IPlayerRepository playerRepository,
            ICashgameDetailsChartModelFactory cashgameDetailsChartModelFactory,
            IAddCashgamePageBuilder addCashgamePageBuilder,
            ICashgameEditPageBuilder cashgameEditPageBuilder,
            IWebContext webContext,
            IRunningCashgamePageBuilder runningCashgamePageBuilder,
            ICashgameListPageBuilder cashgameListPageBuilder,
            ICashgameChartPageBuilder cashgameChartPageBuilder,
            ICashgameSuiteChartModelFactory cashgameSuiteChartModelFactory,
            IActionPageBuilder actionPageBuilder,
            IActionChartModelFactory actionChartModelFactory,
            IBuyinPageBuilder buyinPageBuilder,
            IReportPageBuilder reportPageBuilder,
            ICashoutPageBuilder cashoutPageBuilder,
            IEndPageBuilder endPageBuilder,
            IEditCheckpointPageBuilder editCheckpointPageBuilder,
            ICheckpointRepository checkpointRepository)
        {
            _homegameRepository = homegameRepository;
            _auth = auth;
            _matrixPageBuilder = matrixPageBuilder;
            _cashgameService = cashgameService;
            _cashgameRepository = cashgameRepository;
            _cashgameDetailsPageBuilder = cashgameDetailsPageBuilder;
            _playerRepository = playerRepository;
            _cashgameDetailsChartModelFactory = cashgameDetailsChartModelFactory;
            _addCashgamePageBuilder = addCashgamePageBuilder;
            _cashgameEditPageBuilder = cashgameEditPageBuilder;
            _webContext = webContext;
            _runningCashgamePageBuilder = runningCashgamePageBuilder;
            _cashgameListPageBuilder = cashgameListPageBuilder;
            _cashgameChartPageBuilder = cashgameChartPageBuilder;
            _cashgameSuiteChartModelFactory = cashgameSuiteChartModelFactory;
            _actionPageBuilder = actionPageBuilder;
            _actionChartModelFactory = actionChartModelFactory;
            _buyinPageBuilder = buyinPageBuilder;
            _reportPageBuilder = reportPageBuilder;
            _cashoutPageBuilder = cashoutPageBuilder;
            _endPageBuilder = endPageBuilder;
            _editCheckpointPageBuilder = editCheckpointPageBuilder;
            _checkpointRepository = checkpointRepository;
        }
    }
}