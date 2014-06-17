using Application.Urls;
using Core.Repositories;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Chart;
using Web.Models.NavigationModels;

namespace Web.ModelFactories.CashgameModelFactories.Chart
{
    public class CashgameChartPageBuilder : ICashgameChartPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly ICashgamePageNavigationModelFactory _cashgamePageNavigationModelFactory;
        private readonly ICashgameYearNavigationModelFactory _cashgameYearNavigationModelFactory;
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public CashgameChartPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            ICashgamePageNavigationModelFactory cashgamePageNavigationModelFactory,
            ICashgameYearNavigationModelFactory cashgameYearNavigationModelFactory,
            IHomegameRepository homegameRepository,
            ICashgameRepository cashgameRepository)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _cashgamePageNavigationModelFactory = cashgamePageNavigationModelFactory;
            _cashgameYearNavigationModelFactory = cashgameYearNavigationModelFactory;
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
        }

        public CashgameChartPageModel Build(string slug, int? year)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var years = _cashgameRepository.GetYears(homegame);
            
            return new CashgameChartPageModel
                {
                    BrowserTitle = "Cashgame Chart",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
			        ChartDataUrl = new CashgameChartJsonUrl(homegame.Slug, year),
                    PageNavModel = _cashgamePageNavigationModelFactory.Create(homegame.Slug, CashgamePage.Chart),
                    YearNavModel = _cashgameYearNavigationModelFactory.Create(homegame.Slug, years, CashgamePage.Chart, year)
                };
        }
    }
}