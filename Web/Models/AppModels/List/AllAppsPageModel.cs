using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.AppModels.List
{
    public class AllAppsPageModel : AppPageModel
    {
        public IList<AppListItemModel> AppModels { get; private set; }

        public AllAppsPageModel(AppContext.Result contextResult, AppList.Result appListResult)
            : base("All Apps", contextResult)
        {
            AppModels = appListResult.Items.Select(o => new AppListItemModel(o)).ToList();
        }
    }
}