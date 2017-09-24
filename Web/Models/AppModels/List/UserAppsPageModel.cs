using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.AppModels.List
{
    public class UserAppsPageModel : AppPageModel
    {
        public IList<AppListItemModel> AppModels { get; }
        public bool HasApps { get; }
        public string AddUrl { get; }

        public UserAppsPageModel(CoreContext.Result contextResult, AppListUser.Result appListResult)
            : base(contextResult)
        {
            AppModels = appListResult.Items.Select(o => new AppListItemModel(o)).ToList();
            HasApps = appListResult.Items.Any();
            AddUrl = new AddAppUrl().Relative;
        }

        public override string BrowserTitle => "User Apps";

        public override View GetView()
        {
            return new View("~/Views/Pages/AppList/UserApps.cshtml");
        }
    }
}