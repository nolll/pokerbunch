using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.LocationModels.Details;

namespace Web.Controllers
{
    public class LocationDetailsController : BaseController
    {
        [Authorize]
        [Route(LocationDetailsUrl.Route)]
        public ActionResult List(string id)
        {
            var locationDetails = UseCase.LocationDetails.Execute(new LocationDetails.Request(id));
            var contextResult = GetBunchContext(locationDetails.Slug);
            var model = new LocationDetailsPageModel(contextResult, locationDetails);
            return View(model);
        }
    }
}