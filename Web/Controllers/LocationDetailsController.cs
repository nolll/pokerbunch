using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.LocationModels.Details;

namespace Web.Controllers
{
    public class LocationDetailsController : BunchController
    {
        private readonly LocationDetails _locationDetails;

        public LocationDetailsController(AppSettings appSettings, CoreContext coreContext, BunchContext bunchContext, LocationDetails locationDetails) 
            : base(appSettings, coreContext, bunchContext)
        {
            _locationDetails = locationDetails;
        }

        [Authorize]
        [Route(LocationDetailsUrl.Route)]
        public ActionResult List(string locationId)
        {
            var locationDetails = _locationDetails.Execute(new LocationDetails.Request(locationId));
            var contextResult = GetBunchContext(locationDetails.Slug);
            var model = new LocationDetailsPageModel(AppSettings, contextResult, locationDetails);
            return View(model);
        }
    }
}