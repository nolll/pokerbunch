using System.Collections.Generic;
using Core.Classes;
using Core.Services;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Chart;
using Web.Models.NavigationModels;
using Web.Models.UrlModels;

namespace Web.ModelFactories.CashgameModelFactories
{
    public class CashgameChartPageModelFactory : ICashgameChartPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IUrlProvider _urlProvider;

        public CashgameChartPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            IUrlProvider urlProvider)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _urlProvider = urlProvider;
        }

        public CashgameChartPageModel Create(User user, Homegame homegame, int? year, IList<int> years, Cashgame runningGame)
        {
            return new CashgameChartPageModel
                {
                    BrowserTitle = "Cashgame Chart",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame, runningGame),
			        ChartDataUrl = _urlProvider.GetCashgameChartJsonUrl(homegame, year),
			        CashgameNavModel = new CashgameNavigationModel(homegame, "chart", years, year, runningGame)
                };
        }
    }
}