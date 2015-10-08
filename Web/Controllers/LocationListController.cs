using System.Web.Mvc;
using Core.UseCases;
using Web.Common.Routes;
using Web.Controllers.Base;
using Web.Models.LocationModels.List;

namespace Web.Controllers
{
    public class LocationListController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.LocationList)]
        public ActionResult List(string slug)
        {
            var contextResult = GetBunchContext(slug);
            var locationList = UseCase.LocationList.Execute(new LocationList.Request(CurrentUserName, slug));
            var model = new LocationListPageModel(contextResult, locationList);
            return View("~/Views/Pages/LocationList/LocationList.cshtml", model);
        }
    }
}