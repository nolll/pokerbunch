using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.AppModels.Details;
using Web.Routes;

namespace Web.Controllers
{
    public class AppDetailsController : BaseController
    {
        [Route(WebRoutes.App.Details)]
        public ActionResult Details(string id)
        {
            var context = GetAppContext();
            var appDetailsResult = UseCase.AppDetails.Execute(new AppDetails.Request(id));
            var model = new AppDetailsPageModel(context, appDetailsResult);
            return View(model);
        }
    }
}