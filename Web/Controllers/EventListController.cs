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
        public ActionResult List(string slug)
        {
            var contextResult = GetBunchContext(slug);
            var eventListOutput = UseCase.EventList.Execute(new EventList.Request(slug));
            var model = new EventListPageModel(contextResult, eventListOutput);
            return View(model);
        }
    }
}