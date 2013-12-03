using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Toplist{

	public class CashgameToplistPageModel : IPageModel {

        public string BrowserTitle { get; set; }
	    public PageProperties PageProperties { get; set; }
	    public CashgameToplistTableModel TableModel { get; set; }
	    public CashgameNavigationModel CashgameNavModel { get; set; }
	}

}