using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.AppModels.List;

namespace Web.Controllers
{
    public class AppListController : CoreController
    {
        private readonly AppListUser _appListUser;
        private readonly AppListAll _appListAll;

        public AppListController(AppSettings appSettings, CoreContext coreContext, AppListUser appListUser, AppListAll appListAll) 
            : base(appSettings, coreContext)
        {
            _appListUser = appListUser;
            _appListAll = appListAll;
        }

        [Route(UserAppsUrl.Route)]
        public ActionResult Apps()
        {
            var context = GetAppContext();
            var appListResult = _appListUser.Execute();
            var model = new UserAppsPageModel(AppSettings, context, appListResult);
            return View(model);
        }

        [Route(AllAppsUrl.Route)]
        public ActionResult All()
        {
            var context = GetAppContext();
            var appListResult = _appListAll.Execute();
            var model = new AllAppsPageModel(AppSettings, context, appListResult);
            return View(model);
        }
    }
}