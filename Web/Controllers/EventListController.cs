using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.EventModels.List;

namespace Web.Controllers
{
    public class EventListController : BaseController
    {
        [Authorize]
        [Route(EventListUrl.Route)]
        public ActionResult List(string bunchId)
        {
            var contextResult = GetBunchContext(bunchId);
            var eventListOutput = UseCase.EventList.Execute(new EventList.Request(bunchId));
            var model = new EventListPageModel(contextResult, eventListOutput);
            return View(model);
        }
    }
}