using System.Web.Mvc;
using Core.UseCases;
using Web.Common.Routes;
using Web.Controllers.Base;
using Web.Models.LocationModels.List;
using Web.Security;

namespace Web.Controllers
{
    public class LocationListController : BaseController
    {
        [CustomAuthorize]
        [Route(WebRoutes.Location.List)]
        public ActionResult List(string slug)
        {
            var contextResult = GetBunchContext(slug);
            var locationList = UseCase.LocationList.Execute(new LocationList.Request(Identity.UserName, slug));
            var model = new LocationListPageModel(contextResult, locationList);
            return View("~/Views/Pages/LocationList/LocationList.cshtml", model);
        }
    }
}