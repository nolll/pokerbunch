using System.Collections.Generic;
using System.Linq;
using Core.UseCases.AppContext;
using Core.UseCases.BunchList;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.List
{
	public class BunchListPageModel : AppPageModel
    {
	    public IList<BunchListItemModel> BunchModels { get; private set; }

	    public BunchListPageModel(AppContextResult contextResult, BunchListResult bunchListResult)
            : base("Bunches", contextResult)
	    {
            BunchModels = bunchListResult.Bunches.Select(o => new BunchListItemModel(o)).ToList();
	    }
    }
}