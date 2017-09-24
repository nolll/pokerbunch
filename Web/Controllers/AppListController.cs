using System.Web.Mvc;
using PokerBunch.Common.Routes;
using Web.Controllers.Base;
using Web.Models.AppModels.List;

namespace Web.Controllers
{
    public class AppListController : BaseController
    {
        [Route(WebRoutes.App.List)]
        public ActionResult Apps()
        {
            var context = GetAppContext();
            var appListResult = UseCase.AppListUser.Execute();
            var model = new UserAppsPageModel(context, appListResult);
            return View(model);
        }

        [Route(WebRoutes.App.All)]
        public ActionResult All()
        {
            var context = GetAppContext();
            var appListResult = UseCase.AllAppsList.Execute();
            var model = new AllAppsPageModel(context, appListResult);
            return View(model);
        }
    }
}