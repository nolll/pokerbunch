using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.LocationModels.List;
using Web.Security;

namespace Web.Controllers
{
    public class LocationListController : BaseController
    {
        [CustomAuthorize]
        [Route(LocationListUrl.Route)]
        public ActionResult List(string slug)
        {
            var contextResult = GetBunchContext(slug);
            var locationList = UseCase.LocationList.Execute(new LocationList.Request(slug));
            var model = new LocationListPageModel(contextResult, locationList);
            return View(model);
        }
    }
}