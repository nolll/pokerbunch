using System.Collections.Generic;
using Core.Classes;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Chart;
using Web.Models.NavigationModels;
using Web.Models.UrlModels;

namespace Web.ModelFactories.CashgameModelFactories
{
    public class CashgameChartPageModelFactory : ICashgameChartPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public CashgameChartPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public CashgameChartPageModel Create(User user, Homegame homegame, int? year, IList<int> years, Cashgame runningGame)
        {
            return new CashgameChartPageModel
                {
                    BrowserTitle = "Cashgame Chart",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame, runningGame),
			        ChartDataUrl = new CashgameChartJsonUrlModel(homegame, year),
			        CashgameNavModel = new CashgameNavigationModel(homegame, "chart", years, year, runningGame)
                };
        }
    }
}