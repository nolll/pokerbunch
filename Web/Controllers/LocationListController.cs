using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.LocationModels.List;

namespace Web.Controllers
{
    public class LocationListController : BunchController
    {
        private readonly LocationList _locationList;

        public LocationListController(AppSettings appSettings, CoreContext coreContext, BunchContext bunchContext, LocationList locationList) 
            : base(appSettings, coreContext, bunchContext)
        {
            _locationList = locationList;
        }

        [Authorize]
        [Route(LocationListUrl.Route)]
        public ActionResult List(string bunchId)
        {
            var contextResult = GetBunchContext(bunchId);
            var locationList = _locationList.Execute(new LocationList.Request(bunchId));
            var model = new LocationListPageModel(AppSettings, contextResult, locationList);
            return View(model);
        }
    }
}