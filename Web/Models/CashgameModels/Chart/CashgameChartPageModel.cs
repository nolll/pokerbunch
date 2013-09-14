using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.CashgameModels.Chart
{
    public class CashgameChartPageModel : IPageModel
    {
        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
		public CashgameNavigationModel CashgameNavModel { get; set; }
		public CashgameChartJsonUrlModel ChartDataUrl { get; set; }
	}

}