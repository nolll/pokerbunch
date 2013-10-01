using System.Collections.Generic;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.CashgameModels.Action{

    public class ActionPageModel : IPageModel {

        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
        public List<CheckpointModel> Checkpoints { get; set; }
		public UrlModel ChartDataUrl { get; set; }
		public string Heading { get; set; }
	}

}