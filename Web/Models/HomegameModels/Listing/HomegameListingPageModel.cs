using System.Collections.Generic;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Listing{

	public class HomegameListingPageModel : IPageModel {

	    public string BrowserTitle { get; set; }
	    public PageProperties PageProperties { get; set; }
	    public IList<HomegameListingItemModel> HomegameModels { get; set; }
	}

}