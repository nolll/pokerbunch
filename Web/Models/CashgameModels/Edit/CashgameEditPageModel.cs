using System.Collections.Generic;
using System.Web.Mvc;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Edit{

	public class CashgameEditPageModel : CashgameEditPostModel, IPageModel {

	    public string BrowserTitle { get; set; }
	    public PageProperties PageProperties { get; set; }
		public string IsoDate { get; set; }
		public bool EnableDelete { get; set; }
		public string CancelUrl { get; set; }
		public string DeleteUrl { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }
	}

}