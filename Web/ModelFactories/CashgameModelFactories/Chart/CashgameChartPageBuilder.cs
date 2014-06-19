using Application.Urls;
using Application.UseCases.BunchContext;
using Core.Repositories;
using Web.ModelFactories.NavigationModelFactories;
using Web.Models.CashgameModels.Chart;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.ModelFactories.CashgameModelFactories.Chart
{
    public class CashgameChartPageBuilder : ICashgameChartPageBuilder
    {
        private readonly ICashgamePageNavigationModelFactory _cashgamePageNavigationModelFactory;
        private readonly ICashgameYearNavigationModelFactory _cashgameYearNavigationModelFactory;
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IBunchContextInteractor _bunchContextInteractor;

        public CashgameChartPageBuilder(
            ICashgamePageNavigationModelFactory cashgamePageNavigationModelFactory,
            ICashgameYearNavigationModelFactory cashgameYearNavigationModelFactory,
            IHomegameRepository homegameRepository,
            ICashgameRepository cashgameRepository,
            IBunchContextInteractor bunchContextInteractor)
        {
            _cashgamePageNavigationModelFactory = cashgamePageNavigationModelFactory;
            _cashgameYearNavigationModelFactory = cashgameYearNavigationModelFactory;
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
            _bunchContextInteractor = bunchContextInteractor;
        }

        public CashgameChartPageModel Build(string slug, int? year)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var years = _cashgameRepository.GetYears(homegame);

            var contextResult = _bunchContextInteractor.Execute(new BunchContextRequest{Slug = slug});

            return new CashgameChartPageModel
                {
                    BrowserTitle = "Cashgame Chart",
                    PageProperties = new PageProperties(contextResult),
			        ChartDataUrl = new CashgameChartJsonUrl(homegame.Slug, year),
                    PageNavModel = _cashgamePageNavigationModelFactory.Create(homegame.Slug, CashgamePage.Chart),
                    YearNavModel = _cashgameYearNavigationModelFactory.Create(homegame.Slug, years, CashgamePage.Chart, year)
                };
        }
    }
}