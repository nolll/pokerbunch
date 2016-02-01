using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.List
{
	public class BunchListPageModel : AppPageModel
    {
	    public IList<BunchListItemModel> BunchModels { get; private set; }

	    public BunchListPageModel(CoreContext.Result contextResult, BunchList.Result bunchListResult)
            : base("Bunches", contextResult)
	    {
            BunchModels = bunchListResult.Bunches.Select(o => new BunchListItemModel(o)).ToList();
	    }
    }
}