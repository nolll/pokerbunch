using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using Web.Common.Urls.SiteUrls;
using Web.Models.PageBaseModels;

namespace Web.Models.AppModels.List
{
    public class UserAppsPageModel : AppPageModel
    {
        public IList<AppListItemModel> AppModels { get; private set; }
        public bool HasApps { get; private set; }
        public string AddUrl { get; private set; }

        public UserAppsPageModel(CoreContext.Result contextResult, AppList.Result appListResult)
            : base(contextResult)
        {
            AppModels = appListResult.Items.Select(o => new AppListItemModel(o)).ToList();
            HasApps = appListResult.Items.Any();
            AddUrl = new AddAppUrl().Relative;
        }

        public override string BrowserTitle => "User Apps";
    }
}