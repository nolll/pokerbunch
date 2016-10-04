using System.Web.Mvc;
using Core.UseCases;
using Web.Common.Routes;
using Web.Controllers.Base;
using Web.Models.LocationModels.Details;

namespace Web.Controllers
{
    public class LocationDetailsController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Location.Details)]
        public ActionResult List(int id)
        {
            var locationDetails = UseCase.LocationDetails.Execute(new LocationDetails.Request(Identity.UserName, id));
            var contextResult = GetBunchContext(locationDetails.Slug);
            var model = new LocationDetailsPageModel(contextResult, locationDetails);
            return View("~/Views/Pages/LocationDetails/LocationDetails.cshtml", model);
        }
    }
}