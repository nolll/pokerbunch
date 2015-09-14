using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using Web.Models.PageBaseModels;
using Web.Urls;

namespace Web.Models.AppModels.List
{
    public class UserAppsPageModel : AppPageModel
    {
        public IList<AppListItemModel> AppModels { get; private set; }
        public string AddUrl { get; private set; }

        public UserAppsPageModel(AppContext.Result contextResult, AppList.Result appListResult)
            : base("User Apps", contextResult)
        {
            AppModels = appListResult.Items.Select(o => new AppListItemModel(o)).ToList();
            AddUrl = new AddAppUrl().Relative;
        }
    }
}