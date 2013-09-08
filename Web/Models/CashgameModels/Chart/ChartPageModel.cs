using System.Collections.Generic;
using Core.Classes;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;
using app;

namespace Web.Models.CashgameModels.Chart{
    public class ChartPageModel : HomegamePageModel {

		public CashgameNavigationModel CashgameNavModel { get; set; }
		public CashgameChartJsonUrlModel ChartDataUrl { get; set; }

		public ChartPageModel(User user, Homegame homegame, int? year, IList<int> years, Core.Classes.Cashgame runningGame)
            : base(user, homegame, runningGame)
        {
			ChartDataUrl = new CashgameChartJsonUrlModel(homegame, year);
			CashgameNavModel = new CashgameNavigationModel(homegame, "chart", years, year, runningGame);
		}

        public override string BrowserTitle
        {
            get
            {
                return "Cashgame Chart";
            }
        }

	}

}