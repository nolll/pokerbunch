using System.Collections.Generic;
using System.Linq;
using Core.Settings;
using Core.UseCases;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.List
{
	public class BunchListPageModel : AppPageModel
    {
	    public IList<BunchListItemModel> BunchModels { get; }

	    public BunchListPageModel(AppSettings appSettings, CoreContext.Result contextResult, BunchList.Result bunchListResult)
            : base(appSettings, contextResult)
	    {
            BunchModels = bunchListResult.Bunches.Select(o => new BunchListItemModel(o)).ToList();
	    }

	    public override string BrowserTitle => "Bunches";

	    public override View GetView()
	    {
	        return new View("~/Views/Pages/BunchList/BunchList.cshtml");
	    }
    }
}