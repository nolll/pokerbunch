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
        public ActionResult List(string bunchId)
        {
            var contextResult = GetBunchContext(bunchId);
            var locationList = UseCase.LocationList.Execute(new LocationList.Request(bunchId));
            var model = new LocationListPageModel(contextResult, locationList);
            return View(model);
        }
    }
}