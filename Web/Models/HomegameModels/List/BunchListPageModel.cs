using System.Collections.Generic;
using System.Linq;
using Application.UseCases.ApplicationContext;
using Application.UseCases.BunchList;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.List
{
	public class BunchListPageModel : IPageModel
    {
	    public string BrowserTitle { get; private set; }
	    public PageProperties PageProperties { get; private set; }
	    public IList<BunchListItemModel> BunchModels { get; private set; }

	    public BunchListPageModel(ApplicationContextResult applicationContextResult, BunchListResult bunchListResult)
	    {
	        BrowserTitle = "Bunches";
            PageProperties = new PageProperties(applicationContextResult);
            BunchModels = bunchListResult.Bunches.Select(o => new BunchListItemModel(o)).ToList();
	    }
    }
}