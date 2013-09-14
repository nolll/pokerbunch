using System.Collections.Generic;
using Core.Classes;
using Web.Models.CashgameModels.Chart;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.ModelFactories.CashgameModelFactories
{
    public class CashgameChartPageModelFactory : ICashgameChartPageModelFactory
    {
        public CashgameChartPageModel Create(User user, Homegame homegame, int? year, IList<int> years, Cashgame runningGame)
        {
            return new CashgameChartPageModel
                {
                    BrowserTitle = "Cashgame Chart",
                    PageProperties = new PageProperties(user, homegame, runningGame),
			        ChartDataUrl = new CashgameChartJsonUrlModel(homegame, year),
			        CashgameNavModel = new CashgameNavigationModel(homegame, "chart", years, year, runningGame)
                };
        }
    }
}