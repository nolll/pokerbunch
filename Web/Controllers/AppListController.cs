using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.AppModels.Details;
using Web.Models.AppModels.List;
using Web.Urls;

namespace Web.Controllers
{
    public class AppDetailsController : BaseController
    {
        [Route(Routes.AppDetails)]
        public ActionResult Details(int id)
        {
            var context = GetAppContext();
            var appDetailsResult = UseCase.AppDetails.Execute(new AppDetails.Request(id));
            var model = new AppDetailsPageModel(context, appDetailsResult);
            return View("~/Views/Pages/AppDetails/AppDetails.cshtml", model);
        }
    }

    public class AppListController : BaseController
    {
        [Route(Routes.AllApps)]
        public ActionResult All()
        {
            var context = GetAppContext();
            var appListResult = UseCase.AppList.Execute(new AppList.AllAppsRequest(CurrentUserName));
            var model = new AllAppsPageModel(context, appListResult);
            return View("~/Views/Pages/AppList/AllApps.cshtml", model);
        }

        [Route(Routes.UserApps)]
        public ActionResult UserApps()
        {
            var context = GetAppContext();
            var appListResult = UseCase.AppList.Execute(new AppList.UserAppsRequest(CurrentUserName));
            var model = new UserAppsPageModel(context, appListResult);
            return View("~/Views/Pages/AppList/UserApps.cshtml", model);
        }
    }
}