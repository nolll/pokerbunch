using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.AppModels.Details;
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

    public class ApiDocsController : BaseController
    {
        [Route(Routes.ApiDocs)]
        public ActionResult ApiDocs()
        {
            var context = GetAppContext();
            var model = new ApiDocsPageModel(context);
            return View("~/Views/Pages/ApiDocs/ApiDocs.cshtml", model);
        }
    }
}