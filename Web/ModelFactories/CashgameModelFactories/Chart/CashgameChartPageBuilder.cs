using Application.Urls;
using Application.UseCases.CashgameContext;
using Core.Repositories;
using Web.Models.CashgameModels.Chart;

namespace Web.ModelFactories.CashgameModelFactories.Chart
{
    public class CashgameChartPageBuilder : ICashgameChartPageBuilder
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameContextInteractor _cashgameContextInteractor;

        public CashgameChartPageBuilder(
            IBunchRepository bunchRepository,
            ICashgameContextInteractor cashgameContextInteractor)
        {
            _bunchRepository = bunchRepository;
            _cashgameContextInteractor = cashgameContextInteractor;
        }

        public CashgameChartPageModel Build(string slug, int? year)
        {
            var homegame = _bunchRepository.GetBySlug(slug);

            var cashgameContextResult = _cashgameContextInteractor.Execute(new CashgameContextRequest(slug, year, CashgamePage.Chart));

            return new CashgameChartPageModel(cashgameContextResult)
                {
			        ChartDataUrl = new CashgameChartJsonUrl(homegame.Slug, year),
                };
        }
    }
}