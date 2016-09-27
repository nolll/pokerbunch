using System.Web.Mvc;
using Core.UseCases;
using Web.Common.Routes;
using Web.Controllers.Base;
using Web.Models.EventModels.Details;
using Web.Models.LocationModels.Details;

namespace Web.Controllers
{
    public class EventDetailsController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Event.Details)]
        public ActionResult List(int id)
        {
            var eventDetails = UseCase.EventDetails.Execute(new EventDetails.Request(Identity.UserName, id));
            var contextResult = GetBunchContext(eventDetails.Slug);
            var matrixResult = UseCase.Matrix.Execute(new Matrix.EventMatrixRequest(Identity.UserName, id));
            var model = new EventDetailsPageModel(contextResult, eventDetails, matrixResult);
            return View("~/Views/Pages/EventDetails/EventDetails.cshtml", model);
        }
    }

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