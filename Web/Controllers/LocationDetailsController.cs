using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Routes;
using Web.Controllers.Base;
using Web.Models.LocationModels.Details;

namespace Web.Controllers
{
    public class LocationDetailsController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Location.Details)]
        public ActionResult List(string id)
        {
            var locationDetails = UseCase.LocationDetails.Execute(new LocationDetails.Request(id));
            var contextResult = GetBunchContext(locationDetails.Slug);
            var model = new LocationDetailsPageModel(contextResult, locationDetails);
            return View(model);
        }
    }
}