using System.Collections.Generic;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.List
{
	public class BunchListPageModel : IPageModel
    {
	    public string BrowserTitle { get; set; }
	    public PageProperties PageProperties { get; set; }
	    public IList<BunchListItemModel> BunchModels { get; set; }
	}
}