using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.AppModels.List;
using Web.Urls;

namespace Web.Controllers
{
    public class AppListController : BaseController
    {
        [Route(Routes.AppList)]
        public ActionResult List()
        {
            var context = GetAppContext();
            var appListResult = UseCase.AppList.Execute(new AppList.Request());
            var model = new AppListPageModel(context, appListResult);
            return View("~/Views/Pages/AppList/AppList.cshtml", model);
        }
    }
}