using Application.UseCases.CashgameContext;
using Core.Repositories;
using Core.Services.Interfaces;
using Plumbing;
using Web.Models.CashgameModels.Matrix;

namespace Web.ModelFactories.CashgameModelFactories.Matrix
{
    public class MatrixPageBuilder : IMatrixPageBuilder
    {
        private readonly ICashgameService _cashgameService;
        private readonly IBunchRepository _bunchRepository;

        public MatrixPageBuilder(
            ICashgameService cashgameService,
            IBunchRepository bunchRepository)
        {
            _cashgameService = cashgameService;
            _bunchRepository = bunchRepository;
        }

        public CashgameMatrixPageModel Build(string slug, int? year)
        {
            var homegame = _bunchRepository.GetBySlug(slug);
            var suite = _cashgameService.GetSuite(homegame, year);

            var contextResult = DependencyContainer.Instance.CashgameContext(new CashgameContextRequest(slug, year, CashgamePage.Matrix));

            return new CashgameMatrixPageModel(contextResult)
                {
                    TableModel = CashgameMatrixTableModelFactory.Create(homegame, suite)
                };
        }
    }
}