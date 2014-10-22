using System.Web.Mvc;
using Core.UseCases.BunchContext;
using Core.UseCases.EventList;
using Web.Controllers.Base;
using Web.Models.EventModels.List;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class EventListController : PokerBunchController
    {
        [AuthorizePlayer]
        [Route("{slug}/event/list")]
        public ActionResult List(string slug)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var eventListOutput = UseCase.EventList(new EventListInput(slug));
            var model = new EventListPageModel(contextResult, eventListOutput);
            return View("~/Views/Pages/EventList/EventList.cshtml", model);
        }
    }
}