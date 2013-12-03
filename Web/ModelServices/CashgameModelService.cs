using System.Web;
using Core.Classes;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Web.ModelFactories.CashgameModelFactories.Add;
using Web.ModelFactories.CashgameModelFactories.Details;
using Web.ModelFactories.CashgameModelFactories.Edit;
using Web.ModelFactories.CashgameModelFactories.Facts;
using Web.ModelFactories.CashgameModelFactories.Leaderboard;
using Web.ModelFactories.CashgameModelFactories.Matrix;
using Web.Models.CashgameModels.Add;
using Web.Models.CashgameModels.Details;
using Web.Models.CashgameModels.Edit;
using Web.Models.CashgameModels.Facts;
using Web.Models.CashgameModels.Leaderboard;
using Web.Models.CashgameModels.Matrix;
using Web.Models.ChartModels;

namespace Web.ModelServices
{
    public class CashgameModelService : ICashgameModelService
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly IUserContext _userContext;
        private readonly IMatrixPageModelFactory _matrixPageModelFactory;
        private readonly ICashgameService _cashgameService;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ICashgameLeaderboardPageModelFactory _cashgameLeaderboardPageModelFactory;
        private readonly IUrlProvider _urlProvider;
        private readonly ICashgameDetailsPageModelFactory _cashgameDetailsPageModelFactory;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICashgameDetailsChartModelFactory _cashgameDetailsChartModelFactory;
        private readonly ICashgameFactsPageModelFactory _cashgameFactsPageModelFactory;
        private readonly IAddCashgamePageModelFactory _addCashgamePageModelFactory;
        private readonly ICashgameEditPageModelFactory _cashgameEditPageModelFactory;

        public CashgameModelService(
            IHomegameRepository homegameRepository,
            IUserContext userContext,
            IMatrixPageModelFactory matrixPageModelFactory,
            ICashgameService cashgameService,
            ICashgameRepository cashgameRepository,
            ICashgameLeaderboardPageModelFactory cashgameLeaderboardPageModelFactory,
            IUrlProvider urlProvider,
            ICashgameDetailsPageModelFactory cashgameDetailsPageModelFactory,
            IPlayerRepository playerRepository,
            ICashgameDetailsChartModelFactory cashgameDetailsChartModelFactory,
            ICashgameFactsPageModelFactory cashgameFactsPageModelFactory,
            IAddCashgamePageModelFactory addCashgamePageModelFactory,
            ICashgameEditPageModelFactory cashgameEditPageModelFactory)
        {
            _homegameRepository = homegameRepository;
            _userContext = userContext;
            _matrixPageModelFactory = matrixPageModelFactory;
            _cashgameService = cashgameService;
            _cashgameRepository = cashgameRepository;
            _cashgameLeaderboardPageModelFactory = cashgameLeaderboardPageModelFactory;
            _urlProvider = urlProvider;
            _cashgameDetailsPageModelFactory = cashgameDetailsPageModelFactory;
            _playerRepository = playerRepository;
            _cashgameDetailsChartModelFactory = cashgameDetailsChartModelFactory;
            _cashgameFactsPageModelFactory = cashgameFactsPageModelFactory;
            _addCashgamePageModelFactory = addCashgamePageModelFactory;
            _cashgameEditPageModelFactory = cashgameEditPageModelFactory;
        }

        public string GetIndexUrl(string gameName)
        {
            var homegame = _homegameRepository.GetByName(gameName);
            _userContext.RequirePlayer(homegame);
            var years = _cashgameRepository.GetYears(homegame);
            if (years.Count > 0)
            {
                var year = years[0];
                return _urlProvider.GetCashgameMatrixUrl(homegame, year);
            }
            return _urlProvider.GetCashgameAddUrl(homegame);
        }

        public CashgameMatrixPageModel GetMatrixModel(string gameName, int? year = null)
        {
            var homegame = _homegameRepository.GetByName(gameName);
            _userContext.RequirePlayer(homegame);
            return _matrixPageModelFactory.Create(homegame, _userContext.GetUser(), year);
        }

        public CashgameLeaderboardPageModel GetLeaderboardModel(string gameName, LeaderboardSortOrder sortOrder, int? year = null)
        {
            var homegame = _homegameRepository.GetByName(gameName);
            _userContext.RequirePlayer(homegame);
            var suite = _cashgameService.GetSuite(homegame, year);
            var years = _cashgameRepository.GetYears(homegame);
            return _cashgameLeaderboardPageModelFactory.Create(_userContext.GetUser(), homegame, suite, years, sortOrder, year);
        }

        public CashgameDetailsPageModel GetDetailsModel(string gameName, string dateStr)
        {
            var homegame = _homegameRepository.GetByName(gameName);
            _userContext.RequirePlayer(homegame);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
            if (cashgame == null)
            {
                throw new HttpException(404, "Cashgame not found");
            }
            var user = _userContext.GetUser();
            var player = _playerRepository.GetByUserName(homegame, user.UserName);
            var isManager = _userContext.IsInRole(homegame, Role.Manager);
            return _cashgameDetailsPageModelFactory.Create(user, homegame, cashgame, player, isManager);
        }

        public ChartModel GetDetailsChartJsonModel(string gameName, string dateStr)
        {
            var homegame = _homegameRepository.GetByName(gameName);
            _userContext.RequirePlayer(homegame);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
            if (cashgame == null)
            {
                throw new HttpException(404, "Cashgame not found");
            }
            return _cashgameDetailsChartModelFactory.Create(homegame, cashgame);
        }

        public CashgameFactsPageModel GetFactsModel(string gameName, int? year = null)
        {
            var homegame = _homegameRepository.GetByName(gameName);
            _userContext.RequirePlayer(homegame);
            var facts = _cashgameService.GetFacts(homegame, year);
            var runningGame = _cashgameRepository.GetRunning(homegame);
            var years = _cashgameRepository.GetYears(homegame);
            return _cashgameFactsPageModelFactory.Create(_userContext.GetUser(), homegame, facts, years, year, runningGame);
        }

        public AddCashgamePageModel GetAddModel(string gameName)
        {
            var homegame = _homegameRepository.GetByName(gameName);
            _userContext.RequirePlayer(homegame);
            var runningGame = _cashgameRepository.GetRunning(homegame);
            if (runningGame != null)
            {
                throw new CashgameRunningException();
            }
            var locations = _cashgameRepository.GetLocations(homegame);
            return _addCashgamePageModelFactory.Create(_userContext.GetUser(), homegame, locations);
        }

        public CashgameEditPageModel GetEditModel(string gameName, string dateStr)
        {
            var homegame = _homegameRepository.GetByName(gameName);
            _userContext.RequireManager(homegame);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
            var locations = _cashgameRepository.GetLocations(homegame);
            return _cashgameEditPageModelFactory.Create(_userContext.GetUser(), homegame, cashgame, locations);
        }

    }
}