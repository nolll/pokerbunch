using System.Collections.Generic;
using Core.Classes;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.CashgameModels.Chart
{
    public class CashgameChartPageModel : IPageModel {

        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
		public CashgameNavigationModel CashgameNavModel { get; set; }
		public CashgameChartJsonUrlModel ChartDataUrl { get; set; }

		public CashgameChartPageModel(User user, Homegame homegame, int? year, IList<int> years, Cashgame runningGame)
		{
		    BrowserTitle = "Cashgame Chart";
            PageProperties = new PageProperties(user, homegame, runningGame);
			ChartDataUrl = new CashgameChartJsonUrlModel(homegame, year);
			CashgameNavModel = new CashgameNavigationModel(homegame, "chart", years, year, runningGame);
		}

	}

}