using Application.Urls;
using Application.UseCases.CashgameContext;
using Core.Repositories;
using Core.Services.Interfaces;
using Web.Models.CashgameModels.Matrix;

namespace Web.ModelFactories.CashgameModelFactories.Matrix
{
    public class MatrixPageBuilder : IMatrixPageBuilder
    {
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ICashgameService _cashgameService;
        private readonly ICashgameMatrixTableModelFactory _cashgameMatrixTableModelFactory;
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameContextInteractor _contextInteractor;

        public MatrixPageBuilder(
            ICashgameRepository cashgameRepository,
            ICashgameService cashgameService,
            ICashgameMatrixTableModelFactory cashgameMatrixTableModelFactory,
            IHomegameRepository homegameRepository,
            ICashgameContextInteractor contextInteractor)
        {
            _cashgameRepository = cashgameRepository;
            _cashgameService = cashgameService;
            _cashgameMatrixTableModelFactory = cashgameMatrixTableModelFactory;
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

            var contextResult = _contextInteractor.Execute(new CashgameContextRequest(slug, year, CashgamePage.Matrix));

            return new CashgameMatrixPageModel(contextResult)
                {
                    TableModel = _cashgameMatrixTableModelFactory.Create(homegame, suite),
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