using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.AppModels.List;
using Web.Urls;

namespace Web.Controllers
{
    public class AppListController : BaseController
    {
        [Route(Routes.UserApps)]
        public ActionResult Apps()
        {
            var context = GetAppContext();
            var appListResult = UseCase.AppList.Execute(new AppList.UserAppsRequest(CurrentUserName));
            var model = new UserAppsPageModel(context, appListResult);
            return View("~/Views/Pages/AppList/UserApps.cshtml", model);
        }

        [Route(Routes.AllApps)]
        public ActionResult All()
        {
            var context = GetAppContext();
            var appListResult = UseCase.AppList.Execute(new AppList.AllAppsRequest(CurrentUserName));
            var model = new AllAppsPageModel(context, appListResult);
            return View("~/Views/Pages/AppList/AllApps.cshtml", model);
        }
    }
}