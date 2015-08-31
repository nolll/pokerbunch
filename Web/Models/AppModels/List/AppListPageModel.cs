using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using Web.Models.PageBaseModels;
using Web.Models.UserModels.List;

namespace Web.Models.AppModels.List
{
    public class AppListPageModel : AppPageModel
    {
        public IList<AppListItemModel> AppModels { get; private set; }

        public AppListPageModel(AppContext.Result contextResult, AppList.Result appListResult)
            : base("Users", contextResult)
        {
            AppModels = appListResult.Items.Select(o => new AppListItemModel(o)).ToList();
        }
    }
}