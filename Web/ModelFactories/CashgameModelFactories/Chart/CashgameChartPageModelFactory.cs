using System.Collections.Generic;
using Application.Services;
using Core.Entities;
using Web.ModelFactories.NavigationModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Chart;
using Web.Models.NavigationModels;
using Web.Models.UrlModels;
using Web.Services;

namespace Web.ModelFactories.CashgameModelFactories.Chart
{
    public class CashgameChartPageModelFactory : ICashgameChartPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IUrlProvider _urlProvider;
        private readonly ICashgamePageNavigationModelFactory _cashgamePageNavigationModelFactory;
        private readonly ICashgameYearNavigationModelFactory _cashgameYearNavigationModelFactory;

        public CashgameChartPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            IUrlProvider urlProvider,
            ICashgamePageNavigationModelFactory cashgamePageNavigationModelFactory,
            ICashgameYearNavigationModelFactory cashgameYearNavigationModelFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _urlProvider = urlProvider;
            _cashgamePageNavigationModelFactory = cashgamePageNavigationModelFactory;
            _cashgameYearNavigationModelFactory = cashgameYearNavigationModelFactory;
        }

        public CashgameChartPageModel Create(Homegame homegame, int? year, IList<int> years)
        {
            return new CashgameChartPageModel
                {
                    BrowserTitle = "Cashgame Chart",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
			        ChartDataUrl = new CashgameChartJsonUrlModel(homegame.Slug, year),
                    PageNavModel = _cashgamePageNavigationModelFactory.Create(homegame.Slug, CashgamePage.Chart),
                    YearNavModel = _cashgameYearNavigationModelFactory.Create(homegame.Slug, years, CashgamePage.Chart, year)
                };
        }
    }
}