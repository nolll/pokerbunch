using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Chart
{
    public class CashgameChartPageModel : IPageModel
    {
        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
		public string ChartDataUrl { get; set; }
        public CashgamePageNavigationModel PageNavModel { get; set; }
        public CashgameYearNavigationModel YearNavModel { get; set; }
	}
}