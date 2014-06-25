using Application.Urls;
using Application.UseCases.CashgameContext;
using Core.Repositories;
using Core.Services.Interfaces;
using Web.ModelFactories.CashgameModelFactories.Running;
using Web.Models.CashgameModels.Matrix;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.CashgameModelFactories.Matrix
{
    public class MatrixPageBuilder : IMatrixPageBuilder
    {
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ICashgameService _cashgameService;
        private readonly ICashgameMatrixTableModelFactory _cashgameMatrixTableModelFactory;
        private readonly IBarModelFactory _barModelFactory;
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameContextInteractor _contextInteractor;

        public MatrixPageBuilder(
            ICashgameRepository cashgameRepository,
            ICashgameService cashgameService,
            ICashgameMatrixTableModelFactory cashgameMatrixTableModelFactory,
            IBarModelFactory barModelFactory,
            IHomegameRepository homegameRepository,
            ICashgameContextInteractor contextInteractor)
        {
            _cashgameRepository = cashgameRepository;
            _cashgameService = cashgameService;
            _cashgameMatrixTableModelFactory = cashgameMatrixTableModelFactory;
            _barModelFactory = barModelFactory;
            _homegameRepository = homegameRepository;
            _contextInteractor = contextInteractor;
        }

        public CashgameMatrixPageModel Build(string slug, int? year)
        {
            var homegame = _homegameRepository.GetBySlug(slug);

            var suite = _cashgameService.GetSuite(homegame, year);
            var runningGame = _cashgameRepository.GetRunning(homegame);
            var gameIsRunning = runningGame != null;
            var startGameUrl = GetStartGameUrl(homegame.Slug, gameIsRunning);

            var contextResult = _contextInteractor.Execute(new CashgameContextRequest(slug, year));

            return new CashgameMatrixPageModel(contextResult, CashgamePage.Matrix)
                {
                    TableModel = _cashgameMatrixTableModelFactory.Create(homegame, suite),
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