using Application.Urls;
using Application.UseCases.CashgameContext;
using Core.Repositories;
using Plumbing;
using Web.Models.CashgameModels.Chart;

namespace Web.ModelFactories.CashgameModelFactories.Chart
{
    public class CashgameChartPageBuilder : ICashgameChartPageBuilder
    {
        private readonly IBunchRepository _bunchRepository;

        public CashgameChartPageBuilder(
            IBunchRepository bunchRepository)
        {
            _bunchRepository = bunchRepository;
        }

        public CashgameChartPageModel Build(string slug, int? year)
        {
            var homegame = _bunchRepository.GetBySlug(slug);

            var cashgameContextResult = DependencyContainer.Instance.CashgameContext(new CashgameContextRequest(slug, year, CashgamePage.Chart));

            return new CashgameChartPageModel(cashgameContextResult)
                {
			        ChartDataUrl = new CashgameChartJsonUrl(homegame.Slug, year),
                };
        }
    }
}