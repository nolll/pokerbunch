using Application.Urls;
using Core.Repositories;
using Core.Services.Interfaces;
using Web.ModelFactories.CashgameModelFactories.Running;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Matrix;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.CashgameModelFactories.Matrix
{
    public class MatrixPageBuilder : IMatrixPageBuilder
    {
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ICashgameService _cashgameService;
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly ICashgameMatrixTableModelFactory _cashgameMatrixTableModelFactory;
        private readonly IBarModelFactory _barModelFactory;
        private readonly ICashgamePageNavigationModelFactory _cashgamePageNavigationModelFactory;
        private readonly ICashgameYearNavigationModelFactory _cashgameYearNavigationModelFactory;
        private readonly IHomegameRepository _homegameRepository;

        public MatrixPageBuilder(
            ICashgameRepository cashgameRepository,
            ICashgameService cashgameService,
            IPagePropertiesFactory pagePropertiesFactory,
            ICashgameMatrixTableModelFactory cashgameMatrixTableModelFactory,
            IBarModelFactory barModelFactory,
            ICashgamePageNavigationModelFactory cashgamePageNavigationModelFactory,
            ICashgameYearNavigationModelFactory cashgameYearNavigationModelFactory,
            IHomegameRepository homegameRepository)
        {
            _cashgameRepository = cashgameRepository;
            _cashgameService = cashgameService;
            _pagePropertiesFactory = pagePropertiesFactory;
            _cashgameMatrixTableModelFactory = cashgameMatrixTableModelFactory;
            _barModelFactory = barModelFactory;
            _cashgamePageNavigationModelFactory = cashgamePageNavigationModelFactory;
            _cashgameYearNavigationModelFactory = cashgameYearNavigationModelFactory;
            _homegameRepository = homegameRepository;
        }

        public CashgameMatrixPageModel Build(string slug, int? year)
        {
            var homegame = _homegameRepository.GetBySlug(slug);

            var suite = _cashgameService.GetSuite(homegame, year);
            var runningGame = _cashgameRepository.GetRunning(homegame);
            var years = _cashgameRepository.GetYears(homegame);
            var gameIsRunning = runningGame != null;
            var startGameUrl = GetStartGameUrl(homegame.Slug, gameIsRunning);

            return new CashgameMatrixPageModel
                {
                    BrowserTitle = "Cashgame Matrix",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
                    TableModel = _cashgameMatrixTableModelFactory.Create(homegame, suite),
                    PageNavModel = _cashgamePageNavigationModelFactory.Create(homegame.Slug, CashgamePage.Matrix),
                    YearNavModel = _cashgameYearNavigationModelFactory.Create(homegame.Slug, years, CashgamePage.Matrix, year),
                    BarModel = _barModelFactory.Create(homegame, runningGame),
                    GameIsRunning = gameIsRunning,
                    StartGameUrl = startGameUrl
                };
        }

        private Url GetStartGameUrl(string slug, bool gameIsRunning)
        {
            if (gameIsRunning)
                return new EmptyUrl();
            return new AddCashgameUrl(slug);
        }
    }
}