using Web.Models.CashgameModels.Buyin;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.CashgameModels.Report{

    public class ReportPageModel : ReportPostModel, IPageModel {

        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
		public UrlModel ReportUrl { get; set; }
		
	}

}