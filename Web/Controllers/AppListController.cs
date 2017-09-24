using System.Web.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.AppModels.List;

namespace Web.Controllers
{
    public class AppListController : BaseController
    {
        [Route(UserAppsUrl.Route)]
        public ActionResult Apps()
        {
            var context = GetAppContext();
            var appListResult = UseCase.AppListUser.Execute();
            var model = new UserAppsPageModel(context, appListResult);
            return View(model);
        }

        [Route(AllAppsUrl.Route)]
        public ActionResult All()
        {
            var context = GetAppContext();
            var appListResult = UseCase.AllAppsList.Execute();
            var model = new AllAppsPageModel(context, appListResult);
            return View(model);
        }
    }
}