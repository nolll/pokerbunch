using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.AppModels.List
{
    public class AllAppsPageModel : AppPageModel
    {
        public IList<AppListItemModel> AppModels { get; }
        public bool HasApps { get; }

        public AllAppsPageModel(CoreContext.Result contextResult, AppListAll.Result appListResult)
            : base(contextResult)
        {
            AppModels = appListResult.Items.Select(o => new AppListItemModel(o)).ToList();
            HasApps = appListResult.Items.Any();
        }

        public override string BrowserTitle => "All Apps";

        public override View GetView()
        {
            return new View("~/Views/Pages/AppList/AllApps.cshtml");
        }
    }
}