﻿using System;
using System.Web;
using Application.Exceptions;
using Application.Services.Interfaces;
using Core.Classes;
using Core.Repositories;
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
using Web.ModelFactories.CashgameModelFactories.List;
using Web.ModelFactories.CashgameModelFactories.Matrix;
using Web.ModelFactories.CashgameModelFactories.Report;
using Web.ModelFactories.CashgameModelFactories.Running;
using Web.ModelFactories.CashgameModelFactories.Toplist;
using Web.Models.CashgameModels.Action;
using Web.Models.CashgameModels.Add;
using Web.Models.CashgameModels.Buyin;
using Web.Models.CashgameModels.Cashout;
using Web.Models.CashgameModels.Chart;
using Web.Models.CashgameModels.Details;
using Web.Models.CashgameModels.Edit;
using Web.Models.CashgameModels.End;
using Web.Models.CashgameModels.Facts;
using Web.Models.CashgameModels.List;
using Web.Models.CashgameModels.Report;
using Web.Models.CashgameModels.Running;
using Web.Models.CashgameModels.Toplist;
using Web.Models.CashgameModels.Matrix;
using Web.Models.ChartModels;

namespace Web.ModelServices
{
    public class CashgameModelService : ICashgameModelService
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly IAuthentication _authentication;
        private readonly IAuthorization _authorization;
        private readonly IMatrixPageModelFactory _matrixPageModelFactory;
        private readonly ICashgameService _cashgameService;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ICashgameToplistPageModelFactory _cashgameToplistPageModelFactory;
        private readonly IUrlProvider _urlProvider;
        private readonly ICashgameDetailsPageModelFactory _cashgameDetailsPageModelFactory;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICashgameDetailsChartModelFactory _cashgameDetailsChartModelFactory;
        private readonly ICashgameFactsPageModelFactory _cashgameFactsPageModelFactory;
        private readonly IAddCashgamePageModelFactory _addCashgamePageModelFactory;
        private readonly ICashgameEditPageModelFactory _cashgameEditPageModelFactory;
        private readonly IWebContext _webContext;
        private readonly IRunningCashgamePageModelFactory _runningCashgamePageModelFactory;
        private readonly ICashgameListPageModelFactory _cashgameListPageModelFactory;
        private readonly ICashgameChartPageModelFactory _cashgameChartPageModelFactory;
        private readonly ICashgameSuiteChartModelFactory _cashgameSuiteChartModelFactory;
        private readonly IActionPageModelFactory _actionPageModelFactory;
        private readonly IActionChartModelFactory _actionChartModelFactory;
        private readonly IBuyinPageModelFactory _buyinPageModelFactory;
        private readonly IReportPageModelFactory _reportPageModelFactory;
        private readonly ICashoutPageModelFactory _cashoutPageModelFactory;
        private readonly IEndPageModelFactory _endPageModelFactory;

        public CashgameModelService(
            IHomegameRepository homegameRepository,
            IAuthentication authentication,
            IAuthorization authorization,
            IMatrixPageModelFactory matrixPageModelFactory,
            ICashgameService cashgameService,
            ICashgameRepository cashgameRepository,
            ICashgameToplistPageModelFactory cashgameToplistPageModelFactory,
            IUrlProvider urlProvider,
            ICashgameDetailsPageModelFactory cashgameDetailsPageModelFactory,
            IPlayerRepository playerRepository,
            ICashgameDetailsChartModelFactory cashgameDetailsChartModelFactory,
            ICashgameFactsPageModelFactory cashgameFactsPageModelFactory,
            IAddCashgamePageModelFactory addCashgamePageModelFactory,
            ICashgameEditPageModelFactory cashgameEditPageModelFactory,
            IWebContext webContext,
            IRunningCashgamePageModelFactory runningCashgamePageModelFactory,
            ICashgameListPageModelFactory cashgameListPageModelFactory,
            ICashgameChartPageModelFactory cashgameChartPageModelFactory,
            ICashgameSuiteChartModelFactory cashgameSuiteChartModelFactory,
            IActionPageModelFactory actionPageModelFactory,
            IActionChartModelFactory actionChartModelFactory,
            IBuyinPageModelFactory buyinPageModelFactory,
            IReportPageModelFactory reportPageModelFactory,
            ICashoutPageModelFactory cashoutPageModelFactory,
            IEndPageModelFactory endPageModelFactory)
        {
            _homegameRepository = homegameRepository;
            _authentication = authentication;
            _authorization = authorization;
            _matrixPageModelFactory = matrixPageModelFactory;
            _cashgameService = cashgameService;
            _cashgameRepository = cashgameRepository;
            _cashgameToplistPageModelFactory = cashgameToplistPageModelFactory;
            _urlProvider = urlProvider;
            _cashgameDetailsPageModelFactory = cashgameDetailsPageModelFactory;
            _playerRepository = playerRepository;
            _cashgameDetailsChartModelFactory = cashgameDetailsChartModelFactory;
            _cashgameFactsPageModelFactory = cashgameFactsPageModelFactory;
            _addCashgamePageModelFactory = addCashgamePageModelFactory;
            _cashgameEditPageModelFactory = cashgameEditPageModelFactory;
            _webContext = webContext;
            _runningCashgamePageModelFactory = runningCashgamePageModelFactory;
            _cashgameListPageModelFactory = cashgameListPageModelFactory;
            _cashgameChartPageModelFactory = cashgameChartPageModelFactory;
            _cashgameSuiteChartModelFactory = cashgameSuiteChartModelFactory;
            _actionPageModelFactory = actionPageModelFactory;
            _actionChartModelFactory = actionChartModelFactory;
            _buyinPageModelFactory = buyinPageModelFactory;
            _reportPageModelFactory = reportPageModelFactory;
            _cashoutPageModelFactory = cashoutPageModelFactory;
            _endPageModelFactory = endPageModelFactory;
        }

        public string GetIndexUrl(string slug)
        {
            var year = _cashgameService.GetLatestYear(slug);
            if (year.HasValue)
            {
                return _urlProvider.GetCashgameMatrixUrl(slug, year);
            }
            return _urlProvider.GetCashgameAddUrl(slug);
        }

        public CashgameMatrixPageModel GetMatrixModel(string slug, int? year = null)
        {
            var homegame = _homegameRepository.GetByName(slug);
            var user = _authentication.GetUser();
            return _matrixPageModelFactory.Create(homegame, user, year);
        }

        public CashgameToplistPageModel GetToplistModel(string slug, int? year = null)
        {
            var user = _authentication.GetUser();
            var homegame = _homegameRepository.GetByName(slug);
            var suite = _cashgameService.GetSuite(homegame, year);
            var years = _cashgameRepository.GetYears(homegame);
            var sortOrder = GetToplistSortOrder();
            return _cashgameToplistPageModelFactory.Create(user, homegame, suite, years, sortOrder, year);
        }

        public CashgameDetailsPageModel GetDetailsModel(string slug, string dateStr)
        {
            var homegame = _homegameRepository.GetByName(slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
            if (cashgame == null)
            {
                throw new HttpException(404, "Cashgame not found");
            }
            var user = _authentication.GetUser();
            var player = _playerRepository.GetByUserName(homegame, user.UserName);
            var isManager = _authorization.IsInRole(homegame, Role.Manager);
            return _cashgameDetailsPageModelFactory.Create(user, homegame, cashgame, player, isManager);
        }

        public ChartModel GetDetailsChartJsonModel(string slug, string dateStr)
        {
            var homegame = _homegameRepository.GetByName(slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
            if (cashgame == null)
            {
                throw new HttpException(404, "Cashgame not found");
            }
            return _cashgameDetailsChartModelFactory.Create(homegame, cashgame);
        }

        public CashgameFactsPageModel GetFactsModel(string slug, int? year = null)
        {
            var user = _authentication.GetUser();
            var homegame = _homegameRepository.GetByName(slug);
            var facts = _cashgameService.GetFacts(homegame, year);
            var runningGame = _cashgameRepository.GetRunning(homegame);
            var years = _cashgameRepository.GetYears(homegame);
            return _cashgameFactsPageModelFactory.Create(user, homegame, facts, years, year, runningGame);
        }

        public AddCashgamePageModel GetAddModel(string slug, AddCashgamePostModel postModel)
        {
            var homegame = _homegameRepository.GetByName(slug);
            var runningGame = _cashgameRepository.GetRunning(homegame);
            if (runningGame != null)
            {
                throw new CashgameRunningException();
            }
            var user = _authentication.GetUser();
            var locations = _cashgameRepository.GetLocations(homegame);
            return _addCashgamePageModelFactory.Create(user, homegame, locations, postModel);
        }

        public CashgameEditPageModel GetEditModel(string slug, string dateStr, CashgameEditPostModel postModel)
        {
            var user = _authentication.GetUser();
            var homegame = _homegameRepository.GetByName(slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
            var locations = _cashgameRepository.GetLocations(homegame);
            return _cashgameEditPageModelFactory.Create(user, homegame, cashgame, locations, postModel);
        }

        public RunningCashgamePageModel GetRunningModel(string slug)
        {
            var user = _authentication.GetUser();
            var homegame = _homegameRepository.GetByName(slug);
            var player = _playerRepository.GetByUserName(homegame, user.UserName);
            var cashgame = _cashgameRepository.GetRunning(homegame);
            var isManager = _authorization.IsInRole(homegame, Role.Manager);
            return _runningCashgamePageModelFactory.Create(user, homegame, cashgame, player, isManager);
        }

        public CashgameListPageModel GetListModel(string slug, int? year)
        {
            var homegame = _homegameRepository.GetByName(slug);
            var games = _cashgameRepository.GetPublished(homegame, year);
            var years = _cashgameRepository.GetYears(homegame);
            var sortOrder = GetListSortOrder();
            return _cashgameListPageModelFactory.Create(_authentication.GetUser(), homegame, games, years, sortOrder, year);
        }

        public CashgameChartPageModel GetChartModel(string slug, int? year)
        {
            var homegame = _homegameRepository.GetByName(slug);
            var years = _cashgameRepository.GetYears(homegame);
            return _cashgameChartPageModelFactory.Create(_authentication.GetUser(), homegame, year, years);
        }

        public ChartModel GetChartJsonModel(string slug, int? year)
        {
            var homegame = _homegameRepository.GetByName(slug);
            var suite = _cashgameService.GetSuite(homegame, year);
            return _cashgameSuiteChartModelFactory.Create(suite);
        }

        public ActionPageModel GetActionModel(string slug, string dateStr, string playerName)
        {
            var homegame = _homegameRepository.GetByName(slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
            var player = _playerRepository.GetByName(homegame, playerName);
            var role = _authorization.GetRole(homegame);
            var result = cashgame.GetResult(player.Id);
            return _actionPageModelFactory.Create(_authentication.GetUser(), homegame, cashgame, player, result, role);
        }

        public ChartModel GetActionChartJsonModel(string slug, string dateStr, string playerName)
        {
            var homegame = _homegameRepository.GetByName(slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
            var player = _playerRepository.GetByName(homegame, playerName);
            var result = cashgame.GetResult(player.Id);
            return _actionChartModelFactory.Create(homegame, cashgame, result);
        }

        public BuyinPageModel GetBuyinModel(string slug, string playerName, BuyinPostModel postModel)
        {
            var user = _authentication.GetUser();
            var homegame = _homegameRepository.GetByName(slug);
            var player = _playerRepository.GetByName(homegame, playerName);
            var runningGame = _cashgameRepository.GetRunning(homegame);
            return _buyinPageModelFactory.Create(user, homegame, player, runningGame, postModel);
        }

        public ReportPageModel GetReportModel(string slug, ReportPostModel postModel)
        {
            var user = _authentication.GetUser();
            var homegame = _homegameRepository.GetByName(slug);
            return _reportPageModelFactory.Create(user, homegame, postModel);
        }

        public CashoutPageModel GetCashoutModel(string slug, CashoutPostModel postModel)
        {
            var user = _authentication.GetUser();
            var homegame = _homegameRepository.GetByName(slug);
            return _cashoutPageModelFactory.Create(user, homegame, postModel);
        }

        public EndPageModel GetEndGameModel(string slug)
        {
            var user = _authentication.GetUser();
            var homegame = _homegameRepository.GetByName(slug);
            return _endPageModelFactory.Create(user, homegame);
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

    }
}