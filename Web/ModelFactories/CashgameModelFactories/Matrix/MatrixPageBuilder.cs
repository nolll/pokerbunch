using Application.UseCases.CashgameContext;
using Core.Repositories;
using Core.Services.Interfaces;
using Web.Models.CashgameModels.Matrix;

namespace Web.ModelFactories.CashgameModelFactories.Matrix
{
    public class MatrixPageBuilder : IMatrixPageBuilder
    {
        private readonly ICashgameService _cashgameService;
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameContextInteractor _contextInteractor;

        public MatrixPageBuilder(
            ICashgameService cashgameService,
            IHomegameRepository homegameRepository,
            ICashgameContextInteractor contextInteractor)
        {
            _cashgameService = cashgameService;
            _homegameRepository = homegameRepository;
            _contextInteractor = contextInteractor;
        }

        public CashgameMatrixPageModel Build(string slug, int? year)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var suite = _cashgameService.GetSuite(homegame, year);

            var contextResult = _contextInteractor.Execute(new CashgameContextRequest(slug, year, CashgamePage.Matrix));

            return new CashgameMatrixPageModel(contextResult)
                {
                    TableModel = CashgameMatrixTableModelFactory.Create(homegame, suite)
                };
        }
    }
}