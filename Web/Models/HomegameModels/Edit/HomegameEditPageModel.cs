using System.Collections.Generic;
using System.Web.Mvc;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Edit{

	public class HomegameEditPageModel : HomegameEditPostModel, IPageModel
    {
	    public string BrowserTitle { get; set; }
	    public PageProperties PageProperties { get; set; }
		public string CancelUrl { get; set; }
		public string Heading { get; set; }
	    public List<SelectListItem> CurrencyLayoutSelectItems { get; set; }
		public List<SelectListItem> TimezoneSelectItems { get; set; }
	}

}