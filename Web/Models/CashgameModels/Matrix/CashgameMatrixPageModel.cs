using Web.Models.CashgameModels.Running;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Matrix{
    public class CashgameMatrixPageModel : IPageModel {

        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
        public BarModel BarModel { get; set; }
	    public CashgameMatrixTableModel TableModel { get; set; }
        public CashgamePageNavigationModel PageNavModel { get; set; }
        public CashgameYearNavigationModel YearNavModel { get; set; }
	}

}