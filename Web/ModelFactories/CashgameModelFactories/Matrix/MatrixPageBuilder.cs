using Core.Repositories;
using Core.Services.Interfaces;
using Core.UseCases.CashgameContext;
using Core.UseCases.Matrix;
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
            var bunch = _bunchRepository.GetBySlug(slug);
            var suite = _cashgameService.GetSuite(bunch, year);

            var contextResult = UseCaseContainer.Instance.CashgameContext(new CashgameContextRequest(slug, year, CashgamePage.Matrix));
            var matrixResult = UseCaseContainer.Instance.Matrix(new MatrixRequest(slug, year));

            return new CashgameMatrixPageModel(contextResult, matrixResult, bunch, suite);
        }
    }
}