using System.Web;
using Core.Classes;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Web.ModelFactories.CashgameModelFactories.Add;
using Web.ModelFactories.CashgameModelFactories.Details;
using Web.ModelFactories.CashgameModelFactories.Edit;
using Web.ModelFactories.CashgameModelFactories.Facts;
using Web.ModelFactories.CashgameModelFactories.Matrix;
using Web.ModelFactories.CashgameModelFactories.Toplist;
using Web.Models.CashgameModels.Add;
using Web.Models.CashgameModels.Details;
using Web.Models.CashgameModels.Edit;
using Web.Models.CashgameModels.Facts;
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
            ICashgameEditPageModelFactory cashgameEditPageModelFactory)
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
        }

        public string GetIndexUrl(string gameName)
        {
            _authentication.RequireUser();
            _authorization.RequirePlayer(gameName);
            var homegame = _homegameRepository.GetByName(gameName);
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
            _authentication.RequireUser();
            _authorization.RequirePlayer(gameName);
            var homegame = _homegameRepository.GetByName(gameName);
            return _matrixPageModelFactory.Create(homegame, _authentication.GetUser(), year);
        }

        public CashgameToplistPageModel GetToplistModel(string gameName, ToplistSortOrder sortOrder, int? year = null)
        {
            _authentication.RequireUser();
            _authorization.RequirePlayer(gameName);
            var homegame = _homegameRepository.GetByName(gameName);
            var suite = _cashgameService.GetSuite(homegame, year);
            var years = _cashgameRepository.GetYears(homegame);
            return _cashgameToplistPageModelFactory.Create(_authentication.GetUser(), homegame, suite, years, sortOrder, year);
        }

        public CashgameDetailsPageModel GetDetailsModel(string gameName, string dateStr)
        {
            _authentication.RequireUser();
            _authorization.RequirePlayer(gameName);
            var homegame = _homegameRepository.GetByName(gameName);
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

        public ChartModel GetDetailsChartJsonModel(string gameName, string dateStr)
        {
            _authentication.RequireUser();
            _authorization.RequirePlayer(gameName);
            var homegame = _homegameRepository.GetByName(gameName);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
            if (cashgame == null)
            {
                throw new HttpException(404, "Cashgame not found");
            }
            return _cashgameDetailsChartModelFactory.Create(homegame, cashgame);
        }

        public CashgameFactsPageModel GetFactsModel(string gameName, int? year = null)
        {
            _authentication.RequireUser();
            _authorization.RequirePlayer(gameName);
            var homegame = _homegameRepository.GetByName(gameName);
            var facts = _cashgameService.GetFacts(homegame, year);
            var runningGame = _cashgameRepository.GetRunning(homegame);
            var years = _cashgameRepository.GetYears(homegame);
            return _cashgameFactsPageModelFactory.Create(_authentication.GetUser(), homegame, facts, years, year, runningGame);
        }

        public AddCashgamePageModel GetAddModel(string gameName)
        {
            _authentication.RequireUser();
            _authorization.RequirePlayer(gameName);
            var homegame = _homegameRepository.GetByName(gameName);
            var runningGame = _cashgameRepository.GetRunning(homegame);
            if (runningGame != null)
            {
                throw new CashgameRunningException();
            }
            var locations = _cashgameRepository.GetLocations(homegame);
            return _addCashgamePageModelFactory.Create(_authentication.GetUser(), homegame, locations);
        }

        public CashgameEditPageModel GetEditModel(string gameName, string dateStr)
        {
            _authentication.RequireUser();
            _authorization.RequireManager(gameName);
            var homegame = _homegameRepository.GetByName(gameName);
            var cashgame = _cashgameRepository.GetByDateString(homegame, dateStr);
            var locations = _cashgameRepository.GetLocations(homegame);
            return _cashgameEditPageModelFactory.Create(_authentication.GetUser(), homegame, cashgame, locations);
        }

    }
}