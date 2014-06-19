using System.Collections.Generic;
using System.Linq;
using Application.UseCases.AppContext;
using Application.UseCases.BunchList;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.List
{
	public class BunchListPageModel : IPageModel
    {
	    public string BrowserTitle { get; private set; }
	    public PageProperties PageProperties { get; private set; }
	    public IList<BunchListItemModel> BunchModels { get; private set; }

	    public BunchListPageModel(AppContextResult appContextResult, BunchListResult bunchListResult)
	    {
	        BrowserTitle = "Bunches";
            PageProperties = new PageProperties(appContextResult);
            BunchModels = bunchListResult.Bunches.Select(o => new BunchListItemModel(o)).ToList();
	    }
    }
}