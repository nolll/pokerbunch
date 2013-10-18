using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.CashgameModels.Details{

	public class CashgameDetailsPageModel : IPageModel {

	    public string BrowserTitle { get; set; }
	    public PageProperties PageProperties { get; set; }
	    public string Heading { get; set; }
	    public string Duration { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public string Location { get; set; }

        public bool ShowStartTime { get; set; }
        public bool ShowEndTime { get; set; }

        public bool EnableEdit { get; set; }
        public bool EnableCheckpointsButton { get; set; }
        public bool DurationEnabled { get; set; }

        public UrlModel EditUrl { get; set; }
        public string CheckpointsUrl { get; set; }
        public UrlModel ChartDataUrl { get; set; }

        public string Status { get; set; }

        public CashgameDetailsTableModel CashgameDetailsTableModel { get; set; }
	}

}