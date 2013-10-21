using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Chart
{
    public class CashgameChartPageModel : IPageModel
    {
        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
		public CashgameNavigationModel CashgameNavModel { get; set; }
		public string ChartDataUrl { get; set; }
	}

}